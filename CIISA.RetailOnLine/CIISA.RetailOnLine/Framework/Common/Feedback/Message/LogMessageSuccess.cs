using Acr.UserDialogs;
using CIISA.RetailOnLine.Framework.Common.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Message
{
    public static class LogMessageSuccess
    {
        private const string v_success = "Éxito: ";
        private const string v_aceptar = "Aceptar";

        public static async Task _generalSuccess(string pmessage)
        {
            string _string = string.Empty;

            _string += pmessage;
            _string += Simbol._point;

            await UserDialogs.Instance.AlertAsync(_string, v_success, v_aceptar);
        }

        public static async Task successfulTransaction(string pcodTransaction)
        {
            string _string = string.Empty;

            _string += "Transacción # ";
            _string += Environment.NewLine;
            _string += Environment.NewLine;
            _string += pcodTransaction;
            _string += Environment.NewLine;
            _string += Environment.NewLine;
            _string += " completada";
            _string += Simbol._point;

            await UserDialogs.Instance.AlertAsync(_string, v_success, v_aceptar);
        }

        public static async Task printed()
        {
            string _string = string.Empty;

            _string += "El documento se imprimió correctamente";
            _string += Simbol._point;

            await UserDialogs.Instance.AlertAsync(_string, v_success, v_aceptar);
        }

        public static async Task Reporteprinted(string documento)
        {
            string _string = string.Empty;

            _string += "El reporte de " + documento + " se imprimió correctamente";
            _string += Simbol._point;

            await UserDialogs.Instance.AlertAsync(_string, v_success, v_aceptar);
        }
    }
}
