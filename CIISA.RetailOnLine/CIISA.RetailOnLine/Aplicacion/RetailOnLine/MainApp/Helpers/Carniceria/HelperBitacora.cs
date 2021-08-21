using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.Carniceria
{
    public class HelperBitacora
    {

        public int GuardarBitacora(pnlBitacoraModel bitacora)
        {
            int result = 0;

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            try
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append("INSERT ");
                _sb.Append("INTO ");
                _sb.Append(TablesROL._bitacora + " ");
                _sb.Append("(");
                _sb.Append("COD_CLIENTE, ");
                _sb.Append("FECHAVISITA, ");
                _sb.Append("VOLCOMPRA, ");
                _sb.Append("PORCENTAJECOMPRA, ");
                _sb.Append("SITUACION, ");
                _sb.Append("QUEJAS, ");
                _sb.Append("OPORTUNIDADES, ");
                _sb.Append("COMPETENCIAS ");
                _sb.Append(") ");
                _sb.Append("VALUES ");
                _sb.Append(" (");
                _sb.Append("'" + bitacora.Cod_Cliente + "', ");
                _sb.Append("DATETIME('NOW', 'LOCALTIME'), ");
                _sb.Append("'" + bitacora.Vol_Compra + "', ");
                _sb.Append("'" + bitacora.Porcentaje_Compra + "', ");
                _sb.Append("'" + bitacora.SituacionNegocio + "', ");
                _sb.Append("'" + bitacora.Quejas + "', ");
                _sb.Append("'" + bitacora.Oportunidades + "', ");
                _sb.Append("'" + bitacora.Competencias + "' ");
                _sb.Append(")");

                MultiGeneric.insertRecord(_sb);

                result = 1;
            }
            catch (Exception ex)
            {
                result = -1;
                throw new Exception("guardarTransaccion(bitacora)", ex);
            }

            return result;

        }
    }
}
