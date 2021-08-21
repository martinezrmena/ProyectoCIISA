using Android.Locations;
using CIISA.RetailOnLine.Droid.Framework.Handheld.GPS.ViewController;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using Geolocator.Plugin;
using Geolocator.Plugin.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ServicioDistancia))]
namespace CIISA.RetailOnLine.Droid.Framework.Handheld.GPS.ViewController
{
    public class ServicioDistancia:IServicioDistancia
    {
        IGeolocator ServicioGeolocalizacion = CrossGeolocator.Current;

        private async Task<bool> HayServicioDeGeolocalizacion()
        {
            bool Hay = false;

            Hay = ServicioGeolocalizacion.IsGeolocationEnabled;

            if (!Hay)
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("El servicio de geolocalizacion esta desactivado.");
            }

            return Hay;
        }

        private async Task<double> DistanciaEntreDispositivoYCoordenadas(double LongitudCliente, double LatitudeCliente)
        {
            bool HayGeolocalizacion = ServicioGeolocalizacion.IsGeolocationEnabled;

            double DistanciaEnMetros = 0;

            if (HayGeolocalizacion)
            {
                ServicioGeolocalizacion = CrossGeolocator.Current;
                //TimeSpan Tiempo = TimeSpan.FromMilliseconds(10000);
                Position PosicionDispositivo = await ServicioGeolocalizacion.GetPositionAsync();

                //ESTABLECEMOS LA POSICION ACTUAL
                Location LocacionDispositivo = new Location("");
                LocacionDispositivo.Latitude = PosicionDispositivo.Latitude;
                LocacionDispositivo.Longitude = PosicionDispositivo.Longitude;

                //DATOS DE LA SEGUNDA LOCACIÓN QUE DEBEN SER PUESTOS EN CADA CONSULTA
                Location LocacionCliente = new Location("");
                LocacionCliente.Latitude = LatitudeCliente;
                LocacionCliente.Longitude = LongitudCliente;

                DistanciaEnMetros = LocacionDispositivo.DistanceTo(LocacionCliente);
            }

            return DistanciaEnMetros;
        }

        private async Task<bool> EsValidaLaDistancia(double Distance)
        {
            bool EsValida = false;

            if (Distance <= 50)
            {
                EsValida = true;
            }
            else
            {
                //Mensaje
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Usted se encuentra a " + Distance.ToString() + " metros del cliente, debe estar a 50 metros o menos de la ubicacion del cliente.");
                EsValida = false;
            }

            return EsValida;
        }

        public async Task<bool> ValidacionGeolocalizacion(double LongitudCliente, double LatitudeCliente)
        {
            bool EsValido = false;

            bool HayGeolocalizacion = await HayServicioDeGeolocalizacion();

            if (HayGeolocalizacion)
            {
                double Distancia = await DistanciaEntreDispositivoYCoordenadas(LongitudCliente, LatitudeCliente);

                if (Distancia < 0)
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("Error, Usted se encuentra a "+Distancia.ToString()+" metros del cliente, por favor corrobore las coordenadas del cliente.");
                    EsValido = false;
                }
                else
                {
                    EsValido = await EsValidaLaDistancia(Distancia);
                }
            }
            else
            {
                EsValido = false;
            }

            return EsValido;
        }
    }
}