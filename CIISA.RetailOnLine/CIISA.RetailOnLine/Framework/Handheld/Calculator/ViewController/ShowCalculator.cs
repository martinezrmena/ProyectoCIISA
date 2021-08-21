using CIISA.RetailOnLine.Aplicacion.AuditOnLine.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.Controller;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Calculator.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Calculator.ViewController
{
    public class ShowCalculator
    {
        
        public void showCalculatorForm(SystemCIISA psystemCIISA, decimal pquantity, ctrlTomaFisica PTomaFisica)
        {
            Application.Current.MainPage.Navigation.PushAsync(new viewCalculator(psystemCIISA, pquantity, PTomaFisica));            
        }

        public void showCalculatorForm(SystemCIISA psystemCIISA, decimal pquantity, ctrlAuditoria pctrlAuditoria)
        {
            Application.Current.MainPage.Navigation.PushAsync(new viewCalculator(psystemCIISA, pquantity, pctrlAuditoria));
        }
    }
}
