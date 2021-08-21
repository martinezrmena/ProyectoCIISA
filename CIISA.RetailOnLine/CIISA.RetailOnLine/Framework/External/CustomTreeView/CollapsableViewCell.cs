using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.External.CustomTreeView
{
    [ContentProperty(nameof(EncabezadoView))]
    public class CollapsableViewCell : ViewCell
    {
        private Grid rootGrid;
        private View encabezadoView;
        private View detalleView;
        private View checkView;
#pragma warning disable CS0649 // El campo 'CollapsableViewCell.value' nunca se asigna y siempre tendrá el valor predeterminado
        private Color value;
#pragma warning restore CS0649 // El campo 'CollapsableViewCell.value' nunca se asigna y siempre tendrá el valor predeterminado

        public CollapsableViewCell()
        {
            rootGrid = new Grid
            {
                Padding = 5,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto }
                },
                RowDefinitions =
                {
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                }
            };

            View = rootGrid;

            var check = new Switch();
            CheckView = check;

            var text = new Label();
            text.SetBinding(Label.TextProperty, ".");
            text.TextColor = value;
            EncabezadoView = text;

            var text2 = new Label();
            text2.SetBinding(Label.TextProperty, ".");
            text2.TextColor = value;
            DetalleView = text2;
        }

        public Color CustomTextColor {

            get {return value ;}

            set {

                var text = new Label();
                text.SetBinding(Label.TextProperty, ".");
                text.TextColor = value;
                EncabezadoView = text;

                var text2 = new Label();
                text2.SetBinding(Label.TextProperty, ".");
                text2.TextColor = value;
                DetalleView = text2;
            }

        }


        public View CheckView
        {
            get { return checkView; }
            set
            {
                // jump out if the value is the same or something happened to our layout
                if (checkView == value || View != rootGrid)
                    return;

                OnPropertyChanging();

                // remove the old binding
                if (checkView != null)
                {
                    checkView.RemoveBinding(Switch.IsToggledProperty);
                    rootGrid.Children.Remove(checkView);
                }

                checkView = value;

                // add the new binding
                if (checkView != null)
                {
                    checkView.SetBinding(Switch.IsToggledProperty, nameof(CollapsableItem.IsCollapsed));
                    Grid.SetColumn(checkView, 0);
                    Grid.SetColumnSpan(checkView, 1);
                    Grid.SetRow(checkView, 0);
                    Grid.SetRowSpan(checkView, 1);
                    rootGrid.Children.Add(checkView);
                }

                OnPropertyChanged();
            }
        }

        public View EncabezadoView
        {
            get { return encabezadoView; }
            set
            {
                // jump out if the value is the same or something happened to our layout
                if (encabezadoView == value || View != rootGrid)
                    return;

                OnPropertyChanging();

                // remove the old view
                if (encabezadoView != null)
                {
                    encabezadoView.RemoveBinding(CollapsableItem.EncabezadoProperty);
                    rootGrid.Children.Remove(encabezadoView);
                }

                encabezadoView = value;

                // add the new view
                if (encabezadoView != null)
                {
                    encabezadoView.SetBinding(Label.TextProperty, nameof(CollapsableItem.Encabezado));
                    encabezadoView.SetBinding(Label.TextColorProperty, nameof(CollapsableItem.CustomTextColor));
                    Grid.SetColumn(encabezadoView, 1);
                    Grid.SetColumnSpan(encabezadoView, 1);
                    Grid.SetRow(encabezadoView, 0);
                    Grid.SetRowSpan(encabezadoView, 1);
                    rootGrid.Children.Add(encabezadoView);
                }

                OnPropertyChanged();
            }
        }

        public View DetalleView
        {
            get { return detalleView; }
            set
            {
                // jump out if the value is the same or something happened to our layout
                if (detalleView == value || View != rootGrid)
                    return;

                OnPropertyChanging();

                // remove the old view
                if (detalleView != null)
                {
                    detalleView.RemoveBinding(CollapsableItem.DetalleProperty);
                    rootGrid.Children.Remove(detalleView);
                }

                detalleView = value;

                // add the new view
                if (detalleView != null)
                {
                    detalleView.SetBinding(Label.TextProperty, nameof(CollapsableItem.Detalle));
                    detalleView.SetBinding(Label.IsVisibleProperty, nameof(CollapsableItem.IsCollapsed));
                    detalleView.SetBinding(Label.TextColorProperty, nameof(CollapsableItem.CustomTextColor));
                    Grid.SetColumn(detalleView, 1);
                    Grid.SetColumnSpan(detalleView, 1);
                    Grid.SetRow(detalleView, 1);
                    Grid.SetRowSpan(detalleView, 1);
                    rootGrid.Children.Add(detalleView);
                }

                OnPropertyChanged();
            }
        }
    }
}
