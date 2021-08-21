using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.External.CustomTreeView
{
    public class CollapsableItem : BindableObject
    {
        public static readonly BindableProperty EncabezadoProperty =
            BindableProperty.Create(nameof(Encabezado), typeof(string), typeof(CollapsableItem), null);

        public static readonly BindableProperty DetalleProperty =
            BindableProperty.Create(nameof(Detalle), typeof(string), typeof(CollapsableItem), null);

        public static readonly BindableProperty IsCollapsedProperty =
            BindableProperty.Create(nameof(IsCollapsed), typeof(bool), typeof(CollapsableItem), false);

        public static readonly BindableProperty CustomTextColorProperty =
            BindableProperty.Create(nameof(CustomTextColor), typeof(Color), typeof(CollapsableItem), Color.Black);



        public CollapsableItem(string pencabezado)
        {
            Encabezado = pencabezado;
        }

        public CollapsableItem(string pencabezado,string pdetalle)
        {
            Encabezado = pencabezado;
            Detalle = pdetalle;
        }

        public CollapsableItem(string pencabezado, string pdetalle, bool isCollpased)
        {
            Encabezado = pencabezado;
            Detalle = pdetalle;
            IsCollapsed = isCollpased;
        }

        public string Encabezado
        {
            get { return (string)GetValue(EncabezadoProperty); }
            set { SetValue(EncabezadoProperty, value); }
        }

        public string Detalle
        {
            get { return (string)GetValue(DetalleProperty); }
            set { SetValue(DetalleProperty, value); }
        }

        public bool IsCollapsed
        {
            get { return (bool)GetValue(IsCollapsedProperty); }
            set { SetValue(IsCollapsedProperty, value); }
        }

        public Color CustomTextColor
        {
            get { return (Color)GetValue(CustomTextColorProperty); }
            set { SetValue(CustomTextColorProperty, value); }
        }
    }
}
