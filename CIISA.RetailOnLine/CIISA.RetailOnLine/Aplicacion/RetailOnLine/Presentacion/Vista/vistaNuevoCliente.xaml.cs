using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaNuevoCliente : ContentPage
    {
        private ctrlNuevoCliente controlador = null;
        public int MaxLenght { get; set; }
        SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaNuevoCliente()
        {
            controlador = new ctrlNuevoCliente(this);

            InitializeComponent();

            controlador.ScreenInicialization();

            MaxLenght = 800;
        }

        private async Task pnlInformacion_btnLimpiarNL_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInformacion_btnLimpiarNL_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInformacion_btnLimpiarRS_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInformacion_btnLimpiarRS_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInformacion_btnBorrarTelefono_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInformacion_btnBorrarTelefono_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInformacion_btnLimpiarTelefono_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInformacion_btnLimpiarTelefono_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlConsulta_rdbDimex_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            pnlConsulta_rdbDimex.CheckedChanged -= pnlConsulta_rdbDimex_CheckedChanged;
            pnlConsulta_rdbPasaporte.CheckedChanged -= pnlConsulta_rdbPasaporte_CheckedChanged;
            pnlConsulta_rdbJuridico.CheckedChanged -= pnlConsulta_rdbJuridico_CheckedChanged;
            pnlConsulta_rdbFisico.CheckedChanged -= pnlConsulta_rdbFisico_CheckedChanged;            

            try
            {
                controlador.renderTipoDocumento(false, false, false, true);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            pnlConsulta_rdbDimex.CheckedChanged += pnlConsulta_rdbDimex_CheckedChanged;
            pnlConsulta_rdbPasaporte.CheckedChanged += pnlConsulta_rdbPasaporte_CheckedChanged;
            pnlConsulta_rdbJuridico.CheckedChanged += pnlConsulta_rdbJuridico_CheckedChanged;
            pnlConsulta_rdbFisico.CheckedChanged += pnlConsulta_rdbFisico_CheckedChanged;
        }

        private async void pnlConsulta_rdbPasaporte_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            pnlConsulta_rdbDimex.CheckedChanged -= pnlConsulta_rdbDimex_CheckedChanged;
            pnlConsulta_rdbPasaporte.CheckedChanged -= pnlConsulta_rdbPasaporte_CheckedChanged;
            pnlConsulta_rdbJuridico.CheckedChanged -= pnlConsulta_rdbJuridico_CheckedChanged;
            pnlConsulta_rdbFisico.CheckedChanged -= pnlConsulta_rdbFisico_CheckedChanged;

            try
            {
                controlador.renderTipoDocumento(false, false, true, false);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            pnlConsulta_rdbDimex.CheckedChanged += pnlConsulta_rdbDimex_CheckedChanged;
            pnlConsulta_rdbPasaporte.CheckedChanged += pnlConsulta_rdbPasaporte_CheckedChanged;
            pnlConsulta_rdbJuridico.CheckedChanged += pnlConsulta_rdbJuridico_CheckedChanged;
            pnlConsulta_rdbFisico.CheckedChanged += pnlConsulta_rdbFisico_CheckedChanged;
        }

        private async void pnlConsulta_rdbJuridico_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            pnlConsulta_rdbDimex.CheckedChanged -= pnlConsulta_rdbDimex_CheckedChanged;
            pnlConsulta_rdbPasaporte.CheckedChanged -= pnlConsulta_rdbPasaporte_CheckedChanged;
            pnlConsulta_rdbJuridico.CheckedChanged -= pnlConsulta_rdbJuridico_CheckedChanged;
            pnlConsulta_rdbFisico.CheckedChanged -= pnlConsulta_rdbFisico_CheckedChanged;

            try
            {
                controlador.renderTipoDocumento(false, true, false, false);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            pnlConsulta_rdbDimex.CheckedChanged += pnlConsulta_rdbDimex_CheckedChanged;
            pnlConsulta_rdbPasaporte.CheckedChanged += pnlConsulta_rdbPasaporte_CheckedChanged;
            pnlConsulta_rdbJuridico.CheckedChanged += pnlConsulta_rdbJuridico_CheckedChanged;
            pnlConsulta_rdbFisico.CheckedChanged += pnlConsulta_rdbFisico_CheckedChanged;
        }

        private async void pnlConsulta_rdbFisico_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            pnlConsulta_rdbDimex.CheckedChanged -= pnlConsulta_rdbDimex_CheckedChanged;
            pnlConsulta_rdbPasaporte.CheckedChanged -= pnlConsulta_rdbPasaporte_CheckedChanged;
            pnlConsulta_rdbJuridico.CheckedChanged -= pnlConsulta_rdbJuridico_CheckedChanged;
            pnlConsulta_rdbFisico.CheckedChanged -= pnlConsulta_rdbFisico_CheckedChanged;

            try
            {
                controlador.renderTipoDocumento(true, false, false, false);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            pnlConsulta_rdbDimex.CheckedChanged += pnlConsulta_rdbDimex_CheckedChanged;
            pnlConsulta_rdbPasaporte.CheckedChanged += pnlConsulta_rdbPasaporte_CheckedChanged;
            pnlConsulta_rdbJuridico.CheckedChanged += pnlConsulta_rdbJuridico_CheckedChanged;
            pnlConsulta_rdbFisico.CheckedChanged += pnlConsulta_rdbFisico_CheckedChanged;
        }

        private async Task menu_mniConsultar_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlCliente_stkConsultar);

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.menu_mniConsultar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
            await simulateClickGestures.NoSelectedStack(pnlCliente_stkConsultar);
        }

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlCliente_stkCerrar);
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_mniClose_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlCliente_stkCerrar);
        }

        private async Task pnlInformacion_cbxProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInformacion_cbxProvincia_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlInformacion_cbxCanton_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInformacion_cbxCanton_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniGuardar_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlCliente_stkGuardar);

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.menu_mniGuardar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlCliente_stkGuardar);
        }

        #region Cambios en cliente 
        private async Task pnlInformacion_btnLimpiarNomApo_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInformacion_btnLimpiarNA_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInformacion_btnLimpiarCedApo_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInformacion_btnLimpiarCA_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInformacion_cbxProvinciaApo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInformacion_cbxProvinciaApo_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInformacion_cbxCantonApo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInformacion_cbxCantonApo_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInformacion_cbxTodos_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                controlador.SeleccionarDias();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        #endregion

        private void pnlInformacion_txtObservaciones_TextChanged(object sender, TextChangedEventArgs e)
        {
            ExtendedEditor entry = sender as ExtendedEditor;
            String val = entry.Text;

            if (val.Length > MaxLenght)
            {
                val = e.NewTextValue.Substring(0, MaxLenght);
                entry.Text = val;
            }
        }
    }
}