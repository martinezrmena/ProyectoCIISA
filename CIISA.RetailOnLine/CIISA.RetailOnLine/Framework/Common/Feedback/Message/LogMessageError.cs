using Acr.UserDialogs;
using CIISA.RetailOnLine.Framework.Common.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Message
{
    public static class LogMessageError
    {
        private const string v_error = "Error: ";
        private const string v_aceptar = "Aceptar";

        public static async Task generalError(string pmessage)
        {
            string _string = string.Empty;

            _string += pmessage;
            _string += Simbol._point;

            await UserDialogs.Instance.AlertAsync(_string, v_error, v_aceptar);
        }

        public static async Task incorrectCode()
        {
            await UserDialogs.Instance.AlertAsync("Código incorrecto.", v_error, v_aceptar);
        }
    }
}
