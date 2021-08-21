using Acr.UserDialogs;
using Android.Bluetooth;
using CIISA.RetailOnLine.Droid.Framework.Handheld.Print;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using I18N.West;
using LinkOS.Plugin;
using LinkOS.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using static ESCPOS.Commands;
using ESCPOS;
using ESCPOS.Utils;
using CIISA.RetailOnLine.Droid.Framework.Handheld.Helper;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Constantes;

[assembly: Dependency(typeof(Servicio_Print))]
namespace CIISA.RetailOnLine.Droid.Framework.Handheld.Print
{
    public class Servicio_Print : IServicio_Print
    {
        private IConnection connection = null;
        private bool IsZPL = false;
        private bool IsEscPos = false;
        private bool impresoraFunciona = false;
        private HelperTipoImpresion helperTipoImpresion = new HelperTipoImpresion();
        private string Lenguaje = string.Empty;
        private int TimeOut = 10000;
        private int DelayTime = 2500;

        public async Task connect(string address)
        {
            impresoraFunciona = false;
            if ((connection == null) || (!connection.IsConnected))
            {
                var listaAddress = await ObtenerBluetoothAddress();
                int contador = listaAddress.Count;

                foreach (var addres in listaAddress)
                {
                    try
                    {
                        connection = ConnectionBuilder.Current.Build("BT:" + addres);

                        connection.MaxTimeoutForRead = TimeOut;
                        connection.TimeToWaitForMoreData = TimeOut;
                        connection.TimeToWaitAfterWrite = TimeOut;
                        connection.TimeToWaitAfterRead = TimeOut;
                        connection.Open();
                        await Task.Delay(DelayTime);

                        if (connection.IsConnected)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        contador = contador - 1;
                        if (contador == 0)
                        {
                            throw ex;
                        }
                    }
                }
            }

            if ((connection != null) && (connection.IsConnected))
            {
                impresoraFunciona = true;
                Lenguaje = GetValueLanguage();

                if (Lenguaje.Equals(TipoImpresionConst._ZPL))
                {
                    //En el caso de ESC/POS no es necesario establecer un lenguaje ya que siempre utilizará el mismo
                    //por esa misma razon tampoco se evalua si la impresora acepta ese nuevo lenguaje
                    if ((await SetPrintLanguage(connection)) && (await CheckPrinterStatus(connection)))
                    {
                    }
                }
                else if (Lenguaje.Equals(TipoImpresionConst.ESCPOS))
                {
                    await CheckPrinterStatusEscPos(connection);
                }
            }
        }

        private string GetValueLanguage()
        {
            return helperTipoImpresion.GetValue();
        }

        public void disconnect()
        {
            if ((connection != null) && (connection.IsConnected))
            {
                try
                {
                    //Si la connection es null o el IsConneccted es False entonces connectar
                    if ((connection != null) || (connection.IsConnected))
                    {
                        //Antes de desconectar se busca regresar a otro lenguaje según peticiones del usuario en caso de ser ZPL
                        //Este lenguaje es necesario para ellos para su sistema heredado
                        if (string.IsNullOrEmpty(Lenguaje))
                        {
                            Lenguaje = GetValueLanguage();
                        }

                        //En el caso de ESC/POS no es necesario un cambio de lenguaje ya que siempre utilizará el mismo
                        if (Lenguaje.Equals(TipoImpresionConst._ZPL))
                        {
                            string setLanguage = "! U1 setvar \"device.languages\" \"line_print\"\r\n\r\n! U1 getvar \"device.languages\"\r\n\r\n";
                            byte[] response = connection.SendAndWaitForResponse(Encoding.UTF8.GetBytes(setLanguage), 500, 500);
                            string s = Encoding.UTF8.GetString(response);
                        }

                        connection.Close();
                        connection = null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #region Print
        public async Task print(string texto)
        {
            if ((connection != null) && (connection.IsConnected))
            {

                if (string.IsNullOrEmpty(Lenguaje))
                {
                    Lenguaje = GetValueLanguage();
                }

                switch (Lenguaje)
                {
                    case TipoImpresionConst._ZPL:
                        await PrintZPL(texto);
                        break;
                    case TipoImpresionConst._ESCPOS:
                        await PrintEscPos(texto);
                        break;
                    default:
                        await UserDialogs.Instance.AlertAsync("No se pudo encontrar un lenguaje de impresora.", "Error", "Aceptar");
                        break;
                }
            }
        }

        public async Task PrintZPL(string texto)
        {
            if ((connection != null) && (connection.IsConnected))
            {
                if (IsZPL)
                {
                    texto = Conversion(texto);
                    //string zpl = "^XA^LL48^POI^FO0,0^ACN,18,10^FH_^FD" + texto + "^FS^XZ";
                    string zpl = "^XA^LL32^POI^FO0,0^ACN,18,10^FH_^FD" + texto + "^FS^XZ";
                    try
                    {
                        connection.Write(Encoding.UTF8.GetBytes(zpl));
                    }
                    catch (Exception e)
                    {
                        await UserDialogs.Instance.AlertAsync("Se dio la siguiente excepción: " + e.Message + ".", "Excepción", "Aceptar");
                    }
                }
                else
                {
                    await PrintNotRecognize(texto);
                }
            }
        }

        public async Task PrintNotRecognize(string texto)
        {

            if ((connection != null) && (connection.IsConnected))
            {
                try
                {
                    connection.Write(Encoding.UTF8.GetBytes(texto));
                }
                catch (Exception e)
                {
                    await UserDialogs.Instance.AlertAsync("Se dio la siguiente excepción: " + e.Message + ".", "Excepción", "Aceptar");
                }
            }
        }

        public async Task PrintEscPos(string texto)
        {

            if ((connection != null) && (connection.IsConnected))
            {
                if (IsEscPos)
                {
                    try
                    {

                        byte[] tab = SelectJustification(Justification.Left);
                        connection.Write(tab);

                        string EscPos = "\x1B\x21\x4" + texto + "\n";

                        Encoding nordic = Encoding.GetEncoding("CP437");
                        connection.Write(nordic.GetBytes(texto));
                    }
                    catch (Exception e)
                    {
                        await UserDialogs.Instance.AlertAsync("Se dio la siguiente excepción: " + e.Message + ".", "Excepción", "Aceptar");
                    }
                }
                else
                {
                    await PrintNotRecognize(texto);
                }
            }
        }
        #endregion

        #region BarCode
        public async Task printBarCode(string code)
        {
            if ((connection != null) && (connection.IsConnected))
            {
                if (string.IsNullOrEmpty(Lenguaje))
                {
                    Lenguaje = GetValueLanguage();
                }

                switch (Lenguaje)
                {
                    case TipoImpresionConst._ZPL:
                        await printBarCodeZPL(code);
                        break;
                    case TipoImpresionConst._ESCPOS:
                        await printBarCodeEscPos(code);
                        break;
                    default:
                        await UserDialogs.Instance.AlertAsync("No se pudo encontrar un lenguaje de impresora para llevar a cabo el proceso actual.", "Error", "Aceptar");
                        break;
                }

            }
        }

        public async Task printBarCodeZPL(string code)
        {
            if ((connection != null) && (connection.IsConnected))
            {
                if (IsZPL)
                {
                    string zpl = string.Empty;

                    if (code.Length < 8)
                    {
                        //This is an example of a Code 128 Subsets B
                        zpl = "^XA^MMT^PW575^LL0160^LS0^BY4,3,93^FT104,107^BCN,,Y,N^FD>:" + code + "^FS^PQ1,0,1,Y^XZ\r\n";
                    }
                    else
                    {

                        //This is an example of a Code 128 Subsets C
                        zpl = "^XA^MMT^PW575^LL0160^LS0^BY3,3,80^FT79,108^BCN,,Y,N^FD>:" + code + "^FS^PQ1,0,1,Y^XZ\r\n";
                    }


                    try
                    {
                        connection.Write(Encoding.UTF8.GetBytes(zpl));
                    }
                    catch (Exception e)
                    {
                        await UserDialogs.Instance.AlertAsync("Se dio la siguiente excepción: " + e.Message + ".", "Excepción", "Aceptar");
                    }
                }
                else
                {
                    await PrintNotRecognize(code);
                }
            }
        }

        public async Task printBarCodeEscPos(string code)
        {
            if ((connection != null) && (connection.IsConnected))
            {
                if (IsEscPos)
                {
                    try
                    {
                        ///Se imprime el codigo de barras
                        byte[] tab = SelectJustification(Justification.Center);

                        byte[] barCodeCommand = PrintBarCode(BarCodeType.CODE39, code, 100);

                        byte[] result = tab.Add(barCodeCommand);

                        connection.Write(result);

                    }
                    catch (Exception e)
                    {
                        await UserDialogs.Instance.AlertAsync("Se dio la siguiente excepción: " + e.Message + ".", "Excepción", "Aceptar");
                    }
                }
                else
                {
                    await PrintNotRecognize(code);
                }
            }
        }
        #endregion

        private async Task<bool> SetPrintLanguage(IConnection connection)
        {
            string setLanguage = "! U1 setvar \"device.languages\" \"zpl\"\r\n\r\n! U1 getvar \"device.languages\"\r\n\r\n";
            byte[] response = connection.SendAndWaitForResponse(Encoding.UTF8.GetBytes(setLanguage), 500, 500);
            string s = Encoding.UTF8.GetString(response);
            if (!s.Contains("zpl"))
            {
                //Agregando soporte para CPCL
                if (!s.Contains("line_print"))
                {
                    await UserDialogs.Instance.AlertAsync("La impresora no acepta el lenguaje ZPL", "Información", "Aceptar");
                    IsZPL = false;
                    return false;
                }
            }
            IsZPL = true;
            return true;
        }

        private async Task<bool> CheckPrinterStatus(IConnection connection)
        {
            IZebraPrinter printer = ZebraPrinterFactory.Current.GetInstance(PrinterLanguage.ZPL, connection);
            IPrinterStatus status = printer.CurrentStatus;
            if (!status.IsReadyToPrint)
            {
                await UserDialogs.Instance.AlertAsync("Error de impresión por estado: " + status.ToString(), "Información", "Aceptar");

                return false;
            }
            return true;
        }

        private async Task CheckPrinterStatusEscPos(IConnection connection)
        {
            if (!connection.IsConnected)
            {
                await UserDialogs.Instance.AlertAsync("Error en el estado de la impresora.", "Información", "Aceptar");

                IsEscPos = false;
            }

            IsEscPos = true;
        }

        private string Conversion(string Texto)
        {

            string newText = "";
            char[] charArray = Texto.ToCharArray();
            foreach (char c in charArray)
            {
                switch (c)
                {
                    case 'Ñ':
                        newText += "_a5";
                        break;
                    case 'ñ':
                        newText += "_a4";
                        break;
                    case 'Á':
                        newText += "_b5";
                        break;
                    case 'á':
                        newText += "_a0";
                        break;
                    case 'É':
                        newText += "_90";
                        break;
                    case 'é':
                        newText += "_82";
                        break;
                    case 'Í':
                        newText += "_d6";
                        break;
                    case 'í':
                        newText += "_a1";
                        break;
                    case 'Ó':
                        newText += "_e0";
                        break;
                    case 'ó':
                        newText += "_a2";
                        break;
                    case 'Ú':
                        newText += "_e9";
                        break;
                    case 'ú':
                        newText += "_a3";
                        break;
                    case '_':
                        newText += "_5f";
                        break;
                    default:
                        newText += c;
                        break;
                }
            }

            newText = newText.Replace(Environment.NewLine, "_0a");

            return newText;
        }

        private async Task<List<string>> ObtenerBluetoothAddress()
        {
            List<string> Addresses = new List<string>();

            //1) Obtener el bluethoot adapter
            BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (mBluetoothAdapter == null)
            {
                // Device does not support Bluetooth
                await UserDialogs.Instance.AlertAsync("El dispositivo no soporta Bluetooth", "Alerta", "Aceptar");
            }
            //2)Habilita Bluetooth
            if (!mBluetoothAdapter.IsEnabled)
            {
                //no esta habilitado asi que hay que habilitarlo
                //Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                //startActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
                await UserDialogs.Instance.AlertAsync("El dispositivo tiene deshabilitado el Bluetooth", "Alerta", "Aceptar");
            }
            //3) COnsultar dispositivos sincronizados
            ICollection<BluetoothDevice> pairedDevices = mBluetoothAdapter.BondedDevices;
            // If there are paired devices
            if (pairedDevices.Count > 0)
            {
                // Loop through paired devices
                foreach (BluetoothDevice device in pairedDevices)
                {
                    string parcial = device.Address;
                    parcial = parcial.Replace(":", "");
                    Addresses.Add(parcial);
                }
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("No hay impresora conectada", "Alerta", "Aceptar");
            }

            return Addresses;
        }

        public async Task<bool> HayImpresorasConectadas()
        {
            bool hayImpresoras = false;

            //1) Obtener el bluethoot adapter
            BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (mBluetoothAdapter == null)
            {
                // Device does not support Bluetooth
                await UserDialogs.Instance.AlertAsync("El dispositivo no soporta Bluetooth", "Alerta", "Aceptar");
            }
            //2)Habilita Bluetooth
            if (!mBluetoothAdapter.IsEnabled)
            {
                //no esta habilitado asi que hay que habilitarlo
                //Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                //startActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
                await UserDialogs.Instance.AlertAsync("El dispositivo tiene deshabilitado el Bluetooth", "Alerta", "Aceptar");
            }
            //3) COnsultar dispositivos sincronizados
            ICollection<BluetoothDevice> pairedDevices = mBluetoothAdapter.BondedDevices;
            // If there are paired devices
            if (pairedDevices.Count > 0)
            {
                hayImpresoras = true;
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("No hay impresora conectada", "Alerta", "Aceptar");
                hayImpresoras = false;
            }

            return hayImpresoras;
        }

        public bool ImpresoraFunciona()
        {

            return impresoraFunciona;

        }
    }
}