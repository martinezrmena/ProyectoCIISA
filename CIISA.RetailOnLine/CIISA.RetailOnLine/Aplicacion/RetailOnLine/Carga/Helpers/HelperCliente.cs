using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperCliente
    {

        public StringBuilder insertTablaCliente(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            string NombreApo = pfila.Table.Columns.Contains("NOMBRE_APO") ? pfila["NOMBRE_APO"].ToString() : string.Empty;
            string CedulaApo = pfila.Table.Columns.Contains("CEDULA_APO") ? pfila["CEDULA_APO"].ToString() : string.Empty;
            string ProvinciaApo = pfila.Table.Columns.Contains("PROVINCIA_APO") ? pfila["PROVINCIA_APO"].ToString() : string.Empty;
            string CantonApo = pfila.Table.Columns.Contains("CANTON_APO") ? pfila["CANTON_APO"].ToString() : string.Empty;
            string DistritoApo = pfila.Table.Columns.Contains("DISTRITO_APO") ? pfila["DISTRITO_APO"].ToString() : string.Empty;
            string DireccionApo = pfila.Table.Columns.Contains("DIRECCION_APO") ? pfila["DIRECCION_APO"].ToString() : string.Empty;
            string Observaciones = pfila.Table.Columns.Contains("OBSERVACIONES") ? pfila["OBSERVACIONES"].ToString() : string.Empty;
            string DiasAtencion = pfila.Table.Columns.Contains("DIAS_ATENCION") ? pfila["DIAS_ATENCION"].ToString() : string.Empty;


            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._cliente);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableCliente._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableCliente._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TableCliente._NOMBRE));
            _sb.Append(string.Format("{0}, ", TableCliente._NOMBRE_LARGO));
            _sb.Append(string.Format("{0}, ", TableCliente._CEDULA));
            _sb.Append(string.Format("{0}, ", TableCliente._EXCENTO_IMP));
            _sb.Append(string.Format("{0}, ", TableCliente._PLAZO));

            if (!pfila["F_CIERRE"].ToString().Equals(string.Empty))
            {
                _sb.Append(string.Format("{0}, ", TableCliente._F_CIERRE));
            }

            _sb.Append(string.Format("{0}, ", TableCliente._LISTA_PRECIOS));
            _sb.Append(string.Format("{0}, ", TableCliente._PAIS));
            _sb.Append(string.Format("{0}, ", TableCliente._PROVINCIA));
            _sb.Append(string.Format("{0}, ", TableCliente._CANTON));
            _sb.Append(string.Format("{0}, ", TableCliente._DISTRITO));
            _sb.Append(string.Format("{0}, ", TableCliente._DIRECCION));
            _sb.Append(string.Format("{0}, ", TableCliente._TELEFONO));
            _sb.Append(string.Format("{0}, ", TableCliente._NOMBRE_ENC));
            _sb.Append(string.Format("{0}, ", TableCliente._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableCliente._CLIENTE_NUEVO));
            _sb.Append(string.Format("{0}, ", TableCliente._ENVIADO));
            _sb.Append(string.Format("{0}, ", TableCliente._COPIAS_FAC));
            _sb.Append(string.Format("{0}, ", TableCliente._TIPO_ID_TRIBUTARIO));
            _sb.Append(string.Format("{0}, ", TableCliente._NUEVO_CLIENTE));
            //_sb.Append(string.Format("{0}", "RUTA_COBRO"));
            //Validacion 50mts
            _sb.Append(string.Format("{0}, ", "RUTA_COBRO"));
            _sb.Append(string.Format("{0}, ", TableCliente._LATITUD));
            _sb.Append(string.Format("{0}, ", TableCliente._LONGITUD));
            _sb.Append(string.Format("{0}, ", TableCliente._NOMBRE_APO));
            _sb.Append(string.Format("{0}, ", TableCliente._CEDULA_APO));
            _sb.Append(string.Format("{0}, ", TableCliente._PROVINCIA_APO));
            _sb.Append(string.Format("{0}, ", TableCliente._CANTON_APO));
            _sb.Append(string.Format("{0}, ", TableCliente._DISTRITO_APO));
            _sb.Append(string.Format("{0}, ", TableCliente._DIRECCION_APO));
            _sb.Append(string.Format("{0}, ", TableCliente._OBSERVACIONES));
            _sb.Append(string.Format("{0} ", TableCliente._DIAS_ATENCION));
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_CLIENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["NOMBRE"]));
            _sb.Append(string.Format("'{0}', ", pfila["NOMBRE_LARGO"]));
            _sb.Append(string.Format("'{0}', ", pfila["CEDULA"]));
            _sb.Append(string.Format("'{0}', ", pfila["EXCENTO_IMP"]));
            _sb.Append(string.Format("'{0}', ", pfila["PLAZO"]));

            if (!pfila["F_CIERRE"].ToString().Equals(string.Empty))
            {
                _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(pfila["F_CIERRE"].ToString())));
            }
            
            _sb.Append(string.Format("'{0}', ", pfila["LISTA_PRECIOS"]));
            _sb.Append(string.Format("'{0}', ", pfila["PAIS"]));
            _sb.Append(string.Format("'{0}', ", pfila["PROVINCIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["CANTON"]));
            _sb.Append(string.Format("'{0}', ", pfila["DISTRITO"]));
            _sb.Append(string.Format("'{0}', ", pfila["DIRECCION"]));
            _sb.Append(string.Format("'{0}', ", pfila["TELEFONO"]));
            _sb.Append(string.Format("'{0}', ", pfila["NOMBRE_ENC"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_AGENTE"]));
            _sb.Append(string.Format("'', "));
            _sb.Append(string.Format("'{0}', ", SQL._Si));
            _sb.Append(string.Format("'{0}', ", pfila["COPIAS_FAC"]));
            _sb.Append(string.Format("'', "));
            _sb.Append(string.Format("'{0}', ", SQL._No));
            //_sb.Append(string.Format("'{0}'", pfila["RUTA_COBRO"]));
            //Validacion 50mts
            _sb.Append(string.Format("'{0}', ", pfila["RUTA_COBRO"]));
            _sb.Append(string.Format("'{0}', ", pfila["LATITUD"]));
            _sb.Append(string.Format("'{0}', ", pfila["LONGITUD"]));
            _sb.Append(string.Format("'{0}', ", NombreApo));
            _sb.Append(string.Format("'{0}', ", CedulaApo));
            _sb.Append(string.Format("'{0}', ", ProvinciaApo));
            _sb.Append(string.Format("'{0}', ", CantonApo));
            _sb.Append(string.Format("'{0}', ", DistritoApo));
            _sb.Append(string.Format("'{0}', ", DireccionApo));
            _sb.Append(string.Format("'{0}', ", Observaciones));
            _sb.Append(string.Format("'{0}'", DiasAtencion));
            _sb.Append(")");

            return _sb;
        }

    }
}
