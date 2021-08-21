using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.External.Behaviors
{
    class JustDecimal : Behavior<Entry>
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

            //Validando que sea digito decimal con unicamente dos decimales
            texto = CheckDecimal(texto);

            entry.Text = texto;
        }

        private string CheckDecimal(string texto)
        {
            string Valor = "";
            int Cantidad = 0;
            bool punto = false;

            try
            {
                if (texto.Substring(0, 1).Equals("."))
                {
                    texto = "";
                }
            }
            catch
            {

            }

            foreach (char c in texto)
            {
                if (Cantidad <= 1)
                {
                    if (char.IsDigit(c) || c.Equals('.'))
                    {
                        if (!Valor.Contains(".") && c.Equals('.'))
                        {
                            //AGREGAMOS EL PUNTO SI NO EXISTE
                            Valor += c;
                            punto = true;
                        }

                        if (char.IsDigit(c))
                        {
                            //SI ES DIGITO LO AGREGAMOS
                            Valor += c;
                            if (punto)
                            {
                                Cantidad++;
                            }
                        }

                    }
                }

            }

            return Valor;
        }
    }
}
