using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Respaldo.Helpers
{
    internal class HelperEncabezadoRazonesNV
    {
        internal void RespaldarEncabezadoRazonesNoVenta()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._encabezadoRazonesNVBK);
            _sb.Append(" SELECT * FROM ");
            _sb.Append(TablesROL._encabezadoRazonesNV);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.InsertRecordBackUp(_sb);
        }

    }
}
