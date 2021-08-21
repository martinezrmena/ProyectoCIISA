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

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers
{
    internal class HelperCliente
    {
        internal DataTable buscarClienteSinEnviar()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableCliente._NO_CIA + ", ");
            _sb.Append(TableCliente._NO_CLIENTE + ", ");
            _sb.Append(TableCliente._NOMBRE + ", ");
            _sb.Append(TableCliente._NOMBRE_LARGO + ", ");
            _sb.Append(TableCliente._CEDULA + ", ");
            _sb.Append(TableCliente._EXCENTO_IMP + ", ");
            _sb.Append(TableCliente._PLAZO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableCliente._F_CIERRE + " ) " + TableCliente._F_CIERRE + ", ");
            _sb.Append(TableCliente._LISTA_PRECIOS + ", ");
            _sb.Append(TableCliente._PAIS + ", ");
            _sb.Append(TableCliente._PROVINCIA + ", ");
            _sb.Append(TableCliente._CANTON + ", ");
            _sb.Append(TableCliente._DISTRITO + ", ");
            _sb.Append(TableCliente._DIRECCION + ", ");
            _sb.Append(TableCliente._TELEFONO + ", ");
            _sb.Append(TableCliente._NOMBRE_ENC + ", ");
            _sb.Append(TableCliente._NO_AGENTE + ", ");
            _sb.Append(TableCliente._CLIENTE_NUEVO + ", ");
            _sb.Append(TableCliente._ENVIADO + ", ");
            _sb.Append(TableCliente._COPIAS_FAC + ", ");
            _sb.Append(TableCliente._TIPO_ID_TRIBUTARIO + ", ");
            _sb.Append(TableCliente._NUEVO_CLIENTE + ", ");
            _sb.Append(TableCliente._CLASIFICACION + ", ");
            _sb.Append(TableCliente._EMAIL + ", ");
            _sb.Append(TableCliente._LATITUD + ", ");
            _sb.Append(TableCliente._LONGITUD + ", ");
            _sb.Append(TableCliente._NOMBRE_APO + ", ");
            _sb.Append(TableCliente._CEDULA_APO + ", ");
            _sb.Append(TableCliente._PROVINCIA_APO + ", ");
            _sb.Append(TableCliente._CANTON_APO + ", ");
            _sb.Append(TableCliente._DISTRITO_APO + ", ");
            _sb.Append(TableCliente._DIRECCION_APO + ", ");
            _sb.Append(TableCliente._OBSERVACIONES + ", ");
            _sb.Append(TableCliente._DIAS_ATENCION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._cliente + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableCliente._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableCliente._NUEVO_CLIENTE + " = ");
            _sb.Append("'" + SQL._Si + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
