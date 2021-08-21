using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers
{
    public class FacturaElectronica
    {
        public string v_codProveedor { get; set; }
        public string v_numOrden { get; set; }
        public DateTime? v_fechaOrden { get; set; }
        public string v_numRecibo { get; set; }
        public DateTime? v_fechaRecibo { get; set; }
        public string v_numReclamo { get; set; }
        public DateTime? v_fechaReclamo { get; set; }


        public FacturaElectronica()
        {
            v_codProveedor = string.Empty;
            v_numOrden = string.Empty;
            v_numRecibo = string.Empty;
            v_fechaOrden = null;
            v_fechaRecibo = null;
            v_fechaReclamo = null;
            v_numReclamo = string.Empty;
        }

    }
}
