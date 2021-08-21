using Acr.UserDialogs;
using System;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Message
{
    public class LogMessages
    {
        private static string v_aceptar = "Aceptar";
        private static string v_cancelar = "Cancelar";

        public static string _withOutConnectionArreo = "Sin conexión al Arreo.";
        public static string _withOutConnectionPrinter = "Sin conexión a la impresora.";
        public static string _operationNoCompleted = "No se pudo completar la operación. Comuníquese con soporte técnico.";

        public static async Task<bool> _dialogResultYes(string pmessage, string pcaption)
        {
            return await UserDialogs.Instance.ConfirmAsync(pmessage, pcaption, v_aceptar, v_cancelar, null);
        }

        public static string withConnection()
        {
            return "Usted conectó a la red de CRCIISA";
        }

        public static string withoutConnection()
        {
            string _string = string.Empty;

            _string += "La máquina no posee conexión con el Arreo.";
            _string += Environment.NewLine;
            _string += Environment.NewLine;
            _string += "Verifique: ";
            _string += Environment.NewLine;
            _string += Environment.NewLine;
            _string += "1. Que el teléfono este activo.";
            _string += Environment.NewLine;
            _string += "2. La intesidad de la señal.";
            _string += Environment.NewLine;
            _string += "3. El estado de la conexión.";

            return _string;
        }
    }
}
