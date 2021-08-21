using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperInformacionRuta
    {

        internal StringBuilder insertTablaInformacionRuta(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._informacionRuta);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableInformacionRuta._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableInformacionRuta._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableInformacionRuta._NOMBREAGENTE));
            _sb.Append(string.Format("{0}, ", TableInformacionRuta._TIPO_AGENTE));
            _sb.Append(TableInformacionRuta._ESTADO);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("( ");
            _sb.Append(string.Format("'{0}', ", pfila["CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["COD_AGENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["NOM_AGENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["TIPO_AGENTE"]));
            _sb.Append(string.Format("'{0}'", pfila["ESTADO"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
