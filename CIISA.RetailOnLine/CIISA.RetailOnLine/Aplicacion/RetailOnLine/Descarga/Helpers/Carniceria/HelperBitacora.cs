using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers.Carniceria
{
    internal class HelperBitacora
    {
        internal DataTable buscarBitacora()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableBitacora._COD_CLIENTE + ", ");
            _sb.Append(TableBitacora._FECHAVISITA + ", ");
            _sb.Append(TableBitacora._VOLCOMPRA + ", ");
            _sb.Append(TableBitacora._PORCENTAJECOMPRA + ", ");
            _sb.Append(TableBitacora._SITUACION + ", ");
            _sb.Append(TableBitacora._QUEJAS + ", ");
            _sb.Append(TableBitacora._OPORTUNIDADES + ", ");
            _sb.Append(TableBitacora._COMPETENCIAS + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._bitacora + " ");
            _sb.Append("WHERE ");
            _sb.Append("ENVIADO ='" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
