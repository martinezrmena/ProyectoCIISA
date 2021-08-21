using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Respaldo.Helpers.Carniceria
{
    internal class HelperBitacora
    {
        internal void RespaldarBitacora()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._bitacorabk);
            _sb.Append(" SELECT * FROM ");
            _sb.Append(TablesROL._bitacora);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.InsertRecordBackUp(_sb);
        }
    }
}
