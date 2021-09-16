using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Utilities
{
    public static class CodiceFiscaleHelperFunctions
    {
        static string VOCALI = "AEIOU";

        /// <summary>
        /// Separa le consonanti dalle vocali della stringa in input
        /// </summary>
        /// <param name="input"></param>
        /// <param name="consonanti"></param>
        /// <param name="vocali"></param>
        public static void isolaLettere(string input, out string consonanti, out string vocali)
        {
            vocali = "";
            consonanti = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(Convert.ToChar(input.Substring(i, 1))))
                {
                    if (VOCALI.Contains(input.Substring(i, 1).ToUpper()))
                    {
                        vocali = vocali + input.Substring(i, 1).ToUpper();
                    }
                    else
                    {
                        consonanti = consonanti + input.Substring(i, 1).ToUpper();
                    }
                }
            }
        }

        /// <summary>
        /// Restituisce il codice del cognome
        /// </summary>
        /// <param name="consonanti"></param>
        /// <param name="vocali"></param>
        /// <returns></returns>
        public static string trovaCodiceCognome(string consonanti, string vocali)
        {
            if (consonanti.Length >= 3)
            {
                string cognome = (consonanti.Substring(0, 3)).ToUpper();
                return cognome;
            }

            if (consonanti.Length == 2 && vocali.Length >= 1)
            {
                string cognome = (consonanti + vocali.Substring(0, 1)).ToUpper();
                return cognome;
            }

            if (consonanti.Length == 1 && vocali.Length >= 2)
            {
                string cognome = (consonanti + vocali.Substring(0, 2)).ToUpper();
                return cognome;
            }

            if (consonanti.Length == 1 && vocali.Length == 1)
            {
                string cognome = (consonanti + vocali + "X").ToUpper();
                return cognome;
            }

            if (consonanti.Length == 2 && vocali.Length == 0)
            {
                string cognome = (consonanti + "X").ToUpper();
                return cognome;
            }

            if (consonanti.Length == 1 && vocali.Length == 0)
            {
                string cognome = (consonanti + "XX").ToUpper();
                return cognome;
            }

            if (consonanti.Length == 0 && vocali.Length == 1)
            {
                string cognome = (vocali + "XX").ToUpper();
                return cognome;
            }

            if (consonanti.Length == 0 && vocali.Length == 2)
            {
                string cognome = (vocali + "X").ToUpper();
                return cognome;
            }

            if (consonanti.Length == 0 && vocali.Length > 2)
            {
                string cognome = (vocali.Substring(0, 3)).ToUpper();
                return cognome;
            }

            return vocali;
        }

        /// <summary>
        /// Restituisce il codice del nome
        /// </summary>
        /// <param name="consonanti"></param>
        /// <param name="vocali"></param>
        /// <returns></returns>
        public static string trovaCodiceNome(string consonanti, string vocali)
        {
            if (consonanti.Length > 3)
            {
                string nome = (consonanti.Substring(0, 1) + consonanti.Substring(2, 2)).ToUpper();
                return nome;
            }

            if (consonanti.Length == 3)
            {
                string nome = consonanti.ToUpper();
                return nome;
            }

            if (consonanti.Length == 2 && vocali.Length >= 1)
            {
                string nome = (consonanti + vocali.Substring(0, 1)).ToUpper();
                return nome;
            }

            if (consonanti.Length == 1 && vocali.Length >= 2)
            {
                string nome = (consonanti + vocali.Substring(0, 2)).ToUpper();
                return nome;
            }

            if (consonanti.Length == 1 && vocali.Length == 1)
            {
                string nome = (consonanti + vocali + "X").ToUpper();
                return nome;
            }

            if (consonanti.Length == 2 && vocali.Length == 0)
            {
                string nome = (consonanti + "X").ToUpper();
                return nome;
            }

            if (consonanti.Length == 0 && vocali.Length == 2)
            {
                string nome = (vocali + "X").ToUpper();
                return nome;
            }

            if (consonanti.Length == 1 && vocali.Length == 0)
            {
                string nome = (consonanti + "XX").ToUpper();
                return nome;
            }

            if (consonanti.Length == 0 && vocali.Length == 1)
            {
                string nome = (vocali + "XX").ToUpper();
                return nome;
            }

            if (consonanti.Length == 0 && vocali.Length > 2)
            {
                string nome = (vocali.Substring(0, 3)).ToUpper();
                return nome;
            }

            return "Errore";
        }

        /// <summary>
        /// Restituisce il codice della data di nascita - sesso
        /// </summary>
        /// <param name="mese"></param>
        /// <param name="anno"></param>
        /// <param name="giorno"></param>
        /// <param name="sesso"></param>
        /// <returns></returns>
        public static string trovaCodiceDataSesso(string mese, string anno, string giorno, string sesso)
        {
            string codice = string.Empty;

            if (anno.Length < 4)
            {
                while (anno.Length < 4)
                {
                    anno = "0" + anno;
                }
            }

            codice = anno.Substring(2, 2);

            switch (mese)
            {
                case "1":
                case "01":
                case "GENNAIO": codice = codice + "A"; break;
                case "2":
                case "02":
                case "FEBBRAIO": codice = codice + "B"; break;
                case "3":
                case "03":
                case "MARZO": codice = codice + "C"; break;
                case "4":
                case "04":
                case "APRILE": codice = codice + "D"; break;
                case "5":
                case "05":
                case "MAGGIO": codice = codice + "E"; break;
                case "6":
                case "06":
                case "GIUGNO": codice = codice + "H"; break;
                case "7":
                case "07":
                case "LUGLIO": codice = codice + "L"; break;
                case "8":
                case "08":
                case "AGOSTO": codice = codice + "M"; break;
                case "9":
                case "09":
                case "SETTEMBRE": codice = codice + "P"; break;
                case "10":
                case "OTTOBRE": codice = codice + "R"; break;
                case "11":
                case "NOVEMBRE": codice = codice + "S"; break;
                case "12":
                case "DICEMBRE": codice = codice + "T"; break;
            }

            if (sesso == "M" || sesso == "1")
            {
                if (giorno.Length == 1)
                {
                    giorno = "0" + giorno;
                }

                codice = codice + giorno.ToString();
            }
            else
            {
                codice = codice + (Convert.ToInt32(giorno) + 40).ToString();
            }

            return codice;
        }

        /// <summary>
        /// Restituisce il codice di controllo
        /// </summary>
        /// <param name="cf"></param>
        /// <returns></returns>
        public static char trovaCodiceControllo(string cf)
        {
            int[] arrayDispari = { 1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 2, 4, 18, 20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23 };
            int totale = 0;
            byte[] arrayCodice = new byte[15];

            arrayCodice = Encoding.ASCII.GetBytes(cf.ToUpper());

            for (int i = 0; i < cf.Length; i++)
            {
                if ((i + 1) % 2 == 0)
                {
                    if (char.IsLetter(cf, i))
                        totale += arrayCodice[i] - (byte)'A';
                    else
                        totale += arrayCodice[i] - (byte)'0';
                }
                else
                {
                    if (char.IsLetter(cf, i))
                        totale += arrayDispari[(arrayCodice[i] - (byte)'A')];
                    else
                        totale += arrayDispari[(arrayCodice[i] - (byte)'0')];
                }
            }

            totale %= 26;

            char lettera = (char)(totale + 'A');

            return lettera;
        }
    }
}
