using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    internal class ctrlVisitaCantidad
    {
        private vistaVisitaCantidad view { get; set; }
        private LogicaVisitaEventos v_logica = null;
        private LogicaCarniceriaEventos c_logica = null;
        private bool Cerrar = false;

        internal ctrlVisitaCantidad(vistaVisitaCantidad pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlModificarCantidad").Id))
            {
                view.Title = "Modificar Cantidad";
            }

            ppanel.IsVisible = true;
        }

        private void carga(List<Producto> plistaProductoComprometido)
        {
            view.FindByName<Label>("pnlModificarCantidad_lblProducto").Text =
                view.v_objProducto.v_codProducto
                + Simbol._hyphenWithSpaces
                + view.v_objProducto.descripcion();

            decimal _cantidadComprometido = Numeric._zeroDecimalInitialize;

            UtilLogica _util = new UtilLogica();

            _cantidadComprometido = _util.obtenerCantidadProductoComprometido(
                                                    view.v_objProducto.v_codProducto,
                                                    plistaProductoComprometido
                                                    );

            _cantidadComprometido -= view.v_objProducto.v_cantTransaccion;

            view.FindByName<Entry>("pnlModificarCantidad_txtComprometido").Text = FormatUtil.applyCurrencyFormat(
                                                            _cantidadComprometido
                                                            );

            view.FindByName<Label>("pnlModificarCantidad_lblComprometidoUM").Text = view.v_objProducto.unidad();

            decimal _cantidadInventario = view.v_objProducto.inventarioDisponible();

            view.FindByName<Entry>("pnlModificarCantidad_txtInventario").Text = FormatUtil.applyCurrencyFormat(_cantidadInventario);

            view.FindByName<Label>("pnlModificarCantidad_lblInventarioUM").Text = view.v_objProducto.unidad();

            view.FindByName<Entry>("pnlModificarCantidad_txtReal").Text = FormatUtil.applyCurrencyFormat(_cantidadInventario - _cantidadComprometido);

            view.FindByName<Label>("pnlModificarCantidad_lblRealUM").Text = view.v_objProducto.unidad();

            view.FindByName<ExtendedEntry>("pnlModificarCantidad_txtCantidad").Text =
               view.v_objProducto.v_cantTransaccion.ToString();

            view.FindByName<ExtendedEntry>("pnlModificarCantidad_txtCantidad").Focus();
        }

        internal void ScreenInicialization(Producto pobjProducto, List<Producto> plistaProductoComprometido, LogicaCarniceriaEventos logica)
        {
            c_logica = logica;

            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlModificarCantidad"));

            view.v_objProducto = pobjProducto;

            carga(plistaProductoComprometido);
        }

        internal void ScreenInicialization(Producto pobjProducto, List<Producto> plistaProductoComprometido, LogicaVisitaEventos logica)
        {
            v_logica = logica;

            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlModificarCantidad"));

            view.v_objProducto = pobjProducto;

            carga(plistaProductoComprometido);
        }

        internal void pnlModificarCantidad_btnBorrar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlModificarCantidad_txtCantidad"));
        }

        internal void pnlModificarCantidad_btnPuntoDecimal_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.punto(view.FindByName<ExtendedEntry>("pnlModificarCantidad_txtCantidad"));
        }

        internal void pnlModificarCantidad_btnLimpiar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlModificarCantidad_txtCantidad").Text = string.Empty;
        }

        internal void pnlModificarCantidad_btnAgregarReal_Click()
        {
            view.FindByName<ExtendedEntry>("pnlModificarCantidad_txtCantidad").Text = view.FindByName<Entry>("pnlModificarCantidad_txtReal").Text;
        }

        internal void menu_mniCancelar_Click()
        {
            //view.Close();
            Cerrar = true;
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private bool validarFormulario()
        {
            bool _estadoCorrecto = false;

            ValidateHH _validateHH = new ValidateHH();

            bool _txtVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlModificarCantidad_txtCantidad"));

            if (!_txtVacio)
            {
                bool _cantMayorQueCero = _validateHH.amountGreaterThanZero(view.FindByName<ExtendedEntry>("pnlModificarCantidad_txtCantidad"));

                if (_cantMayorQueCero)
                {
                    _estadoCorrecto = true;
                }
            }

            return _estadoCorrecto;
        }

        internal void menu_mniModificar_Click()
        {
            bool _formulario = validarFormulario();

            if (_formulario)
            {
                decimal _cantTransaccion = FormatUtil.convertStringToDecimal(view.FindByName<ExtendedEntry>("pnlModificarCantidad_txtCantidad").Text);

                view.v_objProducto.v_cantTransaccion = _cantTransaccion;

                view.v_guardar = true;

                //view.Close();
                Cerrar = true;
                Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        internal async Task Cerrando()
        {
            if (Cerrar)
            {
                if (v_logica != null)
                {
                    await v_logica.pnlTransacciones_ltvProductos_ItemActivateParte2(view.v_guardar, view.v_objProducto);
                }
            }
        }
    }
}
