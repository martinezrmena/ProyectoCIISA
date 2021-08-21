using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperDetallePedido
    {
        internal string buscarDetallesPedidosRutaImpresion(string pcodTransaction)
        {
            string _lineas = string.Empty;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DP." + TableDetallePedido._ARTI_DES + ", ");
            _sb.Append("DP." + TableDetallePedido._CAN_DES + ", ");
            _sb.Append("DP." + TableDetallePedido._COMENTARIO + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detallePedido + " DP, ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("DP." + TableDetallePedido._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append("DP." + TableDetallePedido._ARTI_DES + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO + " ");
            _sb.Append("ORDER BY ");
            _sb.Append(TableDetallePedido._NO_TRANSA + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _lineaUno = string.Empty;
                _lineaUno += _position.tabular(_lineaUno.Length, RepPedidosRuta.codigoDescripcion);
                _lineaUno += _fila[TableDetallePedido._ARTI_DES].ToString();
                _lineaUno += " " + Simbol._slash + " ";
                _lineaUno += _fila[TableProducto._DESCPRODUCTO].ToString();
                _lineaUno += Environment.NewLine;

                string _lineaDos = string.Empty;
                _lineaDos += _position.tabular(_lineaDos.Length, RepPedidosRuta.cantidadEspecificacion);
                _lineaDos += _fila[TableDetallePedido._CAN_DES].ToString();
                _lineaDos += " " + Simbol._hyphen + " ";
                _lineaDos += _fila[TableDetallePedido._COMENTARIO].ToString();
                _lineaDos += Environment.NewLine;

                _lineas += _lineaUno;
                _lineas += _lineaDos;
            }

            return _lineas;
        }

    }
}
