using CIISA.RetailOnLine.Aplicacion.AuditOnLine.ListViewModels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.AuditOnLine.Helpers
{
    internal class HelperInventario
    {
        internal void buscarInventarioAuditoria(ListView pltvProductos)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + ", ");
            _sb.Append("I." + TableInventario._CANTIDAD + ", ");
            _sb.Append("I." + TableInventario._DISPONIBLE + ", ");
            _sb.Append("I." + TableInventario._AUDITORIA + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " I, ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("I." + TableInventario._CODPRODUCTO);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = pltvProductos.ItemsSource as ObservableCollection<pnlInventario_ltvInventario>;

            if (Source == null)
            {
                Source = new ObservableCollection<pnlInventario_ltvInventario>();
            }

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlInventario_ltvInventario _lvi = new pnlInventario_ltvInventario();

                _lvi.Codigo = _fila[TableProducto._CODPRODUCTO].ToString();

                _lvi.Descripcion = _fila[TableProducto._DESCPRODUCTO].ToString();

                decimal _cantidad = FormatUtil.convertStringToDecimal(_fila[TableInventario._DISPONIBLE].ToString());

                _lvi.Cantidad = FormatUtil.applyCurrencyFormat(_cantidad);

                decimal _disponible = FormatUtil.convertStringToDecimal(_fila[TableInventario._DISPONIBLE].ToString());

                _lvi.Disponible = FormatUtil.applyCurrencyFormat(_disponible);

                decimal _auditoria = FormatUtil.convertStringToDecimal(_fila[TableInventario._AUDITORIA].ToString());

                _lvi.Auditado = FormatUtil.applyCurrencyFormat(_auditoria);

                _lvi.Diferencia = FormatUtil.applyCurrencyFormat(_auditoria - _disponible);

                Source.Add(_lvi);
            }

            pltvProductos.ItemsSource = Source;
        }

        internal void actualizarInventarioAuditoria(ListView pltvProductos)
        {
            var Source = pltvProductos.ItemsSource as ObservableCollection<pnlInventario_ltvInventario>;

            foreach (var _lvi in Source)
            {
                string _cantidad = _lvi.Auditado;

                StringBuilder _sb = new StringBuilder();

                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._inventario + " ");
                _sb.Append("SET ");
                _sb.Append(TableInventario._AUDITORIA + " = ");
                _sb.Append("'" + FormatUtil.convertStringToDecimal(_cantidad) + "' ");
                _sb.Append("WHERE ");
                _sb.Append(TableInventario._CODPRODUCTO + " = ");
                _sb.Append("'" + _lvi.Codigo + "'");

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                MultiGeneric.updateRecord(_sb);
            }
        }
    }
}
