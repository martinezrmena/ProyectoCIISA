using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class vistaCarniceria : ContentPage
    {
        internal ctrlCarniceria controlador = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        /// <summary>
        ///Pantalla encargada de realizar las lecturas de codigos de barras
        ///En caso de que no se escaneen todas no se podrá guardar
        ///Posee alerta en caso de que la caja se entregue a otro cliente
        ///Permite digitar los códigos de barra en caso de que no sea posible efectuar la lectura del BarCode
        /// </summary>
        /// return Cajas Escaneadas con codigos
        public vistaCarniceria (Cliente cliente, List<pnlTransacciones_ltvProductos> productos)
		{
			InitializeComponent ();

            controlador = new ctrlCarniceria(this, cliente, productos);

            controlador.ScreenInicialization();
        }

        #region Menu Contextual

        private async void menu_mniPruebaImpresion_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.menu_mniPruebaImpresion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async void menu_mniBloquear_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.menu_mniBloquear_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async void menu_mniCoordenadas_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.menu_mniCoordenadas_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async void Cerrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.Cerrar();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        #endregion

        #region BottomBarMenu

        private async Task Revert_Tapped(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlCarniceria_stkRevert);
                await controlador.RevertListView();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlCarniceria_stkRevert);
        }

        private async void ScannBoxes_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await simulateClickGestures.SelectedStack(pnlCarniceria_stkScann);
                await controlador.ScanningBarCode();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlCarniceria_stkScann);
        }

        private async Task Guardar_Tapped(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlCarniceria_stkGuardar);
                //await controlador.menu_GuardarInicialization();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            await simulateClickGestures.NoSelectedStack(pnlCarniceria_stkGuardar);
        }

        private async Task btnEscribirCodigo_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.Escribir_Codigo_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        #endregion

        private async Task pnlCarniceria_ltvBoxes_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            try
            {
                controlador.pnlTransacciones_ltvProductos_Tapped(sender, e);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

    }
}