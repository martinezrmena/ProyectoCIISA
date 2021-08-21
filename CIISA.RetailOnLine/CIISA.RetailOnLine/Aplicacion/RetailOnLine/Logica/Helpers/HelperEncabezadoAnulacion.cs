using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Util;
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
    internal class HelperEncabezadoAnulacion
    {
        internal void buscarListaDeDocumentosAnulados(ListView pltvAnulaciones)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("EA.NO_CIA, ");
            _sb.Append("EA.NO_TRANSA, ");
            _sb.Append("EA.NO_CLIENTE, ");
            _sb.Append("EA.NO_ESTABLECIMIENTO, ");
            _sb.Append("EA.NO_AGENTE, ");
            _sb.Append("EA.TIPO_DOC, ");
            _sb.Append("STRFTIME('%d/%m/%Y', EA.FECHA_CREA ) FECHA_CREA, ");
            _sb.Append("STRFTIME('%d/%m/%Y', EA.FECHA_ENTREGA ) FECHA_ENTREGA, ");
            _sb.Append("EA.TOTAL, ");
            _sb.Append("EA.TOTAL_IMP, ");
            _sb.Append("EA.ENVIADO, ");
            _sb.Append("EA.FECHA_TOMA, ");
            _sb.Append("TT.DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoAnulacion + " EA, " + TablesROL._tipoTransaccion + " TT ");
            _sb.Append("WHERE ");
            _sb.Append("EA.TIPO_DOC = TT.TIPOTRANSACCION");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = pltvAnulaciones.ItemsSource as ObservableCollection<pnlAnulacion_ltvAnulaciones>;

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlAnulacion_ltvAnulaciones _lvi = new pnlAnulacion_ltvAnulaciones();

                _lvi.NO_CIA = _fila["NO_CIA"].ToString();
                _lvi.NO_TRANSA = _fila["NO_TRANSA"].ToString();

                _lvi.NO_CLIENTE = _fila["NO_CLIENTE"].ToString();
                _lvi.NO_ESTABLECIMIENTO = _fila["NO_ESTABLECIMIENTO"].ToString();
                _lvi.NO_AGENTE = _fila["NO_AGENTE"].ToString();

                _lvi.DESCRIPCION = _fila["DESCRIPCION"].ToString();

                _lvi.FECHA_CREA = _fila["FECHA_CREA"].ToString();
                _lvi.FECHA_ENTREGA = _fila["FECHA_ENTREGA"].ToString();

                decimal _tot = FormatUtil.convertStringToDecimal(_fila["TOTAL"].ToString());

                decimal _totalImp = FormatUtil.convertStringToDecimal(_fila["TOTAL_IMP"].ToString());

                string _total_imp = FormatUtil.applyCurrencyFormat(_totalImp);

                string _total = FormatUtil.applyCurrencyFormat(_tot);

                _lvi.TOTAL = _total;
                _lvi.TOTAL_IMP = _total_imp;

                Source.Add(_lvi);
            }

            pltvAnulaciones.ItemsSource = Source;
        }

        internal async void guardarEncabezadoAnulacion(TransaccionEncabezado pobjTransaccion)
        {
            StringBuilder _sb = new StringBuilder();
            GPS_Info.initializeGPS();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._encabezadoAnulacion + " ");
            _sb.Append("(");
            _sb.Append("" + TableEncabezadoAnulacion._NO_CIA + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._NO_TRANSA + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._NO_CLIENTE + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._NO_ESTABLECIMIENTO + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._NO_AGENTE + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._TIPO_DOC + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._FECHA_CREA + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._FECHA_ENTREGA + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._TOTAL + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._TOTAL_IMP + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._ENVIADO + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._FECHA_TOMA + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._LATITUD + ", ");
            _sb.Append("" + TableEncabezadoAnulacion._LONGITUD + "");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + pobjTransaccion.v_codCia + "', '");
            _sb.Append("" + pobjTransaccion.v_codDocumento + "', ");
            _sb.Append("'" + pobjTransaccion.v_codCliente + "', ");
            _sb.Append("'" + pobjTransaccion.v_objEstablecimiento.v_codEstablecimiento + "', ");
            _sb.Append("'" + pobjTransaccion.v_codAgente + "', ");
            _sb.Append("'" + pobjTransaccion.v_objTipoDocumento.GetSigla() + "', ");
            _sb.Append("DATETIME('NOW', 'LOCALTIME'), ");
            _sb.Append("'" + VarTime.dateSQLCE(pobjTransaccion.v_fechaEntrega) + "', ");
            _sb.Append("" + pobjTransaccion.v_total + ", ");
            _sb.Append("" + pobjTransaccion.v_totalImp + ", ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + VarTime.dateSQLCE(pobjTransaccion.v_fechaTomaFisica) + "', ");
            _sb.Append("REPLACE('" + await GPS_Info.v_gpsDevice.GetLatitude() + "',',',''), ");
            _sb.Append("REPLACE('" + await GPS_Info.v_gpsDevice.GetLongitude() + "',',','')");
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }

        internal decimal calcularTotalFlujoEfectivo(string pcodTipoDocumento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(TOTAL) AS TOTAL, ");
            _sb.Append("SUM(TOTAL_IMP) AS TOTAL_IMP ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoAnulacion + " ");
            _sb.Append("WHERE ");
            _sb.Append("TIPO_DOC IN (");
            _sb.Append("'" + ROLTransactions._facturaContadoSigla + "', ");
            _sb.Append("'" + ROLTransactions._reciboDineroSigla + "', ");
            _sb.Append("'" + ROLTransactions._recaudacionSigla + "')");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            decimal _calculo = Numeric._zeroDecimalInitialize;

            foreach (DataRow _fila in _dt.Rows)
            {
                _calculo += FormatUtil.convertStringToDecimal(_fila["TOTAL"].ToString());
            }

            return _calculo;
        }

        internal string buscarCodigoDocumentosAnulados()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_TRANSA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoAnulacion);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            Util _util = new Util();

            return _util.recordListString_dataTable(_dt,"NO_TRANSA");
        }

        internal DateTime buscarFechaHoraDocumento(string pcodTransaction, string pcodTipoTransaccion, string pcodCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoAnulacion._FECHA_CREA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoAnulacion + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoAnulacion._NO_CLIENTE + " = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoAnulacion._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoAnulacion._TIPO_DOC + " = ");
            _sb.Append("'" + pcodTipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _fechaHora = MultiGeneric.readStringText(_sb);

            return FormatUtil.convertStringToDateTimeWithTime(_fechaHora);
        }
    }
}
