using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Constantes;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Exoneracion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class Producto
    {
        #region Variables
        #region Variables Heredadas
        public string v_codProducto { get; set; }

        public string v_codProductoPedido { get; set; }

        public decimal v_cantTransaccion { get; set; }

        public decimal v_cantPedido { get; set; }

        public string v_especificacionOV { get; set; }

        public decimal v_embalaje { get; set; }

        public string v_motivo { get; set; }

        public string v_estado { get; set; }

        public decimal v_precio { get; set; }
        public bool v_precioConsultado = false;

        public decimal v_porcentajeDescuento { get; set; }
        public bool v_porcentajeDescuentoConsultado = false;

        //Descuento GPC
        public decimal v_porcentajeDescuentoGeneral { get; set; }
        public bool v_porcentajeDescuentoGeneralConsultado = false;

        //Monto GPC
        public decimal v_MontoDescuento { get; set; }
        public bool ValoresListView { get; set; }


        public string v_fechaInicioDescuento { get; set; }
        public bool v_fechaInicioDescuentoConsultado = false;

        public string v_fechaVenceDescuento { get; set; }
        public bool v_fechaVenceDescuentoConsultado = false;

        public string v_descripcion { get; set; }
        public bool v_descripcionConsultado = false;

        public bool v_exento { get; set; }
        public bool v_exentoConsultado = false;

        public string v_unidad { get; set; }
        public bool v_unidadConsultado = false;

        //Tipo descuento
        public string v_tipoDescuentoProducto { get; set; }

        public string v_tipoDescuentoGeneral { get; set; }

        public ProductoInventario v_objProductoInventario = null;

        //Es Viscera
        public bool EsViscera { get; set; }
        public string TipoPorcion { get; set; }
        public string ConsecutivoDReses { get; set; }

        //Tipo Agente
        public string TipoAgente { get; set; }

        //Es el porcentaje IVA asignado al producto desde web service sin aplicar ningun calculo
        public decimal PorcentajeIVAInicial { get; set; }

        //Es el porcentaje de IVA que se aplicara al productos: aplicado a su vez la exoneración
        public decimal PorcentajeIVA { get; set; }
        #endregion

        #region Variables Exonera
        public pnlExoneracion_ltvExoneracion Exoneracion { get; set; }
        public string CodCliente { get; set; }
        //Es el porcentaje que se quita del porcentaje IVA inicial a razón de la exoneración
        public decimal Porcenataje_IVA_Exoneracion { get; set; }
        #endregion        
        #endregion

        public Producto()
        {
            v_codProducto = string.Empty;

            v_codProductoPedido = string.Empty;

            v_cantTransaccion = Numeric._zeroDecimalInitialize;

            v_cantPedido = Numeric._zeroDecimalInitialize;

            v_especificacionOV = string.Empty;

            v_embalaje = Numeric._zeroDecimalInitialize;

            v_motivo = string.Empty;

            v_estado = string.Empty;

            v_precio = Numeric._zeroDecimalInitialize;

            v_precioConsultado = false;

            v_porcentajeDescuento = Numeric._zeroDecimalInitialize;

            v_porcentajeDescuentoConsultado = false;

            v_porcentajeDescuentoGeneral = Numeric._zeroDecimalInitialize;

            v_porcentajeDescuentoGeneralConsultado = false;

            v_fechaInicioDescuento = string.Empty;

            v_fechaInicioDescuentoConsultado = false;

            v_fechaVenceDescuento = string.Empty;

            v_fechaVenceDescuentoConsultado = false;

            v_descripcion = string.Empty;

            v_descripcionConsultado = false;

            v_exento = false;

            v_exentoConsultado = false;

            v_unidad = string.Empty;

            v_unidadConsultado = false;

            v_tipoDescuentoProducto = string.Empty;

            v_tipoDescuentoGeneral = string.Empty;

            ValoresListView = false;

            v_objProductoInventario = new ProductoInventario();

            //Visceras
            EsViscera = false;

            TipoPorcion = string.Empty;

            ConsecutivoDReses = string.Empty;

            TipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            Exoneracion = new pnlExoneracion_ltvExoneracion();

            Exoneracion.PORC_EXONERA = Numeric._zeroDecimalInitialize;

            CodCliente = string.Empty;

        }

        #region Metodos Heredados
        public decimal inventarioDisponible()
        {
            return v_objProductoInventario.Disponible(v_codProducto);
        }

        public decimal inventarioVentas()
        {
            Logica_ManagerInventario _manager = new Logica_ManagerInventario();
            return _manager.buscarInventarioProductoVentas(v_codProducto);
        }

        public decimal inventarioRegalias()
        {
            Logica_ManagerInventario _manager = new Logica_ManagerInventario();
            return _manager.buscarInventarioProductoRegalias(v_codProducto);
        }

        public string descripcion()
        {
            if (!v_descripcionConsultado)
            {
                Logica_ManagerProducto _manager = new Logica_ManagerProducto();

                v_descripcion = _manager.buscarDescripcion(v_codProducto);

                v_descripcionConsultado = true;
            }

            return v_descripcion;
        }

        public string unidad()
        {
            if (!v_unidadConsultado)
            {
                Logica_ManagerProducto _manager = new Logica_ManagerProducto();

                v_unidad = _manager.buscarUnidad(v_codProducto);

                v_unidadConsultado = true;
            }

            return v_unidad;
        }

        public decimal precio(Cliente pobjCliente)
        {
            if (!v_precioConsultado)
            {
                Logica_ManagerPrecioProducto _manager = new Logica_ManagerPrecioProducto();
                v_precio = _manager.buscarPrecio(
                                pobjCliente.v_lista_precios,
                                v_codProducto
                                );

                if (EsViscera)
                {
                    if (TipoPorcion.Equals(TypeSlice.PorcionCompletaSigla))
                    {
                        v_precio = Numeric._zeroDecimalInitialize;
                    }
                    else if (TipoPorcion.Equals(TypeSlice.PorcionMitadSigla))
                    {
                        v_precio /= 2;
                    }
                }

                v_precioConsultado = true;
            }

            return v_precio;
        }

        private bool exento()
        {
            if (!v_exentoConsultado)
            {
                Logica_ManagerProducto _manager = new Logica_ManagerProducto();

                v_exento = _manager.BuscarExento(v_codProducto);

                v_exentoConsultado = true;
            }

            return v_exento;
        }

        public string exento(Cliente pobjCliente)
        {
            string _exento = string.Empty;

            if (pobjCliente.v_exento_imp)
            {
                _exento = Simbol._asterisk;
            }
            else
            {
                if (!exento())
                {
                    _exento = Simbol._asterisk;
                }
            }

            return _exento;
        }

        /// <summary>
        ///Consulta el tipo de descuento por producto
        /// </summary>
        /// return un string con el indicador para saber si es porcentaje o monto
        public string tipoDescuentoProducto(Cliente pobjCliente)
        {
            if (string.IsNullOrEmpty(v_tipoDescuentoProducto))
            {
                Logica_ManagerDescuento _manager = new Logica_ManagerDescuento();

                v_tipoDescuentoProducto = _manager.obtenerTipoDescuento(
                                                pobjCliente.v_no_cliente,
                                                v_codProducto
                                                );
            }

            return v_tipoDescuentoProducto;
        }

        /// <summary>
        ///Consulta el tipo de descuento general del cliente
        /// </summary>
        /// return un string con el indicador para saber si es porcentaje o monto
        public string tipoDescuentoGeneral(Cliente pobjCliente)
        {
            if (string.IsNullOrEmpty(v_tipoDescuentoGeneral))
            {
                Logica_ManagerDescuentoGeneral _manager = new Logica_ManagerDescuentoGeneral();

                v_tipoDescuentoGeneral = _manager.obtenerTipoDescuentoGeneral(
                                                pobjCliente.v_no_cliente,
                                                v_codProducto
                                                );
            }

            return v_tipoDescuentoGeneral;
        }

        /// <summary>
        ///Consulta el porcetaje o monto por producto
        /// </summary>
        /// return el valor del descuento por producto desde BD
        public decimal porcentajeDescuento(Cliente pobjCliente)
        {
            if (!v_porcentajeDescuentoConsultado)
            {
                Logica_ManagerDescuento _manager = new Logica_ManagerDescuento();

                v_porcentajeDescuento = _manager.obtenerDescuento(
                                                pobjCliente.v_no_cliente,
                                                v_codProducto,
                                                v_cantTransaccion
                                                );

                v_porcentajeDescuentoConsultado = true;
            }

            return v_porcentajeDescuento;
        }

        /// <summary>
        ///Realiza el calculo del descuento por producto ya sea por porcentaje o monto a aplicar
        /// </summary>
        /// return el valor del descuento por producto aplicado al precio del producto
        public decimal calcularDescuentoAplicadoPorProducto(decimal precio, decimal _ValorDescuentoProducto)
        {

            decimal _montoDescuento = Numeric._zeroDecimalInitialize;

            if (!v_tipoDescuentoProducto.Equals(TipoDescuento._NotExists))
            {
                if (v_tipoDescuentoProducto.Equals(TipoDescuento._Porcentaje))
                {
                    //Si se aplicara por porcentaje
                    _montoDescuento = precio * (_ValorDescuentoProducto / 100);
                }
                else if (v_tipoDescuentoProducto.Equals(TipoDescuento._Monto))
                {
                    //Si se evaluara por monto
                    _montoDescuento = _ValorDescuentoProducto;
                }
            }
            else
            {

                _montoDescuento = 0;
            }

            return _montoDescuento;

        }

        /// <summary>
        ///Consulta el porcetaje o monto general que posee el cliente
        /// </summary>
        /// return el valor del descuento general desde BD
        private decimal porcentajeDescuentoGeneral(Cliente pobjCliente)
        {
            if (!v_porcentajeDescuentoGeneralConsultado)
            {
                Logica_ManagerDescuentoGeneral _manager = new Logica_ManagerDescuentoGeneral();

                v_porcentajeDescuentoGeneral = _manager.obtenerDescuentoGeneral(
                                                pobjCliente.v_no_cliente,
                                                v_codProducto,
                                                v_cantTransaccion
                                                );

                v_porcentajeDescuentoGeneralConsultado = true;
            }

            return v_porcentajeDescuentoGeneral;
        }

        /// <summary>
        ///Realiza el calculo del descuento que posee el cliente ya sea por porcentaje o monto a aplicar
        /// </summary>
        /// return el valor del descuento general aplicado al precio del producto
        public decimal calcularDescuentoAplicadoPorCliente(decimal precio, decimal _ValorDescuentoGeneral)
        {
            decimal _montoDescuento = Numeric._zeroDecimalInitialize;

            if (!v_tipoDescuentoGeneral.Equals(TipoDescuento._NotExists))
            {
                if (v_tipoDescuentoGeneral.Equals(TipoDescuento._Porcentaje))
                {
                    //Si se aplicara por porcentaje
                    _montoDescuento += precio * (_ValorDescuentoGeneral / 100);
                }
                else if (v_tipoDescuentoGeneral.Equals(TipoDescuento._Monto))
                {
                    //Si se evaluara por monto
                    _montoDescuento += _ValorDescuentoGeneral;
                }
            }
            else
            {

                _montoDescuento += 0;

            }

            return _montoDescuento;

        }

        /// <summary>
        ///Metodo para calcular el porcetaje global de descuento aplicado para el producto
        ///Toma descuento por producto + descuento general del cliente segun indicadores
        /// </summary>
        /// return el porcentaje decimal del descuento aplicado
        public decimal porcentajeGlobalDescuento(Cliente pobjCliente)
        {

            decimal valor = Numeric._zeroDecimalInitialize;

            if (ValoresListView)
            {
                valor = v_porcentajeDescuento;
            }

            else
            {

                if (v_tipoDescuentoProducto.Equals(TipoDescuento._Porcentaje))
                {
                    valor = porcentajeDescuento(pobjCliente);
                }

                if (v_tipoDescuentoGeneral.Equals(TipoDescuento._Porcentaje))
                {
                    valor += porcentajeDescuentoGeneral(pobjCliente);
                }

            }

            return valor;
        }

        /// <summary>
        ///Metodo para calcular el monto global de descuento aplicado para el producto
        ///Toma descuento por producto + descuento general del cliente segun indicadores
        /// </summary>
        /// return el monto decimal del descuento aplicado
        public decimal montoGlobalDescuento(Cliente pobjCliente)
        {
            decimal valor = Numeric._zeroDecimalInitialize;

            if (ValoresListView)
            {
                valor = v_MontoDescuento;
            }
            else
            {

                if (v_tipoDescuentoProducto.Equals(TipoDescuento._Monto))
                {
                    valor = porcentajeDescuento(pobjCliente);
                }

                if (v_tipoDescuentoGeneral.Equals(TipoDescuento._Monto))
                {
                    valor += porcentajeDescuentoGeneral(pobjCliente);
                }
            }

            return valor;
        }

        public string fechaInicioDescuento(Cliente pobjCliente)
        {
            if (!v_fechaInicioDescuentoConsultado)
            {
                Logica_ManagerDescuento _manager = new Logica_ManagerDescuento();

                v_fechaInicioDescuento = _manager.obtenerFechaInicio(
                                                pobjCliente.v_no_cliente,
                                                v_codProducto
                                                );

                v_fechaInicioDescuentoConsultado = true;
            }

            return v_fechaInicioDescuento;
        }

        public string fechaVenceDescuento(Cliente pobjCliente)
        {
            if (!v_fechaVenceDescuentoConsultado)
            {
                Logica_ManagerDescuento _manager = new Logica_ManagerDescuento();

                v_fechaVenceDescuento = _manager.getDateExpires(
                                            pobjCliente.v_no_cliente,
                                            v_codProducto
                                            );

                v_fechaVenceDescuentoConsultado = true;
            }

            return v_fechaVenceDescuento;
        }

        private decimal calcularPrecioConDescuento(Cliente pobjCliente)
        {
            decimal _precio = precio(pobjCliente);

            decimal _montoDescuento = calcularMontoDescuento(pobjCliente);

            return _precio - _montoDescuento;
        }

        /// <summary>
        ///Metodo principal para calcular el global de montos y porcentajes de descuentos
        ///aplicados al producto, ademas desencadena la consulta de los indicadores de
        ///descuento general por cliente y por producto
        /// </summary>
        /// return el monto decimal de todos los descuentos aplicados para el producto
        public decimal calcularMontoDescuento(Cliente pobjCliente)
        {
            #region Variables

            decimal _precio = precio(pobjCliente);

            decimal _ValorDescuentoProducto = Numeric._zeroDecimalInitialize;

            decimal _ValorDescuentoGeneral = Numeric._zeroDecimalInitialize;

            decimal _montoDescuentoTotal = Numeric._zeroDecimalInitialize;

            #endregion

            //En caso de que los valores hayan sido provistos desde el listview o por un pedido
            if (ValoresListView)
            {
                _montoDescuentoTotal = _precio * (v_porcentajeDescuento / 100);
                _montoDescuentoTotal += v_MontoDescuento;
            }
            else
            {

                #region Consultas a BD

                v_tipoDescuentoProducto = tipoDescuentoProducto(pobjCliente);

                v_tipoDescuentoGeneral = tipoDescuentoGeneral(pobjCliente);

                _ValorDescuentoProducto = porcentajeDescuento(pobjCliente);

                _ValorDescuentoGeneral = porcentajeDescuentoGeneral(pobjCliente);

                #endregion

                #region Calculos por indicadores

                //Es necesario validar como se aplicara el descuento segun el indicador por producto
                _montoDescuentoTotal = calcularDescuentoAplicadoPorProducto(_precio, _ValorDescuentoProducto);

                //Es necesario validar como se aplicara el descuento segun el indicador por cliente
                _montoDescuentoTotal += calcularDescuentoAplicadoPorCliente(_precio, _ValorDescuentoGeneral);

                #endregion
            }

            return _montoDescuentoTotal;
        }

        public decimal calcularMontoDescuentoPorCantidaDeProducto(Cliente pobjCliente)
        {
            decimal _montoDescuento = calcularMontoDescuento(pobjCliente);

            return _montoDescuento * v_cantTransaccion;
        }

        public decimal calcularMontoLinea(Cliente pobjCliente)
        {
            decimal _precioConDescuentoEImpuesto = calcularMontoLineaUnaUnidad(pobjCliente);

            decimal _precioPorCantidad = _precioConDescuentoEImpuesto * v_cantTransaccion;

            return _precioPorCantidad;
        }

        public decimal calcularMontoLineaUnaUnidad(Cliente pobjCliente)
        {
            decimal _precioConDescuento = calcularPrecioConDescuento(pobjCliente);

            decimal _montoImpuesto = calcularMontoImpuestoUnaUnidad(pobjCliente);

            decimal _precioConImpuestoEImpuesto = _precioConDescuento + _montoImpuesto;

            return _precioConImpuestoEImpuesto;
        }
        #endregion

        #region Metodos Impuesto
        public decimal calcularMontoImpuestoUnaUnidad(Cliente pobjCliente)
        {
            decimal _montoImpuesto = Numeric._zeroDecimalInitialize;
            Logica_ManagerProducto _manager = new Logica_ManagerProducto();
            decimal _precioConDescuento = calcularPrecioConDescuento(pobjCliente);

            PorcentajeIVA = _manager.BuscarProcentajeIVA(v_codProducto);
            PorcentajeIVAInicial = PorcentajeIVA;
            RecalcularImpuesto_Exoneracion();

            decimal value = PorcentajeIVA / 100;
            _montoImpuesto = _precioConDescuento * value;

            return _montoImpuesto;
        }

        public decimal calcularMontoImpuestoPorCantidadDeProducto(Cliente pobjCliente)
        {
            decimal _montoImpuesto = calcularMontoImpuestoUnaUnidad(pobjCliente);

            return _montoImpuesto * v_cantTransaccion;
        }

        public decimal calcularMontoPrecioPorCantidadDeProducto(Cliente pobjCliente)
        {
            decimal _precioConDescuento = calcularPrecioConDescuento(pobjCliente);

            return _precioConDescuento * v_cantTransaccion;
        }

        public void procesarTransaccionDetalle(TransaccionDetalle pobjTransaccionDetalle, Cliente pobjCliente)
        {
            v_precioConsultado = false;

            pobjTransaccionDetalle.v_precioUni = precio(pobjCliente);

            pobjTransaccionDetalle.v_porcDesc = porcentajeDescuento(pobjCliente);
        }
        #endregion

        #region Metodos Exoneracion
        public decimal calcularExoneracion()
        {
            decimal MontoExoneracion = Numeric._zeroDecimalInitialize;
            Logica_ManagerProducto _manager = new Logica_ManagerProducto();
            string CodCompany = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany;

            Exoneracion = _manager.BuscarExoneracion(CodCompany, v_codProducto, CodCliente);

            MontoExoneracion = Exoneracion.PORC_EXONERA;

            return MontoExoneracion;
        }

        public decimal RecalcularImpuesto_Exoneracion()
        {
            decimal Result = Numeric._zeroDecimalInitialize;

            calcularExoneracion();

            if (Exoneracion.PORC_EXONERA > 0)
            {
                //En ningún caso la exoneración puede ser mayor al IVA con el que esta gravado el artículo
                switch (Exoneracion.TIPO)
                {
                    case ExoneracionConst.TipoPorcentualSigla:
                        //En el caso de la exoneración con base al 100% la exoneración no puede ser mayor al 100%
                        if (Exoneracion.PORC_EXONERA > 100)
                        {
                            Exoneracion.PORC_EXONERA = 100;
                        }
                        Porcenataje_IVA_Exoneracion = (Exoneracion.PORC_EXONERA / 100 * PorcentajeIVA);
                        PorcentajeIVA = PorcentajeIVA - Porcenataje_IVA_Exoneracion;
                        Result = PorcentajeIVA;
                        break;

                    case ExoneracionConst.TipoPuntosSigla:
                        //si se exonera con 5 puntos, y el IVA asociado al artículo es de 1% 
                        //la exoneración debe ser por 1 punto no por los 5 porque sobrepasaría el 100% del IVA,
                        //Es decir la exoneracion nunca puede ser mayor que el impuesto
                        if (Exoneracion.PORC_EXONERA > PorcentajeIVA)
                        {
                            Exoneracion.PORC_EXONERA = PorcentajeIVA;
                        }

                        Porcenataje_IVA_Exoneracion = Exoneracion.PORC_EXONERA;
                        PorcentajeIVA = PorcentajeIVA - Porcenataje_IVA_Exoneracion;
                        Result = PorcentajeIVA;
                        break;
                    default:
                        break;
                }
            }

            return Result;
        }
        #endregion
    }
}
