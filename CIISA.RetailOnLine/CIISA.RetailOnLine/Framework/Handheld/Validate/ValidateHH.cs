using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Framework.Handheld.Validate
{
    public class ValidateHH
    {
        public bool emptyTextBox(ExtendedEntry ptextBox)
        {
            bool _empty = false;

            if (ptextBox.Text != null)
            {

                _empty = ptextBox.Text.Equals(string.Empty) || string.IsNullOrWhiteSpace(ptextBox.Text);

            }
            else {

                _empty = true;
            }

            if (_empty)
            {

                RenderPaint.paintRedBackgroundTextBox(ptextBox);

            }
            else
            {
                RenderPaint.paintWhiteBackgroundTextBox(ptextBox);
            }

            return _empty;
        }

        public bool emptyPickerBox(Picker ptextBox)
        {
            bool _empty = false;

            if (ptextBox.SelectedItem != null)
            {

                _empty = ptextBox.SelectedItem.ToString().Equals(string.Empty);

            }
            else
            {

                _empty = true;
            }

            if (_empty)
            {

                RenderPaint.paintRedBackgroundPicker(ptextBox);

            }
            else
            {
                RenderPaint.paintWhiteBackgroundPicker(ptextBox);
            }

            return _empty;
        }

        public bool emptyEditorBox(Editor ptextBox)
        {
            bool _empty = false;

            if (ptextBox.Text != null)
            {

                _empty = ptextBox.Text.Equals(string.Empty);

            }
            else
            {

                _empty = true;
            }

            if (_empty)
            {

                RenderPaint.paintRedBackgroundEditor(ptextBox);

            }
            else
            {
                RenderPaint.paintWhiteBackgroundEditor(ptextBox);
            }

            return _empty;
        }

        public bool emptyDatePicker(DatePicker pdate)
        {
            bool _empty = false;

            if (pdate.Date != null)
            {

                _empty = pdate.Date.ToString().Equals(string.Empty);

            }
            else
            {

                _empty = true;
            }

            if (_empty)
            {

                RenderPaint.paintRedBackgroundDatePicker(pdate);

            }
            else
            {
                RenderPaint.paintWhiteBackgroundDatePicker(pdate);
            }

            return _empty;
        }

        public void backSpace(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;
            int Length = text.Length;

            if (Length > 0)
            {
                text = text.Remove(Length - 1);
            }

            ptextBox.Text = text;
        }

        public void guion(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;            

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._hyphen;
            }

            ptextBox.Text = text;
        }

        public void punto(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._point;
            }

            ptextBox.Text = text;
        }

        public void zero(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._zero;
            }

            ptextBox.Text = text;
        }

        public void one(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._one;
            }

            ptextBox.Text = text;
        }

        public void two(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._two;
            }

            ptextBox.Text = text;
        }

        public void three(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._three;
            }

            ptextBox.Text = text;
        }

        public void four(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._four;
            }

            ptextBox.Text = text;
        }

        public void five(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._five;
            }

            ptextBox.Text = text;
        }

        public void six(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._six;
            }

            ptextBox.Text = text;
        }

        public void seven(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._seven;
            }

            ptextBox.Text = text;
        }

        public void eight(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._eight;
            }

            ptextBox.Text = text;
        }

        public void nine(ExtendedEntry ptextBox)
        {
            string text = ptextBox.Text;

            int Length = text.Length;

            if (Length < ptextBox.MaxLength)
            {
                text = text + BottonNumbers._nine;
            }

            ptextBox.Text = text;
        }

        public bool emptyListView<T>(ListView plistView)
        {
            bool _empty = false;

            var Source = plistView.ItemsSource as ObservableCollection<T>;

            if (Source == null)
            {
                var Source2 = plistView.ItemsSource as SelectableObservableCollection<T>;

                if (Source2 == null)
                {
                    Source2 = new SelectableObservableCollection<T>();
                    _empty = true;
                    RenderPaint.paintRedBackgroundListView(plistView);
                }
                else
                {
                    if (Source2.Count > 0)
                    {
                        RenderPaint.paintWhiteBackgroundListView(plistView);
                    }
                    else
                    {
                        _empty = true;
                        RenderPaint.paintRedBackgroundListView(plistView);
                    }
                }
            }
            else
            {
                if (Source.Count > 0)
                {
                    RenderPaint.paintWhiteBackgroundListView(plistView);
                }
                else
                {
                    _empty = true;
                    RenderPaint.paintRedBackgroundListView(plistView);
                }
            }
            
            return _empty;
        }

        public async Task<bool> listViewItemSelected(ListView plistView)
        {
            bool _selected = false;

            var Objeto = plistView.SelectedItem;

            if(Objeto==null)
            {
                RenderPaint.paintRedBackgroundListView(plistView);

                LogMessageAttention _logMessageAttention = new LogMessageAttention();

                await _logMessageAttention.generalAttention("Debe seleccionar un ítem de la lista");
            }
            else
            {
                _selected = true;

                RenderPaint.paintWhiteBackgroundListView(plistView);
            }

            return _selected;
        }

        public bool JustValidatelistViewItemSelected(ListView plistView)
        {
            bool _selected = false;

            var Objeto = plistView.SelectedItem;

            if (Objeto == null)
            {
                
            }
            else
            {
                _selected = true;
            }

            return _selected;
        }

        public bool listViewChecked<T>(ListView plistView)
        {
            bool _checkedItems = false;

            var Source = plistView.ItemsSource as SelectableObservableCollection<T>;

            foreach (var _lvi in Source)
            {
                if (_lvi.IsSelected)
                {
                    _checkedItems = true;
                }
            }

            return _checkedItems;
        }

        public bool amountGreaterThanZero(ExtendedEntry ptextBox)
        {
            bool _quantity = false;

            decimal _tempQuantity = FormatUtil.convertStringToDecimal(ptextBox.Text);

            if (_tempQuantity > 0)
            {
                _quantity = true;

                RenderPaint.paintWhiteBackgroundTextBox(ptextBox);
            }
            else
            {
                RenderPaint.paintRedBackgroundTextBox(ptextBox);

                ptextBox.Text = string.Empty;
            }

            return _quantity;
        }

        public async Task<string> validarSoloNumerosREGEX(string pstringText)
        {
            string _singleDigit = string.Empty;
            string _newString = string.Empty;
            Regex _regex = new Regex("[0-9]");

            for (int i = 0; i < pstringText.Length; i++)
            {
                _singleDigit = string.Empty;
                _singleDigit += pstringText[i];

                if (_regex.IsMatch(_singleDigit.ToString()))
                {
                    _newString += _singleDigit;
                }
            }

            if (_newString.Length > 8)
            {
                string _string = string.Empty;

                _string += "El número telefónico excede los 8 dígitos";
                _string += Environment.NewLine;
                _string += Environment.NewLine;
                _string += "* Verifique con el Departamento de Liquidaciones";

                LogMessageAttention _logMessageAttention = new LogMessageAttention();

                await _logMessageAttention.generalAttention(_string);
            }

            return _newString.ToString();
        }

        public void paintNumericTextBox_onlyNumbers(ExtendedEntry ptextBox,Char pdigit,bool pkeyboard)
        {
            //KeyPressEventArgs _e = new KeyPressEventArgs(pdigit);

            //ValidateHH _validateHH = new ValidateHH();

            //if (ptextBox.MaxLength > ptextBox.Text.Length)
            //{
            //    _validateHH.onlyNumbers(_e, ptextBox, pkeyboard);
            //}
            //else
            //{
            //    _validateHH.onlyDeleted(_e, ptextBox, pkeyboard);
            //}
        }

        public async Task<bool> quantityFewerThanRange(ExtendedEntry ptextBox,decimal pquantityAvailable,string ptransactionTypeName)
        {
            bool _quantity = false;

            decimal _requestAmount = FormatUtil.convertStringToDecimal(ptextBox.Text);

            if (_requestAmount <= (pquantityAvailable))
            {
                _quantity = true;
            }
            else
            {
                if (pquantityAvailable > 0)
                {
                    if (!ptransactionTypeName.Equals(ROLTransactions._ordenVentaNombre))
                    {
                        if (!ptransactionTypeName.Equals(ROLTransactions._cotizacionNombre))
                        {
                            if (!ptransactionTypeName.Equals(ROLTransactions._devolucionNombre))
                            {
                                if (_requestAmount <= pquantityAvailable)
                                {
                                    ptextBox.Text = FormatUtil.applyCurrencyFormat(_requestAmount);
                                }
                                else
                                {
                                    ptextBox.Text = FormatUtil.applyCurrencyFormat(pquantityAvailable);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ptransactionTypeName.Equals(ROLTransactions._facturaContadoNombre)
                        || ptransactionTypeName.Equals(ROLTransactions._facturaCreditoNombre)
                        || ptransactionTypeName.Equals(ROLTransactions._regaliaNombre))
                    {
                        LogMessageAttention _logMessageAttention = new LogMessageAttention();

                        await _logMessageAttention.generalAttention("Sin existencias para el producto");

                        ptextBox.Text = FormatUtil.applyCurrencyFormat(pquantityAvailable);
                    }
                }
            }

            return _quantity;
        }

        public bool ValidateTextBoxLength(ExtendedEntry ptextBox, int quantity)
        {
            bool validate = false;

            if (ptextBox.Text != null)
            {
                if (ptextBox.Text.Length == quantity)
                {
                    validate = true;

                    RenderPaint.paintWhiteBackgroundTextBox(ptextBox);

                } else {

                    RenderPaint.paintRedBackgroundTextBox(ptextBox);
                }

            }

            return validate;
        }

        public bool validarLength(ExtendedEntry ptextBox) {

            return ValidateTextBoxLength(ptextBox, ptextBox.MaxLength);

        }

    }
}
