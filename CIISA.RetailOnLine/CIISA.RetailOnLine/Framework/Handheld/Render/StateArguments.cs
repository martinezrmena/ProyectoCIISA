using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Render
{
    public class StateArguments : System.EventArgs
    {

        public string v_message;
        public Color v_color;
        public double v_value;

        public StateArguments(string pmessage, Color pcolor, double pvalue)
        {
            v_message = pmessage;
            v_color = pcolor;
            v_value = pvalue;
        }

        public StateArguments(string pmessage, Color pcolor)
        {
            v_message = pmessage;
            v_color = pcolor;
        }

    }
}
