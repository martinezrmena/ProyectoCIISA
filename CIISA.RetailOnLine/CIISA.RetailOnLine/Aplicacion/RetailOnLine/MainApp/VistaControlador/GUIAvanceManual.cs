using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.External.CustomProgressBar;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador
{
    internal class GUIAvanceManual
    {
        private ctrlIdentificarUsuario controlador { get; set; }

        internal GUIAvanceManual(ctrlIdentificarUsuario pcontrolador)
        {
            controlador = pcontrolador;
        }

        private void pintarEstado(string pmensaje, Color pcolor, double pvalue)
        {
            controlador.view.FindByName<Label>("lblEstadoCarga").Text = pmensaje;
            controlador.view.FindByName<Label>("lblEstadoCarga").BackgroundColor = pcolor;
            controlador.view.FindByName<CustomProgressBar>("pgbProgresoCarga").ProgressTo(pvalue, 100, Easing.Linear);
        }

        private void actualizarInterfazUsuario(StateArguments pargumentosDeEstado)
        {
            pintarEstado("ESTADO: " + pargumentosDeEstado.v_message, pargumentosDeEstado.v_color, pargumentosDeEstado.v_value);
        }

        internal async Task actualizarGUIdesdeHilo(string pmensaje, Color pcolor, double pvalue)
        {
            await Task.Run(() => {

                Device.BeginInvokeOnMainThread(() =>
                {
                    actualizarInterfazUsuario(new StateArguments(pmensaje, pcolor, pvalue));
                });

            }).ConfigureAwait(true);
        }

        internal async Task actualizarGUIdesdeHilo_0_porciento()
        {
            await actualizarGUIdesdeHilo(
                "INICIANDO - 0%",
                Color.FromRgb(188, 103, 5),
                0
                );
        }

        internal async Task actualizarGUIdesdeHilo_25_porciento()
        {
            await actualizarGUIdesdeHilo(
                "AVANCE - 25%"
                + Environment.NewLine
                + "RESPALDANDO INFORMACIÓN.",
                Color.FromRgb(10, 108, 3),
                0.2
                );
        } 

        internal async Task actualizarGUIdesdeHilo_65_porciento()
        {
            await actualizarGUIdesdeHilo(
                "AVANCE - 65%"
                + Environment.NewLine
                + "INICIANDO RECARGA DIARIA.",
                Color.FromRgb(10, 108, 3),
                0.6
                );
        }

        internal async Task actualizarGUIdesdeHiloError()
        {

            await Task.Run(() => {

                Device.BeginInvokeOnMainThread(() =>
                {
                    actualizarInterfazUsuario(new StateArguments("ERROR", Color.FromRgb(158, 4, 4), 0));
                });

            }).ConfigureAwait(true);
            
        }

        internal async Task actualizarGUIdesdeHilo_progresoInsercion(int pinserciones, int ptotalRegistros, String Proceso)
        {
            await actualizarGUIdesdeHilo(
                Environment.NewLine
                + "PROCESANDO - ["
                + Proceso + "] "
                + pinserciones
                + Simbol._slash
                + ptotalRegistros
                + Environment.NewLine,
                Color.FromRgb(10, 108, 3),
                0.8
                );
        }

        internal async Task actualizarGUIdesdeHilo_95_porciento()
        {
            await actualizarGUIdesdeHilo(
                "AVANCE - 95%",
                Color.FromRgb(10, 108, 3),
                0.9
                );
        }

        internal async Task actualizarGUIdesdeHilo_100_porciento(DateTime pstartTime)
        {
            await actualizarGUIdesdeHilo(
                "FINALIZÓ - 100%"
                + Environment.NewLine
                + new TimeSpan(VarTime.getNow().Ticks - pstartTime.Ticks).ToString(),
                Color.FromRgb(202, 188, 28),
                1
                );
        }
    }
}
