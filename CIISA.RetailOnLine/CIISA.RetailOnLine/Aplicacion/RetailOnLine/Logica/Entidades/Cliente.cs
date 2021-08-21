using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class Cliente
    {
        public string v_no_cia { get; set; }
        public string v_no_cliente = string.Empty;
        public string v_nombre { get; set; }
        public string v_nombre_largo { get; set; }
        public string v_cedula { get; set; }
        public bool v_exento_imp { get; set; }
        public int v_plazo { get; set; }
        public string v_f_cierre { get; set; }
        public string v_lista_precios { get; set; }
        public Direccion v_objDireccion = new Direccion();
        public string v_telefono { get; set; }
        public string v_nombre_enc { get; set; }
        public string v_no_agente { get; set; }

        public int v_copias_fac { get; set; }

        public string v_codClienteGenerico { get; set; }
        public bool v_nuevoCliente { get; set; }

        public string v_tipoCedula { get; set; }

        public List<AutorizadoFirmar> v_listaAutorizadosFirmar = null;

        public string v_clasificacion { get; set; }

        public Establecimiento v_objEstablecimiento = new Establecimiento();

        public List<string> v_listaFacturas = new List<string>();

        public TransaccionEncabezado v_objTransaccion = new TransaccionEncabezado();

        public TransaccionEncabezado v_objTransaccionTramite = new TransaccionEncabezado();

        public TransaccionEncabezado v_objTransaccionPedido = new TransaccionEncabezado();

        public List<TransaccionEncabezado> v_objListaPedidos = new List<TransaccionEncabezado>();

        public List<TransaccionEncabezado> v_objListaFacturados = new List<TransaccionEncabezado>();

        public string v_email { get; set; }

        //validacion 50mts
        public double v_latitud { get; set; }
        public double v_longitud { get; set; }

        //Se agregan campos nuevos
        public string v_nombre_apo { get; set; }
        public string v_cedula_apo { get; set; }
        public string v_observaciones { get; set; }
        public string v_dias_atencion { get; set; }

        public Cliente(Cliente cliente)
        {
            v_no_cia = cliente.v_no_cia;
            v_no_cliente = cliente.v_no_cliente;
            v_nombre = cliente.v_nombre;
            v_nombre_largo = cliente.v_nombre_largo;
            v_cedula = cliente.v_cedula;
            v_exento_imp = cliente.v_exento_imp;
            v_plazo = cliente.v_plazo;
            v_f_cierre = cliente.v_f_cierre;
            v_lista_precios = cliente.v_lista_precios;
            v_objDireccion = cliente.v_objDireccion;
            v_telefono = cliente.v_telefono;
            v_nombre_enc = cliente.v_nombre_enc;
            v_no_agente = cliente.v_no_agente;
            v_copias_fac = cliente.v_copias_fac;

            v_codClienteGenerico = cliente.v_codClienteGenerico;
            v_nuevoCliente = cliente.v_nuevoCliente;

            v_tipoCedula = cliente.v_tipoCedula;

            v_listaAutorizadosFirmar = cliente.v_listaAutorizadosFirmar;

            v_objEstablecimiento = cliente.v_objEstablecimiento;

            v_listaFacturas = cliente.v_listaFacturas;

            v_objTransaccion = cliente.v_objTransaccion;

            v_objTransaccionTramite = cliente.v_objTransaccionTramite;

            v_objListaPedidos = cliente.v_objListaPedidos;

            v_email = cliente.v_email;

            //Validacion 50mts
            v_latitud = cliente.v_latitud;
            v_longitud = cliente.v_longitud;

            v_nombre_apo = cliente.v_nombre_apo;
            v_cedula_apo = cliente.v_cedula_apo;
            v_observaciones = cliente.v_observaciones;
            v_dias_atencion = cliente.v_dias_atencion;
        }

        public Cliente()
        {
            v_no_cia = string.Empty;
            v_no_cliente = string.Empty;
            v_nombre = string.Empty;
            v_nombre_largo = string.Empty;
            v_cedula = string.Empty;
            v_exento_imp = false;
            v_plazo = 0;
            v_f_cierre = string.Empty;
            v_lista_precios = string.Empty;
            v_objDireccion = new Direccion();
            v_telefono = string.Empty;
            v_nombre_enc = string.Empty;
            v_no_agente = string.Empty;
            v_copias_fac = 0;

            v_codClienteGenerico = string.Empty;
            v_nuevoCliente = false;

            v_tipoCedula = string.Empty;

            v_listaAutorizadosFirmar = new List<AutorizadoFirmar>();

            v_objEstablecimiento = new Establecimiento();

            v_listaFacturas = new List<string>();

            v_objTransaccion = new TransaccionEncabezado();

            v_objTransaccionTramite = new TransaccionEncabezado();

            v_objListaPedidos = new List<TransaccionEncabezado>();

            v_email = string.Empty;

            //Validacion 50mts
            v_latitud = 0;
            v_longitud = 0;

            v_nombre_apo = string.Empty;
            v_cedula_apo = string.Empty;
            v_observaciones = string.Empty;
            v_dias_atencion = string.Empty;            
        }

        private decimal totalFacturasPorPagar()
        {
            Logica_ManagerFactura _manager = new Logica_ManagerFactura();

            return _manager.calcularTotalFacturasPorPagarPorCliente(this);
        }

        public string nombreProvicia()
        {
            Logica_ManagerProvincia _manager = new Logica_ManagerProvincia();

            return _manager.obtenerNombreProvincia(v_objDireccion.v_provincia);
        }

        public string nombreCanton()
        {
            Logica_ManagerCanton _manager = new Logica_ManagerCanton();

            return _manager.obtenerNombreCanton(
                v_objDireccion.v_provincia,
                v_objDireccion.v_canton
                );
        }

        public string nombreDistrito()
        {
            Logica_ManagerDistrito _manager = new Logica_ManagerDistrito();

            return _manager.buscarNombreDistrito(
                v_objDireccion.v_provincia,
                v_objDireccion.v_canton,
                v_objDireccion.v_distrito
                );
        }

        public decimal creditoDisponible()
        {
            decimal _totalFacturasPorPagar = totalFacturasPorPagar();

            decimal _totalRecibos = totalPagoRecibos();

            decimal _creditoDisponible = v_objEstablecimiento.v_objIndicador.v_limiteCredito
                                            - _totalFacturasPorPagar
                                            + _totalRecibos;

            return _creditoDisponible;
        }

        private decimal totalPagoRecibos()
        {
            Logica_ManagerEncabezadoRecibo _manager = new Logica_ManagerEncabezadoRecibo();

            return _manager.calcularTotalRecibosPagosPorCliente(v_no_cliente);
        }

        public string exentoImpuesto()
        {
            string _exentoImpuesto = string.Empty;

            if (v_exento_imp)
            {
                _exentoImpuesto = Indicators._S;
            }
            else
            {
                _exentoImpuesto = Indicators._N;
            }

            return _exentoImpuesto;
        }

        public string condicionCheque()
        {
            string _condicionCheque = string.Empty;

            if (v_objEstablecimiento.v_objIndicador.v_condicionCheque)
            {
                _condicionCheque = Indicators._S;
            }
            else
            {
                _condicionCheque = Indicators._N;
            }

            return _condicionCheque;
        }

        public string fechaCierre()
        {
            return "1900-01-01 12:00:00.00";
        }

        public decimal montoPendienteDePago()
        {
            decimal _montoFacturas = totalFacturasPorPagar();
            decimal _montoRecibos = totalPagoRecibos();

            return _montoFacturas - _montoRecibos;
        }

    }
}
