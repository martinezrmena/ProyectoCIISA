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
using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using Xamarin.Forms;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.webServiceSROL_rute;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.ProxySrol;

[assembly: Dependency(typeof(Service_WebServiceRute))]
namespace CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol
{
    public class Service_WebServiceRute : IService_WebServiceRute
    {
        public string Get_eliminarInventario(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorRuta _vendedorRuta = new VendedorRuta();
            return _vendedorRuta.eliminarInventario(HomologateSystemCIISA_ROL.Rute(psystemCIISA));
        }

        public string Get_generarRutaEInventario(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorRuta _vendedorRuta = new VendedorRuta();
            return _vendedorRuta.generarRutaEInventario(HomologateSystemCIISA_ROL.Rute(psystemCIISA));
        }
    }
}