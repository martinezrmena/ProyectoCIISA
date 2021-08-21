using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Helpers
{
    internal class HelperEncabezadoTransaccion
    {
        internal void consultarTransaccionEncabezados(ListView pltvTransacciones)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCIA + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODDOCUMENTO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODAGENTE + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCLIENTE + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODESTABLECIMIENTO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._FECHACREACION + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoDocumento._FECHAENTREGA + " ) " + TableEncabezadoDocumento._FECHAENTREGA + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._TOTAL + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._TOTAL_IMP + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._ENVIADO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CEDULA + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._NOMBRE + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._MONTO_DEVOL + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._NUMERO_DEVOL + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODFACTURA + ", ");
            _sb.Append("TT." + TableTipoTransaccion._DESCRIPCION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ED, ");
            _sb.Append(TablesROL._tipoTransaccion + " TT ");
            _sb.Append("WHERE ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("TT." + TableTipoTransaccion._TIPOTRANSACCION + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("ED." + TableEncabezadoDocumento._FECHACREACION);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = pltvTransacciones.ItemsSource as ObservableCollection<pnlTransacciones_ltvTransacciones>;

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlTransacciones_ltvTransacciones _lviTransaccion = new pnlTransacciones_ltvTransacciones();
                _lviTransaccion.Compannia = _fila[TableEncabezadoDocumento._CODCIA].ToString();
                _lviTransaccion.Documento = _fila[TableEncabezadoDocumento._CODDOCUMENTO].ToString();
                _lviTransaccion.Agente = _fila[TableEncabezadoDocumento._CODAGENTE].ToString();
                _lviTransaccion.Cliente = _fila[TableEncabezadoDocumento._CODCLIENTE].ToString();
                _lviTransaccion.Establecimiento = _fila[TableEncabezadoDocumento._CODESTABLECIMIENTO].ToString();

                _lviTransaccion.TipoDocumento = _fila[TableTipoTransaccion._DESCRIPCION].ToString();
                _lviTransaccion.Creado = _fila[TableEncabezadoDocumento._FECHACREACION].ToString();
                _lviTransaccion.Entrega = _fila[TableEncabezadoDocumento._FECHAENTREGA].ToString();
                _lviTransaccion.CodFactura = _fila[TableEncabezadoDocumento._CODFACTURA].ToString();

                decimal _total = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoDocumento._TOTAL].ToString());

                _lviTransaccion.Total = FormatUtil.applyCurrencyFormat(_total);

                decimal _impuesto = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoDocumento._TOTAL_IMP].ToString());

                _lviTransaccion.Impuesto = FormatUtil.applyCurrencyFormat(_impuesto);

                if (_fila["ENVIADO"].ToString().Equals(Indicators._N))
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
