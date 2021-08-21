using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.VistaControlador
{
    public class ShowRecargaManual
    {
        public void mostrarPantallaMenuCarga()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaCargaMenu());
        }
    }
}
