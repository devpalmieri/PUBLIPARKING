using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publiparking.Core.Utilities
{
    public class StringHelpers
    {
        private const string m_regExIBAN = @"IT\d{2}[ ][a-zA-Z]\d{3}[ ]\d{4}[ ]\d{4}[ ]\d{4}[ ]\d{4}[ ]\d{3}|IT\d{2}[a-zA-Z]\d{22}";

        public static bool isIBAN(string v_testo)
        {
            Regex v_regex = new Regex(m_regExIBAN);

            return v_regex.IsMatch(v_testo);
        }

        /// <summary>
        /// Restituisce una stringa formattata per i web service
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public static string FormatStringForWS(string pData)
        {
            string wFormatted = pData.Replace("\n", "");
            wFormatted = wFormatted.Replace("\r", "");
            wFormatted = wFormatted.Replace(@"\", @"\\");
            wFormatted = wFormatted.Replace("'", "\'");

            return wFormatted;
        }

        /// <summary>
        /// Estrae una sottostringa, non ritorna MAI null, al più String.Empty
        /// </summary>
        /// <param name="str">Stringa in input</param>
        /// <param name="start0">Indice da cui iniziare, a partire da zero</param>
        /// <param name="length">Numero di caratteri massimo da considerare (puoi impostare int.MaxValue per arrivare fino alla fine della stringa)</param>
        /// <returns></returns>
        public static string SafeSubstring(string str, int start0, int length, string onNullReturn = "")
        {
            if (str == null)
            {
                return onNullReturn;
            }

            return str.Length <= start0 ? String.Empty
                : str.Length - start0 <= length ? str.Substring(start0)
                : str.Substring(start0, length);
        }

        // se instr null o empty restituisce spazi di lunghezza length
        // se instr è più lunga la accorcia troncandola
        // se instr è più corta la espande aggiungendo filler ala fine
        public static string SafeTruncateOrExpandString(string instr, int rquired_length, char filler = ' ')
        {
            var str = SafeSubstring(instr, 0, rquired_length); // => str.Length è <= length
            if (str.Length == rquired_length)
            {
                return str;
            }
            else
            {
                str = String.Concat(str, new String(filler, rquired_length - str.Length));
            }

            return str;
        }

        // se instr null o empty restituisce spazi di lunghezza length
        // se instr è più lunga la accorcia troncandola
        // se instr è più corta la espande aggiungendo filler all'inizio
        public static string SafeTruncateOrExpandBeginningString(string instr, int rquired_length, char filler = ' ')
        {
            var str = SafeSubstring(instr, 0, rquired_length); // => str.Length è <= length
            if (str.Length == rquired_length)
            {
                return str;
            }
            else
            {
                str = String.Concat(new String(filler, rquired_length - str.Length), str);
            }

            return str;
        }


        /// <summary>
        /// Prende gli ultimi "length" caratteri della stringa
        /// </summary>
        /// <param name="str">strings</param>
        /// <param name="length">numero di caratteri che si desidera prendere a partire dalla fine</param>
        /// <returns>Se out of range ritorna String.Empty, mai null</returns>
        public static string SafeRight(string str, int length, string onNullReturn = "")
        {
            if (str == null)
            {
                return onNullReturn;
            }

            return str.Length <= 0 ? String.Empty
                : str.Length <= length ? str
                : str.Substring(str.Length - length, length);
        }

        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            if (s.Length == 1)
            {
                return s.ToUpper();
            }

            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static bool IsDigitsOnly(string strTrimmed, bool canBeNull, bool canBeEmpty)
        {
            if (strTrimmed == null)
            {
                return canBeNull;
            }

            if (strTrimmed == "")
            {
                return canBeEmpty;
            }

            foreach (char c in strTrimmed)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static bool IsDigit(char c)
        {
            return (c >= '0' && c <= '9');
        }

        public static string GetDenominazionsStd(string strTrimmed)
        {
            if (strTrimmed == null)
            {
                return null;
            }

            if (strTrimmed == "")
            {
                return "";
            }

            StringBuilder sbDenStd = new StringBuilder();
            bool spaceAppended = false;
            foreach (char c in strTrimmed)
            {
                if (c == ' ')
                {
                    if (!spaceAppended)
                    {
                        sbDenStd.Append(' ');
                        spaceAppended = true;
                    }
                }
                else if (c >= '0' && c <= '9')
                {
                    sbDenStd.Append(c);
                    spaceAppended = false;
                }
                else if (c >= 'a' && c <= 'z')
                {
                    sbDenStd.Append(c);
                    spaceAppended = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    sbDenStd.Append(c);
                    spaceAppended = false;
                }
                else
                {
                    if (!spaceAppended)
                    {
                        sbDenStd.Append(' ');
                        spaceAppended = true;
                    }
                }
            }

            return sbDenStd.ToString();
        }

        // Non scassiamo nulla, quella che già era usata ritornava true per stringhe vuote!!!
        public static bool IsDigitsOnly(string str)
        {
            //foreach (char c in str)
            //{
            //    if (c < '0' || c > '9')
            //        return false;
            //}
            //return true;
            // Non scassiamo nulla, quella che già era usata ritornava true per stringhe vuote:
            return IsDigitsOnly(str, true, true);
        }

        public static string ReplaceAt(string str, int index, int length, string replace)
        {
            return str
                .Remove(index, Math.Min(length, str.Length - index))
                .Insert(index, replace);
        }

        public static Regex MoreThanOneSpaceRegex = new Regex(@"\s{2,}");

        /// <summary>
        /// Compatibile con Copernico 3 Aci WebService, come da email 28 feb 2019 09:34: <br />
        /// ---<br />
        /// I caratteri ammessi sono quelli corrispondenti ai codici ASCII numero:<br />
        /// 32 (spazio)<br />
        /// 38 (&)<br />
        /// 39 (')<br /> 
        /// dal 44 al  57 ( , - . / numeri)<br />
        /// dal 65 al 90 (solo lettere maiuscole)<br /> 
        /// ---
        /// </summary>
        /// <param name="str">Stringa da convertire</param>
        /// <returns>Stringa con i soli caratteri validi</returns>
        /// <remarks>I caratteri non validi li sostituisco con ASCII 46 (il punto)</remarks>
        public static string ToC3Compatibilty(string str, bool trim = true)
        {
            if (str == null)
            {
                return String.Empty;
            }

            if (str == "")
            {
                return "";
            }

            if (trim)
            {
                str = str.Trim();
            }

            // Per le accentate mi va bene così,il caso generale per i caratteri diacritici
            // è: "CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark"
            // ma attenzione la diacriticità è più generale dell'accento!
            // Qui solo gli accentati presenti sulla tastiera italiana:
            str = str
                .Replace("à", "a'")
                .Replace("è", "e'")
                .Replace("é", "e'")
                .Replace("ì", "i'")
                .Replace("ò", "o'")
                .Replace("ù", "u'")
                .Replace("À", "A'")
                .Replace("È", "E'")
                .Replace("É", "E'")
                .Replace("Ì", "I'")
                .Replace("Ò", "O'")
                .Replace("Ù", "U'");
            StringBuilder sbDenStd = new StringBuilder();
            // ----------------
            str = str.ToUpper();
            // ----------------
            foreach (char c in str)
            {
                // " ", "&", "'"
                if (c == 32 || c == 38 || c == 39)
                {
                    sbDenStd.Append(c);
                }
                // ",-./", 0-9
                else if (c >= 44 && c <= 57)
                {
                    sbDenStd.Append(c);
                }
                // A-Z
                else if (c >= 65 && c <= 90)
                {
                    sbDenStd.Append(c);
                }
                // Se non contemplato
                else
                {
                    sbDenStd.Append(' ');
                }
            }

            // Le '&' non sono valide in XML:
            sbDenStd = sbDenStd.Replace("&", "&amp;");

            // trim e leviamo qualsiasi spazio ripetuto
            string retStr = MoreThanOneSpaceRegex.Replace(
                sbDenStd.ToString().Trim(),
                " ");
            return retStr;
        }

        // Al chiamante la responsabilità di trimmare le stringhe a seconda di ciò che vuole fare
        // ritorna -1 se le string sono uguali (o ambedue vuote o una vuota ed un'altra null)
        // Esempi:
        //      int numOfCharsToConsider = 5;
        //      string sl, sr;
        //
        //      // "Uguali", diciamo!
        //      sl = null;
        //      sr = String.Empty;
        //      bool bdne = StringHelpers.AreFirstNCharEqual(sl, sr, numOfCharsToConsider);
        //
        //      // Diverse
        //      sl = "1234567";
        //      sr = "1";
        //      bool bda = StringHelpers.AreFirstNCharEqual(sl, sr, numOfCharsToConsider);
        //
        //      // Diverse
        //      sl = "12";
        //      sr = "1";
        //      bool bdb = StringHelpers.AreFirstNCharEqual(sl, sr, numOfCharsToConsider);
        //
        //      // Diverse
        //      sl = "1a";
        //      sr = "1b";
        //      bool bdc = StringHelpers.AreFirstNCharEqual(sl, sr, numOfCharsToConsider);
        //
        //      // Diverse
        //      sl = "1234a";
        //      sr = "1234b";
        //      bool bb = StringHelpers.AreFirstNCharEqual(sl, sr, numOfCharsToConsider);
        //
        //      // Diverse
        //      sl = "12a";
        //      sr = "12b";
        //      bool bc = StringHelpers.AreFirstNCharEqual(sl, sr, numOfCharsToConsider);
        //
        //      // Uguali
        //      sl = "123";
        //      sr = "123";
        //      bool bd = StringHelpers.AreFirstNCharEqual(sl, sr, numOfCharsToConsider);
        //
        //      // Uguali
        //      sl = "1234567";
        //      sr = "12345a";
        //      bool be = StringHelpers.AreFirstNCharEqual(sl, sr, numOfCharsToConsider);
        public static bool AreFirstNCharEqual(string sl, string sr, int numOfCharsToConsider = 5, bool bignoreCase = true)
        {
            bool bRet;
            bool nowr = string.IsNullOrWhiteSpace(sr);
            bool nowl = string.IsNullOrWhiteSpace(sl);
            if (nowr && nowl)
            {
                // tutte e due vuote o null: le consideriamo uguali
                return true;
            }

            sl = sl ?? "";
            sr = sr ?? "";

            int rLen = sr.Length;
            int lLen = sl.Length;
            int minLen = Math.Min(rLen, lLen);
            int maxLen = Math.Max(rLen, lLen);
            if (minLen != maxLen && minLen < numOfCharsToConsider)
            {
                // Se le due le stringhe sono di lunghezza diversa,
                // mi basta che una sia più corta di numOfCharsToConsider
                // per dire che hanno i primi caratteri diversi
                // (il primo carattere diverso è quello in (numOfCharsToConsider-minLen) di quella più lunga
                return false;
            }

            // da qui le 2 stringhe o sono più lunghe o uguali di numOfCharsToConsider o sono meno lunghe ma hanno la stessa lunghezza!
            // quindi non commetto errori nel considerare il minimo tra tutte le 3 lunghezze,
            // (infatti non di deve sbagliare nel considerare le i primi caratteri uguali quando sono inferiori al numOfCharsToConsider!)
            int iMax = Math.Min(numOfCharsToConsider, minLen);
            if (bignoreCase)
            {
                sl = sl.ToUpper();
                sr = sr.ToUpper();
            }

            for (int i = 0; i < iMax; ++i)
            {
                if (sl[i] != sr[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreFirst5CharEqual(string sl, string sr, bool bignoreCase = true)
        {
            return AreFirstNCharEqual(sl, sr, 5, bignoreCase);
        }
    }
}