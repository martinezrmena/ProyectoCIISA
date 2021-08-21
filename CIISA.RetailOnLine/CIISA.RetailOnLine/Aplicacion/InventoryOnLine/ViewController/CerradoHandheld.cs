using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Sincronizacion;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.InventoryOnLine.ViewController
{
    public class CerradoHandheld
    {
        public async Task CerrarMaquina(bool pconsolidacionAutomatica)
        {
            string _caption = string.Empty;

            bool _cerrar = false;

            if (pconsolidacionAutomatica)
            {
                _caption = "Consolidación automática";

                _cerrar = true;
            }
            else
            {
                _caption = "Toma Física";

                _cerrar = await LogMessages._dialogResultYes("¿Desea cerrar el Sistema (La Máquina y Backoffice)?", _caption);
            }

            if (_cerrar)
            {
                var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

                Descarga_ManagerGenerico _managerGenerico = new Descarga_ManagerGenerico();

                if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
                {
                    SincronizacionInformacion _sincronizacion = new SincronizacionInformacion();

                    if (pconsolidacionAutomatica)
                    {
                        await _sincronizacion.informacionSincronizar();

                        _managerGenerico.marcarDatosComoNoEnvidos();

                        Logica_ManagerInventario _managerInventarioLogica = new Logica_ManagerInventario();

                        _managerInventarioLogica.recalcularProductoDisponibleEnInventario();

                        ProcesoImpresion _impresion = new ProcesoImpresion();

                        try
                        {
                            await _impresion.imprimirReporteInventarioConExistencias("INVENTARIO CON EXISTENCIAS [CC] 1/2");
                        }
                        catch (Exception)
                        {
                            LogMessageAttention _logMessageAttention = new LogMessageAttention();
                            await _logMessageAttention.generalAttention("No se imprimió el inventario con existencias.");
                        }

                        _managerInventarioLogica.consolidarInventarioAutomaticamente();

                        try
                        {
                            await _impresion.imprimirReporteInventarioConExistencias("INVENTARIO CON EXISTENCIAS [CC] 2/2");
                        }
                        catch (Exception)
                        {
                            LogMessageAttention _logMessageAttention = new LogMessageAttention();
                            await _logMessageAttention.generalAttention("No se imprimió el inventario con existencias.");
                        }
                    }

                    await _sincronizacion.informacionSincronizar();

                    Descarga_ManagerEnvio _manager = new Descarga_ManagerEnvio();
                    await _manager.EnvioPaqueteInformacionPorWS_TomaFisica();

                    Logica_ManagerSistema _managerSistema = new Logica_ManagerSistema();
                    _managerSistema.actualizarEstado(true);

                    CerradoBackOffice _cerradoBackOffice = new CerradoBackOffice();
                    await _cerradoBackOffice.ProcesoCerradoBackoffice();
                }

                _managerGenerico.marcarDatosComoNoEnvidos();

            }
        }
    }
}
