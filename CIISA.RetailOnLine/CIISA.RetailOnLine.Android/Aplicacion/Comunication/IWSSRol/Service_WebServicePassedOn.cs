using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Droid.webServiceSROL_passedOn;
using Xamarin.Forms;

[assembly: Dependency(typeof(Service_WebServicePassedOn))]
namespace CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol
{
    public class Service_WebServicePassedOn: IService_WebServicePassedOn
    {
        public string consultaOrdenesVentaTransmitidas(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, decimal pdia)
        {
            VendedorTransmitido _vendedorTransmitido = new VendedorTransmitido();
            return _vendedorTransmitido.consultaOrdenesVentaTransmitidas(HomologateSystemCIISA_ROL.PassedOn(psystemCIISA), pdia);
        }

        public void Dispose()
        {
            VendedorTransmitido vendedorTransmitido = new VendedorTransmitido();
            vendedorTransmitido.Dispose();
        }

        public string consultaDocumentosTransmitidos(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, decimal pdia)
        {
            VendedorTransmitido vendedor = new VendedorTransmitido();
            return vendedor.consultaDocumentosTransmitidos(HomologateSystemCIISA_ROL.PassedOn(psystemCIISA), pdia);
        }

        public string consultaRecibosRecaudacionesTransmitidas(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, decimal pdia)
        {
            VendedorTransmitido vendedor = new VendedorTransmitido();
            return vendedor.consultaRecibosRecaudacionesTransmitidas(HomologateSystemCIISA_ROL.PassedOn(psystemCIISA), pdia);
        }
    }
}