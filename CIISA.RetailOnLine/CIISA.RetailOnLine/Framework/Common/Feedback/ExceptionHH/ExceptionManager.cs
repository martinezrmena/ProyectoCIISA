using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH
{
    public static class ExceptionManager
    {
        public static async Task ExceptionHandling(Exception pex)
        {
            bool _writeLog = true;

            if (pex.GetType().Name.Contains("WebException"))
            {
                _writeLog = false;

                LogMessageAttention _lma = new LogMessageAttention();
                await _lma.generalAttention(LogMessages._withOutConnectionArreo);
            }

            if (pex.GetType().Name.Contains("SocketException"))
            {
                _writeLog = false;

                LogMessageAttention _lma = new LogMessageAttention();
                await _lma.generalAttention(LogMessages._withOutConnectionArreo);
            }

            if (pex.GetType().Name.Contains("InvalidOperationException"))
            {
                _writeLog = false;

                LogMessageAttention _lma = new LogMessageAttention();
                await _lma.generalAttention(LogMessages._withOutConnectionArreo);
            }

            if (pex.GetType().Name.Contains("Threading"))
            {
                _writeLog = false;

                LogMessageAttention _lma = new LogMessageAttention();
                await _lma.generalAttention(LogMessages._withOutConnectionArreo);
            }

            if (pex.GetType().Name.Contains("ZebraPrinterConnectionException"))
            {
                LogMessageAttention _lma = new LogMessageAttention();
                await _lma.generalAttention(LogMessages._withOutConnectionPrinter);
            }

            if (pex.GetType().Name.Contains("MissingMethodException"))
            {
                LogMessageAttention _lma = new LogMessageAttention();
                await _lma.generalAttention("Falta una librería (externa) del sistema");

                if (pex.Message.ToString().Contains("System.Data.SqlServerCe"))
                {
                    await _lma.generalAttention("Instale el paquete: 4.sqlce.wce5.armv4i.CAB");
                }
            }

            if (pex.GetType().Name.Contains("SoapException"))
            {
                LogMessageAttention _lma = new LogMessageAttention();
                await _lma.generalAttention("Falta una librería (interna) del sistema");
            }

            if (_writeLog)
            {
                string tipo = pex.GetType().ToString();
                tipo = tipo.Replace(".", string.Empty);

                await LogMessageException.logException(
                    Sistema.establecerObjetoSystemCIISA(),
                    LogMessages._operationNoCompleted,
                    "ExcepcionesVarias",
                    tipo,
                    pex
                    );
            }

        }
    }
}
