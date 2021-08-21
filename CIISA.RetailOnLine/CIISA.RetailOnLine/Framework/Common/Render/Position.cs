using CIISA.RetailOnLine.Framework.Common.Character;

namespace CIISA.RetailOnLine.Framework.Common.Render
{
    public class Position
    {
        public string center(int plongTextString)
        {
            int _quantity = (48 - plongTextString) / 2;

            string _space = string.Empty;

            for (int i = 0; i < _quantity; i++)
            {
                _space += Space._one;
            }

            return _space.ToString();
        }

        public string tabular(int plongTextString, int ppositionTab)
        {
            int _quantity = ppositionTab - plongTextString;

            string _space = string.Empty;

            for (int i = 0; i < _quantity; i++)
            {
                _space += Space._one;
            }

            return _space.ToString();
        }
    }
}
