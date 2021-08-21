using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerFechaServidor
    {

        public string buscarFechaConHora()
        {
            HelperFechaServidor _helper = new HelperFechaServidor();

            return _helper.buscarFechaConHora();
        }

    }
}
