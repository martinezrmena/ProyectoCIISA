using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperDescuentoGeneral
    {
        public DataTable obtenerRespaldoDescuentoGeneral()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._CONSECUTIVO));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._PORCENTAJE));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._CANTIDAD));
            //_sb.Append(string.Format("CONVERT(NCHAR(10), {0}, 103) {0}, ", TableDescuentos._FECHA_INICIA));
            _sb.Append(string.Format("DATETIME({0}),", TableDescuentoGeneral._FECHA_INICIA));
            //_sb.Append(string.Format("CONVERT(NCHAR(10), {0}, 103) {0} ", TableDescuentos._FECHA_VENCE));
            _sb.Append(string.Format("DATETIME({0}),", TableDescuentoGeneral._FECHA_VENCE));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._TIPO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._TIPO_PRECIO));
            _sb.Append(string.Format("{0} ", TableDescuentoGeneral._TIPO_DESC));
            _sb.Append("FROM ");
            _sb.Append(TablesROL._descuentoGeneral);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        public StringBuilder insertTablaDescuentoGeneral(DataRow pfila)
        {
            string _fechaInicia = FormatUtil.convertDateOracleToSQLCompact_yyMMdd(
                                        pfila["" + TableDescuentoGeneral._FECHA_INICIA].ToString()
                                        );

            string _fechaVence = FormatUtil.convertDateOracleToSQLCompact_yyMMdd(
                                    pfila["" + TableDescuentoGeneral._FECHA_VENCE].ToString()
                                    );

            string tipo_desc = string.Empty;

            if (_fechaInicia.Equals(string.Empty))
            {
                _fechaInicia = VarTime.getSQLCEDate();
            }
            else
            {

                VarTime.convertDateTimeFromServiceToSQLite(_fechaInicia);
            }

            if (_fechaVence.Equals(string.Empty))
            {
                _fechaVence = VarTime.getSQLCEDate();
            }
            else
            {

                VarTime.convertDateTimeFromServiceToSQLite(_fechaVence);
            }

            if (pfila.Table.Columns.Contains(TableDescuentoGeneral._TIPO_DESC))
            {
                tipo_desc = pfila["" + TableDescuentoGeneral._TIPO_DESC].ToString();
            }
            else
            {
                tipo_desc = TipoDescuento._Porcentaje;
            }

            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._descuentoGeneral);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._PORCENTAJE));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._CANTIDAD));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._FECHA_INICIA));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._FECHA_VENCE));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._TIPO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableDescuentoGeneral._TIPO_PRECIO));
            _sb.Append(TableDescuentoGeneral._TIPO_DESC);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDescuentoGeneral._NO_CIA]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDescuentoGeneral._NO_CLIENTE]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDescuentoGeneral._NO_AGENTE]));
            _sb.Append(string.Format("{0}, ", pfila["" + TableDescuentoGeneral._PORCENTAJE]));
            _sb.Append(string.Format("{0}, ", pfila["" + TableDescuentoGeneral._CANTIDAD]));
            _sb.Append(string.Format("'{0}', ", _fechaInicia));
            _sb.Append(string.Format("'{0}', ", _fechaVence));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDescuentoGeneral._TIPO_AGENTE]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDescuentoGeneral._TIPO_PRECIO]));
            _sb.Append(string.Format("'{0}'", tipo_desc));
            _sb.Append(")");

            return _sb;
        }
    }
}
