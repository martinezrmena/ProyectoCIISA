using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoAutorizadoFirmar
    {
        internal void autorizadoFirmar(List<string> pprintingLinesList,string pcodTipoTransaccion,Cliente pobjCliente)
        {
            string _tipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            if (_tipoAgente.Equals(Agent._ruteroSigla))
            {
                Line _line = new Line();

                if (pobjCliente.v_listaAutorizadosFirmar.Count > 0)
                {
                    _line.printingLinesList(pprintingLinesList, "Autorizados a recibir el producto:", 1);

                    Position _position = new Position();

                    string _lineaUno = string.Empty;

                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionAF.cedula);
                    _lineaUno += "Cédula";
                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionAF.nombre);
                    _lineaUno += "Nombre";

                    _line.printingLinesList(pprintingLinesList, _lineaUno, 1);

                    foreach (AutorizadoFirmar _objAutorizadoFirmar in pobjCliente.v_listaAutorizadosFirmar)
                    {
                        string _lineaDos = string.Empty;

                        _lineaDos += _position.tabular(_lineaDos.Length, PosicionAF.cedula);
                        _lineaDos += _objAutorizadoFirmar.v_cedula;
                        _lineaDos += _position.tabular(_lineaDos.Length, PosicionAF.nombre);
                        _lineaDos += _objAutorizadoFirmar.v_nombre;

                        _line.printingLinesList(pprintingLinesList, _lineaDos, 1);
                    }
                }
                else
                {
                    _line.printingLinesList(pprintingLinesList, "Sin autorizado(s) a recibir el producto.", 2);

                    _line.printingLinesList(pprintingLinesList, "Atención: favor notificar al Departamento de", 1);
                    _line.printingLinesList(pprintingLinesList, "          Cuentas por Cobrar, en caso de error.", 1);
                }

                _line.printingLinesList(pprintingLinesList, string.Empty, 1);
            }
        }
    }
}
