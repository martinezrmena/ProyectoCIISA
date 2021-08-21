using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperTransaccion
    {
        internal TransaccionEncabezado buscarReciboEncabezadoParaAnulaciones(string pcodTransaction,string pcodTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_TRANSA, ");
            _sb.Append("NO_CLIENTE, ");
            _sb.Append("NO_ESTABLECIMIENTO, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("OBSERVACION, ");
            _sb.Append("MONTO, ");
            _sb.Append("FECHA_CREA, ");
            _sb.Append("SALDO, ");
            _sb.Append("TIPO_DOC, ");
            _sb.Append("NO_LINEA, ");
            _sb.Append("MONTO_DEVOL, ");
            _sb.Append("NUMERO_DEVOL, ");
            _sb.Append("ENVIADO, ");
            _sb.Append("ANULADO, ");
            _sb.Append("FECHA_TOMA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_TRANSA = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append("TIPO_DOC = ");
            _sb.Append("'" + pcodTipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            TransaccionEncabezado _objTransaccion = new TransaccionEncabezado();

            foreach (DataRow _fila in _dt.Rows)
            {
                _objTransaccion.v_codCia = _fila["NO_CIA"].ToString();
                _objTransaccion.v_codDocumento = _fila["NO_TRANSA"].ToString();
                _objTransaccion.v_codCliente = _fila["NO_CLIENTE"].ToString();

                string _codEstablecimiento = _fila["NO_ESTABLECIMIENTO"].ToString();
                _objTransaccion.v_objEstablecimiento.v_codEstablecimiento = FormatUtil.convertStringToInt(_codEstablecimiento);

                _objTransaccion.v_codAgente = _fila["NO_AGENTE"].ToString();
                _objTransaccion.v_observacion = _fila["OBSERVACION"].ToString();

                string _monto = _fila["MONTO"].ToString();
                _objTransaccion.v_total = FormatUtil.convertStringToDecimal(_monto);

                string _fechaCreacion = _fila["FECHA_CREA"].ToString();
                _objTransaccion.v_fechaCreacion = FormatUtil.convertStringToDateTimeWithTime(_fechaCreacion);

                string _saldo = _fila["SALDO"].ToString();
                _objTransaccion.v_saldo = FormatUtil.convertStringToDecimal(_saldo);

                _objTransaccion.v_objTipoDocumento.SetSigla(_fila["TIPO_DOC"].ToString());

                string _noLinea = _fila["NO_LINEA"].ToString();
                _objTransaccion.v_noLinea = FormatUtil.convertStringToInt(_noLinea);

                _objTransaccion.v_enviado = _fila["ENVIADO"].ToString();
                _objTransaccion.v_anulado = _fila["ANULADO"].ToString();

                string _fechaTomaFisica = _fila["FECHA_TOMA"].ToString();
                _objTransaccion.v_fechaTomaFisica = FormatUtil.convertStringToDateTimeWithTime(_fechaTomaFisica);
            }

            return _objTransaccion;
        }

        internal void buscarReciboDetalleParaAnulaciones(TransaccionEncabezado pobjTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_TRANSA, ");
            _sb.Append("NO_FACTURA, ");
            _sb.Append("MONTO, ");
            _sb.Append("TIPO_DOC, ");
            _sb.Append("ENVIADO, ");
            _sb.Append("ANULADO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_TRANSA = ");
            _sb.Append("'" + pobjTransaccion.v_codDocumento + "' ");
            _sb.Append("AND ");
            _sb.Append("TIPO_DOC = ");
            _sb.Append("'" + pobjTransaccion.v_objTipoDocumento.GetSigla() + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            int _numeroLinea = 1;

            foreach (DataRow _fila in _dt.Rows)
            {
                TransaccionDetalle _detalle = new TransaccionDetalle();

                _detalle.v_codCia = _fila["NO_CIA"].ToString();
                _detalle.v_codDocumento = _fila["NO_TRANSA"].ToString();
                _detalle.v_noFactura = _fila["NO_FACTURA"].ToString();

                string _monto = string.Empty;
                _monto = _fila["MONTO"].ToString();
                _detalle.v_totalLinea = FormatUtil.convertStringToDecimal(_monto);

                _detalle.v_objTipoDocumento.SetSigla(_fila["TIPO_DOC"].ToString());
                _detalle.v_enviado = _fila["ENVIADO"].ToString();
                _detalle.v_anulado = _fila["ANULADO"].ToString();

                _detalle.v_numLinea = _numeroLinea++;

                pobjTransaccion.v_listaDetalles.Add(_detalle);
            }
        }

        internal TransaccionEncabezado buscarTransaccionEncabezadoParaAnulaciones(string pcodTransaction,string pcodTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoDocumento._CODCIA + ", ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._CODCLIENTE + ", ");
            _sb.Append(TableEncabezadoDocumento._CODESTABLECIMIENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._CODAGENTE + ", ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._FECHAENTREGA + ", ");
            _sb.Append(TableEncabezadoDocumento._FECHACREACION + ", ");
            _sb.Append(TableEncabezadoDocumento._TOTAL + ", ");
            _sb.Append(TableEncabezadoDocumento._TOTAL_IMP + ", ");
            _sb.Append(TableEncabezadoDocumento._ENVIADO + ", ");
            _sb.Append(TableEncabezadoDocumento._FECHA_TOMA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + pcodTipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            TransaccionEncabezado _objTransaccion = new TransaccionEncabezado();

            foreach (DataRow _fila in _dt.Rows)
            {
                _objTransaccion.v_codCia = _fila[TableEncabezadoDocumento._CODCIA].ToString();
                _objTransaccion.v_codDocumento = _fila[TableEncabezadoDocumento._CODDOCUMENTO].ToString();
                _objTransaccion.v_codCliente = _fila[TableEncabezadoDocumento._CODCLIENTE].ToString();

                string _codEstablecimiento = _fila[TableEncabezadoDocumento._CODESTABLECIMIENTO].ToString();
                _objTransaccion.v_objEstablecimiento.v_codEstablecimiento = FormatUtil.convertStringToInt(_codEstablecimiento);
                _objTransaccion.v_codAgente = _fila[TableEncabezadoDocumento._CODAGENTE].ToString();
                _objTransaccion.v_objTipoDocumento.SetSigla(_fila[TableEncabezadoDocumento._CODTIPODOCUMENTO].ToString());

                string _fechaCreacion = _fila[TableEncabezadoDocumento._FECHACREACION].ToString();
                _objTransaccion.v_fechaCreacion = FormatUtil.convertStringToDateTimeWithTime(_fechaCreacion);

                string _fechaEntrega = _fila[TableEncabezadoDocumento._FECHAENTREGA].ToString();
                _objTransaccion.v_fechaEntrega = FormatUtil.convertStringToDateTimeWithTime(_fechaEntrega);

                string _total = _fila[TableEncabezadoDocumento._TOTAL].ToString();
                _objTransaccion.v_total = FormatUtil.convertStringToDecimal(_total);

                string _total_imp = _fila[TableEncabezadoDocumento._TOTAL_IMP].ToString();
                _objTransaccion.v_totalImp = FormatUtil.convertStringToDecimal(_total_imp);

                _objTransaccion.v_enviado = _fila[TableEncabezadoDocumento._ENVIADO].ToString();

                string _fechaTomaFisica = _fila[TableEncabezadoDocumento._FECHA_TOMA].ToString();
                _objTransaccion.v_fechaTomaFisica = FormatUtil.convertStringToDateTimeWithTime(_fechaTomaFisica);

            }

            return _objTransaccion;
        }

        internal TransaccionEncabezado buscarTransaccionDetalleParaAnulaciones(
            TransaccionEncabezado pobjTransaccionEncabezado
            )
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleDocumento._CODCIA + ", ");
            _sb.Append(TableDetalleDocumento._CODDOCUMENTO + ", ");
            _sb.Append(TableDetalleDocumento._NUMLINEA + ", ");
            _sb.Append(TableDetalleDocumento._CODPRODUCTO + ", ");
            _sb.Append(TableDetalleDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append(TableDetalleDocumento._CANTIDAD + ", ");
            _sb.Append(TableDetalleDocumento._COMENTARIO + ", ");
            _sb.Append(TableDetalleDocumento._TOTALLINEA + ", ");
            _sb.Append(TableDetalleDocumento._TOTAL_IMP_LIN + ", ");
            _sb.Append(TableDetalleDocumento._NO_AGENTE + ", ");
            _sb.Append(TableDetalleDocumento._ENVIADO + ", ");
            _sb.Append(TableDetalleDocumento._ESTADODEVOLUCION + ", ");
            _sb.Append(TableDetalleDocumento._ES_VISCERA + ", ");
            _sb.Append(TableDetalleDocumento._TIPO_PORCION + ", ");
            _sb.Append(TableDetalleDocumento._CONSECUTIVODR + ", ");
            _sb.Append(TableDetalleDocumento._ES_FACTURA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetalleDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "' ");
            _sb.Append("AND ");
            _sb.Append(TableDetalleDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_objTipoDocumento.GetSigla() + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                TransaccionDetalle _detalle = new TransaccionDetalle();

                Logica_ManagerProducto _manager = new Logica_ManagerProducto();

                _detalle.v_objProducto = _manager.buscarProductoPorCodigoProducto(_fila[TableDetalleDocumento._CODPRODUCTO].ToString());

                _detalle.v_codCia = _fila[TableDetalleDocumento._CODCIA].ToString();
                _detalle.v_codDocumento = _fila[TableDetalleDocumento._CODDOCUMENTO].ToString();

                string _numLinea = _fila[TableDetalleDocumento._NUMLINEA].ToString();
                _detalle.v_numLinea = FormatUtil.convertStringToInt(_numLinea);

                _detalle.v_objProducto.v_codProducto = _fila[TableDetalleDocumento._CODPRODUCTO].ToString();

                _detalle.v_objTipoDocumento.SetSigla(_fila[TableDetalleDocumento._CODTIPODOCUMENTO].ToString());

                string _cantidad = _fila[TableDetalleDocumento._CANTIDAD].ToString();
                _detalle.v_objProducto.v_cantTransaccion = FormatUtil.convertStringToDecimal(_cantidad);

                _detalle.v_objProducto.v_especificacionOV = _fila[TableDetalleDocumento._COMENTARIO].ToString();

                string _totalLinea = _fila[TableDetalleDocumento._TOTALLINEA].ToString();
                _detalle.v_totalLinea = FormatUtil.convertStringToDecimal(_totalLinea);

                string _totalLinImp = _fila[TableDetalleDocumento._TOTAL_IMP_LIN].ToString();
                _detalle.v_totalLinImp = FormatUtil.convertStringToDecimal(_totalLinImp);

                _detalle.v_noAgente = _fila[TableDetalleDocumento._NO_AGENTE].ToString();
                _detalle.v_enviado = _fila[TableDetalleDocumento._ENVIADO].ToString();

                _detalle.v_objProducto.v_estado = _fila[TableDetalleDocumento._ESTADODEVOLUCION].ToString();

                string viscera = _fila[TableDetalleDocumento._ES_VISCERA].ToString();

                if (viscera.Equals(SQL._Si))
                {
                    _detalle.v_objProducto.EsViscera = true;
                }
                else
                {
                    _detalle.v_objProducto.EsViscera = false;
                }

                _detalle.v_objProducto.TipoPorcion = _fila[TableDetalleDocumento._TIPO_PORCION].ToString();

                _detalle.v_objProducto.ConsecutivoDReses = _fila[TableDetalleDocumento._CONSECUTIVODR].ToString();

                _detalle.v_Es_Factura = _fila[TableDetalleDocumento._ES_FACTURA].ToString();

                pobjTransaccionEncabezado.v_listaDetalles.Add(_detalle);
            }

            return pobjTransaccionEncabezado;
        }
    }
}
