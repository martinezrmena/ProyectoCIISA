using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class originalCopias
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }
        private string TipoAgente { get; set; }

        public originalCopias(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;

            TipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;
        }

        internal async Task imprimirOriginalYCopias(int pcantCopias, string pcodTipoTransaccion, string codTransaccion, Cliente pobjCliente)
        {
            string _original = "Original Cliente.";
            bool _msgContinuar = false;

            List<string> _listaLineasImpresionTemporal = null;

            Line _line = new Line();

            Util _util = new Util();

            for (int i = 0; i < pcantCopias; i++)
            {
                _listaLineasImpresionTemporal = _util.cloneList(v_procesoImpresion.v_listaLineasImpresion);

                if (_msgContinuar)
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.placePrinterQuestion();
                }

                if (i == 0)
                {
                    if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
                    {
                        SegmentoAutorizadoFirmar _segmentoAutorizadoFirmar = new SegmentoAutorizadoFirmar();

                        _segmentoAutorizadoFirmar.autorizadoFirmar(
                            _listaLineasImpresionTemporal,
                            pcodTipoTransaccion,
                            pobjCliente
                            );
                    }

                    if (!pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla)
                        && !pcodTipoTransaccion.Equals(ROLTransactions._cotizacionSigla))
                    {
                        _line.printingLinesList(
                            _listaLineasImpresionTemporal,
                            _original,
                            1
                            );
                    }
                }
                else
                {
                    Copy _copy = new Copy();

                    _line.printingLinesList(_listaLineasImpresionTemporal,
                        _copy.copies(
                            Environment.NewLine,
                            i,
                            pcantCopias - 1
                            ),
                        1
                        );
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla)
                            || pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
                {
                    _line.printLineSpace(_listaLineasImpresionTemporal, 1);

                    SegmentoResolucion _segmentoResolucion = new SegmentoResolucion();

                    _segmentoResolucion.Resolucion(_listaLineasImpresionTemporal);
                }

                _line.finalSpace(_listaLineasImpresionTemporal);

                Print _print = new Print();

                #region  Encabezado Carnicero
                if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla) 
                    || pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
                {
                    SegmentoEncabezadoCarniceria SEC = new SegmentoEncabezadoCarniceria();
                    SEC.subEncabezadoTransaccion(
                        _print,
                        v_procesoImpresion,
                        TipoAgente,
                        codTransaccion,
                        i);
                }
                #endregion

                await _print.print(
                    _listaLineasImpresionTemporal,
                    v_procesoImpresion.v_impresora
                    );

                #region  Pie Carnicero
                if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla)
                    || pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
                {
                    SegmentoPieCarniceria SPC = new SegmentoPieCarniceria();

                    SPC.pieTransaccion(
                    _print,
                    v_procesoImpresion,
                    TipoAgente,
                    pcodTipoTransaccion,
                    codTransaccion,
                    i);
                }
                #endregion

                _msgContinuar = true;
            }
        }
    }
}
