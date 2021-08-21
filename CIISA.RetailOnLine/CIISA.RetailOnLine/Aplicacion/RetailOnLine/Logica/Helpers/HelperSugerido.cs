using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperSugerido
    {
        internal void buscarListaSugeridos(ListView pltvSugeridos, Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("S.NO_CIA, ");
            _sb.Append("S.NO_AGENTE, ");
            _sb.Append("S.CODPRODUCTO, ");
            _sb.Append("S.CANTIDAD, ");
            _sb.Append("P.DESCPRODUCTO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._sugerido + " S, ");
            _sb.Append("PRODUCTO P ");
            _sb.Append("WHERE ");
            _sb.Append("S.NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("S.CODPRODUCTO = ");
            _sb.Append("P.CODPRODUCTO");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = pltvSugeridos.ItemsSource as SelectableObservableCollection<pnlSugerido_ltvSugerido>;

            foreach (DataRow _fila in _dt.Rows)
            {
                string _descProducto = string.Empty;

                pnlSugerido_ltvSugerido _lvi = new pnlSugerido_ltvSugerido();

                _lvi.CODPRODUCTO = _fila["CODPRODUCTO"].ToString();

                _lvi.DESCPRODUCTO = _fila["DESCPRODUCTO"].ToString();

                _lvi.CANTIDAD = _fila["CANTIDAD"].ToString();

                _lvi.NO_CIA = _fila["NO_CIA"].ToString();

                _lvi.NO_AGENTE = _fila["NO_AGENTE"].ToString();

                Source.Add(_lvi);
            }

            pltvSugeridos.ItemsSource = Source;
        }
    }
}
