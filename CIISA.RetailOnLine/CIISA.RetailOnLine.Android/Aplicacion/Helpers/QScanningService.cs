using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Scanner;
using CIISA.RetailOnLine.Droid.Aplicacion.Helpers;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(QScanningService))]
namespace CIISA.RetailOnLine.Droid.Aplicacion.Helpers
{
    public class QScanningService : IQScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();
            string result = string.Empty;

            optionsCustom.BuildMultiFormatReader();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Acerca la cámara al elemento",
                BottomText = "Toca la pantalla para enfocar",

            };

            var ScannerResult = await scanner.Scan(optionsCustom);

            if (ScannerResult != null)
            {
                await Task.Run(() =>
                {
                    result = ScannerResult.Text;
                }
                ).ConfigureAwait(true);
            }

            await Task.Run(() =>
                {
                    scanner.PauseAnalysis();
                    scanner.Cancel();
                }
            ).ConfigureAwait(true);

            return result;
        }
    }
}