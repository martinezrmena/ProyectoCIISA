using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.External.Behaviors
{
    public class JustNumeric : Behavior<Entry>
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
            texto = CheckDigit(texto);

            entry.Text = texto;
        }

        private string CheckDigit(string texto)
        {
            foreach (char c in texto)
            {
                if (!char.IsDigit(c))
                {
                    texto = texto.Remove(texto.Length - 1);
                }
            }

            return texto;
        }
    }
}
