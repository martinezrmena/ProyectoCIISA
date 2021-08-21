using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Controlador;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador
{
    internal class GUIDescarga
    {
        private ctrlDescarga controlador { get; set; }

        internal GUIDescarga(ctrlDescarga pcontrolador)
        {
            controlador = pcontrolador;
        }

        public void pintarEstadoBoton(bool pbtnSend, bool pbtnAbort)
        {
            controlador.renderBotones(pbtnSend, pbtnAbort);
        }

        private void actualizarInterfazUsuarioBoton(StateArgumentsButton pargumentosDeEstadoBoton)
        {
            pintarEstadoBoton(pargumentosDeEstadoBoton.v_btnSend,pargumentosDeEstadoBoton.v_btnAbort);
        }

        public void actualizarGUIdesdeHiloBoton(bool pbtnSend, bool pbtnAbort)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                actualizarInterfazUsuarioBoton(new StateArgumentsButton(pbtnSend, pbtnAbort));
            });
        }

        private void actualizarInterfazUsuario(StateArguments pargumentosEstado)
        {
            pintarEstado("Estado: "+ pargumentosEstado.v_message,pargumentosEstado.v_color);
        }

        public void pintarEstado(string pmensaje, Color pcolor)
        {
            controlador.view.FindByName<Label>("pnlTransacciones_lblEstado").Text = pmensaje;
            controlador.view.FindByName<Label>("pnlTransacciones_lblEstado").BackgroundColor = pcolor;
        }

        public void actualizarGUIdesdeHilo(string pmensaje, Color pcolor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                actualizarInterfazUsuario(new StateArguments(pmensaje, pcolor));
            });
        }

        internal void actualizarGUIdesdeHilo_0_porciento_iniciando()
        {
            actualizarGUIdesdeHilo("INICIANDO - 0%",Color.FromRgb(188, 103, 5));
        }

        internal void actualizarGUIdesdeHilo_100_porciento(DateTime pstartTime)
        {
            actualizarGUIdesdeHilo(
                "FINALIZÓ - 100%"
                + Environment.NewLine
                + new TimeSpan(VarTime.getNow().Ticks - pstartTime.Ticks).ToString(),
                Color.FromRgb(202, 188, 28));
        }

        internal void actualizarGUIdesdeHiloError()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                actualizarInterfazUsuario(new StateArguments("ERROR", Color.FromRgb(158, 4, 4)));
            });

            for (int i = Numeric._zeroInteger; i < 99999999; i++) ;
        }
    }
}
