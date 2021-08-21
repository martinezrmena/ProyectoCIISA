using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerTramite
    {
        public async Task guardarTramite(Cliente pobjCliente)
        {
            Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

            _managerAgenteVendedor.buscarConsecutivoTramite(pobjCliente);

            if (!pobjCliente.v_objTransaccion.v_codDocumento.Equals(string.Empty))
            {
                HelperEncabezadoTramite _helperEncabezadoTramite = new HelperEncabezadoTramite();

                await _helperEncabezadoTramite.guardarEncabezadoTramite(pobjCliente);

                HelperDetalleTramite _helperDetalleTramite = new HelperDetalleTramite();

                _helperDetalleTramite.guardarDetalleTramite(pobjCliente);

                Logica_ManagerEncabezadoTransaccion _managerEncabezadoTransaccion = new Logica_ManagerEncabezadoTransaccion();

                _managerEncabezadoTransaccion.actualizarTramitado(pobjCliente);

                _managerAgenteVendedor.actualizarConsecutivoTramite(pobjCliente);
            }
        }
    }
}
