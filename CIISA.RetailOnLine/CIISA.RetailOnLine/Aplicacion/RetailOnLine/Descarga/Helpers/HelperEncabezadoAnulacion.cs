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
    internal class HelperEncabezadoAnulacion
    {
        internal DataTable buscarEncabezadosAnulacionesSinEnviar(string ptipoDescarga)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoAnulacion._NO_CIA + ", ");
            _sb.Append(TableEncabezadoAnulacion._NO_TRANSA + ", ");
            _sb.Append(TableEncabezadoAnulacion._NO_CLIENTE + ", ");
            _sb.Append(TableEncabezadoAnulacion._NO_ESTABLECIMIENTO + ", ");
            _sb.Append(TableEncabezadoAnulacion._NO_AGENTE + ", ");
            _sb.Append(TableEncabezadoAnulacion._TIPO_DOC + ", ");
            _sb.Append(TableEncabezadoAnulacion._FECHA_CREA + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoAnulacion._FECHA_ENTREGA + " ) " + TableEncabezadoAnulacion._FECHA_ENTREGA + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoAnulacion._TOTAL + ",',','.') " + TableEncabezadoAnulacion._TOTAL + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoAnulacion._TOTAL_IMP + ",',','.') " + TableEncabezadoAnulacion._TOTAL_IMP + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoAnulacion._FECHA_TOMA + " ) " + TableEncabezadoAnulacion._FECHA_TOMA + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoAnulacion._LATITUD + ",',','.') " + TableEncabezadoAnulacion._LATITUD + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoAnulacion._LONGITUD + ",',','.') " + TableEncabezadoAnulacion._LONGITUD + " ");
            _sb.Append("FROM ");

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._encabezadoAnulacionBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._encabezadoAnulacion + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableEncabezadoAnulacion._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "'");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
