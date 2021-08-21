using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.External.CustomTreeView;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlInventarioPedidos
    {
        private vistaInventarioPedidos view { get; set; }

        internal ctrlInventarioPedidos(vistaInventarioPedidos pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlInventario").Id))
            {
                view.Title = "Inventario Pedidos";
            }

            ppanel.IsVisible = true;
        }

        internal void ScreenInicialization(string pcodProducto)
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlInventario"));

            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            _manager.recalcularProductoDisponibleEnInventario();

            _manager.buscarInventarioPedidos(view.FindByName<ListView>("pnlInventario_trvInventario"), pcodProducto);

        }

        internal void menu_mniCerrar_Click()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void pnlInventario_chbExpandir_CheckStateChanged()
        {
            var Source = view.FindByName<ListView>("pnlInventario_trvInventario").ItemsSource as ObservableCollection<CollapsableItem>;

            if (view.FindByName<CheckBox>("pnlInventario_chbExpandir").Checked)
            {
                foreach (var item in Source)
                {
                    item.IsCollapsed = true;
                }
            }
            else
            {
                foreach (var item in Source)
                {
                    item.IsCollapsed = false;
                }
            }

            view.FindByName<ListView>("pnlInventario_trvInventario").ItemsSource = Source;
        }
    }
}
