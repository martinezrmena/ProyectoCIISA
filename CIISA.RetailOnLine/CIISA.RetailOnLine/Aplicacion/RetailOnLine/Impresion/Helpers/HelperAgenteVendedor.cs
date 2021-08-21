using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperAgenteVendedor
    {
        internal string buscarLineasReporteConsecutivoDocumentos(string pcodAgente)
        {
            #region REPORTES: Consecutivo
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_DOC));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_REC));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_CLI));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_OV));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_P));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_DV));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_RG));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_RC));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_AN));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_TR));
            _sb.Append(string.Format("{0}, ", TableAgenteVendedor._CONSECUTIVO_PD));
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_VT + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");
            _sb.Append("WHERE ");
            _sb.Append(string.Format("{0} = ", TableAgenteVendedor._NO_AGENTE));
            _sb.Append(string.Format("'{0}'", pcodAgente));

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            StringBuilder _lineas = new StringBuilder();

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                StringBuilder _lineOne = new StringBuilder();
                _lineOne.Append(_position.tabular(_lineOne.Length, RepConsecutivo.codigo));
                _lineOne.Append("Facturas");
                _lineOne.Append(_position.tabular(_lineOne.Length, RepConsecutivo.local));
                _lineOne.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_DOC)].ToString());
                _lineOne.Append(Environment.NewLine);

                StringBuilder _lineaDos = new StringBuilder();
                _lineaDos.Append(_position.tabular(_lineaDos.Length, RepConsecutivo.codigo));
                _lineaDos.Append(ROLTransactions._reciboDineroNombre);
                _lineaDos.Append(_position.tabular(_lineaDos.Length, RepConsecutivo.local));
                _lineaDos.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_REC)].ToString());
                _lineaDos.Append(Environment.NewLine);

                StringBuilder _lineaTres = new StringBuilder();
                _lineaTres.Append(_position.tabular(_lineaTres.Length, RepConsecutivo.codigo));
                _lineaTres.Append("Cliente");
                _lineaTres.Append(_position.tabular(_lineaTres.Length, RepConsecutivo.local));
                _lineaTres.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_CLI)].ToString());
                _lineaTres.Append(Environment.NewLine);

                StringBuilder _lineaCuatro = new StringBuilder();
                _lineaCuatro.Append(_position.tabular(_lineaCuatro.Length, RepConsecutivo.codigo));
                _lineaCuatro.Append(ROLTransactions._ordenVentaNombre);
                _lineaCuatro.Append(_position.tabular(_lineaCuatro.Length, RepConsecutivo.local));
                _lineaCuatro.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_OV)].ToString());
                _lineaCuatro.Append(Environment.NewLine);

                StringBuilder _lineaCinco = new StringBuilder();
                _lineaCinco.Append(_position.tabular(_lineaCinco.Length, RepConsecutivo.codigo));
                _lineaCinco.Append(ROLTransactions._cotizacionNombre);
                _lineaCinco.Append(_position.tabular(_lineaCinco.Length, RepConsecutivo.local));
                _lineaCinco.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_P)].ToString());
                _lineaCinco.Append(Environment.NewLine);

                StringBuilder _lineaSeis = new StringBuilder();
                _lineaSeis.Append(_position.tabular(_lineaSeis.Length, RepConsecutivo.codigo));
                _lineaSeis.Append(ROLTransactions._regaliaNombre);
                _lineaSeis.Append(_position.tabular(_lineaSeis.Length, RepConsecutivo.local));
                _lineaSeis.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_RG)].ToString());
                _lineaSeis.Append(Environment.NewLine);

                StringBuilder _lineaSiete = new StringBuilder();
                _lineaSiete.Append(_position.tabular(_lineaSiete.Length, RepConsecutivo.codigo));
                _lineaSiete.Append(ROLTransactions._recaudacionNombre);
                _lineaSiete.Append(_position.tabular(_lineaSiete.Length, RepConsecutivo.local));
                _lineaSiete.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_RC)].ToString());
                _lineaSiete.Append(Environment.NewLine);

                StringBuilder _lineaOcho = new StringBuilder();
                _lineaOcho.Append(_position.tabular(_lineaOcho.Length, RepConsecutivo.codigo));
                _lineaOcho.Append(ROLTransactions._anulacionNombre);
                _lineaOcho.Append(_position.tabular(_lineaOcho.Length, RepConsecutivo.local));
                _lineaOcho.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_AN)].ToString());
                _lineaOcho.Append(Environment.NewLine);

                StringBuilder _lineaNueve = new StringBuilder();
                _lineaNueve.Append(_position.tabular(_lineaNueve.Length, RepConsecutivo.codigo));
                _lineaNueve.Append(ROLTransactions._tramiteNombre);
                _lineaNueve.Append(_position.tabular(_lineaNueve.Length, RepConsecutivo.local));
                _lineaNueve.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_TR)].ToString());
                _lineaNueve.Append(Environment.NewLine);

                StringBuilder _lineaDiez = new StringBuilder();
                _lineaDiez.Append(_position.tabular(_lineaDiez.Length, RepConsecutivo.codigo));
                _lineaDiez.Append(ROLTransactions._devolucionNombre);
                _lineaDiez.Append(_position.tabular(_lineaDiez.Length, RepConsecutivo.local));
                _lineaDiez.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_DV)].ToString());
                _lineaDiez.Append(Environment.NewLine);

                StringBuilder _lineaOnce = new StringBuilder();
                _lineaOnce.Append(_position.tabular(_lineaOnce.Length, RepConsecutivo.codigo));
                _lineaOnce.Append(ROLTransactions._pedidoNombre);
                _lineaOnce.Append(_position.tabular(_lineaOnce.Length, RepConsecutivo.local));
                _lineaOnce.Append(_fila[string.Format("{0}", TableAgenteVendedor._CONSECUTIVO_PD)].ToString());
                _lineaOnce.Append(Environment.NewLine);

                _lineas.Append(_lineOne);
                _lineas.Append(_lineaDos);
                _lineas.Append(_lineaTres);
                _lineas.Append(_lineaCuatro);
                _lineas.Append(_lineaCinco);
                _lineas.Append(_lineaSeis);
                _lineas.Append(_lineaSiete);
                _lineas.Append(_lineaOcho);
                _lineas.Append(_lineaNueve);
                _lineas.Append(_lineaDiez);
            }

            return _lineas.ToString();
            #endregion
        }

    }
}
