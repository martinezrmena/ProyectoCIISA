using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using System;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Misc
{
    public class MiscUtils
    {
        public void quantityListViewItems<T>(ListView plistView, Label pcolumn, string ptext)
        {
            StringBuilder _sb = new StringBuilder();

            int Contador = 0;

            var Source = plistView.ItemsSource as ObservableCollection<T>;
            if (Source == null)
            {
                //Source = new ObservableCollection<T>();
                var Source2 = plistView.ItemsSource as SelectableObservableCollection<T>;

                if (Source2 != null)
                {
                    Contador = Source2.Count;
                }
            }
            else
            {
                Contador = Source.Count;
            }

            _sb.Append(ptext);
            _sb.Append(Space._one);
            _sb.Append(Simbol._squareBracketLeft);
            _sb.Append(Contador);
            _sb.Append(Simbol._squareBracketRight);

            pcolumn.Text = _sb.ToString();
        }

        public static void deleteNoSelectedItemsListView<T>(ListView plistView)
        {
            var Source = plistView.ItemsSource as SelectableObservableCollection<T>;

            for (int j = Source.Count - 1; j >= 0; j--)
            {
                var algo = Source[j].IsSelected;

                if (Source[j].IsSelected == false)
                {
                    Source.RemoveAt(j);
                }
            }

            plistView.ItemsSource = Source;
        }

        public static bool getVariableBooleanSQLStateStringEmptyTrue(string pstate)
        {
            bool _state = false;

            if (pstate.Equals(string.Empty))
            {
                _state = true;
            }
            else
            {
                if (pstate.Equals(SQL._Si))
                {
                    _state = true;
                }
                else
                {
                    _state = false;
                }
            }

            return _state;
        }

        public static string getVariableStringSQLState(bool pstate)
        {
            string _state = string.Empty;

            if (pstate)
            {
                _state = SQL._Si;
            }
            else
            {
                _state = SQL._No;
            }

            return _state;
        }

        public static bool getVariableBooleanSQLStateStringEmptyFalse(string pstate)
        {
            bool _state = false;

            if (pstate.Equals(string.Empty))
            {
                _state = false;
            }
            else
            {
                if (pstate.Equals(SQL._Si))
                {
                    _state = true;
                }
                else
                {
                    _state = false;
                }
            }

            return _state;
        }

        public string getWordStringSQLEstado(bool pstate)
        {
            string _state = string.Empty;

            if (pstate)
            {
                _state = Indicators._Si;
            }
            else
            {
                _state = Indicators._No;
            }

            return _state;
        }
    }
}
