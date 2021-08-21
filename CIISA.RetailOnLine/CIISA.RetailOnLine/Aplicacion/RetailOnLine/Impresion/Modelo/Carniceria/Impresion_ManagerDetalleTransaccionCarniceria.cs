using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo.Carniceria
{
    internal class Impresion_ManagerDetalleTransaccionCarniceria
    {
        internal void buscarLineasDetalleImpresion(string pcodTransaction, string pcodTipoTransaccion, List<string> pprintingLinesList, Cliente pobjCliente)
        {
            HelperDetalleTransaccion _helper = new HelperDetalleTransaccion();

            _helper.buscarLineasDetalleImpresion(
                pcodTransaction,
                pcodTipoTransaccion,
                pprintingLinesList,
                pobjCliente
                );
        }
    }
}
