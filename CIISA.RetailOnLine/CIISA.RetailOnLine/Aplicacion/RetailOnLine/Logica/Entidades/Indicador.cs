using CIISA.RetailOnLine.Framework.Common.Misc;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class Indicador
    {

        public bool v_pedido { get; set; }

        public bool v_facturaContado { get; set; }

        public bool v_facturaCredito { get; set; }

        public bool v_respetaLimiteCredito { get; set; }

        public bool v_condicionCheque { get; set; }

        public decimal v_limiteCredito { get; set; }

        public bool v_vencimiento { get; set; }

        public bool v_estado { get; set; }

        public string v_no_agente { get; set; }

        public string v_no_establecimiento { get; set; }

        public string v_cobrador { get; set; }

        public bool v_esCobrador { get; set; }

        //Validacion 50mts
        public bool v_AplicaGeo { get; set; }

        //Validaciones Factura Electrónica

        public bool v_codProveedor { get; set; }

        public bool v_numOrden { get; set; }

        public bool v_fechaOrden { get; set; }

        public bool v_numRecibo { get; set; }

        public bool v_fechaRecibo { get; set; }

        public bool v_numReclamo { get; set; }

        public bool v_fechaReclamo { get; set; }


        public Indicador()
        {
            v_pedido = false;

            v_facturaContado = false;

            v_facturaCredito = false;

            v_respetaLimiteCredito = false;

            v_condicionCheque = false;

            v_limiteCredito = Numeric._zeroDecimalInitialize;

            v_vencimiento = false;

            v_estado = false;

            v_no_agente = string.Empty;

            v_no_establecimiento = string.Empty;

            v_cobrador = string.Empty;

            v_esCobrador = false;

            //Validacion 50mts
            v_AplicaGeo = false;

            //Validaciones Factura Electrónica
            v_codProveedor = false;

            v_numOrden = false;

            v_fechaOrden = false;

            v_numRecibo = false;

            v_fechaRecibo = false;

            v_numReclamo = false;

            v_fechaReclamo = false;

        }

    }
}
