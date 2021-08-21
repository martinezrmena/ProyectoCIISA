using CIISA.RetailOnLine.Aplicacion.AuditOnLine.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.Controller;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Calculator.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Framework.Handheld.Calculator.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class viewCalculator : ContentPage
    {
        private ctrlCalculator controller = null;

        public viewCalculator(SystemCIISA psystemCIISA, decimal pinitialAmount, ctrlTomaFisica pTomaFisica)
        {
            try
            {
                InitializeComponent();

                controller = new ctrlCalculator(this);

                controller.screenInicialization(pinitialAmount, pTomaFisica);
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

        public viewCalculator(SystemCIISA psystemCIISA, decimal pinitialAmount, ctrlAuditoria pctrlAuditoria)
        {
            try
            {
                InitializeComponent();

                controller = new ctrlCalculator(this);

                controller.screenInicialization(pinitialAmount, pctrlAuditoria);
            }
            catch (Exception ex)
            {
                ExceptionHandled(ex);
            }
        }

        internal async Task<decimal> getResult()
        {
            decimal _result = 0M;

            try
            {
                _result = controller.v_result;
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            return _result;
        }


        private async Task pnlCalculator_btnOne_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addToList(pnlCalculator_btnOne);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnTwo_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addToList(pnlCalculator_btnTwo);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnThree_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addToList(pnlCalculator_btnThree);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnFour_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addToList(pnlCalculator_btnFour);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnFive_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addToList(pnlCalculator_btnFive);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnSix_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addToList(pnlCalculator_btnSix);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnSeven_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addToList(pnlCalculator_btnSeven);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnEight_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addToList(pnlCalculator_btnEight);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnNine_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addToList(pnlCalculator_btnNine);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnZero_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addToList(pnlCalculator_btnZero);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
        
        private async Task pnlCalculator_btnPoint_Click(object sender, EventArgs e)
        {
            try
            {
                controller.pnlCalculator_btnPoint_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnSum_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addOperatorToList(pnlCalculator_btnSum);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnSubtraction_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addOperatorToList(pnlCalculator_btnSubtraction);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnMultiplication_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addOperatorToList(pnlCalculator_btnMultiplication);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnDivision_Click(object sender, EventArgs e)
        {
            try
            {
                controller.addOperatorToList(pnlCalculator_btnDivision);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnResult_Click(object sender, EventArgs e)
        {
            try
            {
                controller.pnlCalculator_btnResult_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniClose_Click(object sender, EventArgs e)
        {
            try
            {
                controller.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniAccept_Click(object sender, EventArgs e)
        {
            try
            {
                controller.menu_mniAccept_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnInitialValue_Click(object sender, EventArgs e)
        {
            try
            {
                controller.pnlCalculator_btnInitialValue_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
        
        private async Task pnlCalculator_btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                controller.pnlCalculator_btnDelete_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnIncreaseFont_Click(object sender, EventArgs e)
        {
            try
            {
                controller.pnlCalculator_btnIncreaseFont_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCalculator_btnReduceFont_Click(object sender, EventArgs e)
        {
            try
            {
                controller.pnlCalculator_btnReduceFont_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                await controller.Cerrando();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}