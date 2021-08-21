using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Helpers
{
    internal class HelperEncabezadoRecibo
    {
        internal void consultarReciboEncabezados(ListView pltvTransacciones)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_CIA + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_TRANSA + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_CLIENTE + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_ESTABLECIMIENTO + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_AGENTE + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._OBSERVACION + ", ");
            _sb.Append("REPLACE(ER." + TableEncabezadoRecibo._MONTO + ",',','.') " + TableEncabezadoRecibo._MONTO + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._FECHA_CREA + ", ");
            _sb.Append("REPLACE(ER." + TableEncabezadoRecibo._SALDO + ",',','.') " + TableEncabezadoRecibo._SALDO + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._TIPO_DOC + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_LINEA + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._ENVIADO + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._ANULADO + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._CLIENTE_NUEVO + ", ");
            _sb.Append("TT." + TableTipoTransaccion._DESCRIPCION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoRecibo + " ER, ");
            _sb.Append(TablesROL._tipoTransaccion + " TT ");
            _sb.Append("WHERE ");
            _sb.Append("ER." + TableEncabezadoRecibo._TIPO_DOC + " = ");
            _sb.Append("TT." + TableTipoTransaccion._TIPOTRANSACCION + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = pltvTransacciones.ItemsSource as ObservableCollection<pnlTransacciones_ltvTransacciones>;

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlTransacciones_ltvTransacciones _lviTransaccion = new pnlTransacciones_ltvTransacciones();
                _lviTransaccion.Compannia = _fila[TableEncabezadoRecibo._NO_CIA].ToString();
                _lviTransaccion.Documento = _fila[TableEncabezadoRecibo._NO_TRANSA].ToString();
                _lviTransaccion.Agente = _fila[TableEncabezadoRecibo._NO_AGENTE].ToString();
                _lviTransaccion.Cliente = _fila[TableEncabezadoRecibo._NO_CLIENTE].ToString();
                _lviTransaccion.Establecimiento = _fila[TableEncabezadoRecibo._NO_ESTABLECIMIENTO].ToString();

                _lviTransaccion.TipoDocumento = _fila[TableTipoTransaccion._DESCRIPCION].ToString();
                _lviTransaccion.Creado = _fila[TableEncabezadoRecibo._FECHA_CREA].ToString();
                _lviTransaccion.Entrega = string.Empty;

                decimal _monto = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoRecibo._MONTO].ToString());

                _lviTransaccion.Total = FormatUtil.applyCurrencyFormat(_monto);

                decimal _impuesto = Numeric._zeroDecimalInitialize;

                _lviTransaccion.Impuesto = FormatUtil.applyCurrencyFormat(_impuesto);

                if (_fila[TableEncabezadoRecibo._ENVIADO].ToString().Equals(Indicators._N))
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
