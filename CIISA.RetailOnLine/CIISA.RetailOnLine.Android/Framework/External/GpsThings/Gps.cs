using Android.Content;
using Android.Locations;
using CIISA.RetailOnLine.Droid.Framework.External.GpsThings;
using CIISA.RetailOnLine.Framework.External.GpsThings;
using Xamarin.Forms;

[assembly: Dependency(typeof(Gps))]
namespace CIISA.RetailOnLine.Droid.Framework.External.GpsThings
{
    public class Gps : IGps
    {
        private LocationManager _locationManager;
        private Location _currentLocation;
        

        public Gps()
        {
            if (_locationManager == null)
            {
                _locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);
            }
        }

        public bool Opened()
        {
            bool IsEnabled = false;
            IsEnabled = _locationManager.IsProviderEnabled(LocationManager.GpsProvider);
            return IsEnabled;
        }

        public void Open()
        {
            if (!Opened())
            {
                Intent _intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                _intent.AddFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(_intent);
            }
            else
            {
                _currentLocation = new Location(LocationManager.GpsProvider);
            }
        }

        public void Close()
        {
            //if (Opened())
            //{
            //    _locationManager = null;
            //    _currentLocation = null;
            //}
        }

        public int GetSatellitesInView() {
#pragma warning disable CS0618 // 'LocationManager.GetGpsStatus(GpsStatus)' está obsoleto: 'deprecated'
            var satelliteStatus = _locationManager.GetGpsStatus(null);
#pragma warning restore CS0618 // 'LocationManager.GetGpsStatus(GpsStatus)' está obsoleto: 'deprecated'
            int Ns = satelliteStatus.MaxSatellites;

            return Ns;

        }

        public int GetSatellitesInSolution() {
            int NSatellitesUsed = _locationManager.AllProviders.Count;
            return NSatellitesUsed;
        }

    }
}