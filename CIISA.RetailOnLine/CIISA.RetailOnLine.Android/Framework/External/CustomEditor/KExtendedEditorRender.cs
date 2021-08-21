using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using CIISA.RetailOnLine.Droid.Framework.External.CustomEditor;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(KExtendedEditor), typeof(KExtendedEditorRender))]
namespace CIISA.RetailOnLine.Droid.Framework.External.CustomEditor
{
#pragma warning disable CS0618 // 'EditorRenderer.EditorRenderer()' está obsoleto: 'This constructor is obsolete as of version 2.5. Please use EditorRenderer(Context) instead.'
    class KExtendedEditorRender: EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                ((KExtendedEditor)e.NewElement).PropertyChanging += OnPropertyChanging;
            }

            if (e.OldElement != null)
            {
                ((KExtendedEditor)e.OldElement).PropertyChanging -= OnPropertyChanging;
            }

            // Disable the Keyboard on Focus
            this.Control.ShowSoftInputOnFocus = false;
        }


        private void OnPropertyChanging(object sender, PropertyChangingEventArgs propertyChangingEventArgs)
        {
            // Check if the view is about to get Focus
            if (propertyChangingEventArgs.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
            {
                // incase if the focus was moved from another Entry
                // Forcefully dismiss the Keyboard 
                InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(this.Control.WindowToken, 0);
            }
        }

    }
#pragma warning restore CS0618 // 'EditorRenderer.EditorRenderer()' está obsoleto: 'This constructor is obsolete as of version 2.5. Please use EditorRenderer(Context) instead.'
}