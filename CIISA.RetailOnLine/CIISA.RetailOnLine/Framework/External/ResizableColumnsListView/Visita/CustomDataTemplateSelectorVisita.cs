using CIISA.RetailOnLine.Aplicacion.AuditOnLine.ListViewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.External.ResizableColumnsListView.Visita
{
    public class CustomDataTemplateSelectorVisita : DataTemplateSelector
    {
        private DataTemplate Rutero = new DataTemplate(typeof(VistaRutero));
        private DataTemplate Carnicero = new DataTemplate(typeof(VisitaCarnicero));
        private TemplateStyles Style = new TemplateStyles();

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var value = item as pnlTransacciones_ltvProductos;

            if (value?.Estilo == TemplateStyles.VisitaRutero)
            {
                return Rutero;
            }

            return Carnicero;
        }

        public ObservableCollection<pnlTransacciones_ltvProductos> ChangeStyleListViewTF(ListView AN, String style)
        {
            var Source = new ObservableCollection<pnlTransacciones_ltvProductos>();

            foreach (pnlTransacciones_ltvProductos item in AN.ItemsSource)
            {
                pnlTransacciones_ltvProductos _lvi = new pnlTransacciones_ltvProductos();

                if (style.Equals(TemplateStyles.VisitaCarniceria))
                {
                    _lvi.Estilo = TemplateStyles.VisitaCarniceria;
                }
                else if (style.Equals(TemplateStyles.VisitaRutero))
                {
                    _lvi.Estilo = TemplateStyles.VisitaRutero;
                }

                _lvi._pt_codigo = item._pt_codigo;
                _lvi._pt_cantidad = item._pt_cantidad;
                _lvi._pt_comentario = item._pt_comentario;
                _lvi._pt_precio = item._pt_precio;
                _lvi._pt_motivo = item._pt_motivo;
                _lvi.MontoImpuestoUnaUnidad = item.MontoImpuestoUnaUnidad;

                _lvi._pt_estado = item._pt_estado;
                _lvi._pt_embalaje = item._pt_embalaje;
                _lvi._pt_porcDescuento = item._pt_porcDescuento;
                _lvi._pt_descripcion = item._pt_descripcion;
                _lvi._pt_motivo = item._pt_motivo;
                _lvi._pt_exento = item._pt_exento;

                _lvi.ItemTextColor = item.ItemTextColor;
                _lvi._montoDescuento = item._montoDescuento;
                _lvi.v_especificacionOV = item.v_especificacionOV;
                _lvi.inventarioDisponible = item.inventarioDisponible;

                Source.Add(_lvi);

            }

            return Source;
        }

    }
}
