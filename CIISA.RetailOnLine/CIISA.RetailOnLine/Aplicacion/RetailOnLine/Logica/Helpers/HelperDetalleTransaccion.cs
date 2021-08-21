using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperDetalleTransaccion
    {
        internal void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._detalleDocumento + " ");
            _sb.Append("SET ");
            _sb.Append(TableDetalleDocumento._ANULADO + " = ");
            _sb.Append("'" + MiscUtils.getVariableStringSQLState(true) + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetalleDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pobjTransaccion.v_codDocumento + "'");
            _sb.Append("AND ");
            _sb.Append(TableDetalleDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + pobjTransaccion.v_objTipoDocumento.GetSigla() + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal void guardarDetalleTransaccion(Cliente pobjCliente, bool v_DevolucionFactura = false)
        {
            string DefaultCodMotivo = string.Empty;

            foreach (TransaccionDetalle _objDetalle in pobjCliente.v_objTransaccion.v_listaDetalles)
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append("INSERT ");
                _sb.Append("INTO ");
                _sb.Append(TablesROL._detalleDocumento + " ");
                _sb.Append("(");
                _sb.Append(TableDetalleDocumento._CODCIA + ", ");
                _sb.Append(TableDetalleDocumento._CODDOCUMENTO + ", ");
                _sb.Append(TableDetalleDocumento._CODTIPODOCUMENTO + ", ");
                _sb.Append(TableDetalleDocumento._NUMLINEA + ", ");
                _sb.Append(TableDetalleDocumento._CODPRODUCTO + ", ");
                _sb.Append(TableDetalleDocumento._CANTIDAD + ", ");
                _sb.Append(TableDetalleDocumento._COMENTARIO + ", ");
                _sb.Append(TableDetalleDocumento._TOTALLINEA + ", ");
                _sb.Append(TableDetalleDocumento._TOTAL_IMP_LIN + ", ");
                _sb.Append(TableDetalleDocumento._NO_AGENTE + ", ");
                _sb.Append(TableDetalleDocumento._ENVIADO + ", ");
                _sb.Append(TableDetalleDocumento._PRECIO_UNI + ", ");
                _sb.Append(TableDetalleDocumento._PORC_DEC + ", ");
                _sb.Append(TableDetalleDocumento._MONTO_DESCUENTO + ", ");
                _sb.Append(TableDetalleDocumento._FECHA_CREA + ", ");
                _sb.Append(TableDetalleDocumento._COD_MOTIVO + ", ");
                _sb.Append(TableDetalleDocumento._ANULADO + ", ");
                _sb.Append(TableDetalleDocumento._ESTADODEVOLUCION + ", ");
                _sb.Append(TableDetalleDocumento._EMBALAJE + ", ");
                _sb.Append(TableDetalleDocumento._ES_VISCERA + ", ");
                _sb.Append(TableDetalleDocumento._TIPO_PORCION + ", ");
                _sb.Append(TableDetalleDocumento._CONSECUTIVODR + ", ");
                _sb.Append(TableDetalleDocumento._ES_FACTURA + ", ");
                _sb.Append(TableDetalleDocumento._PORCENTAJE_IMP + ", ");
                _sb.Append(TableDetalleDocumento._PORCENTAJE_IMP_EXO + ", ");
                _sb.Append(TableDetalleDocumento._IMP_EXO + ", ");
                _sb.Append(TableDetalleDocumento._TIPO_EXO + ", ");
                _sb.Append(TableDetalleDocumento._EXONERAID);
                _sb.Append(") ");
                _sb.Append("VALUES ");
                _sb.Append("(");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
                _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "', ");
                _sb.Append("'" + pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla() + "', ");
                _sb.Append("" + _objDetalle.v_numLinea + ", ");
                _sb.Append("'" + _objDetalle.v_objProducto.v_codProducto + "', ");
                _sb.Append("" + _objDetalle.v_objProducto.v_cantTransaccion + ", ");
                _sb.Append("'" + _objDetalle.v_objProducto.v_especificacionOV + "', ");
                _sb.Append("REPLACE('" + _objDetalle.v_totalLinea + "',',',''), ");
                _sb.Append("REPLACE('" + _objDetalle.v_totalLinImp + "',',',''), ");
                _sb.Append("'" + _objDetalle.v_noAgente + "', ");
                _sb.Append("'" + _objDetalle.v_enviado + "', ");
                _sb.Append("REPLACE('" + _objDetalle.v_precioUni + "',',',''), ");
                _sb.Append("" + _objDetalle.v_porcDesc + ", ");
                _sb.Append("REPLACE('" + _objDetalle.v_montoDescuento + "',',',''), ");
                _sb.Append("DATETIME('NOW', 'LOCALTIME'), ");

                //Si es una devolucion por factura y no tiene motivo es necesario asignar uno por defecto
                if (string.IsNullOrEmpty(_objDetalle.v_codMotivo) && v_DevolucionFactura)
                {
                    DefaultCodMotivo = buscarCodigoMotivoDefault(DefaultCodMotivo);

                    _sb.Append("'" + DefaultCodMotivo + "', ");
                }
                else
                {
                    _sb.Append("'" + _objDetalle.v_codMotivo + "', ");
                }

                _sb.Append("'" + _objDetalle.v_anulado + "', ");
                _sb.Append("'" + _objDetalle.v_objProducto.v_estado + "', ");
                _sb.Append("" + _objDetalle.v_objProducto.v_embalaje + ", ");
                _sb.Append("");


                if (_objDetalle.v_objProducto.EsViscera)
                {
                    _sb.Append("'" + SQL._Si + "', ");
                }
                else
                {
                    _sb.Append("'" + SQL._No + "', ");
                }

                _sb.Append("'" + _objDetalle.v_objProducto.TipoPorcion + "', ");
                _sb.Append("'" + _objDetalle.v_objProducto.ConsecutivoDReses + "', ");
                _sb.Append("'" + _objDetalle.v_Es_Factura + "', ");
                _sb.Append("'" + _objDetalle.v_total_imp + "', ");
                _sb.Append("'" + _objDetalle.v_total_imp_exo + "', ");
                _sb.Append("'" + _objDetalle.v_imp_exo + "', ");
                _sb.Append("'" + _objDetalle.v_tipo_exo + "', ");
                _sb.Append("'" + _objDetalle.v_exonera_id + "' ");
                _sb.Append(")");

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                MultiGeneric.insertRecord(_sb);
            }
        }

        internal decimal buscarMontoTransaccion(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM ");
            _sb.Append("(");
            _sb.Append(TableDetalleDocumento._TOTALLINEA + " ");
            _sb.Append("+ ");
            _sb.Append(TableDetalleDocumento._TOTAL_IMP_LIN);
            _sb.Append(") ");
            _sb.Append("TOTAL ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetalleDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "' ");
            _sb.Append("AND ");
            _sb.Append(TableDetalleDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal string buscarCodigoMotivoDefault(string DefaultCodMotivo)
        {
            string CodMotivo = string.Empty;

            if (string.IsNullOrEmpty(DefaultCodMotivo))
            {
                //Si esta vacio entonces buscar
                StringBuilder _sb = new StringBuilder();

                _sb.Append("SELECT ");
                _sb.Append("CODIGO ");
                _sb.Append("FROM ");
                _sb.Append("MOTIVO ");
                _sb.Append("WHERE ");
                _sb.Append("TIPO_DOC = ");
                _sb.Append("'DV' ");
                _sb.Append("LIMIT 1 ");

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                CodMotivo = MultiGeneric.readStringText(_sb);
            }
            else
            {
                CodMotivo = DefaultCodMotivo;
            }


            return CodMotivo;

        }
    }
}