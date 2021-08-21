using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using Geolocator.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClienteModificar : ContentPage
    {

        double latitud = 0;
        double longitud = 0;


        public ClienteModificar()
        {
            InitializeComponent();

            //Se inicializa el mapa con la ubicación 

            Task.Run(async ()=> 
            {
                await GetPosition();
            });

        }

        async Task GetPosition()
        {

            var Posicion = await CrossGeolocator.Current.GetPositionAsync(10000);

            if (Posicion == null)
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("No se encuentra disponible la posición proporcionada por el GPS, por favor verifique el estado de la máquina antes de continuar.");
            }
            else
            {
                latitud = Posicion.Latitude;
                longitud = Posicion.Longitude;
                var locator = CrossGeolocator.Current;
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitud, longitud),
                                   Distance.FromMeters(2500)));
                var defaultPin = new Pin { Type = PinType.Place, Label = "This is my home", Address = "Here", Position = new Position(latitud, longitud) };
                map.Pins.Add(defaultPin);
            }
        }


        #region Obtener Ubicación e insertar Pin       
        //async void ObtenerUbicacion_onClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var hasPermission = await Utils.CheckPermissions(Permission.Location);
        //        if (!hasPermission)
        //            return;

        //        btnObtenerUbicacion.IsEnabled = false;

        //        var locator = CrossGeolocator.Current;

        //        if (locator.IsGeolocationAvailable == false)
        //            return;

        //        if (locator.IsGeolocationEnabled == false)
        //            return;

        //        locator.DesiredAccuracy = 20;


        //        //Obtiene las coordenadas, latitud y longitud
        //        var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(30), null, false);

        //        if (position == null)
        //        {
        //            return;
        //        }

        //        lblLongitud.Text = position.Longitude.ToString();
        //        lblLatitud.Text = position.Latitude.ToString();

        //        //Agrega el pin con las coordenadas en el mapa
        //        AddPins(Constants.EncabezadoInsertarPin, Constants.DetalleInsertarPin, position.Latitude, position.Longitude);

        //        //Obtiene las dirección actual
        //        var address = await locator.GetAddressesForPositionAsync(position, Constants.MapKey);
        //        if (address == null || address.Count() == 0)
        //        {
        //            await DisplayAlert(Constants.Error, Constants.ErrorGeolocator, Constants.Ok);
        //        }

        //        var a = address.FirstOrDefault();

        //        txtLocalizacion.Text = $"{a.Thoroughfare}/{a.Locality}/{a.CountryCode}";
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert(Constants.Error, ex.Message + "-" + ex.InnerException, Constants.Ok);
        //    }
        //    finally
        //    {
        //        btnObtenerUbicacion.IsEnabled = true;
        //    }
        //}


        //private void AddPins(string label,
        //                     string address,
        //                     double latitud,
        //                     double longitud)
        //{
        //    var pin = new Pin
        //    {
        //        Type = PinType.Place,
        //        Position = new Position(latitud, longitud),
        //        Label = label,
        //        Address = address,
        //    };

        //    MyMap.Pins.Add(pin);

        //    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitud, longitud),
        //                       Distance.FromMeters(100)));


        //}
        #endregion

        private void map_MapClicked(object sender, MapClickedEventArgs e)
        {
            try
            {
                var locator = CrossGeolocator.Current;

                //var placemarks = await Xamarin.Essentials.Geocoding.GetPlacemarksAsync(e.Point.Latitude, e.Point.Longitude);
                //var placemark = placemarks?.FirstOrDefault();
                //var value = $"{placemark.Thoroughfare}/{placemark.Locality}/{placemark.CountryCode}";
                AddPins("Encabezado pin", "Detalle pin", e.Point.Latitude, e.Point.Longitude);

                void AddPins(string label, string address, double latitud, double longitud)
                {
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(latitud, longitud),
                        Label = label,
                        Address = address,
                    };
                    map.Pins.Remove(pin);
                    map.Pins.Clear();
                    map.Pins.Add(pin);
                    var txtlongitud = longitud.ToString();
                    var txtlatitud = latitud.ToString();

                    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitud, longitud),
                                       Distance.FromMeters(2500)));



                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}