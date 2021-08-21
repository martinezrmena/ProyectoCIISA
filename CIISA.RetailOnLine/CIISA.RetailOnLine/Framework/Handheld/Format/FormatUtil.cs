using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using System;
using System.Globalization;

namespace CIISA.RetailOnLine.Framework.Handheld.Format
{
    public static class FormatUtil
    {
        public static DateTime covertStringToDateTimeWithoutTime(string pdate)
        {
            DateTime _date = new DateTime();

            if (!pdate.Equals(string.Empty))
            {
                _date = DateTime.ParseExact(pdate, VarTime.getDateFormatCR(), CultureInfo.CurrentCulture);
            }

            return _date;
        }

        private static string replaceCommaForPoint_pointForComma(string pstring)
        {
            string _commaForDollar = pstring.Replace(Simbol._comma, Simbol._dolarSymbol);
            string _pointForNumber = _commaForDollar.Replace(Simbol._point, Simbol._numberSymbol);
            string _dollarForPoint = _pointForNumber.Replace(Simbol._dolarSymbol, Simbol._point);
            string _numberForComma = _dollarForPoint.Replace(Simbol._numberSymbol, Simbol._comma);

            return _numberForComma;
        }

        public static string removePoint(string pstring)
        {
            string _string = pstring;
            int _lenght = _string.Length;

            if (_lenght > 3)
            {
                string _final = _string.Substring(_lenght - 3);

                if (_final.Equals(",00"))
                {
                    _string = pstring.Substring(0, _lenght - 3);
                    pstring = _string;
                }

                if (_final.Equals(".00"))
                {
                    _string = pstring.Substring(0, _lenght - 3);
                    pstring = _string;
                }
            }

            if (pstring.Contains(Simbol._comma) && pstring.Contains(Simbol._point))
            {
                string _tempString = string.Empty;

                _tempString = replaceCommaForPoint_pointForComma(pstring);
                _string = pstring.Replace(Simbol._comma, string.Empty);
                pstring = _string;
            }

            if (pstring.Contains(Simbol._comma) && !pstring.Contains(Simbol._point))
            {
                _string = pstring.Replace(Simbol._comma, string.Empty);
                pstring = _string;
            }

            if (pstring.Equals(string.Empty))
            {
                _string = Numeric._zeroDecimal;
            }

            return _string;
        }

        public static decimal convertStringToDecimal(string pstring)
        {
            Log _log = new Log();

            _log.setDetailValuesParameter("pstring + " + pstring);
            _log.setDetail("1----------------------------------");

            decimal _value = Numeric._zeroDecimalInitialize;

            string _string = string.Empty;

            _string = removePoint(pstring);

            pstring = _string;

            _log.setDetailVarValues("_value = " + _value);
            _log.setDetailVarValues("_string = " + _string);
            _log.setDetailValuesParameter("pstring + " + pstring);
            _log.setDetail("2----------------------------------");

            string _separadorDecimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            if (_separadorDecimal.Equals(Simbol._comma))
            {
                _string = pstring.Replace(Simbol._point, Simbol._comma);
                pstring = _string;

                _log.setDetailVarValues("_value = " + _value);
                _log.setDetailVarValues("_string = " + _string);
                _log.setDetailValuesParameter("pstring + " + pstring);
                _log.setDetail("3----------------------------------");
            }

            if (_separadorDecimal.Equals(Simbol._point))
            {
                _string = pstring.Replace(Simbol._comma, Simbol._point);
                pstring = _string;

                _log.setDetailVarValues("_value = " + _value);
                _log.setDetailVarValues("_string = " + _string);
                _log.setDetailValuesParameter("pstring + " + pstring);
                _log.setDetail("4----------------------------------");
            }

            try
            {
                if (pstring.Contains(Simbol._hyphen))
                {
                    _string = pstring.Replace(Simbol._hyphen, string.Empty);
                    //_value = decimal.Parse(_string, CultureInfo.CreateSpecificCulture(MiscCulture._enUS));
                    _value = decimal.Parse(_string, CultureInfo.CurrentCulture);
                    _value = _value * -1;

                    _log.setDetailVarValues("_value = " + _value);
                    _log.setDetailVarValues("_string = " + _string);
                    _log.setDetailValuesParameter("pstring + " + pstring);
                    _log.setDetail("5----------------------------------");
                }
                else
                {
                    //_value = decimal.Parse(pstring, CultureInfo.CreateSpecificCulture(MiscCulture._enUS));
                    _value = decimal.Parse(pstring, CultureInfo.CurrentCulture);

                    _log.setDetailVarValues("_value = " + _value);
                    _log.setDetailVarValues("_string = " + _string);
                    _log.setDetailValuesParameter("pstring + " + pstring);
                    _log.setDetail("6----------------------------------");

                }
            }
            catch (Exception ex)
            {
                _log.setDetailVarValues("_value = " + _value);
                _log.setDetailVarValues("_string = " + _string);
                _log.setDetailValuesParameter("pstring + " + pstring);
                _log.setDetail("7----------------------------------");

                _log.generateFileTXTHH(
                    Sistema.establecerObjetoSystemCIISA(),
                    "FormatUtil",
                    "convertStringToDecimal"
                    );

                throw new Exception("Error convirtiendo a decimal", ex);
            }

            return _value;
        }

        public static double convertStringToDouble(string pstring)
        {
            Log _log = new Log();

            _log.setDetailValuesParameter("pstring + " + pstring);
            _log.setDetail("1----------------------------------");

            double _value = 0;

            string _string = string.Empty;

            _string = removePoint(pstring);

            pstring = _string;

            _log.setDetailVarValues("_value = " + _value);
            _log.setDetailVarValues("_string = " + _string);
            _log.setDetailValuesParameter("pstring + " + pstring);
            _log.setDetail("2----------------------------------");

            string _separadorDecimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            if (_separadorDecimal.Equals(Simbol._comma))
            {
                _string = pstring.Replace(Simbol._point, Simbol._comma);
                pstring = _string;

                _log.setDetailVarValues("_value = " + _value);
                _log.setDetailVarValues("_string = " + _string);
                _log.setDetailValuesParameter("pstring + " + pstring);
                _log.setDetail("3----------------------------------");
            }

            if (_separadorDecimal.Equals(Simbol._point))
            {
                _string = pstring.Replace(Simbol._comma, Simbol._point);
                pstring = _string;

                _log.setDetailVarValues("_value = " + _value);
                _log.setDetailVarValues("_string = " + _string);
                _log.setDetailValuesParameter("pstring + " + pstring);
                _log.setDetail("4----------------------------------");
            }

            try
            {
                if (pstring.Contains(Simbol._hyphen))
                {
                    _string = pstring.Replace(Simbol._hyphen, string.Empty);
                    //_value = decimal.Parse(_string, CultureInfo.CreateSpecificCulture(MiscCulture._enUS));
                    _value = double.Parse(_string, CultureInfo.CurrentCulture);
                    _value = _value * -1;

                    _log.setDetailVarValues("_value = " + _value);
                    _log.setDetailVarValues("_string = " + _string);
                    _log.setDetailValuesParameter("pstring + " + pstring);
                    _log.setDetail("5----------------------------------");
                }
                else
                {
                    //_value = decimal.Parse(pstring, CultureInfo.CreateSpecificCulture(MiscCulture._enUS));
                    _value = double.Parse(pstring, CultureInfo.CurrentCulture);

                    _log.setDetailVarValues("_value = " + _value);
                    _log.setDetailVarValues("_string = " + _string);
                    _log.setDetailValuesParameter("pstring + " + pstring);
                    _log.setDetail("6----------------------------------");

                }
            }
            catch (Exception ex)
            {
                _log.setDetailVarValues("_value = " + _value);
                _log.setDetailVarValues("_string = " + _string);
                _log.setDetailValuesParameter("pstring + " + pstring);
                _log.setDetail("7----------------------------------");

                _log.generateFileTXTHH(
                    Sistema.establecerObjetoSystemCIISA(),
                    "FormatUtil",
                    "convertStringToDecimal"
                    );

                throw new Exception("Error convirtiendo a decimal", ex);
            }

            return _value;
        }

        public static string applyCurrencyFormat(decimal pamount)
        {
            string _amount = string.Empty;

            //string _tempAmount = string.Format(CultureInfo.CreateSpecificCulture(MiscCulture._enUS), "{0:C}", pamount);
            string _tempAmount = string.Format(CultureInfo.CurrentCulture, "{0:C}", pamount);

            if (_tempAmount.Contains("C"))
            {
                _amount = _tempAmount.Replace("C", string.Empty);
                _tempAmount = _amount;
            }

            if (_tempAmount.Contains(Simbol._colonesSymbol))
            {
                _amount = _tempAmount.Replace(Simbol._colonesSymbol, string.Empty);
                _tempAmount = _amount;
            }

            if (_tempAmount.Contains(Simbol._dolarSymbol))
            {
                _amount = _tempAmount.Replace(Simbol._dolarSymbol, string.Empty);
                _tempAmount = _amount;
            }

            if (_tempAmount.Contains(")"))
            {
                _amount = _tempAmount.Replace(")", string.Empty);
                _tempAmount = _amount;
            }

            if (_tempAmount.Contains("("))
            {
                _amount = _tempAmount.Replace("(", Simbol._hyphen);
                _tempAmount = _amount;
            }

            if (pamount.Equals(string.Empty))
            {
                _amount = "0.00";
            }

            return _amount;
        }

        public static int convertStringToInt(string pstring)
        {
            int _value = Numeric._zeroInteger;

            if (pstring == string.Empty)
            {
                _value = Numeric._zeroInteger;
            }
            else
            {
                _value = int.Parse(pstring);
            }

            return _value;
        }

        public static DateTime convertStringToDateTimeWithTime(string pdate)
        {
            DateTime _date = new DateTime();

            if (!pdate.Equals(string.Empty))
            {
                _date = DateTime.Parse(pdate, CultureInfo.CurrentCulture);
            }

            return _date;
        }

        private static string deleteTimeFromDateTime(string pdate)
        {
            if (pdate.Contains("0:00:00"))
            {
                string _date = pdate.Replace(" 0:00:00", string.Empty);
                pdate = _date;
            }

            if (pdate.Contains("12:00:00"))
            {
                string _date = pdate.Replace(" 12:00:00 AM", string.Empty);
                pdate = _date;
            }

            if (pdate.Contains("12:00:00"))
            {
                string _date = pdate.Replace(" 12:00:00 a.m.", string.Empty);
                pdate = _date;
            }

            if (pdate.Contains("12:00:00"))
            {
                string _date = pdate.Replace(" 12:00:00 a. m.", string.Empty);
                pdate = _date;
            }

            return pdate;
        }

        public static string convertDateOracleToSQLCompact_yyMMdd(string pdate)
        {
            string _date = deleteTimeFromDateTime(pdate);

            Char[] _delimiter = new Char[] { '/' };

            string _newDate = string.Empty;

            foreach (string _subString in _date.Split(_delimiter))
            {
                _newDate = _subString + _newDate;
            }

            return _newDate;
        }
    }
}
