using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Modelo
{
    public class RadioTelefonico_ManagerInformacionRuta
    {

        public DataTable buscarInformacionRuta()
        {
            HelperInformacionRuta _helper = new HelperInformacionRuta();

            return _helper.buscarInformacionRuta();
        }

        public string buscarNombreAgente(string pcodRuta)
        {
            HelperInformacionRuta _helper = new HelperInformacionRuta();

            return _helper.buscarNombreAgente(pcodRuta);
        }

    }
}
