using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperBanco
    {
        internal void buscarListaCuentasBancarias(ListView pltvCuentasCerradas, string pdescripcion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableBanco._BANCO + ", ");
            _sb.Append(TableBanco._DESCRIPCION + ", ");
            _sb.Append(TableBanco._DESCRIP_RUTERO + ", ");
            _sb.Append(TableBanco._SIGLA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._banco + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableBanco._DESCRIPCION + " LIKE ");
            _sb.Append("'%" + pdescripcion + "%'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = pltvCuentasCerradas.ItemsSource as ObservableCollection<pnlCuentaBancaria_ltvCuentas>;

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlCuentaBancaria_ltvCuentas _lvi = new pnlCuentaBancaria_ltvCuentas();
                _lvi.BANCO = _fila[TableBanco._BANCO].ToString();

                _lvi.DESCRIPCION = _fila[TableBanco._DESCRIPCION].ToString();

                _lvi.DESCRIP_RUTERO = _fila[TableBanco._DESCRIP_RUTERO].ToString();

                _lvi.SIGLA = _fila[TableBanco._SIGLA].ToString();

                Source.Add(_lvi);
            }
            pltvCuentasCerradas.ItemsSource = Source;
        }

        internal string obtenerCodigoBanco(string pnomBanco)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableBanco._BANCO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._banco + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableBanco._DESCRIPCION + " = ");
            _sb.Append("'" + pnomBanco + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal DataTable buscarBanco()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableBanco._BANCO + ", ");
            _sb.Append(TableBanco._DESCRIPCION + ", ");
            _sb.Append(TableBanco._DESCRIP_RUTERO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._banco);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);

        }

        internal string obtenerCuentaBanco(string pnomBanco)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableBanco._DESCRIP_RUTERO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._banco + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableBanco._DESCRIPCION + " = ");
            _sb.Append("'" + pnomBanco + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string buscarSigla(string pcodBanco)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableBanco._SIGLA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._banco + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableBanco._BANCO + " = ");
            _sb.Append("'" + pcodBanco + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
