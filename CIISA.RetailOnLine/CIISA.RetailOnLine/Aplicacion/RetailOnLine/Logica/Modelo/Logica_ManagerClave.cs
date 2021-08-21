using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerClave
    {
        public string obtenerCodigoPrincipal()
        {
            HelperClave _helper = new HelperClave();

            return _helper.obtenerCodigoPrincipal();
        }

        public string obtenerCodigoTomaFisica()
        {
            HelperClave _helper = new HelperClave();

            return _helper.obtenerCodigoTomaFisica();
        }

        public string obtenerCodigoFaltantes()
        {
            HelperClave _helper = new HelperClave();

            return _helper.obtenerCodigoFaltantes();
        }

        public string obtenerCodigoConsecutivos()
        {
            HelperClave _helper = new HelperClave();

            return _helper.obtenerCodigoConsecutivos();
        }

        public string mostrarClaves()
        {
            HelperClave _helper = new HelperClave();

            return _helper.mostrarClaves();
        }
    }
}
