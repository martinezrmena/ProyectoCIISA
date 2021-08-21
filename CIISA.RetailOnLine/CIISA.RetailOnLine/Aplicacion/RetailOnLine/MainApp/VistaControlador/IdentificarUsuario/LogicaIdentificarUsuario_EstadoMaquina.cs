using CIISA.RetailOnLine.Aplicacion.RetailOnLine.CargaDiaria.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario
{
    internal class LogicaIdentificarUsuario_EstadoMaquina
    {
        private ctrlIdentificarUsuario controlador = null;

        internal LogicaIdentificarUsuario_EstadoMaquina(ctrlIdentificarUsuario pcontrolador)
        {
            controlador = pcontrolador;
        }

        internal async Task ProcesarCargarEstadoMaquina()
        {
            Logica_ManagerSistema _managerSistema = new Logica_ManagerSistema();

            string _estadoMaquina = _managerSistema.buscarEstado();

            if (_estadoMaquina.Equals(string.Empty))
            {
                //TextBox _textBox = new TextBox();

                LogicaIdentificarUsuario_IURender _liu_iur = new LogicaIdentificarUsuario_IURender(controlador);

                _liu_iur.renderPaneles(controlador.view.FindByName<StackLayout>("pnlRecargaDiaria"));

                Log _log = new Log();

                Carga_ManagerRecargaDiaria _manager = new Carga_ManagerRecargaDiaria();

                await _manager.recargaDiariaTablaSistema(
                    _log,
                    controlador.view.FindByName<Editor>("pnlRecargaDiaria_txtBitacora"),
                    controlador.view.FindByName<Label>("pnlRecargaDiaria_lblInsertando")
                    );

                _liu_iur.renderPaneles(controlador.view.FindByName<StackLayout>("pnlIdentificacion"));
            }
        }
    }
}
