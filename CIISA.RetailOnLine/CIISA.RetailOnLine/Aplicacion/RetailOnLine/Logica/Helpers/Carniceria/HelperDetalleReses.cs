using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers.Carniceria
{
    public class HelperDetalleReses
    {
        /// <summary>
        ///Metodo encargado de modificar el asignado de un producto de detalle reses
        /// </summary>
        internal void CambiarAsignado(string codproducto, bool variable)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append(" SET ");
            _sb.Append(TableDetalleReses._VASIGNADO + " = ");

            if (variable)
            {
                _sb.Append("'" + SQL._Si + "'");
            }
            else {
                _sb.Append("'" + SQL._No + "'");
            }

            _sb.Append(" WHERE ");
            _sb.Append("CONSECUTIVO = '" + codproducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);

        }

        /// <summary>
        ///Metodo encargado verificar la suma de todos los detalles reses
        ///comprometidos por un cliente
        /// </summary>
        /// return decimal con la suma de los pesos que tiene disponible
        internal decimal TotalPesoDisponibleDetalleRes(string codcliente, string articulo)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(PESO) PESO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append(" WHERE ");
            _sb.Append(TableDetalleReses._VENDIDO + " = ");
            _sb.Append("'" + SQL._No + "'");
            _sb.Append(" AND ");
            _sb.Append(TableDetalleReses._COMPROMETIDO + " = ");
            _sb.Append("'" + SQL._Si + "'");
            _sb.Append(" AND ");
            _sb.Append("NO_CLIENTE = '" + codcliente +"'");
            _sb.Append(" AND ");
            _sb.Append("ARTICULO = '" + articulo + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);

        }

        /// <summary>
        ///Metodo encargado de actualizar los vendidos segun la lista de detalle
        ///reses, serviria para anularlo
        /// </summary>
        internal void AnulacionVendidoDetalleRes(string numpedido, string codcliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("SET ");
            _sb.Append(TableDetalleReses._VENDIDO + " = ");
            _sb.Append("'" + SQL._No + "'");
            _sb.Append(" WHERE ");
            _sb.Append("NUM_PEDIDO = " + numpedido);
            _sb.Append(" AND ");
            _sb.Append("NO_CLIENTE = '" + codcliente + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);

        }

        /// <summary>
        ///Metodo encargado de actualizar los comprometidos segun la lista de detalle
        ///reses, serviria para anularlo
        /// </summary>
        internal void AnulacionComprometidoDetalleResProducto(string numpedido, string codcliente, string codproducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("SET ");
            _sb.Append(TableDetalleReses._COMPROMETIDO + " = ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append(TableDetalleReses._VENDIDO + " = ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append(TableDetalleReses._VASIGNADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append(" WHERE ");
            _sb.Append("NUM_PEDIDO = '" + numpedido + "' ");
            _sb.Append(" AND ");
            _sb.Append("NO_CLIENTE = '" + codcliente + "'");
            _sb.Append(" AND ");
            _sb.Append("ARTICULO = '" + codproducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);

        }

        /// <summary>
        ///Metodo encargado de actualizar los comprometidos segun la lista de detalle
        ///reses, serviria para anularlo
        /// </summary>
        internal void AnulacionVendidoDetalleResProducto(string numpedido, string codcliente, string codproducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("SET ");
            _sb.Append(TableDetalleReses._VENDIDO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append(" WHERE ");
            _sb.Append("NUM_PEDIDO = '" + numpedido + "' ");
            _sb.Append(" AND ");
            _sb.Append("NO_CLIENTE = '" + codcliente + "'");
            _sb.Append(" AND ");
            _sb.Append("ARTICULO = '" + codproducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);

        }

        /// <summary>
        ///Metodo Encargado de consultar los detalles reses que el cliente este adquiriendo
        ///al facturar
        /// </summary>
        internal List<pnlTransacciones_ltvDetalleReses> buscarDetalleReses(string articulo, string codcliente, string codpedido)
        {
            List<pnlTransacciones_ltvDetalleReses> _ListaDetalleReses = new List<pnlTransacciones_ltvDetalleReses>();

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleReses._CONSECUTIVO + ", ");
            _sb.Append(TableDetalleReses._NO_CIA + ", ");
            _sb.Append(TableDetalleReses._NUM_PEDIDO + ", ");
            _sb.Append(TableDetalleReses._NO_CLIENTE + ", ");
            _sb.Append(TableDetalleReses._ARTICULO + ", ");
            _sb.Append(TableDetalleReses._IND_TIPO + ", ");
            _sb.Append(TableDetalleReses._FECHA_MATANZA + ", ");
            _sb.Append(TableDetalleReses._LOTE + ", ");
            _sb.Append(TableDetalleReses._NO_ANIMAL + ", ");
            _sb.Append(TableDetalleReses._TIPO_PORCION + ", ");
            _sb.Append(TableDetalleReses._COMPROMETIDO + ", ");
            _sb.Append(TableDetalleReses._VENDIDO + ", ");
            _sb.Append(TableDetalleReses._PESO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE ARTICULO ='" + articulo + "'");
            _sb.Append(" AND ");
            _sb.Append("NO_CLIENTE ='"+codcliente+"'");
            _sb.Append(" AND ");
            _sb.Append("COMPROMETIDO ='" + SQL._Si + "'");
            _sb.Append(" AND ");
            _sb.Append("VENDIDO ='" + SQL._No + "'");

            if (!string.IsNullOrEmpty(codpedido))
            {
                _sb.Append(" AND ");
                _sb.Append("NUM_PEDIDO ='" + codpedido + "'");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable DatosMensaje = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in DatosMensaje.Rows)
            {
                pnlTransacciones_ltvDetalleReses _DetalleReses = new pnlTransacciones_ltvDetalleReses();

                _DetalleReses._vc_consecutivo = _fila["CONSECUTIVO"].ToString();

                _DetalleReses._vc_cia = _fila["NO_CIA"].ToString();

                _DetalleReses._vc_numpedido = _fila["NUM_PEDIDO"].ToString();

                _DetalleReses._vc_nocliente = _fila["NO_CLIENTE"].ToString();

                _DetalleReses._vc_articulo = _fila["ARTICULO"].ToString();

                _DetalleReses._vc_indtipo = _fila["IND_TIPO"].ToString();

                _DetalleReses._vc_fechamatanza = _fila["FECHA_MATANZA"].ToString();

                _DetalleReses._vc_lote = _fila["LOTE"].ToString();

                _DetalleReses._vc_noanimal = _fila["NO_ANIMAL"].ToString();

                _DetalleReses._vc_tipoporcion = _fila["TIPO_PORCION"].ToString();

                _DetalleReses._vc_comprometido = _fila["COMPROMETIDO"].ToString();

                _DetalleReses._vc_vendido = _fila["VENDIDO"].ToString();

                _DetalleReses._vc_peso = Decimal.Parse(_fila["PESO"].ToString());

                _ListaDetalleReses.Add(_DetalleReses);

            }

            return _ListaDetalleReses;
        }

        /// <summary>
        ///Metodo Encargado de consultar los detalles reses que el cliente este adquiriendo en la factura
        ///al facturar
        /// </summary>
        internal List<pnlTransacciones_ltvDetalleReses> buscarDetalleResesFactura(string articulo, string codcliente, string CodFactura)
        {
            List<pnlTransacciones_ltvDetalleReses> _ListaDetalleReses = new List<pnlTransacciones_ltvDetalleReses>();

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleReses._CONSECUTIVO + ", ");
            _sb.Append(TableDetalleReses._NO_CIA + ", ");
            _sb.Append(TableDetalleReses._NUM_PEDIDO + ", ");
            _sb.Append(TableDetalleReses._NO_CLIENTE + ", ");
            _sb.Append(TableDetalleReses._ARTICULO + ", ");
            _sb.Append(TableDetalleReses._IND_TIPO + ", ");
            _sb.Append(TableDetalleReses._FECHA_MATANZA + ", ");
            _sb.Append(TableDetalleReses._LOTE + ", ");
            _sb.Append(TableDetalleReses._NO_ANIMAL + ", ");
            _sb.Append(TableDetalleReses._TIPO_PORCION + ", ");
            _sb.Append(TableDetalleReses._COMPROMETIDO + ", ");
            _sb.Append(TableDetalleReses._VENDIDO + ", ");
            _sb.Append(TableDetalleReses._PESO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE ARTICULO ='" + articulo + "'");
            _sb.Append(" AND ");
            _sb.Append("NO_CLIENTE ='" + codcliente + "'");
            _sb.Append(" AND ");
            _sb.Append("NUM_PEDIDO ='" + CodFactura + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable DatosMensaje = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in DatosMensaje.Rows)
            {
                pnlTransacciones_ltvDetalleReses _DetalleReses = new pnlTransacciones_ltvDetalleReses();

                _DetalleReses._vc_consecutivo = _fila["CONSECUTIVO"].ToString();

                _DetalleReses._vc_cia = _fila["NO_CIA"].ToString();

                _DetalleReses._vc_numpedido = _fila["NUM_PEDIDO"].ToString();

                _DetalleReses._vc_nocliente = _fila["NO_CLIENTE"].ToString();

                _DetalleReses._vc_articulo = _fila["ARTICULO"].ToString();

                _DetalleReses._vc_indtipo = _fila["IND_TIPO"].ToString();

                _DetalleReses._vc_fechamatanza = _fila["FECHA_MATANZA"].ToString();

                _DetalleReses._vc_lote = _fila["LOTE"].ToString();

                _DetalleReses._vc_noanimal = _fila["NO_ANIMAL"].ToString();

                _DetalleReses._vc_tipoporcion = _fila["TIPO_PORCION"].ToString();

                _DetalleReses._vc_comprometido = _fila["COMPROMETIDO"].ToString();

                _DetalleReses._vc_vendido = _fila["VENDIDO"].ToString();

                _DetalleReses._vc_peso = Decimal.Parse(_fila["PESO"].ToString());

                _ListaDetalleReses.Add(_DetalleReses);

            }

            return _ListaDetalleReses;
        }

        /// <summary>
        ///Metodo Encargado de actualizar el detalle res, reasignandolo al nuevo cliente
        ///para ello primero tuvo que haber sido eliminado como comprometido del cliente original
        /// </summary>
        internal void ActualizarDetalleResPropietario(string consecutivo, string codcliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("SET ");
            _sb.Append(TableDetalleReses._COMPROMETIDO + " = ");
            _sb.Append("'" + SQL._Si + "', ");
            _sb.Append(TableDetalleReses._NO_CLIENTE + " = ");
            _sb.Append("'" + codcliente + "' ");
            _sb.Append(" WHERE ");
            _sb.Append("CONSECUTIVO = " + consecutivo);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);

        }

        /// <summary>
        ///Metodo Encargado de actualizar el detalle res, reasignandolo al nuevo num pedido
        /// </summary>
        internal void ActualizarDetalleResNumPedido(string old_numpedido, string new_numpedido)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("SET ");
            _sb.Append(TableDetalleReses._NUM_PEDIDO + " = ");
            _sb.Append("'" + new_numpedido + "'");
            _sb.Append(" WHERE ");
            _sb.Append("NUM_PEDIDO = '" + old_numpedido + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);

        }

        /// <summary>
        ///Metodo Encargado de actualizar el detalle res, reasignandolo al nuevo num pedido
        /// </summary>
        internal void ActualizarNP(Cliente Cliente, string new_numpedido)
        {
            string conditionNotIn = string.Empty;

            foreach (TransaccionDetalle _objTransaccionDetalle in Cliente.v_objTransaccion.v_listaDetalles)
            {
                if (_objTransaccionDetalle.v_Es_Factura.Equals(SQL._Si))
                {
                    if (!string.IsNullOrEmpty(conditionNotIn))
                    {
                        conditionNotIn += Simbol._comma;
                    }

                    conditionNotIn += "'" + _objTransaccionDetalle.v_objProducto.v_codProducto + "'";
                }
            }

            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("SET ");
            _sb.Append(TableDetalleReses._NUM_PEDIDO + " = ");
            _sb.Append("'" + new_numpedido + "'");
            _sb.Append(" WHERE ");
            _sb.Append("NO_CLIENTE = '" + Cliente.v_no_cliente + "'");
            _sb.Append(" AND ");
            _sb.Append("VENDIDO = ");
            _sb.Append("'" + SQL._No + "'");

            if (!string.IsNullOrEmpty(conditionNotIn))
            {
                _sb.Append(" AND ");
                _sb.Append("ARTICULO NOT IN( ");
                _sb.Append(conditionNotIn);
                _sb.Append(") ");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        /// <summary>
        ///Metodo Encargado de actualizar el detalle res, reasignandolo al nuevo num pedido correcto
        /// </summary>
        internal void ActualizarNPReAsignado(Cliente Cliente, string new_numpedido)
        {
            foreach (TransaccionDetalle _objTransaccionDetalle in Cliente.v_objTransaccion.v_listaDetalles ?? new List<TransaccionDetalle>())
            {
                if (_objTransaccionDetalle.v_Es_Factura.Equals(SQL._Si))
                {
                    foreach (var _objDetalleRes in _objTransaccionDetalle.v_listaDetalleReses ?? new List<pnlTransacciones_ltvDetalleReses>())
                    {
                        StringBuilder _sb = new StringBuilder();

                        _sb.Append("UPDATE ");
                        _sb.Append(TablesROL._DetalleReses + " ");
                        _sb.Append("SET ");
                        _sb.Append(TableDetalleReses._NUM_PEDIDO + " = ");
                        _sb.Append("'" + new_numpedido + "'");
                        _sb.Append(" WHERE ");
                        _sb.Append("CONSECUTIVO = '" + _objDetalleRes._vc_consecutivo + "'");
                        _sb.Append(" AND ");
                        _sb.Append("VENDIDO = ");
                        _sb.Append("'" + SQL._No + "'");

                        var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                        MultiGeneric.updateRecord(_sb);
                    }
                }
            }
        }

        /// <summary>
        ///Metodo encargado de definir si el articulo posee detalle res
        /// </summary>
        internal bool ExisteDetalleRes(string articulo, string CodPedido)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ARTICULO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE ");
            _sb.Append("ARTICULO = ");
            _sb.Append("'" + articulo + "' ");
            _sb.Append("AND ");
            _sb.Append("VENDIDO = ");
            _sb.Append("'" + SQL._No + "' ");
            if (!string.IsNullOrEmpty(CodPedido))
            {
                _sb.Append("AND ");
                _sb.Append("NUM_PEDIDO = ");
                _sb.Append("'" + CodPedido + "'");
            }

            return OperationSQL.thereRecord(_sb, "ARTICULO");
        }

        /// <summary>
        ///Metodo encargado de definir si el articulo es parte de detalle reses
        /// </summary>
        internal bool EsDetalleRes(string articulo)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ARTICULO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE ");
            _sb.Append("ARTICULO = ");
            _sb.Append("'" + articulo + "' ");

            return OperationSQL.thereRecord(_sb, "ARTICULO");
        }

        /// <summary>
        ///Metodo encargado de definir si el articulo posee detalle res
        ///comprometido por el cliente actualmente, en cuyo caso debe agregarlo desde
        ///visita al cargar el pedido
        /// </summary>
        internal bool ExisteDetalleResComprometidoCliente(string codcliente, string articulo) {

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ARTICULO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + codcliente + "' ");
            _sb.Append("AND ");
            _sb.Append("ARTICULO = ");
            _sb.Append("'" + articulo + "' ");
            _sb.Append("AND ");
            _sb.Append("COMPROMETIDO = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("AND ");
            _sb.Append("VENDIDO = ");
            _sb.Append("'" + SQL._No + "'");

            return OperationSQL.thereRecord(_sb, "ARTICULO");
        }

        /// <summary>
        ///Metodo encargado de definir si el detalle res existe como pedido
        /// </summary>
        internal bool ExistePedidoDetalleRes(string NumPedido, bool vendido)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NUM_PEDIDO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE ");
            _sb.Append("NUM_PEDIDO = ");
            _sb.Append("'" + NumPedido + "' ");
            _sb.Append("AND ");
            _sb.Append("VENDIDO = ");
            if (vendido)
            {
                _sb.Append("'" + SQL._Si + "'");
            }
            else {
                _sb.Append("'" + SQL._No + "'");
            }
            

            return OperationSQL.thereRecord(_sb, "NUM_PEDIDO");
        }

        /// <summary>
        ///Metodo encargado de alimentar la pantalla principal de detallereses,
        ///encuentra los elementos que un cliente tiene y que no han sido vendidos
        /// </summary>
        internal bool buscarDetallesReses(ListView list, string articulo, string codcliente, string cod_pedido)
        {
            StringBuilder _sb = new StringBuilder();
            bool val = false;

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleReses._CONSECUTIVO + ", ");
            _sb.Append(TableDetalleReses._NO_CIA + ", ");
            _sb.Append(TableDetalleReses._NUM_PEDIDO + ", ");
            _sb.Append(TableDetalleReses._NO_CLIENTE + ", ");
            _sb.Append(TableDetalleReses._ARTICULO + ", ");
            _sb.Append(TableDetalleReses._IND_TIPO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y',"+ TableDetalleReses._FECHA_MATANZA + ") "+ TableDetalleReses._FECHA_MATANZA + ", ");
            _sb.Append(TableDetalleReses._LOTE + ", ");
            _sb.Append(TableDetalleReses._NO_ANIMAL + ", ");
            _sb.Append(TableDetalleReses._TIPO_PORCION + ", ");
            _sb.Append(TableDetalleReses._PESO + ", ");
            _sb.Append(TableDetalleReses._COMPROMETIDO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE ARTICULO ='" + articulo + "'");
            _sb.Append(" AND ");
            _sb.Append("NO_CLIENTE ='" + codcliente + "' ");
            _sb.Append("AND ");
            _sb.Append("VENDIDO = '" +SQL._No+ "'");

            if (!string.IsNullOrEmpty(cod_pedido))
            {
                _sb.Append("AND ");
                _sb.Append("NUM_PEDIDO = '" + cod_pedido + "'");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable DatosMensaje = MultiGeneric.uploadDataTable(_sb);

            var Source = list.ItemsSource as SelectableObservableCollection<pnlTransacciones_ltvDetalleReses>;

            foreach (DataRow _fila in DatosMensaje.Rows)
            {
                pnlTransacciones_ltvDetalleReses _LtvViceras = new pnlTransacciones_ltvDetalleReses();

                _LtvViceras._vc_consecutivo = _fila["CONSECUTIVO"].ToString();

                _LtvViceras._vc_cia = _fila["NO_CIA"].ToString();

                _LtvViceras._vc_numpedido = _fila["NUM_PEDIDO"].ToString();

                _LtvViceras._vc_nocliente = _fila["NO_CLIENTE"].ToString();

                _LtvViceras._vc_articulo = _fila["ARTICULO"].ToString();

                _LtvViceras._vc_indtipo = _fila["IND_TIPO"].ToString();

                _LtvViceras._vc_fechamatanza = _fila["FECHA_MATANZA"].ToString();

                _LtvViceras._vc_lote = _fila["LOTE"].ToString();

                _LtvViceras._vc_noanimal = _fila["NO_ANIMAL"].ToString();

                _LtvViceras._vc_tipoporcion = _fila["TIPO_PORCION"].ToString();

                _LtvViceras._vc_peso = Decimal.Parse(_fila["PESO"].ToString());

                _LtvViceras._vc_comprometido = _fila["COMPROMETIDO"].ToString();

                Source.Add(_LtvViceras);

                val = true;

            }

            foreach (var _lvi in Source)
            {
                if (_lvi.Data._vc_comprometido.Equals(SQL._Si))
                {
                    _lvi.IsSelected = true;
                }
            }

            list.ItemsSource = Source;

            return val;
        }

        /// <summary>
        ///Metodo encargado de alimentar la pantalla principal de detallereses,
        ///encuentra los elementos que un cliente tiene, que no han sido vendidos
        ///y que otro cliente no tiene reservados
        /// </summary>
        internal bool buscarDetallesResesDisponible(ListView list, string articulo, string codcliente)
        {
            StringBuilder _sb = new StringBuilder();

            bool value = false;

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleReses._CONSECUTIVO + ", ");
            _sb.Append(TableDetalleReses._NO_CIA + ", ");
            _sb.Append(TableDetalleReses._NUM_PEDIDO + ", ");
            _sb.Append(TableDetalleReses._NO_CLIENTE + ", ");
            _sb.Append(TableDetalleReses._ARTICULO + ", ");
            _sb.Append(TableDetalleReses._IND_TIPO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableDetalleReses._FECHA_MATANZA + ") " + TableDetalleReses._FECHA_MATANZA + ", ");
            _sb.Append(TableDetalleReses._LOTE + ", ");
            _sb.Append(TableDetalleReses._NO_ANIMAL + ", ");
            _sb.Append(TableDetalleReses._TIPO_PORCION + ", ");
            _sb.Append(TableDetalleReses._PESO + ", ");
            _sb.Append(TableDetalleReses._COMPROMETIDO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE ARTICULO ='" + articulo + "'");
            _sb.Append(" AND ");
            _sb.Append("(NO_CLIENTE ='" + codcliente + "' OR COMPROMETIDO = '"+SQL._No+"') ");
            _sb.Append("AND ");
            _sb.Append("VENDIDO = '" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable DatosMensaje = MultiGeneric.uploadDataTable(_sb);

            var Source = list.ItemsSource as SelectableObservableCollection<pnlTransacciones_ltvDetalleReses>;

            foreach (DataRow _fila in DatosMensaje.Rows)
            {
                pnlTransacciones_ltvDetalleReses _LtvViceras = new pnlTransacciones_ltvDetalleReses();

                _LtvViceras._vc_consecutivo = _fila["CONSECUTIVO"].ToString();

                _LtvViceras._vc_cia = _fila["NO_CIA"].ToString();

                _LtvViceras._vc_numpedido = _fila["NUM_PEDIDO"].ToString();

                _LtvViceras._vc_nocliente = _fila["NO_CLIENTE"].ToString();

                _LtvViceras._vc_articulo = _fila["ARTICULO"].ToString();

                _LtvViceras._vc_indtipo = _fila["IND_TIPO"].ToString();

                _LtvViceras._vc_fechamatanza = _fila["FECHA_MATANZA"].ToString();

                _LtvViceras._vc_lote = _fila["LOTE"].ToString();

                _LtvViceras._vc_noanimal = _fila["NO_ANIMAL"].ToString();

                _LtvViceras._vc_tipoporcion = _fila["TIPO_PORCION"].ToString();

                _LtvViceras._vc_peso = Decimal.Parse(_fila["PESO"].ToString());

                _LtvViceras._vc_comprometido = _fila["COMPROMETIDO"].ToString();

                Source.Add(_LtvViceras);

                value = true;

            }

            foreach (var _lvi in Source)
            {
                if (_lvi.Data._vc_comprometido.Equals(SQL._Si))
                {
                    _lvi.IsSelected = true;
                }
            }

            list.ItemsSource = Source;

            return value;
        }

        /// <summary>
        ///Metodo encargado de actualizar los comprometidos segun la lista de detalle
        ///reses en caso de que un cliente no lo desee
        /// </summary>
        internal void ActualizarComprometidoDetalleRes(string consecutivo, bool comp) {

            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("SET ");
            _sb.Append(TableDetalleReses._COMPROMETIDO + " = ");
            if (comp == true)
            {
                _sb.Append("'" + SQL._Si + "'");
            }
            else {

                _sb.Append("'" + SQL._No + "'");
            }
            _sb.Append(" WHERE ");
            _sb.Append("CONSECUTIVO = " + consecutivo);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);

        }

        /// <summary>
        ///Metodo encargado de actualizar los vendidos segun la lista de detalle
        ///reses, serviria para guardar documento
        /// </summary>
        internal void ActualizarVendidoDetalleRes(string consecutivo, string numpedido, bool vend, string newCodPedido = null)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("SET ");
            _sb.Append(TableDetalleReses._VENDIDO + " = ");
            if (vend == true)
            {
                _sb.Append("'" + SQL._Si + "', ");
                _sb.Append(TableDetalleReses._NUM_PEDIDO + " =");

                if (!string.IsNullOrEmpty(newCodPedido))
                {
                    _sb.Append("'" + newCodPedido + "'");
                }
                else
                {
                    _sb.Append("'" + numpedido + "'");
                }
            }
            else
            {
                _sb.Append("'" + SQL._No + "'");
            }
            
            _sb.Append(" WHERE ");
            _sb.Append("CONSECUTIVO = " + consecutivo);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);

        }

        /// <summary>
        ///Metodo encargado de identificar la suma de pesos que conforman un pedido
        ///segun sus detalle reses, la idea es poder tener un resultado para formar
        ///posteriormente la linea del producto que ira en visita, todo esto
        ///vinculado a un pedido
        /// </summary>
        internal List<pnlTransacciones_ltvDetalleReses> buscarDetalleResesPedido(string No_Cliente, string Num_Pedido, string articulo) {

            StringBuilder _sb = new StringBuilder();

            List<pnlTransacciones_ltvDetalleReses> _ListaDetallesReses = new List<pnlTransacciones_ltvDetalleReses>();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleReses._ARTICULO + ", ");
            _sb.Append(TableDetalleReses._CONSECUTIVO + ", ");
            _sb.Append(TableDetalleReses._PESO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE NO_CLIENTE ='" + No_Cliente + "'");
            _sb.Append(" AND ");
            _sb.Append("NUM_PEDIDO ='" + Num_Pedido + "' ");
            _sb.Append("AND ");
            _sb.Append("COMPROMETIDO = '" + SQL._Si + "' ");
            _sb.Append("AND ");
            _sb.Append("VENDIDO = '" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("ARTICULO = '" + articulo + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable DatosMensaje = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in DatosMensaje.Rows)
            {
                pnlTransacciones_ltvDetalleReses DetalleReses = new pnlTransacciones_ltvDetalleReses();

                DetalleReses._vc_numpedido = Num_Pedido;

                DetalleReses._vc_nocliente = No_Cliente;

                DetalleReses._vc_consecutivo = _fila["CONSECUTIVO"].ToString();

                DetalleReses._vc_articulo = _fila["ARTICULO"].ToString();

                DetalleReses._vc_peso = Decimal.Parse(_fila["PESO"].ToString());

                _ListaDetallesReses.Add(DetalleReses);

            }

            return _ListaDetallesReses;

        }

        /// <summary>
        ///Metodo encargado de identificar los detalles reses para asignar a las visceras
        /// </summary>
        /// return lista de detallereses vinculadas con visceras
        internal List<pnlTransacciones_ltvDetalleReses> BuscarDetallesResesIndicadoresVisceras(string articulo, string codcliente, bool variable) {

            StringBuilder _sb = new StringBuilder();
            List<pnlTransacciones_ltvDetalleReses> _ListaDetallesReses = new List<pnlTransacciones_ltvDetalleReses>();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleReses._CONSECUTIVO + ", ");
            _sb.Append(TableDetalleReses._NO_CIA + ", ");
            _sb.Append(TableDetalleReses._NUM_PEDIDO + ", ");
            _sb.Append(TableDetalleReses._NO_CLIENTE + ", ");
            _sb.Append(TableDetalleReses._ARTICULO + ", ");
            _sb.Append(TableDetalleReses._IND_TIPO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableDetalleReses._FECHA_MATANZA + ") " + TableDetalleReses._FECHA_MATANZA + ", ");
            _sb.Append(TableDetalleReses._LOTE + ", ");
            _sb.Append(TableDetalleReses._NO_ANIMAL + ", ");
            _sb.Append(TableDetalleReses._TIPO_PORCION + ", ");
            _sb.Append(TableDetalleReses._PESO + ", ");
            _sb.Append(TableDetalleReses._COMPROMETIDO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE ARTICULO ='" + articulo + "'");
            _sb.Append(" AND ");
            _sb.Append("NO_CLIENTE ='" + codcliente + "' ");
            _sb.Append("AND ");
            _sb.Append("VENDIDO = '" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("COMPROMETIDO = '" + SQL._Si + "' ");
            _sb.Append("AND ");
            _sb.Append("VASIGNADO = '");

            if (variable)
            {
                _sb.Append(SQL._Si + "' ");
            }
            else
            {
                _sb.Append(SQL._No + "' ");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable DetallesReses = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in DetallesReses.Rows)
            {
                pnlTransacciones_ltvDetalleReses _LtvViceras = new pnlTransacciones_ltvDetalleReses();

                _LtvViceras._vc_consecutivo = _fila["CONSECUTIVO"].ToString();

                _LtvViceras._vc_cia = _fila["NO_CIA"].ToString();

                _LtvViceras._vc_numpedido = _fila["NUM_PEDIDO"].ToString();

                _LtvViceras._vc_nocliente = _fila["NO_CLIENTE"].ToString();

                _LtvViceras._vc_articulo = _fila["ARTICULO"].ToString();

                _LtvViceras._vc_indtipo = _fila["IND_TIPO"].ToString();

                _LtvViceras._vc_fechamatanza = _fila["FECHA_MATANZA"].ToString();

                _LtvViceras._vc_lote = _fila["LOTE"].ToString();

                _LtvViceras._vc_noanimal = _fila["NO_ANIMAL"].ToString();

                _LtvViceras._vc_tipoporcion = _fila["TIPO_PORCION"].ToString();

                _LtvViceras._vc_peso = Decimal.Parse(_fila["PESO"].ToString());

                _LtvViceras._vc_comprometido = _fila["COMPROMETIDO"].ToString();

                _LtvViceras._vcVasignado = SQL._No;

                _ListaDetallesReses.Add(_LtvViceras);

            }

            return _ListaDetallesReses;
        }
    }
}
