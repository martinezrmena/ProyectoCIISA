using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperTipoIdentificacion
    {

        internal StringBuilder insertTablaTipoIdentificacion(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._tipoIdentificacion);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableTipoIdentificacion._CODIGO));
            _sb.Append(string.Format("{0}, ", TableTipoIdentificacion._DESCRIPCION));
            _sb.Append(string.Format("{0}, ", TableTipoIdentificacion._ETIQUETA));
            _sb.Append(string.Format("{0}, ", TableTipoIdentificacion._FORMATO));
            _sb.Append(TableTipoIdentificacion._DIGITO_VERIF);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["CODIGO"]));
            _sb.Append(string.Format("'{0}', ", pfila["DESCRIPCION"]));
            _sb.Append(string.Format("'{0}', ", pfila["ETIQUETA"]));
            _sb.Append(string.Format("'{0}', ", pfila["FORMATO"]));
            _sb.Append(string.Format("'{0}'", pfila["DIGITO_VERIF"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
