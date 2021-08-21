using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria
{
    //La clase no se utilizo debido a especificaciones del cliente
    public class ctrlWriteBarCode
    {
        internal vistaWriteBarCode view = null;

        internal LogMessageAttention _logMessageAttention = null;

        internal ctrlCarniceria carniceria = null;

        internal ctrlWriteBarCode(vistaWriteBarCode p_view, ctrlCarniceria ctrl)
        {
            view = p_view;

            _logMessageAttention = new LogMessageAttention();

            carniceria = ctrl;
        }

        internal void ScreenInicialization()
        {
            renderPaneles(view.FindByName<StackLayout>("pnlWriteBarCode"));
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlWriteBarCode"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlWriteBarCode").Id))
            {
                view.FindByName<Label>("lblTitle").Text = "Escribir Código";
            }

            ppanel.IsVisible = true;
        }

        /// <summary>
        ///Metodo que valida el codigo de barras proporcionado
        /// </summary>
        /// return codigo de barras proporcionado de manera manual
        internal async void Aceptar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("txbCodigo")))
            {
                string code = view.FindByName<ExtendedEntry>("txbCodigo").Text;

                if (!code.Length.Equals(14))
                {
                    await _logMessageAttention.generalAttention("El código proporcionado no posee el formato correcto " +
                                                                "(XXXXXXXXXXXXXX) 14 digitos.");
                }
                else {

                    carniceria.Escribir_Codigo_Parte2(code);

                    await PopupNavigation.Instance.PopAsync(true);
                }
            }
        }
    }
}
