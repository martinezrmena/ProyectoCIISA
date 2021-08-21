using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.External.Behaviors
{
    class JustAlphanumeric : Behavior<Entry> 
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            string texto = entry.Text;

            //Validando que sea digito
            texto = CheckCharacter(texto);

            entry.Text = texto;
        }

        private string CheckCharacter(string texto)
        {
            foreach (char c in texto)
            {
                if ((c >= 65 && c <= 90)
                || (c >= 97 && c <= 122)
                || (c >= 48 && c<= 57)
                || c == 8
                || c == 'ñ'
                || c == 45
                || c == 'Ñ' || c == ' ')
                {

                }
                else
                {
                    texto = texto.Remove(texto.Length - 1);
                }
            }

            return texto;
        }
    }

}
