using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers.Carniceria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria
{
    public class Logica_ManagerPedidos
    {
        public void buscarListaPedidosBackOffice(ListView pltvPedidos, Cliente pobjCliente)
        {
            HelperPedidos _helper = new HelperPedidos();

            _helper.buscarListaPedidosBackOffice(pltvPedidos, pobjCliente);
        }
    }
}
