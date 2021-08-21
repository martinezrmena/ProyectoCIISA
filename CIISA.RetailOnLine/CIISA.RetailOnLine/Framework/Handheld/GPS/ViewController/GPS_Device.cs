using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.External.GpsThings;
using Geolocator.Plugin;
using Geolocator.Plugin.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController
{
    public class GPS_Device
    {
        private Position position = null;
        private IGeolocator locator = null;
        private string v_data = string.Empty;

        public GPS_Device() {
            StartListening();
        }

        #region GPS_CHANGE_POSITION

        public void StartListening()
        {
            if (CrossGeolocator.Current.IsListening)
                return;

            CrossGeolocator.Current.StartListening(1000, 1, true);

            CrossGeolocator.Current.PositionChanged += PositionChanged;
            CrossGeolocator.Current.PositionError += PositionError;
        }

        private async void PositionChanged(object sender, PositionEventArgs e)
        {
            try
            {
                position = e.Position;
                await UpdateData();
            }
            catch (Exception)
            {

            }
            
        }

        private async void PositionError(object sender, PositionErrorEventArgs e)
        {
            await UpdateData();
        }

        public void StopListening()
        {
            if (!CrossGeolocator.Current.IsListening)
                return;

            CrossGeolocator.Current.StopListening();

            CrossGeolocator.Current.PositionChanged -= PositionChanged;
            CrossGeolocator.Current.PositionError -= PositionError;
        }

        #endregion

        public async Task StartGps()
        {
            var v_gps = DependencyService.Get<IGps>();

            if (!v_gps.Opened())
            {
                v_gps.Open();
            }

            if (!CrossGeolocator.Current.IsListening)
            {
                StartListening();
            }

            await UpdateData();
        }

        public void Closed()
        {
            var v_gps = DependencyService.Get<IGps>();

            if (v_gps.Opened())
            {
                v_gps.Close();
            }

            if (CrossGeolocator.Current.IsListening)
            {
                StopListening();
            }
        }

        private void Inicializar()
        {
            if (locator == null)
            {
                locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
            }
        }

        public async Task<Position> GetPosition()
        {
            Inicializar();

            try
            {
                if (position == null)
                {
                    TimeSpan timeSpan = new TimeSpan(0, 0, 40);
                    CancellationTokenSource source = new CancellationTokenSource(timeSpan);
                    CancellationToken token = source.Token;

                    position = await locator.GetPositionAsync(40000, token);
                }
            }
            catch (Exception ex)
            {
                position = null;
            }

            return position;
        }

        public async Task<double> GetLatitude()
        {
            await GetPosition();

            double _value = 0;

            _value = position != null ? position.Latitude : 0;

            return Math.Round(_value, 8);
        }

        public async Task<double> GetLongitude()
        {
            await GetPosition();

            double _value = 0;

            _value = position != null ? position.Longitude: 0;

            return Math.Round(_value, 8);
        }

        public async Task ShowCoordinates()
        {
            string _coordenadas = "COORDENADAS";
            _coordenadas += Environment.NewLine;
            _coordenadas += Environment.NewLine;

            _coordenadas += "Latitud:   ";
            _coordenadas += await GPS_Info.v_gpsDevice.GetLatitude() + ".";
            _coordenadas += Environment.NewLine;
            _coordenadas += Environment.NewLine;

            _coordenadas += "Longitud: ";
            _coordenadas += await GPS_Info.v_gpsDevice.GetLongitude() + ".";

            LogMessageAttention _lma = new LogMessageAttention();

            if (position == null)
            {
                await _lma.generalAttention("No se encuentra disponible la posición proporcionada por el GPS, por favor verifique el estado de la máquina antes de continuar.");
            }
            else
            {
                await _lma.generalAttention(_coordenadas);
            }
        }

        public string GetData()
        {
            return v_data;
        }

        public void SetData(string value) {
            v_data = value;
        }

        public async Task<double> GetSeaLevelAltitude()
        {
            await GetPosition();

            double _value = 0;

            _value = position != null ? position.Altitude : 0;

            return Math.Round(_value, 0);
        }

        public async Task<double> GetSpeed()
        {
            await GetPosition();

            double _value = 0;

            _value = position != null ? position.Speed : 0;

            return Math.Round(_value, 0);
        }

        public async Task<string> GetTime_Formatted()
        {
            DateTime Date = await GetTime();
            string _value = VarTime.dateCR(Date);

            _value += " " + Date.ToString("hh:mm:ss tt");

            return _value.ToString();
        }

        public async Task<DateTime> GetTime()
        {
            DateTime _value = DateTime.Now;
            await GetPosition();
            _value = position != null ? DateTime.Parse(position.Timestamp.ToString()) : DateTime.Now;

            return _value;
        }

        public string GetServiceState() {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            if (locator.IsGeolocationEnabled)
            {
                return "Habilitado";
            }
            else
            {
                return "Deshabilitado";
            }

        }

        private async Task UpdateData()
        {
            string _value = string.Empty;
            var v_gps = DependencyService.Get<IGps>();

            _value += "Estado del Servicio: " + GetServiceState() + Environment.NewLine;
            _value += "Estado del Dispositivo: " + "Encendido" + Environment.NewLine;

            _value += Environment.NewLine;

            var result = await GetPosition();

            if (result == null)
            {
                string Fecha = VarTime.dateCR(DateTime.Now);
                Fecha += " " + DateTime.Now.ToString("hh:mm:ss tt");

                _value += "Fecha/Hora: " + Fecha + Environment.NewLine;

                _value += Environment.NewLine;
                _value += Environment.NewLine;

                _value += "No se encuentra disponible la posición proporcionada por el GPS, por favor verifique el estado de la máquina antes de continuar.";

                _value += Environment.NewLine;
                _value += Environment.NewLine;

            }
            else
            {
                _value += "Número de Satélites:  " + v_gps.GetSatellitesInSolution()
                + "/"
                + v_gps.GetSatellitesInView()
                + " ("
                + v_gps.GetSatellitesInView()
                + ")"
                + Environment.NewLine;
                _value += "Fecha/Hora: " + await GPS_Info.v_gpsDevice.GetTime_Formatted() + Environment.NewLine;

                _value += Environment.NewLine;

                _value += "Latitud (DD):  " + await GPS_Info.v_gpsDevice.GetLatitude() + Environment.NewLine;
                _value += "Longitud (DD): " + await GPS_Info.v_gpsDevice.GetLongitude() + Environment.NewLine;

                _value += Environment.NewLine;

                _value += "Velocidad: " + await GPS_Info.v_gpsDevice.GetSpeed() + " m/s" + Environment.NewLine;
                _value += "Altitud: " + await GPS_Info.v_gpsDevice.GetSeaLevelAltitude() + " m";
            }

            v_data = _value;
        }

    }
}
