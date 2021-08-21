using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.External.ResizableColumnsListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels
{
    public class pnlTransacciones_ltvProductos
    {
        public string _pt_codigo { get; set; }
        public string _pt_cantidad { get; set; }
        public string _pt_comentario { get; set; }
        public string _pt_precio { get; set; }
        public string _pt_motivo { get; set; }
        public string MontoImpuestoUnaUnidad { get; set; }
        public string _pt_estado { get; set; }
        public string _pt_embalaje { get; set; }        
        public string _pt_porcDescuento { get; set; }
        public string _pt_montDescuento { get; set; }
        public string _pt_descripcion { get; set; }
        public string _pt_exento { get; set; }
        public Color ItemTextColor { get; set; }
        public string Estilo { get; set; }

        //Requerimiento de viceras
        public string _pt_viceras { get; set; }
        public string _pt_tipoporcion { get; set; }
        public string consecutivo_DReses { get; set; }

        //Dudosos
        public string _montoDescuento { get; set; }
        public string v_especificacionOV { get; set; }
        public string inventarioDisponible { get; set; }

        //Carniceria
        public string TipoAgente { get; set; }

        //Tipo Documento
        public string Es_Factura { get; set; }

        //TipoIVA
        public string Clave_IVA { get; set; }

        public pnlTransacciones_ltvProductos()
        {
            _pt_codigo = string.Empty;
            _pt_cantidad = string.Empty;
            _pt_comentario = string.Empty;
            _pt_motivo = string.Empty;
            _pt_estado = string.Empty;
            _pt_embalaje = string.Empty;
            _pt_precio = string.Empty;
            _pt_porcDescuento = string.Empty;
            _pt_montDescuento = string.Empty;
            _pt_descripcion = string.Empty;
            _pt_exento = string.Empty;
            ItemTextColor = Color.Default;

            MontoImpuestoUnaUnidad = string.Empty;
            _montoDescuento = string.Empty;
            v_especificacionOV = string.Empty;
            inventarioDisponible = string.Empty;

            //Es necesario validar el tipo de plantilla
            Estilo = TemplateStyles.VisitaCarniceria;

            //Viceras
            _pt_viceras = string.Empty;
            _pt_tipoporcion = string.Empty;
            consecutivo_DReses = string.Empty;
            Es_Factura = string.Empty;
            Clave_IVA = string.Empty;
        }
    }
}
