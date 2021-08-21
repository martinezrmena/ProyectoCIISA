using System;
using System.Globalization;

namespace CIISA.RetailOnLine.Framework.Common.Misc
{
    public static class SQL
    {
        public const string SQLITEDATE = "DATETIME('NOW', 'LOCALTIME')";

        public const string _No = "N";

        public const string _Si = "S";

        public const string _pending = "P";

        public const string _close = "C";

        public const string _ROL = "R";

        public const string _NAF = "N";

        public const string _ruteroRoute = "R";

        public const string _activeRoute = "A";

        public const string _inactiveRoute = "I";

        public const string _ORA00001 = "ORA-00001";

        public const string _true = "True";

        public const string _false = "False";

        public static string ToDate(DateTime pdateTime, bool pwithTime)
        {
            string _string = string.Empty;

            if (pwithTime)
            {
                _string += "TO_DATE('";
                _string += pdateTime.Day;
                _string += "/";
                _string += pdateTime.Month;
                _string += "/";
                _string += pdateTime.Year;
                _string += " ";
                _string += String.Format("{0:hh}", pdateTime);
                _string += ":";
                _string += String.Format("{0:mm}", pdateTime);
                _string += ":";
                _string += String.Format("{0:ss}", pdateTime);
                _string += " ";
                _string += String.Format(CultureInfo.InvariantCulture, "{0:tt}", pdateTime);
                _string += "','DD-MM-YYYY HH:MI:SS AM')";
            }
            else
            {
                _string += "TO_DATE('";
                _string += pdateTime.Day;
                _string += "/";
                _string += pdateTime.Month;
                _string += "/";
                _string += pdateTime.Year;
                _string += "','DD-MM-YYYY HH:MI:SS AM')";
            }

            return _string;
        }

    }
}
