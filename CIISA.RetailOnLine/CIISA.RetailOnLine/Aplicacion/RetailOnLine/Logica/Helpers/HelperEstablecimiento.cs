using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperEstablecimiento
    {
        internal int CantidadEstablecimientosCliente(string CodCliente) {

            StringBuilder _sb = new StringBuilder();
            _sb.Append("SELECT ");
            _sb.Append("COUNT(*) AS NEstablecimiento ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._establecimiento);
            _sb.Append(" WHERE ");
            _sb.Append("CODCLIENTE ");
            _sb.Append("= ");
            _sb.Append("'" + CodCliente + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return int.Parse(MultiGeneric.readDecimal(_sb).ToString());

        }

        internal void buscarEstablecimientosCliente(ListView list, Cliente pobjCliente) {

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("C.CODESTABLECIMIENTO, ");
            _sb.Append("C.DESCESTABLECIMIENTO, ");
            _sb.Append("C.DIRECCION, ");
            _sb.Append("C.CODLOCALIZACION, ");
            _sb.Append("C.DIRECCIONEXACTA, ");
            _sb.Append("A.NOMBRE, ");
            _sb.Append("B.IND_ESTADO, ");
            _sb.Append("B.NO_AGENTE, ");
            _sb.Append("B.COBRADOR, ");
            _sb.Append("B.IND_COBRO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._cliente + " A, ");
            _sb.Append(TablesROL._indicadorFactura + " B, ");
            _sb.Append(TablesROL._establecimiento + " C ");
            _sb.Append("WHERE ");
            _sb.Append("A.NO_CLIENTE = ");
            _sb.Append("B.NO_CLIENTE ");
            _sb.Append("AND ");
            _sb.Append("A.NO_AGENTE = ");
            _sb.Append("B.COBRADOR ");

            _sb.Append("AND ");
            _sb.Append("A.NO_CLIENTE = ");
            _sb.Append("C.CODCLIENTE ");
            _sb.Append("AND ");
            _sb.Append("B.NO_ESTABLECIMIENTO = ");
            _sb.Append("C.CODESTABLECIMIENTO ");

            _sb.Append("AND ");

            _sb.Append("A.NO_CLIENTE ");
            _sb.Append("= ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = list.ItemsSource as ObservableCollection<pnlEstablecimiento_ltvEstablecimientos>;

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlEstablecimiento_ltvEstablecimientos _lvi = new pnlEstablecimiento_ltvEstablecimientos();

                _lvi.CODESTABLECIMIENTO = _fila["CODESTABLECIMIENTO"].ToString();
                _lvi.DESCESTABLECIMIENTO = _fila["DESCESTABLECIMIENTO"].ToString();
                _lvi.DIRECCION = _fila["DIRECCION"].ToString();
                _lvi.CODLOCALIZACION = _fila["CODLOCALIZACION"].ToString();
                _lvi.DIRECCIONEXACTA = _fila["DIRECCIONEXACTA"].ToString();
                _lvi.NO_AGENTE = _fila["NO_AGENTE"].ToString();
                _lvi.COBRADOR = _fila["COBRADOR"].ToString();
                _lvi.IND_COBRO = _fila["IND_COBRO"].ToString();

                Source.Add(_lvi);
            }

            list.ItemsSource = Source;

        }

        internal async Task buscarEstablecimientoPorCodigoEstablecimiento(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("CODESTABLECIMIENTO, ");
            _sb.Append("DESCESTABLECIMIENTO, ");
            _sb.Append("DIRECCION, ");
            _sb.Append("CODLOCALIZACION, ");
            _sb.Append("DIRECCIONEXACTA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._establecimiento + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODCLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("CODESTABLECIMIENTO ");
            _sb.Append("LIKE '%" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + "%'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                int _codEstablecimiento = FormatUtil.convertStringToInt(_fila["CODESTABLECIMIENTO"].ToString());
                pobjCliente.v_objEstablecimiento.v_codEstablecimiento = _codEstablecimiento;
                pobjCliente.v_objEstablecimiento.v_descEstablecimiento = _fila["DESCESTABLECIMIENTO"].ToString();
                pobjCliente.v_objEstablecimiento.v_direccion = _fila["DIRECCION"].ToString();
                pobjCliente.v_objEstablecimiento.v_codLocalizacion = _fila["CODLOCALIZACION"].ToString();
                pobjCliente.v_objEstablecimiento.v_direccionExacta = _fila["DIRECCIONEXACTA"].ToString();

                Logica_ManagerIndicador _manager = new Logica_ManagerIndicador();
                await _manager.buscarIndicadoresCliente(pobjCliente);
            }
        }

        internal void nuevoEstablecimiento(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._establecimiento + " ");
            _sb.Append("(");
            _sb.Append("CODCLIENTE, ");
            _sb.Append("CODESTABLECIMIENTO, ");
            _sb.Append("DESCESTABLECIMIENTO, ");
            _sb.Append("DIRECCION, ");
            _sb.Append("CODLOCALIZACION, ");
            _sb.Append("DIRECCIONEXACTA");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + pobjCliente.v_no_cliente + "', ");
            _sb.Append("" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + ", ");
            _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_descEstablecimiento + "', ");
            _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_direccion + "', ");
            _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_codLocalizacion + "', ");
            _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_direccionExacta + "'");
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }

        internal bool ExisteEstablecimientoCliente(string pcodCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("CODESTABLECIMIENTO, ");
            _sb.Append("DESCESTABLECIMIENTO, ");
            _sb.Append("DIRECCION, ");
            _sb.Append("CODLOCALIZACION, ");
            _sb.Append("DIRECCIONEXACTA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._establecimiento + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODCLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            if (_dt.Rows != null)
            {
                if (_dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
