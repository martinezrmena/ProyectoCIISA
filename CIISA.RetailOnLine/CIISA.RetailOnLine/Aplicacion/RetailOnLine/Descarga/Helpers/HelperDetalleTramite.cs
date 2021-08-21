using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ValidateHH;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers
{
    internal class HelperDetalleTramite
    {
        private void llenarTreeViewDocumentos(DataTable pdt, ListView ptrvDocumentos, Color color)
        {
            if (DataTableValidate.validateDataTable(pdt))
            {
                foreach (DataRow _fila in pdt.Rows)
                {
                    string _codDocumento = _fila[TableDetalleTramite._NO_TRANSA].ToString();

                    string _noFactura = _fila[TableDetalleTramite._NO_FACTURA].ToString();

                    string _codCliente = _fila[TableEncabezadoTramite._NO_CLIENTE].ToString();

                    string _nomCliente = _fila[TableCliente._NOMBRE].ToString();

                    string _monto = _fila[TableDetalleTramite._MONTO].ToString();

                    decimal _monto2 = FormatUtil.convertStringToDecimal(_monto);

                    _monto = FormatUtil.applyCurrencyFormat(_monto2);

                    _codDocumento = _codDocumento + " " + ROLTransactions._tramiteNombre + " / " + _codCliente + " " + _nomCliente + " / " + _noFactura + "  " + _monto;

                    Util _util = new Util();

                    _util.evaluateAddNode(ptrvDocumentos,_codDocumento, color);
                }
            }
        }

        internal void consultaTramiteDetalles(ListView ptrvDocumentos, Color color)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DT." + TableDetalleTramite._NO_TRANSA + ", ");
            _sb.Append("DT." + TableDetalleTramite._NO_LINEA + ", ");
            _sb.Append("DT." + TableDetalleTramite._NO_FACTURA + ", ");
            _sb.Append("DT." + TableDetalleTramite._MONTO + ", ");
            _sb.Append("DT." + TableDetalleTramite._ENVIADO + ", ");
            _sb.Append("CL." + TableCliente._NOMBRE + ", ");
            _sb.Append("ET." + TableEncabezadoTramite._NO_CLIENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleTramite + " DT, ");
            _sb.Append(TablesROL._encabezadoTramite + " ET, ");
            _sb.Append(TablesROL._cliente + " CL ");
            _sb.Append("WHERE ");
            _sb.Append("DT." + TableDetalleTramite._NO_TRANSA + " = ");
            _sb.Append("ET." + TableEncabezadoTramite._NO_TRANSA + " ");
            _sb.Append("AND ");
            _sb.Append("CL." + TableCliente._NO_CLIENTE + " = ");
            _sb.Append("ET." + TableEncabezadoTramite._NO_CLIENTE + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            llenarTreeViewDocumentos(_dt, ptrvDocumentos, color);
        }

        internal DataTable buscarDetallesTramitesinEnviar(string ptipoDescarga, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleTramite._NO_CIA + ", ");
            _sb.Append(TableDetalleTramite._NO_TRANSA + ", ");
            _sb.Append(TableDetalleTramite._NO_LINEA + ", ");
            _sb.Append(TableDetalleTramite._NO_FACTURA + ", ");
            _sb.Append("REPLACE(" + TableDetalleTramite._MONTO + ",',','.') " + TableDetalleTramite._MONTO + ", ");
            _sb.Append(TableDetalleTramite._ENVIADO + " ");
            _sb.Append("FROM ");

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._detalleTramiteBK + " ");
                _sb.Append("WHERE ");
            }
            else
            {
                _sb.Append(TablesROL._detalleTramite + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableDetalleTramite._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
                _sb.Append("AND ");
            }

            _sb.Append(TableEncabezadoTramite._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }

        internal DataTable buscarDetallesTramitesinEnviar(string ptipoDescarga)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleTramite._NO_CIA + ", ");
            _sb.Append(TableDetalleTramite._NO_TRANSA + ", ");
            _sb.Append(TableDetalleTramite._NO_LINEA + ", ");
            _sb.Append(TableDetalleTramite._NO_FACTURA + ", ");
            _sb.Append("REPLACE(" + TableDetalleTramite._MONTO + ",',','.') " + TableDetalleTramite._MONTO + ", ");
            _sb.Append(TableDetalleTramite._ENVIADO + " ");
            _sb.Append("FROM ");

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._detalleTramiteBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._detalleTramite + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableDetalleTramite._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
