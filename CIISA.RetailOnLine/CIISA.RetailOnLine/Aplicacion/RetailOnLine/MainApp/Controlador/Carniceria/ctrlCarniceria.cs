using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Display.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria
{
    /// <summary>
    /// Clase encargada de dirigir el escaneo de cajas
    /// Esta clase no fue implementada por completo debido a cambios especificados por el cliente
    /// </summary>
    public class ctrlCarniceria
    {
        public vistaCarniceria view { get; set; }
        public Cliente v_objCliente = null;
        internal bool v_finalizoConstructor2 = false;
        public int TappedN = 0;
        private int v_fila = Numeric._zeroInteger;
        internal bool Escanenado = false;
        internal ITaskActivity DPB = DependencyService.Get<ITaskActivity>();
        internal LogMessageAttention _logMessageAttention = null;
        //internal LogicaCarniceriaRender logicaCarniceriaRender = null;
        internal pnlBarcode_Reader BarcodeReader = null;
        internal List<pnlTransacciones_ltvProductos> productos = null;


        internal ctrlCarniceria(vistaCarniceria p_view, Cliente cliente, List<pnlTransacciones_ltvProductos> p)
        {
            view = p_view;
            //logicaCarniceriaRender = new LogicaCarniceriaRender(view);
            v_objCliente = cliente;
            _logMessageAttention = new LogMessageAttention();
            BarcodeReader = new pnlBarcode_Reader();
            productos = p;
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlCarniceria"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlCarniceria").Id))
            {
                view.Title = "Carniceria";
            }

            ppanel.IsVisible = true;
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlCarniceria"));
            renderComponents();
            //logicaCarniceriaRender.renderMenu(false);            
        }

        internal void renderComponents() {

            view.FindByName<Label>("pnlCarniceria_lblNombreCliente").Text =
                    v_objCliente.v_no_cliente
                    + Simbol._hyphenWithSpaces
                    + v_objCliente.v_nombre; ;

            view.FindByName<ListView>("pnlCarniceria_ltvBoxes").ItemsSource = new ObservableCollection<pnlCarniceria_Boxes>();

            Logica_ManagerCarniceria manager = new Logica_ManagerCarniceria();

            manager.buscarListaCajasEscanear(
                view.FindByName<ListView>("pnlCarniceria_ltvBoxes"), productos);

        }

        #region Menu Contextual

        internal void menu_mniBloquear_Click()
        {
            ShowDisplay _show = new ShowDisplay();

            _show.showLockScreenForm(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));
        }

        internal async Task menu_mniPruebaImpresion_Click()
        {
            Logica_ManagerImpresora _manager = new Logica_ManagerImpresora();

            string _puertoImpresora = _manager.obtenerPuertoImpresora();

            PrintTest _printTest = new PrintTest();

            await _printTest.testPrint(_puertoImpresora);
        }

        internal async Task menu_mniCoordenadas_Click()
        {
            await GPS_Info.v_gpsDevice.ShowCoordinates();
        }
        
        internal void Cerrar()
        {
            view.Navigation.PopAsync();
        }

        #endregion

        #region BottomBarMenu

        /// <summary>
        ///Metodo que sirve para eliminar los codigos escaneados de todas las cajas
        /// </summary>
        internal async Task RevertListView()
        {

            if (await LogMessages._dialogResultYes("Se perderán las cajas escaneadas, ¿Desea Continuar?", "Alerta: Reiniciar Escaneo."))
            {
                //logicaCarniceriaRender.revertScannListView(view.FindByName<ListView>("pnlCarniceria_ltvBoxes"));
            }
        }

        /// <summary>
        ///Desencadena el proceso de escaneo para agregar codigos a las cajas de la lista
        /// </summary>
        internal async Task ScanningBarCode()
        {
            var ListView = view.FindByName<ListView>("pnlCarniceria_ltvBoxes");
            var Source = ListView.ItemsSource as ObservableCollection<pnlCarniceria_Boxes>;
            int i = Numeric._zeroInteger;
            int failed = Numeric._zeroInteger;

            //Deshabilitamos las opciones mientras se escanea
            Scanning_Progress(true);

            //Agregar el escaner (*) a los elementos del listview
            var Destination = new ObservableCollection<pnlCarniceria_Boxes>();

            foreach (var _lvi in Source)
            {
                string Code = string.Empty;

                //Le indicamos al usuario que item se esta modificando
                DPB.SelectListViewItem(view.FindByName<ListView>("pnlCarniceria_ltvBoxes"), Source, i);

                if (!_lvi.ESCANEADO.Equals(Simbol._asterisk))
                {
                    //Realizamos el escaneo sobre cada caja
                    //LogicaCarniceriaScanner logicaCarniceriaScanner = new LogicaCarniceriaScanner();
                    //Code = await logicaCarniceriaScanner.ScannBarCode();
                    await Task.Delay(1000);
                }

                //Evaluamos si el codigo se escaneo correctamente
                if (!string.IsNullOrEmpty(Code))
                {
                    _lvi.ESCANEADO = Simbol._asterisk;
                    _lvi.CODIGO = Code;
                }
                else {
                    //El escaneo fallo o no trajo nada de cualquier manera es un error
                    await _logMessageAttention.generalAttention("La caja "+(i+1)+" no pudo escanearse de manera correcta.");
                    failed++;
                }

                Destination.Add(_lvi);
                i++;
            }

            view.FindByName<ListView>("pnlCarniceria_ltvBoxes").ItemsSource = Destination;

            //Habilitamos las opciones al finalizar el escaneo
            Scanning_Progress(true);

            if (failed > 0)
            {
                await _logMessageAttention.generalAttention("El proceso de escaneo termino con " + failed + " errores, " +
                    "se recomienda agregar manualmente estos códigos.");
            }
            else {
                //Si el escaneo se completo sobre todas las cajas entonces habilitar guardar
                //logicaCarniceriaRender.renderMenu(true);
            }
        }

        internal void menu_mniGuardar_Click()
        {
            //LogicaCarniceriaMenu _logica = new LogicaCarniceriaMenu(view);

            //_logica.menu_mniGuardar_Click();
        }

        #endregion

        internal void renderComponentesPnlTransacciones(bool pcarga)
        {
            //LogicaCarniceriaRender _logica = new LogicaCarniceriaRender(view);

            //_logica.renderComponentesPnlTransacciones(pcarga);

            //_logica.renderMenu(false);
        }

        internal void pnlTransacciones_ltvProductos_Tapped(object sender, ItemTappedEventArgs e)
        {

            //var index = (view.FindByName<ListView>("pnlCarniceria_ltvBoxes").ItemsSource as ObservableCollection<pnlCarniceria_Boxes>).IndexOf(e.Item as pnlCarniceria_Boxes);

            //var a = 0;
            //view.FindByName<ListView>("pnlCarniceria_ltvBoxes").SelectedItem = false;

        }

        protected void InterruptSelection()
        {
            view.FindByName<ListView>("pnlCarniceria_ltvBoxes").SelectedItem = null;

            TappedN = 0;

        }

        internal async void Escribir_Codigo_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlCarniceria_ltvBoxes")))
            {
                await PopupNavigation.Instance.PushAsync(new vistaWriteBarCode(this));
            }
        }

        public async void Escribir_Codigo_Parte2(string code) {

            BarcodeReader.Split_BarCode(code);

            if (!BarcodeReader.CODCLIENTE.Equals(v_objCliente.v_no_cliente))
            {
                await _logMessageAttention.generalAttention("La caja no será entregada al cliente que la solicito.");
            }

            var _lvi = view.FindByName<ListView>("pnlCarniceria_ltvBoxes").SelectedItem as pnlCarniceria_Boxes;
            var Source = view.FindByName<ListView>("pnlCarniceria_ltvBoxes").ItemsSource as ObservableCollection<pnlCarniceria_Boxes>;
            var Seleccionado = view.FindByName<ListView>("pnlCarniceria_ltvBoxes").SelectedItem as pnlCarniceria_Boxes;

            v_fila = Source.IndexOf(view.FindByName<ListView>("pnlCarniceria_ltvBoxes").SelectedItem);

            _lvi.ESCANEADO = Simbol._asterisk;
            _lvi.CODIGO = code;
            _lvi.FECHAESCANEO = DateTime.Now.ToString();

            Source.RemoveAt(v_fila);
            Source.Insert(v_fila, _lvi);

            //if (logicaCarniceriaRender.CheckScannBoxes(view.FindByName<ListView>("pnlCarniceria_ltvBoxes")))
            //{
            //    logicaCarniceriaRender.renderMenu(true);
            //}
        }

        /// <summary>
        ///Metodo que sirve deshabilitar opciones en caso de que el sistema
        ///se encuentre escaneando una caja
        /// </summary>
        internal void Scanning_Progress(bool scanning) {

            Escanenado = scanning;

            view.FindByName<ListView>("pnlCarniceria_ltvBoxes").IsEnabled = !scanning;

            view.FindByName<Button>("btnEscribirCodigo").IsEnabled = !scanning;

        }

    }
}
