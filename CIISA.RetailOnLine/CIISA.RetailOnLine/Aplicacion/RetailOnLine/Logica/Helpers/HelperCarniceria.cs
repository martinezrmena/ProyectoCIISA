using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperCarniceria
    {
        internal string obtenerCodigoAgente()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAgenteVendedor._NO_AGENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");

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

    }
}
