using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerRecibo
    {
        public async Task<string> guardarRecibo(Cliente pobjCliente,ListView ppnlAbono_ltvAbonos,bool patomicidadTransaccional)
        {
            if (!patomicidadTransaccional)
            {
                var MultiGeneric = DependencyService.Get<IMultiGeneric>();
                try
                {
                    MultiGeneric.BeginTransaction();

                    Logica_ManagerAgenteVendedor _manager = new Logica_ManagerAgenteVendedor();

                    _manager.buscarConsecutivoTransaccion(pobjCliente);

                    Logica_ManagerEncabezadoRecibo _managerEncabezadoRecibo = new Logica_ManagerEncabezadoRecibo();

                    await _managerEncabezadoRecibo.guardarReciboEncabezado(pobjCliente);

                    if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
                    {
                    }
                    else
                    {
                        Logica_ManagerDetalleRecibo _managerDetalleRecibo = new Logica_ManagerDetalleRecibo();

                        _managerDetalleRecibo.guardarReciboDetalle(ppnlAbono_ltvAbonos, pobjCliente);
                    }

                    Logica_ManagerPagoRecibo _managerPagoRecibo = new Logica_ManagerPagoRecibo();

                    _managerPagoRecibo.guardarPagoRecibo(pobjCliente);

                    if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
                    {
                        _manager.actualizarConsecutivoRecaudacion();
                    }
                    else
                    {
                        _manager.actualizarConsecutivoRecibo();
                    }

                    MultiGeneric.Commit();

                    patomicidadTransaccional = true;
                }
                catch (Exception ex)
                {
                    MultiGeneric.Rollback();

                    pobjCliente.v_objTransaccion.v_codDocumento = string.Empty;

                    throw new Exception("guardarRecibo(...)", ex);
                }
            }

            return pobjCliente.v_objTransaccion.v_codDocumento;
        }
    }
}
