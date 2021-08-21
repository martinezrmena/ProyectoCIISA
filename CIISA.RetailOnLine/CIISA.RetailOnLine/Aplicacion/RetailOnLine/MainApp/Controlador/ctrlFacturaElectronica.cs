using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    internal class ctrlFacturaElectronica
    {
        internal CodigoProveedor codigoProveedor = new CodigoProveedor();
        internal vistaFacturaElectronica view = null;
        internal FacturaElectronica facturaElectronica = null;
        internal bool cerrar = false;
        internal LogicaFacturaElectronica _logica = null;
        internal string v_TipoDocumento { get; set; }


        public ctrlFacturaElectronica(vistaFacturaElectronica _view, string TipoDocumento, string tipo_pantalla)
        {
            view = _view;
            v_TipoDocumento = TipoDocumento;
            facturaElectronica = new FacturaElectronica();
            _logica = new LogicaFacturaElectronica(view, v_TipoDocumento, tipo_pantalla);
            ScreenInitialization();
        }

        internal void ScreenInitialization()
        {
            _logica.IndicadoresRestriction();
        }

        internal async Task Guardar()
        {

            if (await LogMessages._dialogResultYes("¿Son correctos los datos proporcionados?"+
                                                    Environment.NewLine + 
                                                    _logica.mensaje_adicionales_Fe_II(_logica.FacturaElectronicaData()), "IMPORTANTE"))
            {
                if (!_logica.ValidarIndicadores()) {

                    return;
                }

                cerrar = true;
                await Cerrando();

                //Permite cerrar el pop up
                await PopupNavigation.Instance.PopAsync(true);
            }
        }

        internal void pnlFacturaElectronica_EliminarOrdenCompra() {

            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtOrdenCompra"));
        }

        internal void pnlFacturaElectronica_EliminarTodosOrdenCompra() {

            view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtOrdenCompra").Text = string.Empty;
        }

        internal void pnlFacturaElectronica_EliminarRecibo()
        {

            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtOrdenRecibo"));
        }

        internal void pnlFacturaElectronica_EliminarTodosRecibo()
        {

            view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtOrdenRecibo").Text = string.Empty;
        }

        internal void pnlFacturaElectronica_EliminarReclamo()
        {

            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtReclamo"));
        }

        internal void pnlFacturaElectronica_EliminarTodosReclamo()
        {

            view.FindByName<ExtendedEntry>("pnlFacturaElectronicatxtReclamo").Text = string.Empty;
        }

        internal async Task Cerrando() {

            if (cerrar)
            {
                if (view.ctrlVisita != null)
                {
                    await view.ctrlVisita.menu_GuardarInicializationParte2(_logica.FacturaElectronicaData());
                }
            }

        }
    }
}
