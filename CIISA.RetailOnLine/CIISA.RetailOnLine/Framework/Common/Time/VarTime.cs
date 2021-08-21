using System;
using System.Globalization;

namespace CIISA.RetailOnLine.Framework.Common.Time
{
    public static class VarTime
    {
        #region "formatos"
        private static string _dateFormatCR = "dd/MM/yyyy";
        private static readonly string _dateFormatSQLCE = "yyyy-MM-dd";
        private static readonly string _dateFormatLog = "yyyyMMdd";
        private static readonly string _timeFormat = "hh:mm:ss";
        public static readonly string _timeFormatWithMiliseconds = "hh:mm:ss:ms";
        private static readonly string _meridianFormat = "tt";
        private static readonly string _completeDateTimeFormat = "t";
        private static readonly string _dateTimeFormatSqlite = "yyyy-MM-dd hh:mm:ss.ms";
        private static readonly string _dateTimeFormatSQliteComplete = "yyyy-MM-dd HH:mm:ss";


        private static readonly string _dateFormatDataTable = "yyMMdd";
        #endregion "formatos"

        #region "variables"
        public static string getDateFormatCR()
        {
            return _dateFormatCR;
        }

        public static DateTime getNow()
        {
            return DateTime.Now;
        }

        public static DateTime getNowSQLite()
        {
            string Date = DateTime.Now.ToString(_dateTimeFormatSQliteComplete);

            return DateTime.Parse(Date);
        }

        public static string getLogDate()
        {
            return DateTime.Now.ToString(_dateFormatLog);
        }

        public static string getDateCR()
        {
            return DateTime.Now.ToString(_dateFormatCR);
        }

        public static string getTimeCR()
        {
            return DateTime.Now.ToString(_timeFormat, CultureInfo.CurrentCulture);
        }

        public static string getMeridian()
        {
            return DateTime.Now.ToString(_meridianFormat, CultureInfo.InvariantCulture);
        }

        public static string getCompleteDateTime()
        {
            return DateTime.Now.ToString(_completeDateTimeFormat);
        }

        public static string getSQLCEDate()
        {
            return DateTime.Now.ToString(_dateFormatSQLCE);
        }

        public static string getDataTableDate()
        {
            return DateTime.Now.ToString(_dateFormatDataTable);
        }

        public static string getDateTimeSQLCE(DateTime pdateTime)
        {
            return dateSQLCE(pdateTime) + " " + timeSQLCE(pdateTime) + " " + meridianSQLCE(pdateTime);
        }

        public static string getDateTimeSqlite(DateTime pdateTime)
        {
            return pdateTime.ToString(_dateTimeFormatSqlite);
        }

        public static string getDateTimeSQLiteComplete(DateTime? pdateTime) {

            if (pdateTime != null)
            {
                return ((DateTime)pdateTime).ToString(_dateTimeFormatSQliteComplete);
            }
            else {

                return null;
            }

        }

        public static DateTime convertStringToDateTime(string Date) {

            return Convert.ToDateTime(Date);
        }

        public static string convertDateTimeFromServiceToSQLite(string DateFromService) {

            try
            {
                string fecha2 = string.Empty;
                if (!DateFromService.Equals(string.Empty))
                {
                    DateTime fecha = DateTime.ParseExact(DateFromService, "yyMMdd", CultureInfo.CurrentCulture);
                    fecha2 = getDateTimeSqlite(fecha);
                }
                return fecha2;
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public static string convertDateTimeFromServiceToSQLitePlecaVersion(string DateFromService)
        {
            string cultura = CultureInfo.CurrentCulture.Name;
            try
            {
                string fecha2 = string.Empty;
                if (!DateFromService.Equals(string.Empty))
                {
                    int index = DateFromService.IndexOf(".");
                    string sub = DateFromService.Substring(0, index);
                    sub = sub + DateFromService.Substring(index + 2, 1);

                    DateTime fecha = DateTime.Parse(sub, CultureInfo.CurrentCulture);
                    fecha2 = getDateTimeSqlite(fecha);
                }
                return fecha2;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion "variables"

        #region "metodos"
        public static string dateCR(DateTime pdate)
        {
            return pdate.ToString(_dateFormatCR);
        }

        public static string getDateExpiresCR(Double pdays)
        {
            return DateTime.Now.AddDays(pdays).ToString(_dateFormatCR);
        }

        public static int getDayOfWeek(DateTime pday)
        {
            return (int)pday.DayOfWeek;
        }

        public static int getDayOfWeek()
        {
            return (int)VarTime.getNow().DayOfWeek;
        }

        public static string dateSQLCE(DateTime pdate)
        {
            return pdate.ToString(_dateFormatSQLCE);
        }

        public static string timeSQLCE(DateTime pdate)
        {
            return pdate.ToString(_timeFormat);
        }

        public static string meridianSQLCE(DateTime pdate)
        {
            return pdate.ToString(_meridianFormat, CultureInfo.InvariantCulture);
        }

        public static DateTime getDateExpires(Double pdays)
        {
            string Date = DateTime.Now.AddDays(pdays).ToString(_dateTimeFormatSQliteComplete);

            return DateTime.Parse(Date);
        }

        public static string getDateExpiresSQLCE(Double pdays)
        {
            return DateTime.Now.AddDays(pdays).ToString(_dateFormatSQLCE);
        }
        #endregion "metodos"
    }
}
