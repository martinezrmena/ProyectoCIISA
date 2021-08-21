using CIISA.RetailOnLine.Framework.Handheld.GPS.Controller;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController
{
    internal class GUI_status
    {
        private ctrlGPS controlador { get; set; }

        internal GUI_status(ctrlGPS pcontrolador)
        {
            controlador = pcontrolador;
        }

        #region"hilo label"

        internal void actualizarGUIdesdeHilo(string pmensaje, Color pcolor)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                actualizarInterfazUsuario(new StateArguments(pmensaje, pcolor));
            });
        }

        private void actualizarInterfazUsuario(StateArguments pargumentosDeEstado)
        {
            pintarEstado(pargumentosDeEstado.v_message, pargumentosDeEstado.v_color);
        }

        private void pintarEstado(string pmensaje, Color pcolor)
        {
            controlador.view.FindByName<Label>("pnlGps_lblStatus").Text = pmensaje;
            controlador.view.FindByName<Label>("pnlGps_lblStatus").BackgroundColor = pcolor;
        }

        #endregion

    }
}
