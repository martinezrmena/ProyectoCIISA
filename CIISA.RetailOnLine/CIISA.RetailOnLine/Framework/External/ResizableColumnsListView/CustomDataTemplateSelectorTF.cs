using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.ListViewMoldels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.External.ResizableColumnsListView
{
    public class CustomDataTemplateSelectorTF : DataTemplateSelector
    {
        private DataTemplate ContractedGrid = new DataTemplate(typeof(TemplateContractedGridTF));
        private DataTemplate ExpandedGrid = new DataTemplate(typeof(TemplateExpandableGridTF));
        private TemplateStyles Style = new TemplateStyles();

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var value = item as pnlTomaFisica_ltvInventario;

            if (value?.Estilo == Style.ContractedStyle)
            {
                return ContractedGrid;
            }

            return ExpandedGrid;
        }

        public ObservableCollection<pnlTomaFisica_ltvInventario> ChangeStyleListViewTF(ListView TF, String style) {
            var Source = new ObservableCollection<pnlTomaFisica_ltvInventario>();
          
            foreach (pnlTomaFisica_ltvInventario item in TF.ItemsSource)
            {
                pnlTomaFisica_ltvInventario _lvi = new pnlTomaFisica_ltvInventario();

                if (style.Equals(Style.ContractedStyle))
                {
                    _lvi.Estilo = Style.ContractedStyle;
                }
                else if (style.Equals(Style.ExpandedStyle)) {
                    _lvi.Estilo = Style.ExpandedStyle;
                }
                _lvi.Cantidad = item.Cantidad;
                _lvi.Codigo = item.Codigo;
                _lvi.Descripcion = item.Descripcion;
                _lvi.Diferencia = item.Diferencia;
                _lvi.TomaFisica = item.TomaFisica;

                Source.Add(_lvi);

            }

            return Source;
        }

    }
}
