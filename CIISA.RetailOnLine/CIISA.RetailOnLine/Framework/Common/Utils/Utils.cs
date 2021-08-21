using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Common.Utils
{
    public class Utils
    {
        public string recordListString_list(List<string> plist)
        {
            string _list = string.Empty;

            int _i = 1;

            foreach (string _register in plist)
            {
                _list += _register;

                if (plist.Count > _i)
                {
                    _list += ", ";
                }

                _i++;
            }

            return _list.ToString();
        }

        public string recordListString(List<string> plist)
        {
            string _list = string.Empty;

            int _i = 1;

            foreach (string _register in plist)
            {
                _list += "'" + _register + "'";

                if (plist.Count > _i)
                {
                    _list += ", ";
                }

                _i++;
            }

            return _list.ToString();
        }
    }
}
