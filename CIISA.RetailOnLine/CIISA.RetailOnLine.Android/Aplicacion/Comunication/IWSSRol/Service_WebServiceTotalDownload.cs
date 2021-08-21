using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Droid.webServiceSROL_totalDownload;
using Xamarin.Forms;

[assembly: Dependency(typeof(Service_WebServiceTotalDownload))]
namespace CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol
{
    public class Service_WebServiceTotalDownload : IService_WebServiceTotalDownload
    {
        public string Get_TotalSend_Automatic(string pdatosSROL,string ptipoRutero,bool ptomaFisica,CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA,bool pwriteDataTables)
        {
            VendedorEnvioTotal _vendedorEnvioTotal = new VendedorEnvioTotal();
            return _vendedorEnvioTotal.TotalSend_Automatic(pdatosSROL, ptipoRutero,ptomaFisica,HomologateSystemCIISA_ROL.TotalDownload(psystemCIISA),pwriteDataTables);
        }

        public string Get_TotalSend(string pdatosSROL, string ptipoRutero,bool ptomaFisica, CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, bool pwriteDataTables)
        {
            VendedorEnvioTotal _vendedorEnvioTotal = new VendedorEnvioTotal();
            return _vendedorEnvioTotal.TotalSend(pdatosSROL, ptipoRutero, ptomaFisica, HomologateSystemCIISA_ROL.TotalDownload(psystemCIISA), pwriteDataTables);
        }
    }
}