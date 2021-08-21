using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using System;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class ProductoInventario
    {
        public string v_codCia { get; set; }
        public string v_codAgente { get; set; }
        public DateTime v_fechaToma { get; set; }
        public string v_codProducto { get; set; }
        public decimal v_cantidad { get; set; }
        public decimal v_ventas { get; set; }
        public decimal v_devolucionesBuenas { get; set; }
        public decimal v_devolucionesMalas { get; set; }
        public decimal v_regalias { get; set; }
        public decimal v_anulaciones { get; set; }
        public decimal v_anulacionesBuenas { get; set; }
        public decimal v_anulacionesMalas { get; set; }

        public decimal v_disponible { get; set; }
        public bool v_disponibleConsultado = false;

        public string v_fechaCrea { get; set; }

        public ProductoInventario()
        {
            v_codCia = string.Empty;
            v_codAgente = string.Empty;
            v_fechaToma = VarTime.getNow();
            v_codProducto = string.Empty;
            v_cantidad = Numeric._zeroDecimalInitialize;
            v_ventas = Numeric._zeroDecimalInitialize;
            v_devolucionesBuenas = Numeric._zeroDecimalInitialize;
            v_devolucionesMalas = Numeric._zeroDecimalInitialize;
            v_regalias = Numeric._zeroDecimalInitialize;
            v_anulacionesBuenas = Numeric._zeroDecimalInitialize;
            v_anulacionesMalas = Numeric._zeroDecimalInitialize;

            v_disponible = Numeric._zeroDecimalInitialize;
            v_disponibleConsultado = false;

            v_fechaCrea = string.Empty;
        }

        public void estableceraValoresInventario(string pcodProducto)
        {
            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            ProductoInventario _objProductoInventario = _manager.buscarInventarioProducto(pcodProducto);

            v_codCia = _objProductoInventario.v_codCia;
            v_codAgente = _objProductoInventario.v_codAgente;
            v_fechaToma = _objProductoInventario.v_fechaToma;
            v_codProducto = _objProductoInventario.v_codProducto;
            v_cantidad = _objProductoInventario.v_cantidad;
            v_ventas = _objProductoInventario.v_ventas;
            v_devolucionesBuenas = _objProductoInventario.v_devolucionesBuenas;
            v_devolucionesMalas = _objProductoInventario.v_devolucionesMalas;
            v_regalias = _objProductoInventario.v_regalias;
            v_anulacionesBuenas = _objProductoInventario.v_anulacionesBuenas;
            v_anulacionesMalas = _objProductoInventario.v_anulacionesMalas;

            v_disponible = _objProductoInventario.v_disponible;
            v_disponibleConsultado = _objProductoInventario.v_disponibleConsultado;

            v_fechaCrea = _objProductoInventario.v_fechaCrea;
        }

        public decimal Disponible(string pcodProducto)
        {
            if (!v_disponibleConsultado)
            {
                Logica_ManagerInventario _manager = new Logica_ManagerInventario();

                v_disponible = _manager.buscarInventarioProductoDisponible(pcodProducto);

                v_disponibleConsultado = true;

                return v_disponible;
            }
            else
            {
                return v_disponible;
            }
        }
    }
}
