using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperEncabezadoTransaccion
    {
        internal int calcularTotalClientesFacturadosParaHoy()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DISTINCT ");
            _sb.Append(TableEncabezadoDocumento._CODCLIENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " IN (");
            _sb.Append("'" + ROLTransactions._facturaCreditoSigla + "', ");
            _sb.Append("'" + ROLTransactions._facturaContadoSigla + "') ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._FECHACREACION + " +1 > Date('Now')");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            return _dt.Rows.Count;
        }

        internal void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("SET ");
            _sb.Append(TableEncabezadoDocumento._ANULADO + " = ");
            _sb.Append("'" + MiscUtils.getVariableStringSQLState(true) + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pobjTransaccion.v_codDocumento + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + pobjTransaccion.v_objTipoDocumento.GetSigla() + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal async Task guardarEncabezadoTransaccion(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();
            GPS_Info.initializeGPS();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("(");
            _sb.Append(TableEncabezadoDocumento._CODCIA + ", ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._CODAGENTE + ", ");
            _sb.Append(TableEncabezadoDocumento._CODCLIENTE + ", ");
            _sb.Append(TableEncabezadoDocumento._CODESTABLECIMIENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._FECHACREACION + ", ");
            _sb.Append(TableEncabezadoDocumento._FECHAENTREGA + ", ");
            _sb.Append(TableEncabezadoDocumento._TOTAL + ", ");
            _sb.Append(TableEncabezadoDocumento._TOTAL_IMP + ", ");
            _sb.Append(TableEncabezadoDocumento._ENVIADO + ", ");
            _sb.Append(TableEncabezadoDocumento._CEDULA + ", ");
            _sb.Append(TableEncabezadoDocumento._NOMBRE + ", ");
            _sb.Append(TableEncabezadoDocumento._CLIENTE_NUEVO + ", ");
            _sb.Append(TableEncabezadoDocumento._MONTO_DEVOL + ", ");
            _sb.Append(TableEncabezadoDocumento._NUMERO_DEVOL + ", ");
            _sb.Append(TableEncabezadoDocumento._ANULADO + ", ");
            _sb.Append(TableEncabezadoDocumento._TRAMITE + ", ");
            _sb.Append(TableEncabezadoDocumento._MONTO_DESCUENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._NUEVO_CLIENTE + ", ");
            _sb.Append(TableEncabezadoDocumento._FECHA_TOMA + ", ");
            _sb.Append(TableEncabezadoDocumento._LATITUD + ", ");
            _sb.Append(TableEncabezadoDocumento._LONGITUD + ", ");

            #region Factura Electronica
            _sb.Append(TableEncabezadoDocumento._NUM_ORDEN + ", ");

            if (pobjCliente.v_objTransaccion.v_objFacturaElectronica.v_fechaOrden != null)
            {
                _sb.Append(TableEncabezadoDocumento._FECHA_ORDEN + ", ");
            }

            _sb.Append(TableEncabezadoDocumento._NUM_RECEPCION + ", ");
            _sb.Append(TableEncabezadoDocumento._NUM_RECLAMO + ", ");

            if (pobjCliente.v_objTransaccion.v_objFacturaElectronica.v_fechaReclamo != null)
            {
                _sb.Append(TableEncabezadoDocumento._FECHA_RECLAMO + ", ");
            }

            _sb.Append(TableEncabezadoDocumento._COD_PROVEEDOR);
            #endregion

            if (pobjCliente.v_objTransaccion.v_DevolucionFactura)
            {
                _sb.Append(", " + TableEncabezadoDocumento._CODFACTURA);
            }

            if ((pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla() == ROLTransactions._facturaContadoSigla || pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla() == ROLTransactions._facturaCreditoSigla) 
                &&  !string.IsNullOrEmpty(pobjCliente.v_objTransaccion.v_codPedido))
            {
                _sb.Append(", " + TableEncabezadoDocumento._CODPEDIDO);
            }

            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codAgente + "', ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "', ");
            _sb.Append("" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + ", ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla() + "', ");
            _sb.Append("DATETIME('NOW', 'LOCALTIME'), ");
            _sb.Append("'" + VarTime.getDateExpiresSQLCE(Pedido._diasParaEntrega) + "', ");
            _sb.Append("REPLACE('" + pobjCliente.v_objTransaccion.v_total + "',',',''), ");
            _sb.Append("REPLACE('" + pobjCliente.v_objTransaccion.v_totalImp + "',',',''), ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_enviado + "', ");
            _sb.Append("'" + pobjCliente.v_cedula + "', ");

            string _tipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            if (_tipoAgente.Equals(Agent._ruteroSigla))
            {
                _sb.Append("'" + pobjCliente.v_nombre + "', ");
            }

            if (_tipoAgente.Equals(Agent._supermercadoSigla))
            {
                _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_descEstablecimiento + "', ");
            }

            if (_tipoAgente.Equals(Agent._carniceroSigla))
            {
                _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_descEstablecimiento + "', ");
            }

            if (_tipoAgente.Equals(Agent._cobradorSigla))
            {
                _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_descEstablecimiento + "', ");
            }

            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codClienteGenerico + "', ");
            _sb.Append("REPLACE('" + Numeric._zeroInteger + "',',',''), ");
            _sb.Append("'" + string.Empty + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_anulado + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_tramite + "', ");
            _sb.Append("REPLACE('" + pobjCliente.v_objTransaccion.v_montoDescuento + "',',',''), ");
            _sb.Append("'" + MiscUtils.getVariableStringSQLState(pobjCliente.v_nuevoCliente) + "', ");
            _sb.Append("'" + VarTime.getDateTimeSQLiteComplete(pobjCliente.v_objTransaccion.v_fechaTomaFisica) + "', ");
            _sb.Append("REPLACE('" + await GPS_Info.v_gpsDevice.GetLatitude() + "',',',''), ");
            _sb.Append("REPLACE('" + await GPS_Info.v_gpsDevice.GetLongitude() + "',',',''), ");

            #region Factura Electronica
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_objFacturaElectronica.v_numOrden + "', ");

            if (pobjCliente.v_objTransaccion.v_objFacturaElectronica.v_fechaOrden != null)
            {
                _sb.Append("'" + VarTime.getDateTimeSQLiteComplete(pobjCliente.v_objTransaccion.v_objFacturaElectronica.v_fechaOrden) + "', ");
            }

            _sb.Append("'" + pobjCliente.v_objTransaccion.v_objFacturaElectronica.v_numRecibo + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_objFacturaElectronica.v_numReclamo + "', ");

            if (pobjCliente.v_objTransaccion.v_objFacturaElectronica.v_fechaReclamo != null)
            {
                _sb.Append("'" + VarTime.getDateTimeSQLiteComplete(pobjCliente.v_objTransaccion.v_objFacturaElectronica.v_fechaReclamo) + "', ");
            }

            _sb.Append("'" + pobjCliente.v_objTransaccion.v_objFacturaElectronica.v_codProveedor + "' ");
            #endregion

            if (pobjCliente.v_objTransaccion.v_DevolucionFactura)
            {
                _sb.Append(", '" + pobjCliente.v_objTransaccion.v_codFactura + "' ");
            }

            if ((pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla() == ROLTransactions._facturaContadoSigla || pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla() == ROLTransactions._facturaCreditoSigla)
                && !string.IsNullOrEmpty(pobjCliente.v_objTransaccion.v_codPedido))
            {
                _sb.Append(", '" + pobjCliente.v_objTransaccion.v_codPedido + "' ");
            }

            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }

        internal void actualizarTramitado(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("SET ");
            _sb.Append(TableEncabezadoDocumento._TRAMITE + " = ");
            _sb.Append("'" + MiscUtils.getVariableStringSQLState(true) + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_facturaTramitar + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + ROLTransactions._facturaCreditoSigla + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        private StringBuilder sentenciaTramite(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("MAX(" + TableEncabezadoDocumento._CODDOCUMENTO + ") " + TableEncabezadoDocumento._CODDOCUMENTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + ROLTransactions._facturaCreditoSigla + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODCLIENTE + " = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._TRAMITE + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("DATE(" + TableEncabezadoDocumento._FECHACREACION + ",'+1 DAY') > DATE('NOW')");

            return _sb;
        }

        private string buscarCodigoDocumentoTramite(Cliente pobjCliente)
        {
            StringBuilder _sb = sentenciaTramite(pobjCliente);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal void buscarListaTransaccionEncabezadosParaTramite(ListView pltvTransacciones, Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCIA + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODDOCUMENTO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODAGENTE + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCLIENTE + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODESTABLECIMIENTO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoDocumento._FECHACREACION + " ) " + TableEncabezadoDocumento._FECHACREACION + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoDocumento._FECHAENTREGA + " ) " + TableEncabezadoDocumento._FECHAENTREGA + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._TOTAL + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._TOTAL_IMP + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._MONTO_DESCUENTO + ", ");
            _sb.Append("TT.DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ED, " + TablesROL._tipoTransaccion + " TT ");
            _sb.Append("WHERE ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + buscarCodigoDocumentoTramite(pobjCliente) + "' ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + ROLTransactions._facturaCreditoSigla + "' ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCLIENTE + " = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._TRAMITE + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("DATE(ED." + TableEncabezadoDocumento._FECHACREACION + ",'+1 DAY') > DATE('NOW') ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("TT.TIPOTRANSACCION");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            ObservableCollection<pnlTramite_ltvDocumentos> Lista = new ObservableCollection<pnlTramite_ltvDocumentos>();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlTramite_ltvDocumentos _lvi = new pnlTramite_ltvDocumentos();
                _lvi.Documento = _fila[TableEncabezadoDocumento._CODDOCUMENTO].ToString();
                _lvi.Cliente = _fila[TableEncabezadoDocumento._CODCLIENTE].ToString();
                _lvi.Establecimiento = _fila[TableEncabezadoDocumento._CODESTABLECIMIENTO].ToString();
                _lvi.Creado = _fila[TableEncabezadoDocumento._FECHACREACION].ToString();
                _lvi.Entrega = _fila[TableEncabezadoDocumento._FECHAENTREGA].ToString();

                decimal _tot = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoDocumento._TOTAL].ToString());

                decimal _montoDes = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoDocumento._MONTO_DESCUENTO].ToString());

                string _total = FormatUtil.applyCurrencyFormat(_tot - _montoDes);

                _lvi.Total = _total;

                decimal _totalImp = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoDocumento._TOTAL_IMP].ToString());

                string _total_imp = FormatUtil.applyCurrencyFormat(_totalImp);

                _lvi.Impuesto = _total_imp;

                string _monto_descuento = FormatUtil.applyCurrencyFormat(_montoDes);

                _lvi.Descuento = _monto_descuento;

                Lista.Add(_lvi);
            }

            pltvTransacciones.ItemsSource = Lista;

            if (Lista.Count == 1)
            {
                for (int i = 0; i < Lista.Count; i++)
                {
                    pltvTransacciones.SelectedItem = Lista[i];
                }
            }
        }

        internal decimal calcularTotalFlujoEfectivo(string pcodTipoDocumento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(" + TableEncabezadoDocumento._TOTAL + ") AS " + TableEncabezadoDocumento._TOTAL + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + pcodTipoDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal DateTime buscarFechaHoraDocumento(string pcodTransaction, string pcodTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoDocumento._FECHACREACION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + pcodTipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _fechaHora = MultiGeneric.readStringText(_sb);

            return FormatUtil.convertStringToDateTimeWithTime(_fechaHora);
        }
    }
}
