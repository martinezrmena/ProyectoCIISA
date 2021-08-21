using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.External.CustomTreeView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaDescarga : ContentPage
    {
        private ctrlDescarga controlador = null;
        internal string v_tipoDescarga = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        internal vistaDescarga(string ppantallaInvoca, string ptipoDescarga, bool pesUsuarioLiquidador)
        {
            try
            {
                v_tipoDescarga = ptipoDescarga;

                controlador = new ctrlDescarga(this, pesUsuarioLiquidador);

                InitializeComponent();

                controlador.ScreenInicialization(ppantallaInvoca);
            }
            catch (Exception ex)
            {
                ExceptionHandled(ex);
            }

        }

        private async void ExceptionHandled(Exception ex)
        {
            await ExceptionManager.ExceptionHandling(ex);
        }

        private async void menu_mniClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlTransacciones_stkCerrar);
                controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlTransacciones_stkCerrar);
        }

        #region Botones
        private async Task pnlTransacciones_btnEnviar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlTransacciones_btnEnviar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTransacciones_btnAbortar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlTransacciones_btnAbortar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTransacciones_btnRefrescar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTransacciones_btnRefrescar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlTransacciones_btnPruebaConexion_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlTransacciones_btnPruebaConexion);
            var DPB = DependencyService.Get<ITaskActivity>();
            DPB.PruebaConexion();
            await simulateClickGestures.NoSelectedStack(pnlTransacciones_btnPruebaConexion);
        }

        private async Task pnlTransacciones_btnMarcarNoEnviados_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.menu_mniMenu_mniMarcarComoNoEnviados_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private void NewListView(Color color) {
            var personDataTemplate = new DataTemplate(() =>
            {
                return new CollapsableViewCell { CustomTextColor = color };
            });

            //pnlTransacciones_trvTransacciones = new ListView{ItemsSource = new ObservableCollection<CollapsableItem>(), ItemTemplate = personDataTemplate, CollapsableListView.IsCollapsable ="True" };
        }
        #endregion

        private async Task pnlTransacciones_chkExpandirLineas_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                controlador.pnlTransacciones_chkExpandirLineas_CheckStateChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}