using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.TablesNAF;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperDescuento
    {
        public DataTable obtenerRespaldoDescuento()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(string.Format("{0}, ", TableDescuentos._CONSECUTIVO));
            _sb.Append(string.Format("{0}, ", TableDescuentos._CODCIA));
            _sb.Append(string.Format("{0}, ", TableDescuentos._CODCLIENTE));
            _sb.Append(string.Format("{0}, ", TableDescuentos._CODPRODUCTO));
            _sb.Append(string.Format("{0}, ", TableDescuentos._PORCENTAJE));
            _sb.Append(string.Format("{0}, ", TableDescuentos._CANTIDAD));
            //_sb.Append(string.Format("CONVERT(NCHAR(10), {0}, 103) {0}, ", TableDescuentos._FECHA_INICIA));
            _sb.Append(string.Format("DATETIME({0}), ", TableDescuentos._FECHA_INICIA));
            //_sb.Append(string.Format("CONVERT(NCHAR(10), {0}, 103) {0} ", TableDescuentos._FECHA_VENCE));
            _sb.Append(string.Format("DATETIME({0}), ", TableDescuentos._FECHA_VENCE));
            _sb.Append(string.Format("{0} ", TableDescuentos._TIPODESC));
            _sb.Append("FROM ");
            _sb.Append(TablesROL._descuentos);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        public StringBuilder insertTablaDescuento(DataRow pfila)
        {
            string _fechaInicia = FormatUtil.convertDateOracleToSQLCompact_yyMMdd(
                                        pfila["" + VistaDescuentosNAF.FECHA_INICIA].ToString()
                                        );

            string _fechaVence = FormatUtil.convertDateOracleToSQLCompact_yyMMdd(
                                    pfila["" + VistaDescuentosNAF.FECHA_VENCE].ToString()
                                    );

            string tipo_desc = string.Empty;

            if (pfila.Table.Columns.Contains(VistaDescuentosNAF.TIPO_DESC))
            {
                tipo_desc = pfila["" + VistaDescuentosNAF.TIPO_DESC].ToString();
            }
            else
            {
                tipo_desc = TipoDescuento._Porcentaje;
            }

            if (_fechaInicia.Equals(string.Empty))
            {
                _fechaInicia = VarTime.getSQLCEDate();
            }

            if (_fechaVence.Equals(string.Empty))
            {
                _fechaVence = VarTime.getSQLCEDate();
            }

            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._descuentos);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableDescuentos._CODCIA));
            _sb.Append(string.Format("{0}, ", TableDescuentos._CODCLIENTE));
            _sb.Append(string.Format("{0}, ", TableDescuentos._CODPRODUCTO));
            _sb.Append(string.Format("{0}, ", TableDescuentos._PORCENTAJE));
            _sb.Append(string.Format("{0}, ", TableDescuentos._CANTIDAD));
            _sb.Append(string.Format("{0}, ", TableDescuentos._FECHA_INICIA));
            _sb.Append(string.Format("{0}, ", TableDescuentos._FECHA_VENCE));
            _sb.Append(TableDescuentos._TIPODESC);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["" + VistaDescuentosNAF.NO_CIA]));
            _sb.Append(string.Format("'{0}', ", pfila["" + VistaDescuentosNAF.NO_CLIENTE]));
            _sb.Append(string.Format("'{0}', ", pfila["" + VistaDescuentosNAF.ARTICULO]));
            _sb.Append(string.Format("{0}, ", pfila["" + VistaDescuentosNAF.PORCENTAJE]));
            _sb.Append(string.Format("{0}, ", pfila["" + VistaDescuentosNAF.CANTIDAD]));
            //_sb.Append(string.Format("'{0}', ", _fechaInicia));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(_fechaInicia)));
            //_sb.Append(string.Format("'{0}'", _fechaVence));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(_fechaVence)));
            _sb.Append(string.Format("'{0}' ", tipo_desc));
            _sb.Append(")");

            return _sb;
        }

        public StringBuilder insertTablaDescuentoRespaldo(DataRow pfila)
        {
            string _fechaInicia = FormatUtil.convertDateOracleToSQLCompact_yyMMdd(
                                        pfila[TableDescuentos._FECHA_INICIA].ToString()
                                        );

            string _fechaVence = FormatUtil.convertDateOracleToSQLCompact_yyMMdd(
                                    pfila[TableDescuentos._FECHA_VENCE].ToString()
                                    );

            if (_fechaInicia.Equals(string.Empty))
            {
                _fechaInicia = VarTime.getSQLCEDate();
            }

            if (_fechaVence.Equals(string.Empty))
            {
                _fechaVence = VarTime.getSQLCEDate();
            }

            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._descuentos);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableDescuentos._CODCIA));
            _sb.Append(string.Format("{0}, ", TableDescuentos._CODCLIENTE));
            _sb.Append(string.Format("{0}, ", TableDescuentos._CODPRODUCTO));
            _sb.Append(string.Format("{0}, ", TableDescuentos._PORCENTAJE));
            _sb.Append(string.Format("{0}, ", TableDescuentos._CANTIDAD));
            _sb.Append(string.Format("{0}, ", TableDescuentos._FECHA_INICIA));
            _sb.Append(string.Format("{0}, ", TableDescuentos._FECHA_VENCE));
            _sb.Append(TableDescuentos._TIPODESC);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila[TableDescuentos._CODCIA]));
            _sb.Append(string.Format("'{0}', ", pfila[TableDescuentos._CODCLIENTE]));
            _sb.Append(string.Format("'{0}', ", pfila[TableDescuentos._CODPRODUCTO]));
            _sb.Append(string.Format("{0}, ", pfila[TableDescuentos._PORCENTAJE]));
            _sb.Append(string.Format("{0}, ", pfila[TableDescuentos._CANTIDAD]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(_fechaInicia)));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(_fechaVence)));
            _sb.Append(string.Format("'{0}', ", pfila[TableDescuentos._TIPODESC]));
            _sb.Append(")");

            return _sb;
        }
    }
}
