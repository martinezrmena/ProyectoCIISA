using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperEspecificacion
    {
        internal DataTable buscarEspecificacion()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("CODIGO, ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._especificacion);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string obtenerEspecificacionMotivo(string pcodEspecificacion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._especificacion + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODIGO = '" + pcodEspecificacion + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal bool buscarEspecificacionPorDescripcion(string pdescripcion)
        {
            bool _existe = false;

            DataTable _dt = buscarEspecificacion();

            foreach (DataRow _fila in _dt.Rows)
            {
                if (pdescripcion.Equals(_fila["DESCRIPCION"].ToString()))
                {
                    _existe = true;
                }
            }

            return _existe;
        }
    }
}
