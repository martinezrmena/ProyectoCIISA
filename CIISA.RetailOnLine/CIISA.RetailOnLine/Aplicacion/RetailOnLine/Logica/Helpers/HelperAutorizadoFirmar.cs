using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperAutorizadoFirmar
    {
        internal void buscarAutorizadosFirmar(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAutorizadoFirmar._NO_CIA + ", ");
            _sb.Append(TableAutorizadoFirmar._GRUPO + ", ");
            _sb.Append(TableAutorizadoFirmar._CLIENTE + ", ");
            _sb.Append(TableAutorizadoFirmar._CEDULA_AUTORIZADO + ", ");
            _sb.Append(TableAutorizadoFirmar._NOMBRE_AUTORIZADO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._autorizadoFirmar + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableAutorizadoFirmar._CLIENTE + " = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                AutorizadoFirmar _objAutorizadoFirmar = new AutorizadoFirmar();

                _objAutorizadoFirmar.v_noCia = _fila[TableAutorizadoFirmar._NO_CIA].ToString();
                _objAutorizadoFirmar.v_grupo = _fila[TableAutorizadoFirmar._GRUPO].ToString();
                _objAutorizadoFirmar.v_codCliente = _fila[TableAutorizadoFirmar._CLIENTE].ToString();
                _objAutorizadoFirmar.v_cedula = _fila[TableAutorizadoFirmar._CEDULA_AUTORIZADO].ToString();
                _objAutorizadoFirmar.v_nombre = _fila[TableAutorizadoFirmar._NOMBRE_AUTORIZADO].ToString();

                pobjCliente.v_listaAutorizadosFirmar.Add(_objAutorizadoFirmar);
            }
        }
    }
}
