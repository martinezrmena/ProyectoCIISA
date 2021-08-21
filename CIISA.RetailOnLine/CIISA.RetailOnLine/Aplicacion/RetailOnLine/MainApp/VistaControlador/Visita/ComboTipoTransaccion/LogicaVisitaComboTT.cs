using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.ComboTipoTransaccion
{
    internal class LogicaVisitaComboTT
    {
        private vistaVisita view = null;

        internal LogicaVisitaComboTT(vistaVisita pview)
        {
            view = pview;
        }

        internal void establecerProductoParaModificacionLogica(Producto pobjProducto)
        {
            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._ordenVentaNombre))
            {
                pobjProducto.v_especificacionOV = EspecificacionProducto._ningunoNombre;
                pobjProducto.v_motivo = string.Empty;
                pobjProducto.v_estado = string.Empty;
            }

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._regaliaNombre))
            {
                pobjProducto.v_especificacionOV = string.Empty;
                pobjProducto.v_motivo = MotivoRegalia._autorizadoPorGerenciaNombre;
                pobjProducto.v_estado = string.Empty;
            }

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._devolucionNombre))
            {
                pobjProducto.v_especificacionOV = string.Empty;
                pobjProducto.v_motivo = MotivoDevolucion._sinMotivo;
                pobjProducto.v_estado = Pedido._devolucionMala;
            }

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._cotizacionNombre)
                || view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaContadoNombre)
                || view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaCreditoNombre))
            {
                pobjProducto.v_especificacionOV = string.Empty;
                pobjProducto.v_motivo = string.Empty;
                pobjProducto.v_estado = string.Empty;
            }

        }
    }
}
