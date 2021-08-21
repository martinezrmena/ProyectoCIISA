using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Framework.Handheld.Render
{
    public static class RenderHiden
    {

        public static void hideLabel(Label plabel)
        {
            plabel.IsVisible = false;
        }

        public static void hideTextBox(ExtendedEntry ptextBox)
        {
            ptextBox.IsVisible = false;
        }

        public static void hideButton(Button pbutton)
        {
            pbutton.IsVisible = false;
        }

        public static void hideButton(Picker pcomboBox)
        {
            pcomboBox.IsVisible = false;
        }

    }
}
