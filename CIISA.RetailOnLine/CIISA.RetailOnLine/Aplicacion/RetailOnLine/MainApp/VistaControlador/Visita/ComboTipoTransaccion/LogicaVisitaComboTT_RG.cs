using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.ComboTipoTransaccion
{
    internal class LogicaVisitaComboTT_RG
    {
        private vistaVisita view = null;

        internal LogicaVisitaComboTT_RG(vistaVisita pview)
        {
            view = pview;
        }

        internal async Task regalia_SelectedIndexChanged()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                var source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                int NValores = source.Count;

                for (int i = Numeric._zeroInteger; i < NValores; i++)
                {
                    var _lvi = source[i];

                    Producto _objProducto = new Producto();

                    LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                    _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

                    LogicaVisitaComboTT _logicaVisita = new LogicaVisitaComboTT(view);

                    _logicaVisita.establecerProductoParaModificacionLogica(_objProducto);

                    await _logica.verificarPrecio(_objProducto, source.IndexOf(_lvi), true);

                    if (NValores != source.Count)
                    {
                        i--;
                        NValores = source.Count;
                    }
                }

                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention(
                    "Debe ingresar un motivo al/los producto/s correcto."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "* Por definición el estado de/los producto/s es: sin motivo");
            }
        }
    }
}
