using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerAutorizadoFirmar
    {
        public void buscarListaAutorizadosFirmar(Cliente pobjCliente)
        {
            HelperAutorizadoFirmar _helper = new HelperAutorizadoFirmar();

            _helper.buscarAutorizadosFirmar(pobjCliente);
        }
    }
}
