using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
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
    internal class HelperDetalleAnulacion
    {
        internal DataTable buscarDetallesAnulacionesSinEnviar(string ptipoDescarga)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleAnulacion._NO_CIA + ", ");
            _sb.Append(TableDetalleAnulacion._NO_TRANSA + ", ");
            _sb.Append(TableDetalleAnulacion._NO_LINEA + ", ");
            _sb.Append(TableDetalleAnulacion._NO_ARTI + ", ");
            _sb.Append("REPLACE(" + TableDetalleAnulacion._CANTIDAD + ",',','.') " + TableDetalleAnulacion._CANTIDAD + ", ");
            _sb.Append(TableDetalleAnulacion._COMENTARIO + ", ");
            _sb.Append("REPLACE(" + TableDetalleAnulacion._TOTAL_LIN + ",',','.') " + TableDetalleAnulacion._TOTAL_LIN + ", ");
            _sb.Append("REPLACE(" + TableDetalleAnulacion._TOTAL_IMP_LIN + ",',','.') " + TableDetalleAnulacion._TOTAL_IMP_LIN + ", ");
            _sb.Append(TableDetalleAnulacion._NO_AGENTE + " ");
            _sb.Append("FROM ");

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._detalleAnulacionBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._detalleAnulacion + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableDetalleAnulacion._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "'");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
