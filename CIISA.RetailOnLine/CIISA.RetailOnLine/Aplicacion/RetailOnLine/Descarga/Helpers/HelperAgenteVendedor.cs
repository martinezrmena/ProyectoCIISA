using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers
{
    internal class HelperAgenteVendedor
    {
        internal DataTable buscarConsecutivosDocumentos()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_DOC + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_REC + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_CLI + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_OV + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_P + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_DV + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_RG + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_RC + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_AN + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_TR + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_PD + ", ");
            _sb.Append(TableAgenteVendedor._CONSECUTIVO_VT + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
