using CIISA.RetailOnLine.Framework.Common.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria
{
    public class pnlTransacciones_ltvDetalleReses
    {
        public string _vc_consecutivo { get; set; }
        public string _vc_cia { get; set; }
        public string _vc_numpedido { get; set; }
        public string _vc_nocliente { get; set; }
        public string _vc_articulo { get; set; }
        public string _vc_indtipo { get; set; }
        public string _vc_fechamatanza { get; set; }
        public string _vc_lote { get; set; }
        public string _vc_noanimal { get; set; }
        public string _vc_tipoporcion { get; set; }
        public Decimal _vc_peso { get; set; }
        public string _vc_comprometido { get; set; }
        public string _vc_vendido { get; set; }
        public string _vc_seleccionado { get; set; }
        public string _vcVasignado { get; set; }

        public pnlTransacciones_ltvDetalleReses()
        {
            _vc_consecutivo = string.Empty;
            _vc_cia = string.Empty;
            _vc_numpedido = string.Empty;
            _vc_nocliente = string.Empty;
            _vc_articulo = string.Empty;
            _vc_indtipo = string.Empty;
            _vc_fechamatanza = string.Empty;
            _vc_lote = string.Empty;
            _vc_noanimal = string.Empty;
            _vc_tipoporcion = string.Empty;
            _vc_peso = Numeric._zeroDecimalInitialize;
            _vc_comprometido = string.Empty;
            _vc_vendido = string.Empty;
            _vc_seleccionado = string.Empty;
            _vcVasignado = string.Empty;
        }
    }
}
