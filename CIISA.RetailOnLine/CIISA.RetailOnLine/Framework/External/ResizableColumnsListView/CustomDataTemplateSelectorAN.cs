using CIISA.RetailOnLine.Aplicacion.AuditOnLine.ListViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.External.ResizableColumnsListView
{
    public class CustomDataTemplateSelectorAN : DataTemplateSelector
    {
        private DataTemplate ContractedGrid = new DataTemplate(typeof(TemplateContractedGridAN));
        private DataTemplate ExpandedGrid = new DataTemplate(typeof(TemplateExpandableGridAN));
        private TemplateStyles Style = new TemplateStyles();

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var value = item as pnlInventario_ltvInventario;

            if (value?.Estilo == Style.ContractedStyle)
            {
                return ContractedGrid;
            }

            return ExpandedGrid;
        }

        public ObservableCollection<pnlInventario_ltvInventario> ChangeStyleListViewTF(ListView AN, String style)
        {
            var Source = new ObservableCollection<pnlInventario_ltvInventario>();

            foreach (pnlInventario_ltvInventario item in AN.ItemsSource)
            {
                pnlInventario_ltvInventario _lvi = new pnlInventario_ltvInventario();

                if (style.Equals(Style.ContractedStyle))
                {
                    _lvi.Estilo = Style.ContractedStyle;
                }
                else if (style.Equals(Style.ExpandedStyle))
                {
                    _lvi.Estilo = Style.ExpandedStyle;
                }
                _lvi.Cantidad = item.Cantidad;
                _lvi.Codigo = item.Codigo;
                _lvi.Descripcion = item.Descripcion;
                _lvi.Diferencia = item.Diferencia;
                _lvi.Disponible = item.Disponible;
                _lvi.Auditado = item.Auditado;

                Source.Add(_lvi);

            }

            return Source;
        }

    }
}
