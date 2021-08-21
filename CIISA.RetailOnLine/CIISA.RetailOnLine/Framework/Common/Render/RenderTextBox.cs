using System.Collections.Generic;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Common.Render
{
    public class RenderTextBox
    {
        public void writeReverseText(
            List<string> plista,
            Editor ptextBox
            )
        {
            plista.Reverse();

            string _text = string.Empty;

            foreach (string _line in plista)
            {
                _text += _line;
            }

            ptextBox.Text = _text;

            plista.Reverse();
        }
    }
}
