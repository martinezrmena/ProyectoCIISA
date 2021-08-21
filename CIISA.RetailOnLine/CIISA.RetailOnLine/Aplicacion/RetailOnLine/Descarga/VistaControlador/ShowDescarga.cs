using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador
{
    public class ShowDescarga
    {
        public void mostrarPantallaDescarga(string ppantallaInvoca, string ptipoDescarga, bool pesUsuarioLiquidador)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaDescarga(ppantallaInvoca, ptipoDescarga, pesUsuarioLiquidador));
        }
    }
}
