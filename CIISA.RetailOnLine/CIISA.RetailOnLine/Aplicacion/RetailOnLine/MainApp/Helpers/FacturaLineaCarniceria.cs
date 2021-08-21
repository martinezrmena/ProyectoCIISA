using CIISA.RetailOnLine.Framework.Common.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers
{
    public class FacturaLineaCarniceria
    {
        //Generar Factura

        #region  Variables para las líneas
        public int v_linea { get; set; }
        public double v_precio { get; set; }
        public double v_precio_normal { get; set; }
        public double v_monto_bruto { get; set; }
        public double v_monto_descuento { get; set; }
        public double v_monto_promocional { get; set; }
        public double v_monto_lin_des { get; set; }
        public double v_monto_impuesto { get; set; }
        public double v_alm_o_pes { get; set; }
        public double v_precio_dol { get; set; }
        public DateTime? v_fecha_e { get; set; }
        public DateTime? v_fecha_s { get; set; }
        public string v_tipo_cambio { get; set; }
        #endregion

        #region Variables para encabezado
        public double v_subtotal { get; set; }
        public double v_imp_venta { get; set; }
        public string v_nom_cliente { get; set; }
        #endregion

        #region Otras variables
        public int v_num_fact_interno { get; set; }
        public int v_cant_facturas { get; set; }
        public double v_fact_ini { get; set; }
        public int v_conversion { get; set; }
        public int v_uda_pes { get; set; }
        public int v_cajas { get; set; }
        public int v_tarifa { get; set; }
        public string v_transporte { get; set; }
        public string vtransporte { get; set; }
        public int vcantidad { get; set; }
        public double v_descuento { get; set; }
        public string v_tipo { get; set; }
        public string v_unidad { get; set; }
        public string v_unidad_descuento { get; set; }
        public string v_dato { get; set; }
        public double v_desc { get; set; }
        public string v_supermercado { get; set; }
        public double v_precio_cerdo { get; set; }
        public double v_porc_cerdo { get; set; }
        public double vmonto_negociado { get; set; }
        public string varticulo_facturado { get; set; }
        public string vdiferencial { get; set; }
        public int vlote { get; set; }
        public string vexiste { get; set; }
        public double vpeso_canal { get; set; }
        public int vcan_disp { get; set; }
        public double vprecio_cerdo { get; set; }
        public int v_promocional { get; set; }
        public int v_porcentaje { get; set; }
        public int v_media { get; set; }
        public int v_entera { get; set; }
        public int v_monto_tr { get; set; }
        #endregion

        public FacturaLineaCarniceria()
        {
            #region  Variables para las líneas
            v_linea = Numeric._zeroInteger;
            v_precio = Numeric._zeroInteger;
            v_precio_normal = Numeric._zeroInteger;
            v_monto_bruto = Numeric._zeroInteger;
            v_monto_descuento = Numeric._zeroInteger;
            v_monto_promocional = Numeric._zeroInteger;
            v_monto_lin_des = Numeric._zeroInteger;
            v_monto_impuesto = Numeric._zeroInteger;
            v_alm_o_pes = Numeric._zeroInteger;
            v_precio_dol = Numeric._zeroInteger;
            v_fecha_e = null;
            v_fecha_s = null;
            v_tipo_cambio = string.Empty;
            #endregion

            #region Variables para encabezado
            v_subtotal = Numeric._zeroInteger;
            v_imp_venta = Numeric._zeroInteger;
            v_nom_cliente = string.Empty;
            #endregion

            #region Otras variables
            v_num_fact_interno = Numeric._zeroInteger;
            v_cant_facturas = Numeric._zeroInteger;
            v_fact_ini = Numeric._zeroInteger;
            v_conversion = Numeric._zeroInteger;
            v_uda_pes = Numeric._zeroInteger;
            v_cajas = Numeric._zeroInteger;
            v_tarifa = Numeric._zeroInteger;
            v_transporte = string.Empty;
            vtransporte = string.Empty;
            vcantidad = Numeric._zeroInteger;
            v_descuento = Numeric._zeroInteger;
            v_tipo = string.Empty;
            v_unidad = string.Empty;
            v_unidad_descuento = string.Empty;
            v_dato = string.Empty;
            v_desc = Numeric._zeroInteger;
            v_supermercado = string.Empty;
            v_precio_cerdo = Numeric._zeroInteger;
            v_porc_cerdo = Numeric._zeroInteger;
            vmonto_negociado = Numeric._zeroInteger;
            varticulo_facturado = string.Empty;
            vdiferencial = string.Empty;
            vlote = Numeric._zeroInteger;
            vexiste = string.Empty;
            vpeso_canal = Numeric._zeroInteger;
            vcan_disp = Numeric._zeroInteger;
            vprecio_cerdo = Numeric._zeroInteger;
            v_promocional = Numeric._zeroInteger;
            v_porcentaje = Numeric._zeroInteger;
            v_media = Numeric._zeroInteger;
            v_entera = Numeric._zeroInteger;
            v_monto_tr = Numeric._zeroInteger;
            #endregion

        }
    }
}
