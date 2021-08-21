using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperCuentaCerrada
    {
        internal void buscarListaCuentasCerradas(ListView pltvCuentasCerradas, string pcodCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("NO_CLIENTE, ");
            _sb.Append("NOMBRE, ");
            _sb.Append("STRFTIME('%d/%m/%Y',F_CIERRE) F_CIERRE, ");
            _sb.Append("MOTIVO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._cuentaCerrada + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE ");
            _sb.Append("LIKE ");
            _sb.Append("'%" + pcodCliente + "%'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = pltvCuentasCerradas.ItemsSource as ObservableCollection<pnlCuentas_ltvCuentas>;

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlCuentas_ltvCuentas _lvi = new pnlCuentas_ltvCuentas();

                _lvi.NO_CIA = _fila["NO_CIA"].ToString();

                _lvi.NO_AGENTE = _fila["NO_AGENTE"].ToString();

                _lvi.NO_CLIENTE = _fila["NO_CLIENTE"].ToString();

                _lvi.NOMBRE = _fila["NOMBRE"].ToString();

                _lvi.F_CIERRE = _fila["F_CIERRE"].ToString();

                _lvi.MOTIVO = _fila["MOTIVO"].ToString();

                Source.Add(_lvi);
            }

            pltvCuentasCerradas.ItemsSource = Source;
        }
    }
}
