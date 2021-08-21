using CIISA.RetailOnLine.Aplicacion.AuditOnLine.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.Controller;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Calculator.View;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Framework.Handheld.Calculator.Controller
{
    public class ctrlCalculator
    {
        private viewCalculator view { get; set; }

        private string v_value;
        internal decimal v_result;
        private List<string> v_temporalList;
        public ctrlTomaFisica v_TomaFisica = null;
        public ctrlAuditoria v_ctrlAuditoria = null;
        private decimal v_initialAmount;
        public bool Cerrar = false;

        public ctrlCalculator(viewCalculator pview)
        {
            view = pview;
        }

        internal void screenInicialization(decimal pinitialAmount, ctrlTomaFisica pTomaFisica)
        {
            v_TomaFisica = pTomaFisica;
            RenderWindows.paintWindow(view);
            renderPanels(view.FindByName<StackLayout>("pnlCalculator"));
            v_value = string.Empty;
            v_result = Numeric._zeroDecimalInitialize;
            v_temporalList = new List<string>();
            v_initialAmount = pinitialAmount;
            view.FindByName<Button>("pnlCalculator_btnInitialValue").Text = "Valor Inicial [" + pinitialAmount + "]";
            SetEnableOperatorBttns(false);
        }

        internal void screenInicialization(decimal pinitialAmount, ctrlAuditoria pctrlAuditoria)
        {
            v_ctrlAuditoria = pctrlAuditoria;
            RenderWindows.paintWindow(view);
            renderPanels(view.FindByName<StackLayout>("pnlCalculator"));
            v_value = string.Empty;
            v_result = Numeric._zeroDecimalInitialize;
            v_temporalList = new List<string>();
            v_initialAmount = pinitialAmount;
            view.FindByName<Button>("pnlCalculator_btnInitialValue").Text = "Valor Inicial [" + pinitialAmount + "]";
            SetEnableOperatorBttns(false);
        }

        private void renderPanels(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlCalculator"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlCalculator").Id))
            {
                view.Title = "Calculadora";
            }

            ppanel.IsVisible = true;
        }

        private void SetEnableOperatorBttns(bool enable)
        {
            view.FindByName<Button>("pnlCalculator_btnSum").IsEnabled = enable;
            view.FindByName<Button>("pnlCalculator_btnSubtraction").IsEnabled = enable;
            view.FindByName<Button>("pnlCalculator_btnMultiplication").IsEnabled = enable;
            view.FindByName<Button>("pnlCalculator_btnDivision").IsEnabled = enable;
            view.FindByName<Button>("pnlCalculator_btnResult").IsEnabled = enable;
        }

        public void addOperatorToList(Button pbtnOperador)
        {
            v_temporalList.Add(v_value);
            view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text += pbtnOperador.Text;
            v_value = string.Empty;
            v_temporalList.Add(pbtnOperador.Text);
            view.FindByName<Button>("pnlCalculator_btnPoint").IsEnabled = true;
            SetEnableOperatorBttns(false);
            view.FindByName<ToolbarItem>("menu_mniAccept").IsEnabled = false;
        }

        internal void addToList(Button pboton)
        {
            v_value += pboton.Text;
            view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text += pboton.Text;
            SetEnableOperatorBttns(true);
        }

        private void reset()
        {
            v_value = string.Empty;
            view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text = string.Empty;
            v_temporalList.Clear();
            view.FindByName<Button>("pnlCalculator_btnPoint").IsEnabled = true;
        }

        internal void pnlCalculator_btnReduceFont_Click()
        {
            double _actualSize = view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").FontSize;
            view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").FontSize = _actualSize - 1;
        }

        internal void pnlCalculator_btnIncreaseFont_Click()
        {
            double _actualSize = view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").FontSize;
            view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").FontSize = _actualSize + 1;
        }

        internal void pnlCalculator_btnResult_Click()
        {
            v_temporalList.Add(v_value);
            v_temporalList.Add(view.FindByName<Button>("pnlCalculator_btnResult").Text);

            float m_res = float.Parse(v_temporalList[GenericIndexListView._zero].ToString());

            for (int i = Numeric._zeroInteger; i < v_temporalList.Count; i++)
            {
                if (v_temporalList[i].ToString() == VarOperators._sum)
                {
                    view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text = string.Empty;
                    m_res += float.Parse(v_temporalList[i + 1].ToString());
                    view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text = m_res.ToString();
                }
                else if (v_temporalList[i].ToString() == VarOperators._subtraction)
                {
                    view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text = string.Empty;
                    m_res -= float.Parse(v_temporalList[i + 1].ToString());
                    view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text = m_res.ToString();
                }
                else if (v_temporalList[i].ToString() == VarOperators._multiplication)
                {
                    view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text = string.Empty;
                    m_res *= float.Parse(v_temporalList[i + 1].ToString());
                    view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text = m_res.ToString();
                }
                else if (v_temporalList[i].ToString() == VarOperators._division)
                {
                    view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text = string.Empty;
                    m_res /= float.Parse(v_temporalList[i + 1].ToString());
                    view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text = m_res.ToString();
                }
            }

            v_temporalList.Clear();
            v_value = view.FindByName<Entry>("pnlCalculator_txtResult").Text;

            for (int i = Numeric._zeroInteger; i < v_value.Length; i++)
            {
                if (v_value[i].ToString() == Simbol._point)
                {

                    view.FindByName<Button>("pnlCalculator_btnPoint").IsEnabled = false;
                    break;
                }
                else
                {
                    view.FindByName<Button>("pnlCalculator_btnPoint").IsEnabled = true;
                }
            }


            view.FindByName<Button>("pnlCalculator_btnResult").IsEnabled = false;
            view.FindByName<ToolbarItem>("menu_mniAccept").IsEnabled = true;
        }

        internal void pnlCalculator_btnDelete_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlCalculator_txtResult"));
        }

        internal void pnlCalculator_btnInitialValue_Click()
        {
            v_result = Numeric._zeroDecimalInitialize;
            v_temporalList.Clear();
            view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text = string.Empty;

            Button _boton = new Button();
            _boton.Text = v_initialAmount + string.Empty;

            addToList(_boton);
        }

        internal void menu_mniAccept_Click()
        {
            v_result = FormatUtil.convertStringToDecimal(view.FindByName<ExtendedEntry>("pnlCalculator_txtResult").Text);
            Cerrar = true;
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void pnlCalculator_btnPoint_Click()
        {
            addToList(view.FindByName<Button>("pnlCalculator_btnPoint"));
            view.FindByName<Button>("pnlCalculator_btnPoint").IsEnabled = false;
        }

        internal void menu_mniClose_Click()
        {
            v_result = v_initialAmount;
            Cerrar = true;
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal async Task Cerrando() {
            if (Cerrar)
            {
                if (v_TomaFisica != null)
                {
                    await v_TomaFisica.pnlTomaFisica_btnCalculadora_ClickParte2(v_result);
                }
                if (v_ctrlAuditoria != null)
                {
                    v_ctrlAuditoria.pnlInventario_btnCalculadora_ClickParte2(v_result);
                }
            }

            
        }

    }
}
