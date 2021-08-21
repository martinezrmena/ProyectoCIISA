using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Scanner;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria.Scanner
{
    internal class LogicaCarniceriaScanner
    {
        public string Cadena { get; set; }

        public IQScanningService scanner { get; set; }

        public string result { get; set; }

        internal LogicaCarniceriaScanner() {
            Cadena = string.Empty;
            scanner = DependencyService.Get<IQScanningService>();
            result = string.Empty;
        }

        internal async Task<string> ScannBarCode() {

            result = await scanner.ScanAsync();

            if (!string.IsNullOrEmpty(result))
            {
                Cadena = result;                
            }

            return Cadena;
        }

        internal async Task<bool> ScannBarCode(ListView list)
        {
            result = await scanner.ScanAsync();

            if (!string.IsNullOrEmpty(result))
            {
                Cadena = result;

                //el resultado debe verificarse con los elementos de la lista
                //en caso de que haya un match con algun código de la lista
                //debe modificarse su estado a checked
                var Source = list.ItemsSource as SelectableObservableCollection<pnlCarniceria_Boxes>;

                foreach (var _lvi in Source)
                {
                    if (_lvi.Data.CODIGO.Equals(result))
                    {
                        _lvi.IsSelected = true;

                        return true;
                    }
                }
            }


            return false;
        }

    }
}
