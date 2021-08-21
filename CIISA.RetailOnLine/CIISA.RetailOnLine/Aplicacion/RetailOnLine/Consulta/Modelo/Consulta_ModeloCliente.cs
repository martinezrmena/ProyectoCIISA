using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Modelo
{
    public class Consulta_ModeloCliente
    {
        public bool ExisteClienteEnNAF(string pcedula)
        {
            var Servicio = DependencyService.Get<IService_WebServiceConsultation>();

            bool _existe = Servicio.Get_existeCliente(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), pcedula);

            return _existe;
        }

        public bool ExisteCliente(string pcedula)
        {
            HelperCliente _helper = new HelperCliente();

            return _helper.existeCliente(pcedula);
        }
    }
}
