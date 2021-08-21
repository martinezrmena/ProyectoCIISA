using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Droid.webServiceSROL_download;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(Service_WebServiceDownload))]
namespace CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol
{
    public class Service_WebServiceDownload : IService_WebServiceDownload
    {
        public string descargaCierreMaquina(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, bool pestado,DateTime pfecha)
        {
            VendedorDescarga _vendedor = new VendedorDescarga();
            return _vendedor.descargaCierreMaquina(HomologateSystemCIISA_ROL.Download(psystemCIISA), pestado,pfecha);
        }
    }
}