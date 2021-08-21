using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.VistaControlador
{
    internal class GUIRecargaManual
    {
        private ctrlCargaMenu controlador { get; set; }

        internal GUIRecargaManual(ctrlCargaMenu pcontrolador)
        {
            controlador = pcontrolador;
        }

        internal void pintarEstadoBoton(bool pbtnCargar, bool pbtnAbortar, bool pbtnCerrar)
        {
            controlador.renderBotones(pbtnCargar, pbtnAbortar, pbtnCerrar);
        }

        internal void actualizarInterfazUsuarioBoton(StateArgumentsButton pargumentosDeEstadoBoton)
        {
            pintarEstadoBoton(
                pargumentosDeEstadoBoton.v_btnUpload,
                pargumentosDeEstadoBoton.v_btnAbort,
                pargumentosDeEstadoBoton.v_btnClose
                );
        }

        internal void actualizarGUIdesdeHiloBoton(bool pbtnCargar, bool pbtnAbort, bool pbtnCerrar)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                actualizarInterfazUsuarioBoton(new StateArgumentsButton(pbtnCargar, pbtnAbort, pbtnCerrar));
            });
            //controlador.view.Invoke(new ManejadorDeEventosDeEstadoBoton(
            //    actualizarInterfazUsuarioBoton),
            //    new StateArgumentsButton(pbtnCargar, pbtnAbort, pbtnCerrar)
            //    );
        }


        internal void pintarEstado(string pmensaje, Color pcolor)
        {
            controlador.view.FindByName<Label>("pnlEstado_lblEstado").Text = pmensaje;
            controlador.view.FindByName<Label>("pnlEstado_lblEstado").BackgroundColor = pcolor;
        }
        internal void actualizarInterfazUsuario(StateArguments pargumentosDeEstado)
        {
            pintarEstado("Estado: "
                + pargumentosDeEstado.v_message,
                pargumentosDeEstado.v_color);
        }

        internal void actualizarGUIdesdeHilo(string pmensaje, Color pcolor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                actualizarInterfazUsuario(new StateArguments(pmensaje, pcolor));
            });
            //controlador.view.Invoke(new ManejadorDeEventosDeEstado(
            //    actualizarInterfazUsuario),
            //    new StateArguments(pmensaje, pcolor)
            //    );
        }

        internal void actualizarGUIdesdeHilo_0_porciento()
        {
            actualizarGUIdesdeHilo(
                "INICIANDO - 0%",
                Color.FromRgb(188, 103, 5)
                );
        }


        internal void pintarResultadoCarga(string pmensaje, Color pcolor)
        {
            controlador.view.FindByName<ExtendedEditor>("pnlMenu_txtBitacora").Text = Environment.NewLine;
            controlador.view.FindByName<ExtendedEditor>("pnlMenu_txtBitacora").Text = pmensaje;
            controlador.view.FindByName<ExtendedEditor>("pnlMenu_txtBitacora").BackgroundColor = pcolor;
        }

        internal void actualizarInterfazUsuarioResultadoCarga(StateArguments pargumentosDeEstado)
        {
            pintarResultadoCarga(
                pargumentosDeEstado.v_message,
                pargumentosDeEstado.v_color);
        }

        internal void actualizarGUIdesdeHiloResultadoCarga(string pmensaje, Color pcolor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                actualizarInterfazUsuarioResultadoCarga(new StateArguments(pmensaje, pcolor));
            });
            //controlador.view.Invoke(new ManejadorDeEventosDeEstado(
            //    actualizarInterfazUsuarioResultadoCarga),
            //    new StateArguments(pmensaje, pcolor)
            //    );
        }


        internal void actualizarGUIdesdeHilo_25_porciento()
        {
            actualizarGUIdesdeHilo(
                "AVANCE - 25%"
                + Environment.NewLine
                + "Solicitando Información",
                Color.FromRgb(10, 108, 3)
                );
        }

        internal void actualizarGUIdesdeHilo_45_porciento()
        {
            actualizarGUIdesdeHilo(
                "AVANCE - 45%"
                + Environment.NewLine
                + "Información descargada",
                Color.FromRgb(10, 108, 3)
                );
        }
        internal void actualizarGUIdesdeHilo_65_porciento()
        {
            actualizarGUIdesdeHilo(
                "AVANCE - 65%"
                + Environment.NewLine
                + "Borrado Tabla",
                Color.FromRgb(10, 108, 3)
                );
        }


        internal void actualizarGUIdesdeHiloError()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                actualizarInterfazUsuario(new StateArguments("ERROR", Color.FromRgb(158, 4, 4)));
            });
            //controlador.view.Invoke(new ManejadorDeEventosDeEstado(
            //    actualizarInterfazUsuario),
            //    new StateArguments("ERROR", Color.Red)
            //    );

            //for (int i = Numeric._zeroInteger; i < 99999999; i++) ;
        }


        internal void actualizarGUIdesdeHilo_progresoInsercion(int pinserciones,int ptotalRegistros)
        {
            actualizarGUIdesdeHilo(
                "AVANCE - "
                + pinserciones
                + Simbol._slash
                + ptotalRegistros
                + Environment.NewLine
                + "Procesando ...",
                Color.FromRgb(10, 108, 3)
                );
        }

        internal void actualizarGUIdesdeHilo_85_porciento()
        {
            actualizarGUIdesdeHilo(
                "AVANCE - 85%",
                Color.FromRgb(10, 108, 3)
                );
        }

        internal void actualizarGUIdesdeHilo_95_porciento()
        {
            actualizarGUIdesdeHilo(
                "AVANCE - 95%",
                Color.FromRgb(10, 108, 3)
                );
        }

        internal void actualizarGUIdesdeHilo_100_porciento(DateTime pstartTime)
        {

            actualizarGUIdesdeHilo(
                "FINALIZÓ - 100%"
                + Environment.NewLine
                + new TimeSpan(VarTime.getNow().Ticks - pstartTime.Ticks).ToString(),
                Color.FromRgb(202, 188, 28)
                );
        }
    }
}
