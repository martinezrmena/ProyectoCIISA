using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.External.CustomTreeView
{
    public static class CollapsableListView
    {
        public static readonly BindableProperty IsCollapsableProperty =
            BindableProperty.CreateAttached("IsCollapsable", typeof(bool), typeof(ListView), false, propertyChanged: OnIsCollapsableChanged);

        public static bool GetIsCollapsable(BindableObject view) => (bool)view.GetValue(IsCollapsableProperty);

        public static void SetIsCollapsable(BindableObject view, bool value) => view.SetValue(IsCollapsableProperty, value);

        private static void OnIsCollapsableChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var listView = bindable as ListView;
            if (listView != null)
            {
                // always remove event
                listView.ItemSelected -= OnItemCollapsed;

                // add the event if true
                if (true.Equals(newValue))
                {
                    listView.ItemSelected += OnItemCollapsed;
                }
            }
        }
                
        private static void OnItemCollapsed(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as CollapsableItem;
            if (item != null)
            {
                // toggle the selection property
                item.IsCollapsed = !item.IsCollapsed;
            }

            // deselect the item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
