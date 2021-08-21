using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.ListviewModels
{
    public class pnlTelefono_ltvConfiguracion
    {
        public string Configuracion { get; set; }
        public string Valor { get; set; }
        public Color TextColor { get; set; }

        public pnlTelefono_ltvConfiguracion()
        {
            Configuracion = string.Empty;
            Valor = string.Empty;
            TextColor = Color.Default;
        }
    }
}
