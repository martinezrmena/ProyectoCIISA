using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Framework.Common.Misc;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades
{
    public class TransaccionDetalle
    {
        public string v_codCia { get; set; }
        public string v_codDocumento { get; set; }
        public TipoTransaccion v_objTipoDocumento { get; set; }
        public int v_numLinea { get; set; }

        public Producto v_objProducto = new Producto();
        public List<pnlTransacciones_ltvDetalleReses> v_listaDetalleReses = new List<pnlTransacciones_ltvDetalleReses>();

        public decimal v_totalLinea { get; set; }
        public decimal v_totalLinImp { get; set; }
        public string v_noAgente { get; set; }
        public string v_enviado { get; set; }
        public decimal v_precioUni { get; set; }
        public decimal v_porcDesc { get; set; }
        public decimal v_montoDescuento { get; set; }
        public string v_fechaCrea { get; set; }
        public string v_codMotivo { get; set; }
        public string v_anulado { get; set; }

        public string v_noFactura { get; set; }

        public string v_Es_Factura { get; set; }

        #region Exoneracion
        public decimal v_total_imp { get; set; }

        public decimal v_total_imp_exo { get; set; }

        public string v_exonera_id { get; set; }

        public string v_tipo_exo { get; set; }

        public string v_imp_exo { get; set; }
        #endregion

        public TransaccionDetalle()
        {
            v_codCia = string.Empty;
            v_codDocumento = string.Empty;
            v_objTipoDocumento = new TipoTransaccion();
            v_numLinea = 0;

            v_objProducto = new Producto();

            v_totalLinea = Numeric._zeroDecimalInitialize;
            v_totalLinImp = Numeric._zeroDecimalInitialize;
            v_noAgente = string.Empty;
            v_enviado = string.Empty;
            v_precioUni = Numeric._zeroDecimalInitialize;
            v_porcDesc = Numeric._zeroDecimalInitialize;
            v_montoDescuento = Numeric._zeroDecimalInitialize;
            v_fechaCrea = string.Empty;
            v_codMotivo = string.Empty;
            v_anulado = string.Empty;
            v_noFactura = string.Empty;
            v_Es_Factura = string.Empty;
            v_total_imp = 0;
            v_total_imp_exo = 0;
            v_exonera_id = string.Empty;
            v_tipo_exo = string.Empty;
        }
    }
}
