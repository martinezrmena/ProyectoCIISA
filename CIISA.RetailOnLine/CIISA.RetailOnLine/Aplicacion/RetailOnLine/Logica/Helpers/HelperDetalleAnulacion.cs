using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperDetalleAnulacion
    {
        internal void guardarDetalleAnulacionDeTransacciones(TransaccionEncabezado pobjTransaccion)
        {
            foreach (TransaccionDetalle pobjTransaccionDetalle in pobjTransaccion.v_listaDetalles)
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append("INSERT ");
                _sb.Append("INTO ");
                _sb.Append(TablesROL._detalleAnulacion + " ");
                _sb.Append("(");
                _sb.Append(TableDetalleAnulacion._NO_CIA + ",");
                _sb.Append(TableDetalleAnulacion._NO_TRANSA + ",");
                _sb.Append(TableDetalleAnulacion._NO_LINEA + ", ");
                _sb.Append(TableDetalleAnulacion._NO_ARTI + ", ");
                _sb.Append(TableDetalleAnulacion._TIPO_DOC + ", ");
                _sb.Append(TableDetalleAnulacion._CANTIDAD + ", ");
                _sb.Append(TableDetalleAnulacion._COMENTARIO + ", ");
                _sb.Append(TableDetalleAnulacion._TOTAL_LIN + ", ");
                _sb.Append(TableDetalleAnulacion._TOTAL_IMP_LIN + ", ");
                _sb.Append(TableDetalleAnulacion._NO_AGENTE + ", ");
                _sb.Append(TableDetalleAnulacion._ENVIADO);
                _sb.Append(") ");
                _sb.Append("VALUES ");
                _sb.Append("(");
                _sb.Append("'" + pobjTransaccionDetalle.v_codCia + "', ");
                _sb.Append("'" + pobjTransaccionDetalle.v_codDocumento + "', ");
                _sb.Append("'" + pobjTransaccionDetalle.v_numLinea + "', ");
                _sb.Append("'" + pobjTransaccionDetalle.v_objProducto.v_codProducto + "', ");
                _sb.Append("'" + pobjTransaccionDetalle.v_objTipoDocumento.GetSigla() + "', ");
                _sb.Append("'" + pobjTransaccionDetalle.v_objProducto.v_cantTransaccion + "', ");
                _sb.Append("'" + pobjTransaccionDetalle.v_objProducto.v_especificacionOV + "', ");
                _sb.Append("" + pobjTransaccionDetalle.v_totalLinea + ", ");
                _sb.Append("" + pobjTransaccionDetalle.v_totalLinImp + ", ");
                _sb.Append("'" + pobjTransaccion.v_codAgente + "', ");
                _sb.Append("'" + SQL._No + "'");
                _sb.Append(")");

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                MultiGeneric.insertRecord(_sb);
            }
        }
    }
}
