using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Droid.webServiceSROL_consultation;
using System.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(Service_WebServiceConsultation))]
namespace CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol
{
    public class Service_WebServiceConsultation : IService_WebServiceConsultation
    {
        public bool Get_existeCliente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcedulaCliente)
        {
            VendedorConsulta _vendedorConsulta = new VendedorConsulta();
            return _vendedorConsulta.existeCliente(HomologateSystemCIISA_ROL.Consultation(psystemCIISA),pcedulaCliente);
        }

        public string Get_consultaClientePadronNacional(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcedulaCliente)
        {
            VendedorConsulta _vendedorConsulta = new VendedorConsulta();
            return _vendedorConsulta.consultaClientePadronNacional(HomologateSystemCIISA_ROL.Consultation(psystemCIISA), pcedulaCliente);
        }

        public string Get_consultaInventarioDocumento(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorConsulta _vendedorConsulta = new VendedorConsulta();
            return _vendedorConsulta.consultaInventarioDocumento(HomologateSystemCIISA_ROL.Consultation(psystemCIISA));
        }

        public string Get_consultaBitaRecarga(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorConsulta _vendedorConsulta = new VendedorConsulta();
            return _vendedorConsulta.consultaBitaRecarga(HomologateSystemCIISA_ROL.Consultation(psystemCIISA));
        }

        public bool estadoSistemaCerradoPorFecha(DataTable pFechaToma, CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorConsulta _vendedorConsulta = new VendedorConsulta();
            return _vendedorConsulta.estadoSistemaCerradoPorFecha(pFechaToma, HomologateSystemCIISA_ROL.Consultation(psystemCIISA));
        }
    }
}