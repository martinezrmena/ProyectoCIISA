using CIISA.RetailOnLine.Framework.Common.Misc;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class ReciboDineroDetalle
    {
        public string v_no_cia { get; set; }
        public string v_no_transa { get; set; }
        public string v_no_factura { get; set; }
        public decimal v_monto { get; set; }
        public string v_tipo_doc { get; set; }
        public string v_enviado { get; set; }
        public string v_anulado { get; set; }

        public ReciboDineroDetalle()
        {
            v_no_cia = string.Empty;
            v_no_transa = string.Empty;
            v_no_factura = string.Empty;
            v_monto = Numeric._zeroDecimalInitialize;
            v_tipo_doc = string.Empty;
            v_enviado = string.Empty;
            v_anulado = string.Empty;
        }

    }
}
