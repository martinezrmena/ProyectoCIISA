using Acr.UserDialogs;
using CIISA.RetailOnLine.Framework.Common.Character;
using System;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Message
{
    public class LogMessageAttention
    {
        private const string v_attention = "Atención: ";
        private const string v_error = "Error: ";
        private const string v_aceptar = "Aceptar";
        private static string v_cancelar = "Cancelar";
        public const string _withoutAuthorizedToSignText = "Sin Autorizados para Firmar registrados";

        public async Task generalAttention(string pmessage)
        {

            string _string = string.Empty;

            _string += pmessage;
            _string += Simbol._point;

            await UserDialogs.Instance.AlertAsync(pmessage, v_attention, v_aceptar);
        }

        public async Task withoutAuthorizedToSign()
        {
            await UserDialogs.Instance.AlertAsync("Sin Autorizados para Firmar registrados", v_attention, v_aceptar);
        }

        public async Task<bool> placePrinterQuestion()
        {
            string _string = string.Empty;

            _string += "Posicione la impresora. ¿Continuar?";
            _string += Environment.NewLine;
            _string += Environment.NewLine;
            _string += "* Mantenga la impresora en posición hasta que termine la impresión.";

            return await UserDialogs.Instance.ConfirmAsync(_string, v_attention, v_aceptar, v_cancelar, null);

            //return MessageBox.Show(
            //    _string,
            //    v_attention,
            //    MessageBoxButtons.RetryCancel,
            //    MessageBoxIcon.Question,
            //    MessageBoxDefaultButton.Button1) == DialogResult.Cancel;
        }

        public async Task printerConnectionError(int intentos)
        {

            string _string = string.Empty;

            _string += "Revise si la impresora ";
            _string += "(intento" + intentos + "/3)";
            _string += Environment.NewLine;
            _string += Simbol._bulletPoint + " Está apagada";
            _string += Simbol._point;
            _string += Environment.NewLine;
            _string += Simbol._bulletPoint + " Tiene papel";
            _string += Simbol._point;
            _string += Environment.NewLine;
            _string += Simbol._bulletPoint + " Tiene carga (luz amarilla)";
            _string += Simbol._point;
            _string += Environment.NewLine;
            _string += Simbol._bulletPoint + " Está conectada (luz azul)";
            _string += Simbol._point;
            _string += Environment.NewLine;
            _string += "* En todo caso apague y encienda la impresora.";

            await UserDialogs.Instance.AlertAsync(_string, v_error, v_aceptar);
        }

    }
}
