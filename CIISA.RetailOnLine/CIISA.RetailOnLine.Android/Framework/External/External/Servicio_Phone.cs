using Android.Content;
using Android.Provider;
using Android.Telephony;
using CIISA.RetailOnLine.Droid.Framework.External.External;
using CIISA.RetailOnLine.Framework.External;
using Xamarin.Forms;

[assembly: Dependency(typeof(Servicio_Phone))]
namespace CIISA.RetailOnLine.Droid.Framework.External.External
{
    public class Servicio_Phone : IServicio_Phone
    {
        private TelephonyManager _telephonyManager;
        private GsmSignalStrengthListener _signalStrengthListener;
        public int intensidadSennial = 0;

        private CustomServiceStateListener _serviceStateListener;
        public string estadoServicio = string.Empty;

        public Servicio_Phone()
        {
            if (_telephonyManager == null)
            {
                _telephonyManager = (TelephonyManager)Android.App.Application.Context.GetSystemService(Context.TelephonyService);

                _signalStrengthListener = new GsmSignalStrengthListener();
                _telephonyManager.Listen(_signalStrengthListener, PhoneStateListenerFlags.SignalStrengths);
                _signalStrengthListener.SignalStrengthChanged += HandleSignalStrengthChanged;

                _serviceStateListener = new CustomServiceStateListener();
                _telephonyManager.Listen(_serviceStateListener, PhoneStateListenerFlags.ServiceState);
                _serviceStateListener.ServiceStateChanged += HandleServiceStateChanged;
            }
        }

        public string NetworkType()
        {
            string NetworkType = _telephonyManager.NetworkType.ToString();
            return NetworkType;
        }

        public string GetSimStatus()
        {
            string status = _telephonyManager.SimState.ToString();

            if (status == "Absent")
            {
                status = "Ausente";
            }
            else if (status == "NetworkLocked")
            {
                status = "Red Bloqueada";
            }
            else if (status == "PinRequired")
            {
                status = "PIN requerido";
            }
            else if (status == "PukRequired")
            {
                status = "PUK requerido";
            }
            else if (status == "Ready")
            {
                status = "Conectado";
            }
            else if (status == "Unknown")
            {
                status = "Deconocido";
            }


            return status;
        }

        public string GetNetworkOperator()
        {
            string Operator = _telephonyManager.NetworkOperatorName;
            return Operator;
        }

        public string GetNetworkStatus()
        {
            string respuesta = string.Empty;

            respuesta = estadoServicio;

            return respuesta;
        }

        public string GetSIMID()
        {
            string valor = _telephonyManager.SimSerialNumber;
            return valor;
        }

        public string GetManufacturer()
        {
            string mfg = Android.OS.Build.Manufacturer;

            return mfg;
        }

        public string GetRevision()
        {
            string fw = Android.OS.Build.RadioVersion;

            return fw;
        }

        public string GetModel()
        {
            string Modelo = Android.OS.Build.Model;

            return Modelo;
        }

        public string GetIMEI()
        {
#pragma warning disable CS0618 // 'TelephonyManager.DeviceId' está obsoleto: 'deprecated'
            string valor = _telephonyManager.DeviceId;
#pragma warning restore CS0618 // 'TelephonyManager.DeviceId' está obsoleto: 'deprecated'
            return valor;
        }

        public string PhoneType()
        {
            string PhoneType = _telephonyManager.PhoneType.ToString();
            return PhoneType;
        }

        public string GetSignalStrength()
        {            
            string respuesta = string.Empty;

            if (intensidadSennial != 99)
            {
                //ConvertirSennial();
                respuesta = intensidadSennial.ToString();
            }
            else
            {
                respuesta = "No detectable";
            }

            return respuesta;
        }

        private void ConvertirSennial()
        {
            if (intensidadSennial != 99)
            {
                intensidadSennial = (intensidadSennial * 100) / 31;
            }
        }

        private void HandleSignalStrengthChanged(int strength)
        {
            _signalStrengthListener.SignalStrengthChanged -= HandleSignalStrengthChanged;
            _telephonyManager.Listen(_signalStrengthListener, PhoneStateListenerFlags.None);

            intensidadSennial = strength;
        }
        
        private void HandleServiceStateChanged(string Algo)
        {
            _serviceStateListener.ServiceStateChanged -= HandleServiceStateChanged;
            _telephonyManager.Listen(_serviceStateListener, PhoneStateListenerFlags.None);

            estadoServicio = Algo;
        }
    }

    public class GsmSignalStrengthListener : PhoneStateListener
    {
        public delegate void SignalStrengthChangedDelegate(int strength);

        public event SignalStrengthChangedDelegate SignalStrengthChanged;

        public override void OnSignalStrengthsChanged(SignalStrength newSignalStrength)
        {
            if (newSignalStrength.IsGsm)
            {
                if (SignalStrengthChanged != null)
                {
                    SignalStrengthChanged(newSignalStrength.GsmSignalStrength);
                }
            }
        }
    }

    public class CustomServiceStateListener : PhoneStateListener
    {
        public delegate void ServiceStateChangedDelegate(string algo);
        public event ServiceStateChangedDelegate ServiceStateChanged;
        public override void OnServiceStateChanged(ServiceState newserviceState)
        {
            ServiceStateChanged(newserviceState.State.ToString());
        }
    }

}