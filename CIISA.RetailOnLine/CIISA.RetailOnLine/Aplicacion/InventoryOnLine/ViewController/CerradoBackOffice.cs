using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.SystemHH.ViewController;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.InventoryOnLine.ViewController
{
    internal class CerradoBackOffice
    {
        internal async Task ProcesoCerradoBackoffice()
        {
            Logica_ManagerSistema _managerSistema = new Logica_ManagerSistema();

            string _estadoSistemaHH = _managerSistema.buscarEstado();

            if (_estadoSistemaHH.Equals(SQL._close))
            {
                bool _actualizarNAF = false;

                //TestConnectionSROL _testConnectionSROL = new TestConnectionSROL();

                cierreNAF();

                _actualizarNAF = consultaEstadoRutaNAF();

                if (!_actualizarNAF)
                {
                    _actualizarNAF = true;

                    LogMessageAttention _logMessageAttention = new LogMessageAttention();

                    await _logMessageAttention.generalAttention("Debe cerrar manualmente la máquina en el Backoffice. Notifique al Departamento de Liquidaciones");
                }

                if (_actualizarNAF)
                {
                    ApplicationExit _applicationExit = new ApplicationExit();

                    await _applicationExit.DirectExit();
                }

            }
            else
            {
                await LogMessageError.generalError("No se cerró el sistema a nivel de la máquina y backoffice, intente de nuevo");
            }


        }

        private void cierreNAF()
        {
            Logica_ManagerInventario _managerInventarioLogica = new Logica_ManagerInventario();

            DateTime _fechaTomaHH = _managerInventarioLogica.buscarFechaTomaFisica();

            var ServicioDownload = DependencyService.Get<IService_WebServiceDownload>();

            ServicioDownload.descargaCierreMaquina(
                Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL),
                true,
                _fechaTomaHH
            );
        }

        private bool consultaEstadoRutaNAF()
        {
            bool _actualizarNAF = false;

            var ServicioConsultation = DependencyService.Get<IService_WebServiceConsultation>();//new TestConnectionSROL();

            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            DataTable _dt = _manager.buscarFechaTomaFisicaActualizarEnviado();

            _actualizarNAF = ServicioConsultation.estadoSistemaCerradoPorFecha(
                                _dt,
                                Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)
                                );

            return _actualizarNAF;
        }

    }
}
