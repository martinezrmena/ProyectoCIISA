using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Respaldo.Helpers
{
    internal class HelperDetalleTransaccion
    {
        internal void RespaldarDetalleTransaccion()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._detalleDocumentoBK);
            _sb.Append(" SELECT * FROM ");
            _sb.Append(TablesROL._detalleDocumento);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.InsertRecordBackUp(_sb);
        }

    }
}
