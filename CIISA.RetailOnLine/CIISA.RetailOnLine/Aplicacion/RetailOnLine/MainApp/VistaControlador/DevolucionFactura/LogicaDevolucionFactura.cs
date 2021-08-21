using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.DevolucionFactura;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.VistaControlador;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.DevolucionFactura
{
    public class LogicaDevolucionFactura
    {
        private vistaVisita view = null;

        internal LogicaDevolucionFactura(vistaVisita pview)
        {
            view = pview;
        }

        private void extraerDetalleFactura(Producto pobjProducto, LogicaVisitaLtvProducto plogica, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            foreach (TransaccionDetalle _objDetalle in pobjTransaccionEncabezado.v_listaDetalles)
            {
                pobjProducto = _objDetalle.v_objProducto;

                pobjProducto.v_estado = Pedido._devolucionMala;

                plogica.agregarProductoALaListaTransaccion(
                        pobjProducto,
                        Source.Count - 1,
                        false
                        );
            }
        }

        public void cargarProductosFacturados()
        {
            Producto _Producto = new Producto();
            bool _existeProductosFactura = ExisteProductosFacturaCliente(view.controlador.v_objCliente);

            if (_existeProductosFactura)
            {
                foreach (TransaccionEncabezado _objTransaccionEncabezado in view.controlador.v_objCliente.v_objListaFacturados)
                {
                    LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                    extraerDetalleFactura(_Producto, _logica, _objTransaccionEncabezado);

                    //guardamos el codigo de la transaccion
                    view.controlador.COD_DOCUMENTO = _objTransaccionEncabezado.v_codDocumento;
                }
            }
        }

        private List<TransaccionEncabezado> buscarProductosFactura(Cliente cliente, string COD_FACTURA)
        {
            Helper_EncabezadoDocumento _helper = new Helper_EncabezadoDocumento();

            return _helper.buscarProductosPorFactura(cliente, COD_FACTURA);
        }

        public bool ExisteProductosFacturaCliente(Cliente pobjCliente)
        {
            Helper_EncabezadoDocumento _helper = new Helper_EncabezadoDocumento();

            bool _existePedido = _helper.ExisteFacturaDocumentoCliente(view.controlador.COD_FACTURA);

            if (_existePedido)
            {
                pobjCliente.v_objListaFacturados = buscarProductosFactura(pobjCliente, view.controlador.COD_FACTURA);
            }

            return _existePedido;
        }

    }
}
