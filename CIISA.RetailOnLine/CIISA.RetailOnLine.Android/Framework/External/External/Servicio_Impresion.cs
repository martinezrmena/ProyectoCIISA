using Acr.UserDialogs;
using CIISA.RetailOnLine.Droid.Framework.External.External;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using LinkOS.Plugin;
using LinkOS.Plugin.Abstractions;
using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Servicio_Impresion))]
namespace CIISA.RetailOnLine.Droid.Framework.External.External
{
    public class Servicio_Impresion : IServicio_Impresion
    {
        private IConnection connection = null;

        private async Task<bool> SetPrintLanguage(IConnection connection)
        {
            //await UserDialogs.Instance.AlertAsync("Estableciendo el lenguaje de la impresora", "Info", "Ok");
            string setLanguage = "! U1 setvar \"device.languages\" \"zpl\"\r\n\r\n! U1 getvar \"device.languages\"\r\n\r\n";
            byte[] response = connection.SendAndWaitForResponse(Encoding.ASCII.GetBytes(setLanguage), 500, 500);
            string s = Encoding.ASCII.GetString(response);
            if (!s.Contains("zpl"))
            {
                //Log.Debug(tag, "Not a ZPL printer.");
                await UserDialogs.Instance.AlertAsync("No acepta lenguaje ZPL", "Información", "Aceptar");
                return false;
            }
            return true;
        }

        private async Task<bool> SetPrintCaret(IConnection connection)
        {
            //await UserDialogs.Instance.AlertAsync("Estableciendo el lenguaje de la impresora", "Info", "Ok");
            string setLanguage = "! U1 setvar \"zpl.caret\" \"^\"\r\n\r\n! U1 getvar \"zpl.caret\"\r\n\r\n";
            byte[] response = connection.SendAndWaitForResponse(Encoding.ASCII.GetBytes(setLanguage), 500, 500);
            string s = Encoding.ASCII.GetString(response);
            if (!s.Contains("^"))
            {
                //Log.Debug(tag, "Not a ZPL printer.");
                await UserDialogs.Instance.AlertAsync("No acepta el caracter ^ como identificador de comandos.", "Información", "Aceptar");
                return false;
            }
            return true;
        }

        private async Task<bool> CheckPrinterStatus(IConnection connection)
        {
            //await UserDialogs.Instance.AlertAsync("Revisando estado de la impresora", "Info", "Ok");
            IZebraPrinter printer = ZebraPrinterFactory.Current.GetInstance(PrinterLanguage.ZPL, connection);
            IPrinterStatus status = printer.CurrentStatus;
            if (!status.IsReadyToPrint)
            {
                //Log.Debug(tag, "Printer in Error: " + status.ToString());
                await UserDialogs.Instance.AlertAsync("Error de imprsion por estado: " + status.ToString(), "Información", "Aceptar");

            }
            return true;
        }

        public async Task Print(string address,string texto)
        {
            //string zpl2 = "^XA^LL100^FO0,0^AA,18,10^FDHello World^FS^XZ";
            string zpl = "^XA^LL1000^FO0,0^FB600,60,,^FD" + texto + "^FS^XZ";
            //string zpl = "^XA^CF0,15,15^LL500^FO0,0^FB500,10,,^FD" + texto + "^FS^XZ";

            await UserDialogs.Instance.AlertAsync("El proceso de impresión comenzará."+Environment.NewLine+"La conexión se hara con la string: "+address+", se imprimira el siguiente texto: " + texto +".", "Proceso de Impresion", "OK");

            try
            {
                if ((connection == null) || (!connection.IsConnected))
                {
                    //await UserDialogs.Instance.AlertAsync("Estableciendo Coneccion.", "Info", "OK");
                    connection = ConnectionBuilder.Current.Build(address);
                    connection.Open();
                    //await UserDialogs.Instance.AlertAsync("Coneccion abierta", "Info", "OK");
                }
                if ((await SetPrintLanguage(connection)) && (await CheckPrinterStatus(connection)))
                {
                    //await UserDialogs.Instance.AlertAsync("Empiezara a imprimir.", "Info", "OK");
                    connection.Write(Encoding.ASCII.GetBytes(zpl));
                    await UserDialogs.Instance.AlertAsync("Deberia haber terminado de imprimir", "Info", "OK");
                }
            }
            catch (Exception e)
            {
                //if the device is unable to connect, an exception is thrown  
                //Log.Debug(tag, e.ToString());
                //await ExceptionManager.ExceptionHandling(e);
                await UserDialogs.Instance.AlertAsync("Se dio la siguiente expecion: " + e.Message +".", "Exepcion", "OK");
            }
        }
    }
}