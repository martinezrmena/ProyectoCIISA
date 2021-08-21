using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    public class HelperDetalleFormaPago
    {
        internal string DetalleFormaPago(ListView pLista) {

            string _lineaDatos = string.Empty;
            StringBuilder sb = new StringBuilder();
            var Source = pLista.ItemsSource as ObservableCollection<pnlFormaPago_ltvPagos>;

            if (Source != null)
            {
                if (Source.Count > 0)
                {
                    _lineaDatos += "Forma Pago";
                    _lineaDatos += " / ";
                    _lineaDatos += "Monto";
                    sb.AppendLine(_lineaDatos);
                    sb.AppendLine(Environment.NewLine);

                    foreach (var item in Source)
                    {
                        _lineaDatos = string.Empty;
                        _lineaDatos += item.FormaPago;
                        _lineaDatos += " / ";
                        _lineaDatos += item.Monto;
                        sb.AppendLine("-> " + _lineaDatos);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
