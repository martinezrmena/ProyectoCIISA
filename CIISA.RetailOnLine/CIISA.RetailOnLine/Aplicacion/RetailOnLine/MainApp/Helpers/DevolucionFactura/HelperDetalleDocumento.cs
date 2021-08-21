using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.DevolucionFactura
{
    public class HelperDetalleDocumento
    {
        internal List<TransaccionDetalle> buscarDetalleDocumentoPorCodigoFactura(string CODFACTURA, string CODAGENTE)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("CODCIA, ");
            _sb.Append("CODDOCUMENTO, ");
            _sb.Append("NUMLINEA, ");
            _sb.Append("CODPRODUCTO, ");
            _sb.Append("CANTIDAD, ");
            _sb.Append("COMENTARIO, ");
            _sb.Append("TOTALLINEA, ");
            _sb.Append("ES_FACTURA, ");
            _sb.Append("TOTAL_IMP_LIN, ");
            _sb.Append("FECHA_CREA, ");
            _sb.Append("ENVIADO, ");
            _sb.Append("ES_VISCERA, ");
            _sb.Append("TIPO_PORCION, ");
            _sb.Append("CONSECUTIVODR ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODDOCUMENTO = ");
            _sb.Append("'" + CODFACTURA + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            List<TransaccionDetalle> _listaDetalles = new List<TransaccionDetalle>();

            foreach (DataRow _fila in _dt.Rows)
            {
                TransaccionDetalle _objDetalleTransaccion = new TransaccionDetalle();

                _objDetalleTransaccion.v_codCia = _fila["CODCIA"].ToString();
                _objDetalleTransaccion.v_codDocumento = _fila["CODDOCUMENTO"].ToString();

                _objDetalleTransaccion.v_numLinea = FormatUtil.convertStringToInt(_fila["NUMLINEA"].ToString());

                HelperProducto _helper = new HelperProducto();

                _objDetalleTransaccion.v_objProducto =
                _helper.buscarProductoPorCodigoProducto(_fila["CODPRODUCTO"].ToString());

                _objDetalleTransaccion.v_objProducto.v_cantTransaccion =
                FormatUtil.convertStringToDecimal(_fila["CANTIDAD"].ToString());

                _objDetalleTransaccion.v_objProducto.v_especificacionOV = _fila["COMENTARIO"].ToString();

                _objDetalleTransaccion.v_totalLinea =
                FormatUtil.convertStringToDecimal(_fila["TOTALLINEA"].ToString());

                _objDetalleTransaccion.v_objTipoDocumento.SetSigla(ROLTransactions._devolucionFacturaSigla);

                _objDetalleTransaccion.v_noAgente = CODAGENTE;
                _objDetalleTransaccion.v_fechaCrea = _fila["FECHA_CREA"].ToString();
                _objDetalleTransaccion.v_enviado = _fila["ENVIADO"].ToString();

                _objDetalleTransaccion.v_anulado = SQL._No;
                _objDetalleTransaccion.v_codMotivo = string.Empty;
                _objDetalleTransaccion.v_objProducto.v_estado = string.Empty;
                _objDetalleTransaccion.v_noFactura = string.Empty;
                _objDetalleTransaccion.v_Es_Factura = _fila["ES_FACTURA"].ToString();

                string viscera = _fila["ES_VISCERA"].ToString();

                //Es víscera
                if (!string.IsNullOrEmpty(viscera))
                {
                    if (viscera.Equals(SQL._Si))
                    {
                        _objDetalleTransaccion.v_objProducto.EsViscera = true;
                    }
                    else if (viscera.Equals(SQL._No))
                    {
                        _objDetalleTransaccion.v_objProducto.EsViscera = false;
                    }
                }

                _objDetalleTransaccion.v_objProducto.TipoPorcion = _fila["TIPO_PORCION"].ToString();

                _objDetalleTransaccion.v_objProducto.ConsecutivoDReses = _fila["CONSECUTIVODR"].ToString();

                _listaDetalles.Add(_objDetalleTransaccion);
            }

            return _listaDetalles;
        }
    }
}
