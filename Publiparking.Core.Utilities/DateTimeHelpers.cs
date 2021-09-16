using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Utilities
{
    public static class DateTimeHelpers
    {
        public enum MonthEnum : int
        {
            GENNAIO = 1,
            FEBBRAIO = 2,
            MARZO = 3,
            APRILE = 4,
            MAGGIO = 5,
            GIUGNO = 6,
            LUGLIO = 7,
            AGOSTO = 8,
            SETTEMBRE = 9,
            OTTOBRE = 10,
            NOVEMBRE = 11,
            DICEMBRE = 12,
        }
        public enum DayEnum : int
        {
            Lunedì = 1,
            Martedì = 2,
            Mercoledì = 3,
            Govedì = 4,
            Venerdì = 5,
            Sabato = 6,
            Domenica = 7,

        }
        // https://stackoverflow.com/a/25136172
        // https://github.com/markwhitaker/MonthDiff
        public static int GetTotalMonthDiff(DateTime dtStart, DateTime dtEnd)
        {
            var earlyDate = (dtStart > dtEnd) ? dtEnd.Date : dtStart.Date;
            var lateDate = (dtStart > dtEnd) ? dtStart.Date : dtEnd.Date;

            // Start with 1 month's difference and keep incrementing
            // until we overshoot the late date
            int monthsDiff = 1;
            while (earlyDate.AddMonths(monthsDiff) <= lateDate)
            {
                monthsDiff++;
            }

            return monthsDiff - 1;
        }

        public static readonly DateTime MinSqlServerDatetime = new DateTime(1753, 1, 1);
        public static readonly DateTime MaxSqlServerDatetime = new DateTime(9999, 12, 31);

        public static DateTime ClampSqlServerDatetime(DateTime dt)
        {
            if (dt < MinSqlServerDatetime)
            {
                return MinSqlServerDatetime;
            }

            if (dt > MaxSqlServerDatetime)
            {
                return MaxSqlServerDatetime;
            }

            return dt;
        }

        public static bool IsValidDatetimeMSSQl(DateTime dt)
        {
            if (dt.Year < 1753)
            {
                return false;
            }

            if (dt.Year > 9999)
            {
                return false;
            }

            return true;
        }
        public static string GetMonthName(int nMonth)
        {
            var month = (MonthEnum)nMonth;
            return month.ToString();
        }
        public static string GetDayName(int nDay)
        {
            var day = (MonthEnum)nDay;
            return day.ToString();
        }

        public static bool isHoliday(DateTime p_data)
        {

            bool risp = false;
            List<string> v_giorniFestivi = new List<string>()
            {"01/01","06/01","25/04","01/05","02/06","15/08","01/11","08/12","25/12",
             "26/12",
             //"09/04/2012","01/04/2013","21/04/2014","06/04/2015","28/03/2016","17/04/2017","02/04/2018","22/04/2019",
             "13/04/2020","05/04/2021","18/04/2022","10/04/2023","01/04/2024","21/04/2025","06/04/2026","29/03/2027","17/04/2028",
             "02/04/2029","22/04/2030","14/04/2031","29/03/2032","18/04/2033","10/04/2034","26/03/2035","14/04/2036","06/04/2037",
             "26/04/2038","11/03/2039","02/04/2040","22/04/2041","07/04/2042","03/03/2043","18/04/2044","10/04/2045","26/03/2046",
             "15/04/2047","06/04/2048","19/04/2048","11/04/2050"
            };
            IList<DateTime> v_feste = new List<DateTime>();
            string v_annoCorrente = DateTime.Now.Year.ToString();
            foreach (string v_giorno in v_giorniFestivi)
            {
                DateTime v_data;
                string v_giorno_mese_anno;
                if (v_giorno.Length == 5)
                {
                    v_giorno_mese_anno = v_giorno + "/" + v_annoCorrente;
                }
                else
                {
                    v_giorno_mese_anno = v_giorno;
                }
                v_data = DateTime.ParseExact(v_giorno_mese_anno, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                v_feste.Add(v_data);
            }

            if (v_feste.Contains(p_data) | p_data.DayOfWeek == DayOfWeek.Sunday)
                risp = true;

            return risp;
        }

        public class TryParseDateError
        {
            public string error { get; set; }
            public string field { get; set; }
        }

        public class TryParseDateResult
        {
            private DateTime? _parsedDate;
            public DateTime ParsedDate
            {
                get
                {
                    if (HasError)
                    {
                        throw new Exception("Cannot get date if HasErrors is true");
                    }

                    return _parsedDate.Value;
                }
            }

            // Se ParsedDate è null ci sono errori
            public TryParseDateError Error { get; set; }

            public bool HasError => Error != null;

            public TryParseDateResult(TryParseDateError error)
            {
                Error = error;
                _parsedDate = null;
            }

            public TryParseDateResult(DateTime parsedDate)
            {
                Error = null;
                _parsedDate = parsedDate;
            }
        }

        public static TryParseDateResult TryParseDate_ddMMyyyy(
            string inputDate,
            DateTime dtNowDate,
            string modelFieldName,
            bool bCheckDateIsNotInTheFuture)
        {
            if (String.IsNullOrWhiteSpace(inputDate))
            {
                return new TryParseDateResult(new TryParseDateError
                {
                    error = "Inserire una data",
                    field = modelFieldName
                });
            }
            if (!DateTime.TryParseExact(inputDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtParsed))
            {
                return new TryParseDateResult(new TryParseDateError
                {
                    error = "Inserire una data valida",
                    field = modelFieldName
                });
            }
            if (bCheckDateIsNotInTheFuture && dtParsed > dtNowDate)
            {
                return new TryParseDateResult(new TryParseDateError
                {
                    error = $"La data deve essere <= {dtNowDate:dd/MM/yyyy}",
                    field = modelFieldName
                });

            }
            if (!DateTimeHelpers.IsValidDatetimeMSSQl(dtParsed))
            {
                // N.B. o troppo ne futuro o troppo nel passato, basta controllare sia maggiore o minore di oggi!
                if (dtParsed < dtNowDate)
                {
                    return new TryParseDateResult(new TryParseDateError
                    {
                        error = $"Data troppo vecchia",
                        field = modelFieldName
                    });
                }
                else
                {
                    return new TryParseDateResult(new TryParseDateError
                    {
                        error = $"Data troppo grande",
                        field = modelFieldName
                    });
                }
            }
            return new TryParseDateResult(dtParsed);
        }
    } // class
}