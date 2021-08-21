using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Common.Feedback
{
    public static class HelpFB
    {
        public static async Task closeClientWindow()
        {
            string _string = string.Empty;

            _string += "Si no puede cerrar de esta pantalla, realice: ";
            _string += Environment.NewLine;
            _string += Environment.NewLine;
            _string += "1. Busque un cliente.";
            _string += Environment.NewLine;
            _string += "2. Seleccione un cliente.";
            _string += Environment.NewLine;
            _string += "3. Dé doble clic sobre el cliente.";

            LogMessageAttention _logMessageAttention = new LogMessageAttention();

            await _logMessageAttention.generalAttention(
                _string
                );
        }

        public static async Task closeProductWindow()
        {
            string _string = string.Empty;

            _string += "Si no puede cerrar de esta pantalla, realice: ";
            _string += Environment.NewLine;
            _string += Environment.NewLine;
            _string += "1. Agregue 0 (cero) en la cantidad del producto.";
            _string += Environment.NewLine;
            _string += "2. Presione el botón agregar.";

            LogMessageAttention _logMessageAttention = new LogMessageAttention();

            await _logMessageAttention.generalAttention(
                _string
                );
        }
    }
}
