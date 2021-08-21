using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario
{
    internal class LogicaIdentificarUsuario_IURender
    {
        private ctrlIdentificarUsuario controlador = null;

        internal LogicaIdentificarUsuario_IURender(ctrlIdentificarUsuario pcontrolador)
        {
            controlador = pcontrolador;
        }

        internal void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(controlador.view.FindByName<StackLayout>("pnlIdentificacion"));
            RenderPanels.paintPanel(controlador.view.FindByName<StackLayout>("pnlRecargaDiaria"));

            if (ppanel.Id.Equals(controlador.view.FindByName<StackLayout>("pnlIdentificacion").Id))
            {
                controlador.view.Title = "Identificar Usuario";
            }

            if (ppanel.Id.Equals(controlador.view.FindByName<StackLayout>("pnlRecargaDiaria").Id))
            {
                controlador.view.Title = "Recarga Diaria";
            }

            ppanel.IsVisible = true;
        }

        internal void renderComponents()
        {
            //controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion").Focus();

            llenarComboBoxCompannia();

            controlador.view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").TextColor = Color.Gray;
            controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtRecarga").TextColor = Color.Gray;
            pintarTecladoRecarga(false);

            controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion").Text = string.Empty;
            controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtContrasenna").Text = string.Empty;
            controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtRecarga").Text = string.Empty;

            String var1 = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany;
            String var2 = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent;
            String var3 = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codRute;
            String var4 = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;
            String var5 = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._name;
            String var6 = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._initials;
            String var7 = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._version;

            controlador.view.FindByName<Label>("pnlIdentificacion_lblVersion").Text = "Sistema "
                                                    + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._initials
                                                    + " © "
                                                    + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._version
                                                    + ".1 CIISA / T.I. 2017"
                                                    ;

            controlador.view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").Checked = false;
            controlador.view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").Checked = false;
        }

        internal void llenarComboBoxCompannia()
        {
            controlador.view.FindByName<Picker>("pnlIdentificacion_cbxCompannia").Items.Clear();

            controlador.view.FindByName<Picker>("pnlIdentificacion_cbxCompannia").Items.Add(Companias._elArreoNombre);
            controlador.view.FindByName<Picker>("pnlIdentificacion_cbxCompannia").Items.Add(Companias._camsaNombre);
            controlador.view.FindByName<Picker>("pnlIdentificacion_cbxCompannia").Items.Add(Companias._cosechasMarinasNombre);
            controlador.view.FindByName<Picker>("pnlIdentificacion_cbxCompannia").Items.Add(Companias._belcaFoodNombre);

            controlador.view.FindByName<Picker>("pnlIdentificacion_cbxCompannia").SelectedIndex = 0;
        }

        internal void pintarTecladoRecarga(bool pvisible)
        {
            controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtRecarga").IsVisible = pvisible;

            controlador.view.FindByName<Button>("pnlIdentificacion_btnBorrarRecarga").IsVisible = pvisible;
            controlador.view.FindByName<Button>("pnlIdentificacion_btnLimpiarRecarga").IsVisible = pvisible;
        }

        internal void RenderMenuBottomIdentificacion(bool render) {

            controlador.view.FindByName<Grid>("pnlIdentificacion_grdOptions").IsVisible = render;
            controlador.view.FindByName<StackLayout>("pnlIdentificacion_stkAcceder").IsVisible = false;
            controlador.view.FindByName<StackLayout>("pnlIdentificacion_stkLiquidador").IsVisible = false;

        }

        internal void RenderMenuBottomRecargaDiaria(bool render)
        {
            controlador.view.FindByName<Grid>("pnlRecargaDiaria_grdOptions").IsVisible = render;
            controlador.view.FindByName<StackLayout>("pnlRecargaDiaria_stkSalir").IsVisible = false;
        }

        internal void renderMenu()
        {
            controlador.view.ToolbarItems.Clear();
            RenderMenuBottomIdentificacion(false);
            RenderMenuBottomRecargaDiaria(false);

            if (controlador.view.FindByName<StackLayout>("pnlIdentificacion").IsVisible)
            {
                RenderMenuBottomIdentificacion(true);

                if (Variable._thereDataBase)
                {
                    controlador.view.FindByName<StackLayout>("pnlIdentificacion_stkAcceder").IsVisible = true;
                    controlador.view.FindByName<StackLayout>("pnlIdentificacion_stkLiquidador").IsVisible = true;
                    //controlador.view.ToolbarItems.Add(controlador.view.FindByName<ToolbarItem>("menu_mniAcceder"));
                    //controlador.view.ToolbarItems.Add(controlador.view.FindByName<ToolbarItem>("menu_mniLiquidadores"));
                    controlador.view.ToolbarItems.Add(controlador.view.FindByName<ToolbarItem>("menu_mniPruebaImpresion"));
                    controlador.view.ToolbarItems.Add(controlador.view.FindByName<ToolbarItem>("menu_mniPruebaConexion"));
                }
                else
                {
                    controlador.view.FindByName<StackLayout>("pnlIdentificacion_stkAcceder").IsVisible = true;
                    //controlador.view.ToolbarItems.Add(controlador.view.FindByName<ToolbarItem>("menu_mniAcceder"));
                    controlador.view.ToolbarItems.Add(controlador.view.FindByName<ToolbarItem>("menu_mniPruebaConexion"));
                }
                controlador.view.ToolbarItems.Add(controlador.view.FindByName<ToolbarItem>("menu_mniEnergia"));
            }

            if (controlador.view.FindByName<StackLayout>("pnlRecargaDiaria").IsVisible)
            {
                RenderMenuBottomRecargaDiaria(true);
                controlador.view.FindByName<StackLayout>("pnlRecargaDiaria_stkSalir").IsVisible = true;
                //controlador.view.ToolbarItems.Add(controlador.view.FindByName<ToolbarItem>("menu_mniSalir"));
            }

        }
    }
}
