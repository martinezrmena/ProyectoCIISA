using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria
{
    public class Logica_ManagerDetalleReses
    {
        public void CambiarVendido(Cliente pobjCliente, bool comp, string codpedido, string NewCodPedido)
        {
            HelperDetalleReses _helper = new HelperDetalleReses();

            foreach (TransaccionDetalle _objDetalle in pobjCliente.v_objTransaccion.v_listaDetalles)
            {
                foreach (pnlTransacciones_ltvDetalleReses item in _objDetalle.v_listaDetalleReses)
                {
                    _helper.ActualizarVendidoDetalleRes(item._vc_consecutivo, codpedido, true, NewCodPedido);
                }
            }
        }

        public void CambiarAsignado(Cliente pobjCliente, bool comp)
        {
            Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();

            foreach (TransaccionDetalle _objDetalle in pobjCliente.v_objTransaccion.v_listaDetalles)
            {
                foreach (pnlTransacciones_ltvDetalleReses item in _objDetalle.v_listaDetalleReses)
                {
                    logica.CambiarAsignacion(item._vc_consecutivo, false);
                }
            }
        }

    }
}
