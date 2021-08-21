using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlBitacora
    {
        #region Properties
        private vistaBitacora view { get; set; }
        private pnlBitacoraModel bitacora = new pnlBitacoraModel();
        private HelperBitacora helper = new HelperBitacora();
        public LogMessageAttention _logMessageAttention = new LogMessageAttention();
        #endregion

        public ctrlBitacora(vistaBitacora _view)
        {
            view = _view;
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlBitacora"));
            renderComponents();
        }

        private void renderComponents()
        {
            llenarComboBoxMotivo(view.FindByName<Picker>("pkSituacionNegocio"));
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlBitacora").Id))
            {
                view.Title = "Bitacora";
            }

            ppanel.IsVisible = true;
        }

        private void llenarComboBoxMotivo(Picker pcomboBox)
        {
            pcomboBox.Items.Clear();

            pcomboBox.Items.Add("Creciendo");
            pcomboBox.Items.Add("Estable");
            pcomboBox.Items.Add("Decreciendo");
        }

        internal async Task ProcesarInformacion()
        {
            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await LogMessages._dialogResultYes("¿Desea ingresar los datos para la bitacora actual?", "Agregar bitacora"))
            {
                if (ValidarFormularioBitacora())
                {
                    //Guardar la bitacora
                    if (helper.GuardarBitacora(bitacora) == 1)
                    {
                        await _logMessageAttention.generalAttention("La información se guardo correctamente.");
                        await Close_Click();
                    }
                    
                    ////Enviar clase por medio de web service
                    //var DPB = DependencyService.Get<ITaskActivity>();
                    //DPB.StartSendBitacora(bitacora);
                }
                else
                {
                    await _logMessageAttention.generalAttention("Existen campos sin completar, revise la información proporcionada.");
                }
            }
        }

        /// <summary>
        /// Metodo encargado de validar los datos del formulario antes de enviarlos
        /// </summary>
        /// <returns>Si el formulario se encuentra completo</returns>
        private bool ValidarFormularioBitacora()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (_validateHH.emptyDatePicker(view.FindByName<DatePicker>("dtpFechaVisita")))
            {
                return false;
            }

            if (_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("txbCodigoCliente")))
            {
                return false;
            }

            if (_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("txbVolCompra")))
            {
                return false;
            }

            if (_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("txbPorcentajeCompra")))
            {
                return false;
            }

            if (_validateHH.emptyPickerBox(view.FindByName<Picker>("pkSituacionNegocio")))
            {
                return false;
            }

            if (_validateHH.emptyEditorBox(view.FindByName<Editor>("txbQuejas")))
            {
                return false;
            }

            if (_validateHH.emptyEditorBox(view.FindByName<Editor>("txbOportunidades")))
            {
                return false;
            }

            if (_validateHH.emptyEditorBox(view.FindByName<Editor>("txbCompetencias")))
            {
                return false;
            }

            bitacora.FechaVisita = view.FindByName<DatePicker>("dtpFechaVisita").Date.ToString("dd/MM/yyyy");
            bitacora.Cod_Cliente = view.FindByName<ExtendedEntry>("txbCodigoCliente").Text;
            bitacora.Vol_Compra = view.FindByName<ExtendedEntry>("txbVolCompra").Text;

            bitacora.Porcentaje_Compra = view.FindByName<ExtendedEntry>("txbPorcentajeCompra").Text;
            bitacora.SituacionNegocio = view.FindByName<Picker>("pkSituacionNegocio").SelectedItem.ToString();
            bitacora.Quejas = view.FindByName<Editor>("txbQuejas").Text;
            bitacora.Oportunidades = view.FindByName<Editor>("txbOportunidades").Text;
            bitacora.Competencias = view.FindByName<Editor>("txbCompetencias").Text;

            return true;
        }

        internal async Task Close_Click()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
