using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperProducto
    {
        internal bool buscarEsViscera(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ES_VISCERA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._producto + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _fila in _dt.Rows)
                {
                    string EsViscera = _fila["ES_VISCERA"].ToString();

                    if (EsViscera.Equals(SQL._Si))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;

        }

        internal string buscarDescripcion(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DESCPRODUCTO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._producto + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string buscarUnidad(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("UNIDAD ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._producto + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal bool BuscarExento(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("EXENTO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._producto + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _exento = MultiGeneric.readStringText(_sb);

            return _exento.Equals(Variable._true);
        }

        internal decimal BuscarProcentajeIVA(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("PORCENTAJE_IVA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._producto + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            decimal porcentajeIVA;

            decimal.TryParse(MultiGeneric.readStringText(_sb), out porcentajeIVA);

            return porcentajeIVA;
        }

        internal Producto buscarProductoPorCodigoProducto(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DATOS.CODPRODUCTO, ");
            _sb.Append("DATOS.DESCPRODUCTO, ");
            _sb.Append("DATOS.UNIDAD, ");
            _sb.Append("DATOS.EXENTO, ");
            _sb.Append("SUM(DATOS.DISPONIBLE) DISPONIBLE ");
            _sb.Append("FROM ");
            _sb.Append("(");
            _sb.Append("SELECT ");
            _sb.Append("P.CODPRODUCTO, ");
            _sb.Append("P.DESCPRODUCTO, ");
            _sb.Append("P.EXENTO, ");
            _sb.Append("P.UNIDAD, ");
            _sb.Append("I.DISPONIBLE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._producto + " P, ");
            _sb.Append(TablesROL._inventario + " I ");
            _sb.Append("WHERE ");
            _sb.Append("P.CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "' ");
            _sb.Append("AND ");
            _sb.Append("P.CODPRODUCTO = ");
            _sb.Append("I.CODPRODUCTO ");
            _sb.Append("UNION ");
            _sb.Append("SELECT ");
            _sb.Append("P.CODPRODUCTO, ");
            _sb.Append("P.DESCPRODUCTO, ");
            _sb.Append("P.EXENTO, ");
            _sb.Append("P.UNIDAD, ");
            _sb.Append("0 DISPONIBLE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("P.CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "'");
            _sb.Append(") ");
            _sb.Append("DATOS ");
            _sb.Append("GROUP BY ");
            _sb.Append("CODPRODUCTO, ");
            _sb.Append("DESCPRODUCTO, ");
            _sb.Append("EXENTO, ");
            _sb.Append("UNIDAD");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            Producto _objProducto = new Producto();

            foreach (DataRow _fila in _dt.Rows)
            {
                _objProducto.v_codProducto = _fila["CODPRODUCTO"].ToString();

                Logica_ManagerEmbalaje _manager = new Logica_ManagerEmbalaje();

                string _embalaje = _manager.buscarEmbalajePorDefinicion(
                                    _fila["CODPRODUCTO"].ToString()
                                    );

                if (_embalaje.Equals(string.Empty))
                {
                    _objProducto.v_embalaje = Numeric._zeroDecimalInitialize;
                }
                else
                {
                    _objProducto.v_embalaje = FormatUtil.convertStringToDecimal(_embalaje);
                }
            }

            return _objProducto;
        }

        internal void buscarListaProducto(ListView pltvProductos,string pnomTipoTransaccion,string ptipoBusqueda,string pfiltro,List<Producto> plistaProductoComprometido)
        {
            StringBuilder _sb = new StringBuilder();

            if (ptipoBusqueda.Equals(VarComboBox._cbxCode))
            {
                _sb.Append("SELECT ");
                _sb.Append("DATOS.CODPRODUCTO, ");
                _sb.Append("DATOS.DESCPRODUCTO, ");
                _sb.Append("SUM(DATOS.DISPONIBLE) DISPONIBLE, ");
                _sb.Append("DATOS.EXENTO ");
                _sb.Append("FROM ");
                _sb.Append("(");
                _sb.Append("SELECT ");
                _sb.Append("P.CODPRODUCTO, ");
                _sb.Append("P.DESCPRODUCTO, ");
                _sb.Append("I.DISPONIBLE, ");
                _sb.Append("P.EXENTO ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._producto + " P, ");
                _sb.Append(TablesROL._inventario + " I ");
                _sb.Append("WHERE ");
                _sb.Append("P.CODPRODUCTO ");
                _sb.Append("LIKE ");
                _sb.Append("'%" + pfiltro + "%' ");
                _sb.Append("AND ");
                _sb.Append("P.CODPRODUCTO = ");
                _sb.Append("I.CODPRODUCTO ");
                _sb.Append("UNION ");
                _sb.Append("SELECT ");
                _sb.Append("P.CODPRODUCTO, ");
                _sb.Append("P.DESCPRODUCTO, ");
                _sb.Append("0 DISPONIBLE, ");
                _sb.Append("P.EXENTO ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._producto + " P ");
                _sb.Append("WHERE ");
                _sb.Append("P.CODPRODUCTO ");
                _sb.Append("LIKE ");
                _sb.Append("'%" + pfiltro + "%'");
                _sb.Append(")");
                _sb.Append("DATOS ");
                _sb.Append("GROUP BY ");
                _sb.Append("CODPRODUCTO, ");
                _sb.Append("DESCPRODUCTO, ");
                _sb.Append("EXENTO");
            }

            if (ptipoBusqueda.Equals(VarComboBox._cbxDescription))
            {
                _sb.Append("SELECT ");
                _sb.Append("DATOS.CODPRODUCTO, ");
                _sb.Append("DATOS.DESCPRODUCTO, ");
                _sb.Append("SUM(DATOS.DISPONIBLE) DISPONIBLE, ");
                _sb.Append("DATOS.EXENTO ");
                _sb.Append("FROM ");
                _sb.Append("(");
                _sb.Append("SELECT ");
                _sb.Append("P.CODPRODUCTO, ");
                _sb.Append("P.DESCPRODUCTO, ");
                _sb.Append("I.CANTIDAD DISPONIBLE, ");
                _sb.Append("P.EXENTO ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._producto + " P, " + TablesROL._inventario + " I ");
                _sb.Append("WHERE ");
                _sb.Append("P.DESCPRODUCTO ");
                _sb.Append("LIKE ");
                _sb.Append("'%" + pfiltro + "%' ");
                _sb.Append("AND ");
                _sb.Append("P.CODPRODUCTO = ");
                _sb.Append("I.CODPRODUCTO ");
                _sb.Append("UNION ");
                _sb.Append("SELECT ");
                _sb.Append("P.CODPRODUCTO, ");
                _sb.Append("P.DESCPRODUCTO, ");
                _sb.Append("0 DISPONIBLE, ");
                _sb.Append("P.EXENTO ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._producto + " P ");
                _sb.Append("WHERE ");
                _sb.Append("P.DESCPRODUCTO ");
                _sb.Append("LIKE ");
                _sb.Append("'%" + pfiltro + "%'");
                _sb.Append(")");
                _sb.Append("DATOS ");
                _sb.Append("GROUP BY ");
                _sb.Append("CODPRODUCTO, ");
                _sb.Append("DESCPRODUCTO, ");
                _sb.Append("EXENTO");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            bool _existeProductoDespachado = false;

            var Source = pltvProductos.ItemsSource as ObservableCollection<pnlProductos_ltvProductos>;

            foreach (DataRow _fila in _dt.Rows)
            {
                decimal _cantProductoInventario = FormatUtil.convertStringToDecimal(_fila["DISPONIBLE"].ToString());

                UtilLogica _util = new UtilLogica();

                decimal _cantProductoComprometido = _util.obtenerCantidadProductoComprometido(
                                                _fila["CODPRODUCTO"].ToString(),
                                                plistaProductoComprometido
                                                );

                decimal _cantProductoReal = _cantProductoInventario - _cantProductoComprometido;

                pnlProductos_ltvProductos _lvi = new pnlProductos_ltvProductos();
                //ListViewItem _lvi = null;

                if (pnomTipoTransaccion.Equals(ROLTransactions._facturaContadoNombre)
                    || pnomTipoTransaccion.Equals(ROLTransactions._facturaCreditoNombre)
                    || pnomTipoTransaccion.Equals(ROLTransactions._regaliaNombre))
                {
                    _lvi.Disponible = FormatUtil.applyCurrencyFormat(_cantProductoReal);
                    //_lvi = new ListViewItem(FormatUtil.applyCurrencyFormat(
                    //                            _cantProductoReal
                    //                            ));

                    HelperDetallePedido _helper = new HelperDetallePedido();

                    _existeProductoDespachado = _helper.existeProductoDespachado(
                                                        _fila["CODPRODUCTO"].ToString()
                                                        );

                    decimal _cantProductoPedidos = Numeric._zeroDecimalInitialize;

                    if (_existeProductoDespachado)
                    {
                        _cantProductoPedidos = _helper.obtenerInventarioReservadoPedidos(
                                                        _fila["CODPRODUCTO"].ToString()
                                                        );
                    }
                    else
                    {
                        _cantProductoPedidos = Numeric._zeroInteger;
                    }

                    _lvi.Reservado=FormatUtil.applyCurrencyFormat(
                                                _cantProductoPedidos
                                                );
                }
                else
                {
                    _lvi.Disponible=FormatUtil.applyCurrencyFormat(
                                                _cantProductoInventario
                                                );

                    _lvi.Reservado="N/A";
                }

                _lvi.Codigo=_fila["CODPRODUCTO"].ToString();
                _lvi.Descripcion=_fila["DESCPRODUCTO"].ToString();
                _lvi.Exento=_fila["EXENTO"].ToString();

                if (pnomTipoTransaccion.Equals(ROLTransactions._facturaContadoNombre)
                    || pnomTipoTransaccion.Equals(ROLTransactions._facturaCreditoNombre)
                    || pnomTipoTransaccion.Equals(ROLTransactions._regaliaNombre))
                {
                    if (_cantProductoReal > 0)
                    {
                        //pltvProductos.Items.Add(_lvi);
                        Source.Add(_lvi);
                    }
                }
                else
                {                    
                    if (_cantProductoReal <= 0)
                    {
                        //RenderPaint.paintRedItemListView(_lvi);
                        _lvi.ItemColor = Color.Red;
                    }
                    else
                    {
                        _lvi.ItemColor = Color.Default;
                    }

                    //pltvProductos.Items.Add(_lvi);
                    Source.Add(_lvi);
                }

                //pltvProductos.Update();
                pltvProductos.ItemsSource = Source;
            }

            //if (pltvProductos.Items.Count == 1)
            if (Source.Count == 1)
            {
                //for (int i = 0; i < pltvProductos.Items.Count; i++)
                for (int i = 0; i < Source.Count; i++)
                {
                    //pltvProductos.Items[i].Selected = true;
                    //pltvProductos.Items[i].Focused = true;
                    pltvProductos.SelectedItem = Source[i];
                }
            }
        }
    }
}
