using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Misc;
using System;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class TransaccionEncabezado
    {
        public string v_codCia { get; set; }
        public string v_codDocumento { get; set; }
        public string v_codAgente { get; set; }
        public string v_codCliente { get; set; }

        public Establecimiento v_objEstablecimiento = new Establecimiento();

        public TipoTransaccion v_objTipoDocumento { get; set; }
        public DateTime v_fechaCreacion { get; set; }
        public DateTime v_fechaEntrega { get; set; }
        public DateTime v_fechaTomaFisica { get; set; }

        public decimal v_total { get; set; }
        public decimal v_totalImp { get; set; }
        public string v_enviado { get; set; }

        public string v_codClienteGenerico { get; set; }

        public string v_anulado { get; set; }
        public string v_tramite { get; set; }
        public decimal v_montoDescuento { get; set; }
        public string v_motivoAnulacion { get; set; }

        public string v_observacion { get; set; }

        public decimal v_saldo { get; set; }

        public int v_noLinea { get; set; }

        public string v_indAutomatico { get; set; }

        public List<TransaccionDetalle> v_listaDetalles = new List<TransaccionDetalle>();

        public bool v_recuperarDocumento = false;

        public string v_facturaTramitar { get; set; }

        public List<FormaPago> v_listaFormaPago { get; set; }
        
        //Factura Electronica
        public FacturaElectronica v_objFacturaElectronica { get; set; }

        //Devolución de factura
        public string  v_codFactura { get; set; }

        public string v_codPedido { get; set; }

        public bool v_DevolucionFactura { get; set; }

        //Pedidos diferentes de respaldos
        public string v_Pedido_Planta { get; set; }

        public string Cod_RecuperarPedido { get; set; }

        public TransaccionEncabezado()
        {
            v_codCia = string.Empty;
            v_codDocumento = string.Empty;
            v_codAgente = string.Empty;
            v_codCliente = string.Empty;
            v_objEstablecimiento = new Establecimiento();
            v_objTipoDocumento = new TipoTransaccion();
            v_fechaCreacion = new DateTime();
            v_fechaEntrega = new DateTime();
            v_fechaTomaFisica = new DateTime();
            v_total = Numeric._zeroDecimalInitialize;
            v_totalImp = Numeric._zeroDecimalInitialize; ;
            v_enviado = string.Empty;
            v_codClienteGenerico = string.Empty;
            v_anulado = string.Empty;
            v_tramite = string.Empty;
            v_montoDescuento = Numeric._zeroDecimalInitialize;
            v_motivoAnulacion = string.Empty;
            v_observacion = string.Empty;
            v_saldo = Numeric._zeroDecimalInitialize; ;
            v_noLinea = 0;
            v_indAutomatico = string.Empty;
            v_listaDetalles = new List<TransaccionDetalle>();
            v_recuperarDocumento = false;
            v_facturaTramitar = string.Empty;
            v_listaFormaPago = new List<FormaPago>();

            //Factura Electronica
            v_objFacturaElectronica = new FacturaElectronica();

            //Devolución de factura
            v_codFactura = string.Empty;
            v_codPedido = string.Empty;
            v_DevolucionFactura = false;

            //Pedidos diferentes de respaldos
            v_Pedido_Planta = string.Empty;
            Cod_RecuperarPedido = string.Empty;
        }

        public TransaccionEncabezado(TransaccionEncabezado transaccionEncabezado)
        {
            v_codCia = transaccionEncabezado.v_codCia;
            v_codDocumento = transaccionEncabezado.v_codDocumento;
            v_codAgente = transaccionEncabezado.v_codAgente;
            v_codCliente = transaccionEncabezado.v_codCliente;
            v_objEstablecimiento = transaccionEncabezado.v_objEstablecimiento;
            v_objTipoDocumento = new TipoTransaccion(transaccionEncabezado.v_objTipoDocumento);
            v_fechaCreacion = transaccionEncabezado.v_fechaCreacion;
            v_fechaEntrega = transaccionEncabezado.v_fechaEntrega;
            v_fechaTomaFisica = transaccionEncabezado.v_fechaTomaFisica;
            v_total = transaccionEncabezado.v_total;
            v_totalImp = transaccionEncabezado.v_totalImp;
            v_enviado = transaccionEncabezado.v_enviado;
            v_codClienteGenerico = transaccionEncabezado.v_codClienteGenerico;
            v_anulado = transaccionEncabezado.v_anulado;
            v_tramite = transaccionEncabezado.v_tramite;
            v_montoDescuento = transaccionEncabezado.v_montoDescuento;
            v_motivoAnulacion = transaccionEncabezado.v_motivoAnulacion;
            v_observacion = transaccionEncabezado.v_observacion;
            v_saldo = transaccionEncabezado.v_saldo;
            v_noLinea = transaccionEncabezado.v_noLinea;
            v_indAutomatico = transaccionEncabezado.v_indAutomatico;
            v_listaDetalles = transaccionEncabezado.v_listaDetalles;
            v_recuperarDocumento = transaccionEncabezado.v_recuperarDocumento;
            v_facturaTramitar = transaccionEncabezado.v_facturaTramitar;
            v_listaFormaPago = transaccionEncabezado.v_listaFormaPago;

            //Factura Electronica
            v_objFacturaElectronica = transaccionEncabezado.v_objFacturaElectronica;

            //Devolución de factura
            v_codFactura = transaccionEncabezado.v_codFactura;

            //Vinculacion del pedido
            v_codPedido = transaccionEncabezado.v_codPedido;
            v_DevolucionFactura = transaccionEncabezado.v_DevolucionFactura;

            //Pedidos diferentes de respaldos
            v_Pedido_Planta = transaccionEncabezado.v_Pedido_Planta;
        }

    }
}
