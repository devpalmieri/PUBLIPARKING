using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Publiparking.Core.Utilities
{
    public static class cVerificaGiorniFestivi
    {
        public static string CheckFestivi(DateTime date, bool IncludeSabato)
        {
            if (Festivi(date.Year, date.Month, date.Day))
            {
                return date.DayOfWeek.ToString();
            }

            if (IncludeSabato)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday ||
                    date.DayOfWeek == DayOfWeek.Sunday)
                {
                    return date.DayOfWeek.ToString();
                }
            }
            else
            {
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    return date.DayOfWeek.ToString();
                }
            }

            return "";
        }

        /// Verifica se il giorno della data è un festivo
        /// </summary>
        /// <param name="anno">Anno a 4 cifre</param>
        /// <param name="mese">Mese a 2 cifre</param>
        /// <param name="giorno">Giorno a 2 cifre</param>
        /// <returns>Vero o Falso</returns>
        private static bool Festivi(int anno, int mese, int giorno)
        {
            DateTime dt;
            try
            {
                dt = new DateTime(anno, mese, giorno);
                if (DayOfWeek.Saturday.Equals(dt.DayOfWeek))
                    return true;

                if (DayOfWeek.Sunday.Equals(dt.DayOfWeek))
                    return true;

                /*capodanno*/
                if (giorno == 1 && mese == 1)
                    return true;

                /*6 gennaio epifania*/
                if (giorno == 6 && mese == 1)
                    return true;

                /*25 aprile*/
                if (giorno == 25 && mese == 4)
                    return true;

                /*1 maggio*/
                if (giorno == 1 && mese == 5)
                    return true;

                /*29 giugno s.pietro e paolo*/
                if (giorno == 29 && mese == 6)
                    return true;

                /*15 agosto*/
                if (giorno == 15 && mese == 8)
                    return true;

                /*2 giugno*/
                if (giorno == 2 && mese == 6)
                    return true;

                /*2 novembre*/
                if (giorno == 2 && mese == 11)
                    return true;

                /*8 dicembre*/
                if (giorno == 8 && mese == 12)
                    return true;

                /*natale*/
                if (giorno == 25 && mese == 12)
                    return true;

                /*s stefano*/
                if (giorno == 26 && mese == 12)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}