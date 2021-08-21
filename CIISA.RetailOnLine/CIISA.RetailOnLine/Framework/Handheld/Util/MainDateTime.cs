using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Handheld.Util
{
    public class MainDateTime
    {
        public async Task<Boolean> mainDateTimeHandheld(SystemCIISA psystemCIISA)
        {
            Boolean _value = false;

            if (await LogMessages._dialogResultYes("Fecha y Hora (Máquina): "
                + Environment.NewLine
                + Environment.NewLine
                + VarTime.getDateCR()
                + Simbol._hyphenWithSpaces
                + VarTime.getCompleteDateTime()
                + Environment.NewLine
                + Environment.NewLine
                + "¿Es correcta, continuar? ",
                "Verificar Fecha Máquina"))
            {
                _value = true;
            }
            else
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append("1. Salga del sistema.");
                _sb.Append(Environment.NewLine);
                _sb.Append("2. Vaya al escritorio de máquina.");
                _sb.Append(Environment.NewLine);
                _sb.Append("3. Clic en la fecha.");
                _sb.Append(Environment.NewLine);
                _sb.Append("4. Ingrese la fecha y hora correcta.");
                _sb.Append(Environment.NewLine);
                _sb.Append("5. Clic en Ok.");

                LogMessageAttention _lma = new LogMessageAttention();

                await _lma.generalAttention(_sb.ToString());
            }

            return _value;
        }
    }
}
