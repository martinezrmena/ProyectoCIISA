using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperTipoIdentificacion
    {
        internal DataTable buscarTipoIdentificacion()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("CODIGO, ");
            _sb.Append("DESCRIPCION, ");
            _sb.Append("FORMATO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._tipoIdentificacion);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string buscarCodigoTipoIdentificacion(string ptipoIdentificacion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("CODIGO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._tipoIdentificacion + " ");
            _sb.Append("WHERE ");
            _sb.Append("DESCRIPCION like ");
            _sb.Append("'%" + ptipoIdentificacion + "%'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
