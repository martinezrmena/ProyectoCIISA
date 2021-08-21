using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaLevantarObjetos
    {
        private vistaVisita view = null;

        internal LogicaVisitaLevantarObjetos(vistaVisita pview)
        {
            view = pview;
        }

        internal TransaccionEncabezado levantarObjetoTransaccionEncabezado()
        {
            bool FacElectronica = false;
            FacturaElectronica facturaElectronica_Temp = new FacturaElectronica();
            TransaccionEncabezado _objTransaccion = new TransaccionEncabezado();

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaContadoNombre)
                        || view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaCreditoNombre)
                        || view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._devolucionNombre))
            {
                FacElectronica = true;
                facturaElectronica_Temp = view.controlador.v_objCliente.v_objTransaccion.v_objFacturaElectronica;
            }

            _objTransaccion.v_anulado = SQL._No;
            _objTransaccion.v_codAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent;
            _objTransaccion.v_codCia = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany;
            _objTransaccion.v_codCliente = view.controlador.v_objCliente.v_no_cliente;
            _objTransaccion.v_codClienteGenerico = Agent.getCodClienteGenerico();
            _objTransaccion.v_codDocumento = string.Empty;

            _objTransaccion.v_objTipoDocumento.SetNombre(view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString());
            _objTransaccion.v_enviado = SQL._No;

            _objTransaccion.v_fechaCreacion = VarTime.getNowSQLite();
            _objTransaccion.v_fechaEntrega = VarTime.getDateExpires(Pedido._diasParaEntrega);

            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            _objTransaccion.v_fechaTomaFisica = _manager.buscarFechaTomaFisica();

            _objTransaccion.v_indAutomatico = SQL._No;
            var source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;
            _objTransaccion.v_noLinea = source.Count;

            _objTransaccion.v_observacion = string.Empty;
            _objTransaccion.v_saldo = Numeric._zeroDecimalInitialize;

            LogicaVisitaCalculos _logicaCalculos = new LogicaVisitaCalculos(view);
            _logicaCalculos.calcularMontoTotal_Impuesto_DescuentoTransaccion(_objTransaccion);

            _objTransaccion.v_tramite = SQL._No;

            if (FacElectronica)
            {
                _objTransaccion.v_objFacturaElectronica = facturaElectronica_Temp;
            }

            _objTransaccion.v_codFactura = view.controlador.v_objCliente.v_objTransaccion.v_codFactura;

            _objTransaccion.v_codPedido = view.controlador.COD_PEDIDO;

            _objTransaccion.v_DevolucionFactura = view.controlador.v_DevolucionFactura;

            view.controlador.v_objCliente.v_objTransaccion = _objTransaccion;

            return _objTransaccion;
        }

        internal void levantarObjetoTransaccionDetalle(TransaccionEncabezado pobjTransaccion)
        {
            var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            LogicaCarniceriaGuardar logica = new LogicaCarniceriaGuardar(view.controlador);

            Logica_ManagerCarniceria clogica = new Logica_ManagerCarniceria();

            foreach (var _lvi in Source)
            {
                LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                Producto _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

                TransaccionDetalle _objTransaccionDetalle = new TransaccionDetalle();

                _objTransaccionDetalle.v_codCia = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany;
                _objTransaccionDetalle.v_codDocumento = string.Empty;
                _objTransaccionDetalle.v_objTipoDocumento = pobjTransaccion.v_objTipoDocumento;                
                _objTransaccionDetalle.v_numLinea = Source.IndexOf(_lvi) + 1;

                _objTransaccionDetalle.v_objProducto = _objProducto;

                _objTransaccionDetalle.v_totalLinea = _objProducto.calcularMontoPrecioPorCantidadDeProducto(view.controlador.v_objCliente);

                _objTransaccionDetalle.v_totalLinImp = _objProducto.calcularMontoImpuestoPorCantidadDeProducto(view.controlador.v_objCliente);
                _objTransaccionDetalle.v_precioUni = _objProducto.precio(view.controlador.v_objCliente);
                _objTransaccionDetalle.v_porcDesc = _objProducto.porcentajeDescuento(view.controlador.v_objCliente);
                _objTransaccionDetalle.v_montoDescuento = _objProducto.calcularMontoDescuento(view.controlador.v_objCliente);

                _objTransaccionDetalle.v_noAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent;
                _objTransaccionDetalle.v_enviado = SQL._No;
                _objTransaccionDetalle.v_fechaCrea = null;

                Logica_ManagerMotivo _manager = new Logica_ManagerMotivo();

                _objTransaccionDetalle.v_codMotivo = _manager.obtenerCodigoMotivo(
                                                            view.controlador.v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla(),
                                                            _objProducto.v_motivo
                                                            );

                _objTransaccionDetalle.v_anulado = SQL._No;
                _objTransaccionDetalle.v_noFactura = string.Empty;
                _objTransaccionDetalle.v_total_imp = _objProducto.PorcentajeIVAInicial;
                _objTransaccionDetalle.v_total_imp_exo = _objProducto.PorcentajeIVA;
                _objTransaccionDetalle.v_tipo_exo = _objProducto.Exoneracion.TIPO;
                _objTransaccionDetalle.v_imp_exo = _objProducto.Exoneracion.PORC_EXONERA.ToString();
                _objTransaccionDetalle.v_exonera_id = _objProducto.Exoneracion.EXONERAID;

                if (_lvi.Es_Factura.Equals(SQL._Si))
                {
                    //ES NECESARIO VALIDAR SI EL PRODUCTO POSEE DETALLE RESES
                    if (clogica.buscarDetalleResExiste(_objTransaccionDetalle.v_objProducto.v_codProducto, view.controlador.COD_PEDIDO))
                    {
                        _objTransaccionDetalle.v_listaDetalleReses = logica.BuscarDetalleRes(
                                                                           _objTransaccionDetalle.v_objProducto.v_codProducto,
                                                                           view.controlador.v_objCliente,
                                                                           view.controlador.COD_PEDIDO);

                    }
                }

                _objTransaccionDetalle.v_Es_Factura = _lvi.Es_Factura;

                pobjTransaccion.v_listaDetalles.Add(_objTransaccionDetalle);

            }
        }
    }
}
