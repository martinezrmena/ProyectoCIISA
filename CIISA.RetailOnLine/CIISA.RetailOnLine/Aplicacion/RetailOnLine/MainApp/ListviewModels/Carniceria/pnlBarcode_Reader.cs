using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria
{
    /// <summary>
    ///Clase encargada de desfragmentar el Código de barras proporcionado
    /// </summary>
    /// return Cajas Escaneadas con codigos
    public class pnlBarcode_Reader
    {
        public string CODCLIENTE { get; set; }
        public string CODCAJA { get; set; }
        public string CODESTABLECIMIENTO { get; set; }

        public pnlBarcode_Reader() {
            CODCLIENTE = string.Empty;
            CODCAJA = string.Empty;
            CODESTABLECIMIENTO = string.Empty;
        }

        public void Split_BarCode(string code) {

            CODCLIENTE = code.Substring(0,4);
            CODCAJA = code.Substring(4, 8);
            CODESTABLECIMIENTO = code.Substring(8);

        }
    }
}
