using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria
{
    internal class LogicaCarniceriaActualizar
    {
        private vistaVisita view = null;
        private Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();
        private LogMessageAttention _logMessageAttention = new LogMessageAttention();

        internal LogicaCarniceriaActualizar(vistaVisita pview)
        {
            view = pview;
        }

        internal async Task<bool> actualizarFilasDetallesReses(Producto v_objproducto)
        {
            if (logica.EsDetalleRes(v_objproducto.v_codProducto))
            {
                //Debe validarse linea a linea para saber si es un detalle res
                //en ese caso debe eliminarse de la lista
                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaContadoNombre) ||
                        view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaCreditoNombre))
                {
                    //Verificar si existe un detalle res
                    if (logica.buscarDetalleResComprometidoCliente(
                        view.controlador.v_objCliente.v_no_cliente,
                        v_objproducto.v_codProducto))
                    {
                        //Si existe debe validarse lo que suman los detalle reses
                        decimal valor_disponible = logica.TotalPesoDisponibleDetalleRes(
                                            view.controlador.v_objCliente.v_no_cliente,
                                            v_objproducto.v_codProducto);

                        decimal valor_actual = v_objproducto.v_cantTransaccion;

                        if (valor_actual > valor_disponible)
                        {
                            v_objproducto.v_cantTransaccion = valor_disponible;
                            await _logMessageAttention.generalAttention("Se ajusto la linea: " + Environment.NewLine + Environment.NewLine
                                                                            + v_objproducto.v_codProducto + " - " + v_objproducto.v_descripcion + Environment.NewLine + Environment.NewLine
                                                                            + "por el inventario que el articulo posee comprometido, " + Environment.NewLine + Environment.NewLine
                                                                            + " de " + valor_actual + " a " + valor_disponible + " KGS.");

                            v_objproducto.v_cantTransaccion = valor_disponible;

                        }
                    }
                    else
                    {
                        await _logMessageAttention.generalAttention("Se elimino el producto '" + v_objproducto.v_codProducto + "' porque no posee inventario comprometido.");
                        return false;
                    }
                }
            }

            return true;

        }

        internal bool ValidarActualizar() {

            string c = view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString();

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaContadoNombre) ||
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaCreditoNombre))
            {
                return true;
            }

            return false;

        }

    }
}
