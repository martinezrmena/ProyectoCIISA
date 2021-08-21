using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Message
{
    public static class LogMessageException
    {
        private const string v_exception = "Excepción: ";
        private const string v_aceptar = "Aceptar";

        public static async Task logException(SystemCIISA psystemCIISA,string pmessage,string pclass,string pmethod,Exception pexception)
        {
            if (pmessage.Equals(string.Empty))
            {
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(pmessage);
                sb.AppendLine("DETALLES:");
                sb.AppendLine("Mensaje: " + pexception.Message);
                sb.AppendLine("Source: " + pexception.Source);
                sb.AppendLine("stack: " + pexception.StackTrace);

                string Mensaje = sb.ToString();

                pmessage = pmessage + " -> " + pexception.Message;
                //await UserDialogs.Instance.AlertAsync(pmessage, v_exception, v_aceptar);
                await UserDialogs.Instance.AlertAsync(Mensaje, v_exception, v_aceptar);
            }

            Log _log = new Log();

            if (!pmessage.Equals(string.Empty))
            {
                _log.setDetailError(pmessage);
            }

            _log.setDetailException(pexception.ToString());

            _log.setDetailStackTrace(pexception.StackTrace);

            _log.generateFileTXTException(psystemCIISA,pclass,pmethod);
        }
    }
}
