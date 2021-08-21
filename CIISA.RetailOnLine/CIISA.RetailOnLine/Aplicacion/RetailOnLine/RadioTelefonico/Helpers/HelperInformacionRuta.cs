using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Helpers
{
    internal class HelperInformacionRuta
    {

        internal DataTable buscarInformacionRuta()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInformacionRuta._NO_CIA + ", ");
            _sb.Append(TableInformacionRuta._NO_AGENTE + ", ");
            _sb.Append(TableInformacionRuta._NOMBREAGENTE + ", ");
            _sb.Append(TableInformacionRuta._TIPO_AGENTE + ", ");
            _sb.Append(TableInformacionRuta._ESTADO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._informacionRuta);
            _sb.Append(" ORDER BY ");
            _sb.Append(TableInformacionRuta._NO_AGENTE);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string buscarNombreAgente(string pcodRuta)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInformacionRuta._NOMBREAGENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._informacionRuta + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableInformacionRuta._NO_AGENTE + " = ");
            _sb.Append("'" + pcodRuta + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
