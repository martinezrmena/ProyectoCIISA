using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperAnulacion
    {

        public void eliminarIndicadorAnulacion(string cod_agente, string cod_transaccion) {

            StringBuilder _sb = new StringBuilder();

            _sb.Append("DELETE FROM ");
            _sb.Append(TablesROL._indAnulacion + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableIndicadorAnulacion._NO_AGENTE + " = ");
            _sb.Append("'" + cod_agente + "' ");
            _sb.Append("AND ");
            _sb.Append(TableIndicadorAnulacion._NO_TRANSA + " = ");
            _sb.Append("'" + cod_transaccion + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.deleteTable(_sb);

        }

        public bool validarAnulacion(string cod_agente, string cod_transaccion) {

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableIndicadorAnulacion._NO_CIA + ", ");
            _sb.Append(TableIndicadorAnulacion._NO_AGENTE + ", ");
            _sb.Append(TableIndicadorAnulacion._NO_TRANSA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._indAnulacion + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableIndicadorAnulacion._NO_AGENTE + " = ");
            _sb.Append("'" + cod_agente + "' ");
            _sb.Append("AND ");
            _sb.Append(TableIndicadorAnulacion._NO_TRANSA + " = ");
            _sb.Append("'" + cod_transaccion + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlAnulacion_ltvIndicadorAnulacion _lvi = new pnlAnulacion_ltvIndicadorAnulacion();

                _lvi.NO_AGENTE = _fila["NO_AGENTE"].ToString();
                _lvi.NO_TRANSA = _fila["NO_TRANSA"].ToString();

                if (_lvi.NO_AGENTE.Equals(cod_agente) &&
                    _lvi.NO_TRANSA.Equals(cod_transaccion))
                {
                    return true;
                }

            }

            return false;

        }

        public DateTime buscarUltimaTransaccionRealizada()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT MAX(DATETIME(X." + TableEncabezadoDocumento._FECHACREACION + ")) ");
            _sb.Append("AS " + TableEncabezadoDocumento._FECHACREACION + " ");
            _sb.Append("FROM (");
            _sb.Append("SELECT MAX(DATETIME(" + TableEncabezadoDocumento._FECHACREACION + ")) ");
            _sb.Append("AS " + TableEncabezadoDocumento._FECHACREACION + " ");
            _sb.Append("FROM " + TablesROL._encabezadoDocumento + " ");
            _sb.Append("UNION ");
            _sb.Append("SELECT MAX(DATETIME(" + TableEncabezadoRecibo._FECHA_CREA + ")) ");
            _sb.Append("AS " + TableEncabezadoRecibo._FECHA_CREA + " ");
            _sb.Append("FROM " + TablesROL._encabezadoRecibo + " ");
            _sb.Append("UNION ");
            _sb.Append("SELECT MAX(DATETIME(" + TableEncabezadoTramite._FECHA_CREA + ")) ");
            _sb.Append("AS " + TableEncabezadoTramite._FECHA_CREA + " ");
            _sb.Append("FROM " + TablesROL._encabezadoTramite);
            _sb.Append(") X ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _fechaDocumento = MultiGeneric.readStringText(_sb);

            return FormatUtil.convertStringToDateTimeWithTime(_fechaDocumento);
        }

        internal void buscarListaTransaccionEncabezadosParaAnulaciones(ListView pltvTransacciones, DateTime pfechaDocumento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCIA + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCLIENTE + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODAGENTE + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODESTABLECIMIENTO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODDOCUMENTO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._FECHACREACION + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._TOTAL + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._ANULADO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._ENVIADO + ", ");
            _sb.Append("T." + TableTipoTransaccion._DESCRIPCION + ", ");
            _sb.Append("C." + TableCliente._NOMBRE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ED, ");
            _sb.Append(TablesROL._tipoTransaccion + " T, ");
            _sb.Append(TablesROL._cliente + " C ");
            _sb.Append("WHERE ");
            _sb.Append("DATETIME(ED." + TableEncabezadoDocumento._FECHACREACION + ") = DATETIME(");
            _sb.Append("'" + pfechaDocumento.ToString("yyyy-MM-dd HH:mm:ss") + "') ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("T." + TableTipoTransaccion._TIPOTRANSACCION + " ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCLIENTE + " = ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            _sb.Append("UNION ");
            _sb.Append("SELECT ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_CIA + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_CLIENTE + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_AGENTE + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_ESTABLECIMIENTO + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_TRANSA + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._FECHA_CREA + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._TIPO_DOC + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._MONTO + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._ANULADO + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._ENVIADO + ", ");
            _sb.Append("T." + TableTipoTransaccion._DESCRIPCION + ", ");
            _sb.Append("C." + TableCliente._NOMBRE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoRecibo + " ER, ");
            _sb.Append(TablesROL._tipoTransaccion + " T, ");
            _sb.Append(TablesROL._cliente + " C ");
            _sb.Append("WHERE ");
            _sb.Append("DATETIME(ER." + TableEncabezadoRecibo._FECHA_CREA + ") = DATETIME(");
            _sb.Append("'" + pfechaDocumento.ToString("yyyy-MM-dd HH:mm:ss") + "') ");
            _sb.Append("AND ");
            _sb.Append("ER." + TableEncabezadoRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("ER." + TableEncabezadoRecibo._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("ER." + TableEncabezadoRecibo._TIPO_DOC + " = ");
            _sb.Append("T." + TableTipoTransaccion._TIPOTRANSACCION + " ");
            _sb.Append("AND ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_CLIENTE + " = ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = pltvTransacciones.ItemsSource as ObservableCollection<pnlAnulacion_ltvTransacciones>;

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlAnulacion_ltvTransacciones _lvi = new pnlAnulacion_ltvTransacciones();

                _lvi.CODCIA = _fila["CODCIA"].ToString();
                _lvi.CODDOCUMENTO = _fila["CODDOCUMENTO"].ToString();

                _lvi.CODCLIENTE = _fila["CODCLIENTE"].ToString();
                _lvi.CODESTABLECIMIENTO = _fila["CODESTABLECIMIENTO"].ToString();

                _lvi.NOMBRE = _fila["NOMBRE"].ToString();

                _lvi.CODAGENTE = _fila["CODAGENTE"].ToString();
                _lvi.CODTIPODOCUMENTO = _fila["CODTIPODOCUMENTO"].ToString();

                _lvi.DESCRIPCION = _fila["DESCRIPCION"].ToString();

                _lvi.FECHACREACION = _fila["FECHACREACION"].ToString();

                decimal _tot = FormatUtil.convertStringToDecimal(_fila["TOTAL"].ToString());

                _lvi.TOTAL = FormatUtil.applyCurrencyFormat(_tot);

                Source.Add(_lvi);
            }

            pltvTransacciones.ItemsSource = Source;


            if (Source.Count == 1)
            {
                for (int i = 0; i < Source.Count; i++)
                {
                    pltvTransacciones.SelectedItem = Source[i];
                }
            }
        }
    }
}
