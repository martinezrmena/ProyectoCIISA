using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.ListViewMoldels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.External.ResizableColumnsListView;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.InventoryOnLine.Helpers
{
    class HelperInventario
    {
        internal void buscarInventarioTomaFisica(ListView ppnlTomaFisica_ltvInventario)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + ", ");
            _sb.Append("I." + TableInventario._DISPONIBLE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " I, ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("I." + TableInventario._CODPRODUCTO);

            /*DataTable _dt = MultiGeneric.uploadDataTable(_sb);
            ppnlTomaFisica_ltvInventario.Items.Clear();*/
            ppnlTomaFisica_ltvInventario.ItemsSource = new ObservableCollection<pnlTomaFisica_ltvInventario>();

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            DataTable _dt = MultiGeneric.uploadDataTable(_sb);
            var Source = ppnlTomaFisica_ltvInventario.ItemsSource as ObservableCollection<pnlTomaFisica_ltvInventario>;
            TemplateStyles Style = new TemplateStyles();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlTomaFisica_ltvInventario _lvi = new pnlTomaFisica_ltvInventario();
                //
                _lvi.Estilo = Style.ExpandedStyle;

                _lvi.Codigo = _fila[TableInventario._CODPRODUCTO].ToString();

                _lvi.Descripcion = _fila[TableProducto._DESCPRODUCTO].ToString();

                _lvi.Cantidad = double.Parse(_fila[TableInventario._DISPONIBLE].ToString()).ToString("N2");

                _lvi.TomaFisica = double.Parse(_fila[TableInventario._DISPONIBLE].ToString()).ToString("N2");

                _lvi.Diferencia = Numeric._zeroDecimal;
                
                Source.Add(_lvi);
            }

            ppnlTomaFisica_ltvInventario.ItemsSource = Source;
        } 

        internal void actualizarConsolidado()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("SET ");
            _sb.Append(TableInventario._CONSOLIDADO + " = ");
            _sb.Append("'" + SQL._Si + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            MultiGeneric.updateRecord(_sb);
        }

        internal void actualizarInventarioTomaFisica(ListView ppnlTomaFisica_ltvInventario)
        {
            //foreach (ListViewItem _lvi in ppnlTomaFisica_ltvInventario.Items)
            foreach (pnlTomaFisica_ltvInventario _lvi in ppnlTomaFisica_ltvInventario.ItemsSource)
            {
                string _codProducto = _lvi.Codigo;
                string _cantidad = _lvi.TomaFisica;//SubItems[IndiceTomaFisica._tomaFisica].Text;
                decimal _cant = FormatUtil.convertStringToDecimal(_cantidad);

                StringBuilder _sb = new StringBuilder();

                _sb.Append("UPDATE ");
                _sb.Append(TablesROL._inventario + " ");
                _sb.Append("SET ");
                _sb.Append(TableInventario._TOMA_FISICA + " = ");
                _sb.Append("'"+ _cant + "', ");
                _sb.Append(TableInventario._FECHATOMACONSOLIDA + " = ");
                //_sb.Append("GETDATE() ");
                _sb.Append("DATE('NOW') ");
                _sb.Append("WHERE ");
                _sb.Append(TableInventario._CODPRODUCTO + " = ");
                _sb.Append("'" + _codProducto + "'");

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();
                MultiGeneric.updateRecord(_sb);
            }
        }
    }
}
