using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.ValidateHH;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using CIISA.RetailOnLine.Framework.Server.WS;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario
{
    internal class LogicaIdentificarUsuario_IdentificarUsuario
    {
        private ctrlIdentificarUsuario controlador = null;

        internal LogicaIdentificarUsuario_IdentificarUsuario(ctrlIdentificarUsuario pcontrolador)
        {
            controlador = pcontrolador;
        }

        internal async Task identificarUsuario()
        {
            var _establishDatabase = DependencyService.Get<IEstablishDatabase>();

            _establishDatabase.thereDataBase();

            string _codAgente = controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion").Text;

            LogicaIdentificarUsuario _liu = new LogicaIdentificarUsuario(controlador);
            string _codCompannia = _liu.obtenerCodigoCompannia(controlador.view.FindByName<Picker>("pnlIdentificacion_cbxCompannia").SelectedIndex);

            GlobalVariables.AgentVariables_Temporal(_codCompannia, _codAgente);

            if (Variable._thereDataBase)
            {
                await obtenerDatosUsuarioLocal();
            }
            else
            {
                await obtenerDatosUsuarioRemoto();
            }
        }

        private async Task obtenerDatosUsuarioLocal()
        {
            Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

            DataTable _dt = _managerAgenteVendedor.buscarAgenteVendedor(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent);

            LogicaIdentificarUsuario_Verificar _liu_vu = new LogicaIdentificarUsuario_Verificar(controlador);

            await _liu_vu.verificarUsuario(_dt);
        }

        private async Task obtenerDatosUsuarioRemoto()
        {
            ValidateHH _validateHH = new ValidateHH();

            bool _identification = _validateHH.emptyTextBox(controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion"));
            bool _contrasenna = _validateHH.emptyTextBox(controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtContrasenna"));

            if (!_identification)
            {
                var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

                var servicio = DependencyService.Get<IService_WebServiceUpload>();
                string _url = servicio.Get_Url();

                if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
                {
                    string _memoryStream = servicio.Get_cargaAgenteVendedor(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

                    if (!_memoryStream.Equals(TypeEvents._errorWS))
                    {
                        var _memoryCompress = DependencyService.Get<IMC_HH>();

                        DataTable _dt = _memoryCompress.Unzip_HH_DataTable("identificarUsuarioRemoto", TypeTransaction._upload, _memoryStream, TablesROL._agenteVendedor);

                        if (await DataTableValidate.validateDataTable(_dt, "Identificando al usuario vía WEB, la información recibida del servidor es nula"))
                        {
                            LogicaIdentificarUsuario_Verificar _liu_vu = new LogicaIdentificarUsuario_Verificar(controlador);

                            await _liu_vu.verificarUsuario(_dt);
                        }
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(TypeEvents._errorWS, "Error", "Aceptar");
                    }
                }
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();

                await _logMessageAttention.generalAttention("Debe ingresar los datos requeridos");
            }
        }
    }
}
