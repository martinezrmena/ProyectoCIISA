using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerBanco
    {
        public void buscarListaCuentasBancarias(ListView pltvClientes,string pdescripcion)
        {
            HelperBanco _helper = new HelperBanco();

            _helper.buscarListaCuentasBancarias(pltvClientes,pdescripcion);
        }

        public string obtenerCodigoBanco(string pnomBanco)
        {
            HelperBanco _helper = new HelperBanco();

            return _helper.obtenerCodigoBanco(pnomBanco);
        }

        public DataTable buscarBanco()
        {
            HelperBanco _helper = new HelperBanco();

            return _helper.buscarBanco();
        }

        public string obtenerCuentaBanco(string pnomBanco)
        {
            HelperBanco _helper = new HelperBanco();

            return _helper.obtenerCuentaBanco(pnomBanco);
        }

        public string buscarSigla(string pcodBanco)
        {
            HelperBanco _helper = new HelperBanco();

            return _helper.buscarSigla(pcodBanco);
        }
    }
}
