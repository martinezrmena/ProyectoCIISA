using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperRazonesNV
    {
        internal DataTable buscarRazonesNV()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("CODIGO, ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._razonesNV + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string obtenerCodigoRazonesNV(string pnomRazon)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            //_sb.Append("NO_CIA, ");
            _sb.Append("CODIGO ");
            //_sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._razonesNV + " ");
            _sb.Append("WHERE ");
            _sb.Append("DESCRIPCION = ");
            _sb.Append("'" + pnomRazon + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerDescripcionRazonesNV(string pcodRazonNV)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            //_sb.Append("NO_CIA, ");
            //_sb.Append("CODIGO, ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._razonesNV + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODIGO = ");
            _sb.Append("'" + pcodRazonNV + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
