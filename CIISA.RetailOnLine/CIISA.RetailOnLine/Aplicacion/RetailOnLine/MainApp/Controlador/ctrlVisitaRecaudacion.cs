using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.ComboTipoTransaccion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    internal class ctrlVisitaRecaudacion
    {
        private vistaVisitaRecaudacion view { get; set; }
        private vistaVisita viewVisita { get; set; }

        private Cliente v_objCliente = new Cliente();
        private bool Cerrado = false;

        internal ctrlVisitaRecaudacion(vistaVisitaRecaudacion pview,vistaVisita pviewVisita)
        {
            view = pview;
            viewVisita = pviewVisita;
        }

        internal void ScreenInicialization(Cliente pobjCliente)
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlRecaudacion"));

            v_objCliente = pobjCliente;

            renderComponents();
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlRecaudacion").Id))
            {
                view.Title = "Recaudación";
            }

            ppanel.IsVisible = true;
        }

        private void renderComponents()
        {
            llenarComboBoxRecaudacion();

            string _tipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            if (_tipoAgente.Equals(Agent._ruteroSigla))
            {
                view.FindByName<Label>("pnlRecaudacion_lblCliente").Text =
                    v_objCliente.v_no_cliente
                    + Simbol._hyphenWithSpaces
                    + v_objCliente.v_nombre;
            }

            if (_tipoAgente.Equals(Agent._supermercadoSigla) || _tipoAgente.Equals(Agent._carniceroSigla) || _tipoAgente.Equals(Agent._cobradorSigla))
            {
                view.FindByName<Label>("pnlRecaudacion_lblCliente").Text =
                    v_objCliente.v_no_cliente
                    + Simbol._point
                    + v_objCliente.v_objEstablecimiento.v_codEstablecimiento
                    + Simbol._hyphenWithSpaces
                    + v_objCliente.v_objEstablecimiento.v_descEstablecimiento;
            }
        }

        private void llenarComboBoxRecaudacion()
        {
            view.FindByName<Picker>("pnlRecaudacion_cbxMotivo").Items.Clear();

            Logica_ManagerMotivo _manager = new Logica_ManagerMotivo();

            DataTable _dt = _manager.buscarMotivoPorCodigoTransaccion(ROLTransactions._recaudacionSigla);

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlRecaudacion_cbxMotivo"),
                "Descripcion"
                );
        }


        internal void menu_mniAgregar_Click()
        {
            view.v_motivo = view.FindByName<Picker>("pnlRecaudacion_cbxMotivo").SelectedItem.ToString();
            Cerrado = true;
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void menu_mniCancelar_Click()
        {
            view.v_motivo = string.Empty;
            Cerrado = true;
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal async Task Cerrando()
        {
            if (Cerrado)
            {
                LogicaVisitaComboTT_RC logicaVisitaComboTT_RC = new LogicaVisitaComboTT_RC(viewVisita);
                await logicaVisitaComboTT_RC.Respuesta(view.v_motivo);
            }
        }

    }
}
