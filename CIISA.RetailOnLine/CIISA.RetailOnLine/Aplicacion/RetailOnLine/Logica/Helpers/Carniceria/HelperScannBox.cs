using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers.Carniceria
{
    internal class HelperScannBox
    {
        internal void buscarListaBoxes(ListView pltvTransacciones, List<pnlTransacciones_ltvProductos> productos)
        {
            //La lista de "cajas" vendra desde una lista

            var Source = pltvTransacciones.ItemsSource as ObservableCollection<pnlCarniceria_Boxes>;

            foreach (var _fila in productos)
            {
                pnlCarniceria_Boxes _lvi = new pnlCarniceria_Boxes();

                _lvi.ESCANEADO = string.Empty;
                _lvi.CODIGO = string.Empty;
                _lvi.DESCRIPCION = _fila._pt_descripcion;
                _lvi.FECHAESCANEO = string.Empty;

                Source.Add(_lvi);
            }

            pltvTransacciones.ItemsSource = Source;

        }


    }
}
