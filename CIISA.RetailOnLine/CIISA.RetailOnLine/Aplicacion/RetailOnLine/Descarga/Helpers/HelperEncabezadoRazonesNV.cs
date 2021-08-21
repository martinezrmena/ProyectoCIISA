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
    internal class HelperEncabezadoRazonesNV
    {
        internal DataTable buscarEncabezadosRazonesNVSinEnviar(string ptipoDescarga)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoRazonesNV._NO_CIA + ", ");
            _sb.Append(TableEncabezadoRazonesNV._NO_CLIENTE + ", ");
            _sb.Append(TableEncabezadoRazonesNV._CODIGO + ", ");
            _sb.Append(TableEncabezadoRazonesNV._NO_AGENTE + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoRazonesNV._FECHA_RUTA + " ) " + TableEncabezadoRazonesNV._FECHA_RUTA + ", ");
            _sb.Append(TableEncabezadoRazonesNV._FECHA_CREA_MAQUINA + " ");
            _sb.Append("FROM ");

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._encabezadoRazonesNVBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._encabezadoRazonesNV + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableEncabezadoRazonesNV._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
