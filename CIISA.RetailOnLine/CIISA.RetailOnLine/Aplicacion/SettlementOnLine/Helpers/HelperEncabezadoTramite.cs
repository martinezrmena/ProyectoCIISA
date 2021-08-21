using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Helpers
{
    internal class HelperEncabezadoTramite
    {

        internal void consultarTramiteEncabezados(ListView pltvTransacciones)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoTramite._NO_CIA + ", ");
            _sb.Append(TableEncabezadoTramite._NO_TRANSA + ", ");
            _sb.Append(TableEncabezadoTramite._NO_CLIENTE + ", ");
            _sb.Append(TableEncabezadoTramite._NO_ESTABLECIMIENTO + ", ");
            _sb.Append(TableEncabezadoTramite._NO_AGENTE + ", ");
            _sb.Append(TableEncabezadoTramite._FECHA_CREA + ", ");
            _sb.Append(TableEncabezadoTramite._MONTO + ", ");
            _sb.Append(TableEncabezadoTramite._ENVIADO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoTramite._FECHA_TOMA + " ) " + TableEncabezadoTramite._FECHA_TOMA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoTramite + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = pltvTransacciones.ItemsSource as ObservableCollection<pnlTransacciones_ltvTransacciones>;

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlTransacciones_ltvTransacciones _lviTransaccion = new pnlTransacciones_ltvTransacciones();
                _lviTransaccion.Compannia = _fila[TableEncabezadoTramite._NO_CIA].ToString();
                _lviTransaccion.Documento = _fila[TableEncabezadoTramite._NO_TRANSA].ToString();
                _lviTransaccion.Agente = _fila[TableEncabezadoTramite._NO_AGENTE].ToString();
                _lviTransaccion.Cliente = _fila[TableEncabezadoTramite._NO_CLIENTE].ToString();
                _lviTransaccion.Establecimiento = _fila[TableEncabezadoTramite._NO_ESTABLECIMIENTO].ToString();

                _lviTransaccion.TipoDocumento = ROLTransactions._tramiteNombre;
                _lviTransaccion.Creado =_fila[TableEncabezadoTramite._FECHA_CREA].ToString();
                _lviTransaccion.Entrega = string.Empty;

                decimal _monto = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoTramite._MONTO].ToString());

                _lviTransaccion.Total = FormatUtil.applyCurrencyFormat(_monto);

                if (_fila[TableEncabezadoTramite._ENVIADO].ToString().Equals(Indicators._N))
                {
                    _lviTransaccion.Enviado = Indicators._No;
                }
                else
                {
                    _lviTransaccion.Enviado = Indicators._Si;
                }

                Source.Add(_lviTransaccion);
            }

            pltvTransacciones.ItemsSource = Source;
        }

    }
}
