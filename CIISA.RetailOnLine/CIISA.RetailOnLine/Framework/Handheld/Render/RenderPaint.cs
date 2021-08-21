using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Framework.Handheld.Render
{
    public static class RenderPaint
    {
        public static void paintRedBackgroundTextBox(ExtendedEntry ptextBox)
        {
            ptextBox.BackgroundColor = Color.FromRgb(253, 112, 108);
            ptextBox.TextColor = Color.White;
        }

        public static void paintRedBackgroundPicker(Picker ptextBox)
        {
            ptextBox.BackgroundColor = Color.FromRgb(253, 112, 108);
            ptextBox.TextColor = Color.White;
        }

        public static void paintRedBackgroundEditor(Editor ptextBox)
        {
            ptextBox.BackgroundColor = Color.FromRgb(253, 112, 108);
            ptextBox.TextColor = Color.White;
        }

        public static void paintRedBackgroundDatePicker(DatePicker pdatepicker)
        {
            pdatepicker.BackgroundColor = Color.FromRgb(253, 112, 108);
            pdatepicker.TextColor = Color.White;
        }

        public static void paintWhiteBackgroundDatePicker(DatePicker pdatepicker)
        {
            pdatepicker.BackgroundColor = Color.White;
            pdatepicker.TextColor = Color.Black;
        }

        public static void paintWhiteBackgroundEditor(Editor ptextBox)
        {
            ptextBox.BackgroundColor = Color.White;
            ptextBox.TextColor = Color.Black;
        }

        public static void paintWhiteBackgroundPicker(Picker ptextBox)
        {
            ptextBox.BackgroundColor = Color.White;
            ptextBox.TextColor = Color.Black;
        }

        public static void paintWhiteBackgroundTextBox(ExtendedEntry ptextBox)
        {
            ptextBox.BackgroundColor = Color.White;
            ptextBox.TextColor = Color.Black;
        }

        public static void paintWhiteBackgroundListView(ListView plistView)
        {
            plistView.BackgroundColor = Color.White;
        }

        public static void paintRedBackgroundListView(ListView plistView)
        {
            plistView.BackgroundColor = (Color)App.Current.Resources["RedColor"];
        }

        public static void paintGrayBackgroundTextBox(ExtendedEntry ptextBox)
        {
            ptextBox.BackgroundColor = Color.LightGray;
            ptextBox.TextColor = Color.Black;
        }
    }
}
