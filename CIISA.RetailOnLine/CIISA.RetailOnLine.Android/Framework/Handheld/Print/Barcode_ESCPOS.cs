using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CIISA.RetailOnLine.Droid.Framework.Handheld.Print
{
    public class Barcode_ESCPOS
    {
        // Set Barcode height
        static byte[] SetBarcodeHeight = new byte[] { 0x1D, 0x68, 0x69 };
        // Set Barcode width
        static byte[] SetBarcodeWidth = new byte[] { 0x1D, 0x77, 0x40 };
        // Begin barcode printing
        static byte[] EAN13BarCodeStart = new byte[] { 0x1D, 0x6B, 67, 13 };

        public string BarcodeString(string barcode)
        {
            string s = Encoding.ASCII.GetString(SetBarcodeHeight);
            s += Encoding.ASCII.GetString(SetBarcodeWidth);
            s += string.Format("{0}{1}", Encoding.ASCII.GetString(EAN13BarCodeStart), barcode);
            return s;
        }

    }
}