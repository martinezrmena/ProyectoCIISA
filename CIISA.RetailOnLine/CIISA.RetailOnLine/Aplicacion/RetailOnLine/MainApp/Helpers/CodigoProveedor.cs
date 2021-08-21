using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers
{
    public class CodigoProveedor
    {
        public string dryText { get; set; }
        public string dryValue { get; set; }
        public string FrozenText { get; set; }
        public string FrozenValue { get; set; }

        public CodigoProveedor()
        {
            dryText = "Seco";
            dryValue = "018104920";
            FrozenText = "Congelado";
            FrozenValue = "018104910";
        }
    }
}
