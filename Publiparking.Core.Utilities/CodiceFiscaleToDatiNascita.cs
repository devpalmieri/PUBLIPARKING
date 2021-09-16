using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Utilities
{
    public interface IDatiDiNascita
    {
        string CF { get; }


        string SessoStr { get; } // "M"/"F"
        int GiornoDiNascita { get; }
        int MeseDiNascita { get; }
        int AnnoDiNascita { get; }
        string CodiceBelfiore { get; }
    }

    public class DatiDiNascita : IDatiDiNascita
    {
        public string CF { get; set; }

        public string SessoStr { get; set; } // "M"/"F"

        public int GiornoDiNascita { get; set; }
        public int MeseDiNascita { get; set; }
        public int AnnoDiNascita { get; set; }
        public string CodiceBelfiore { get; set; }

        public static string CalcolaCodiceFiscale(IDatiDiNascita datiNascitaDestinatario, string nome, string cognome)
        {
            return CodiceFiscaleToDatiNascita.CalcolaCodiceFiscale(datiNascitaDestinatario, nome, cognome);
        }
    }

    public class CodiceFiscaleToDatiNascita : IDatiDiNascita
    {
        public static readonly CodiceFiscaleToDatiNascita Empty = new CodiceFiscaleToDatiNascita();

        #region Private mv

        private string _cf;

        private char _sesso;
        private string _sessoStr;
        private int _giornoDiNascita;
        private int _annoDiNascita_2cifre;
        private bool _annoNascitaIsMillenial;
        private int _meseDiNascita;
        private string _meseDiNascitaTesto;
        private string _codiceBelfiore;
        private char _check;

        #endregion

        public string CF { get { return _cf; } }

        // 'M'/'F'
        public char Sesso { get { return _sesso; } }
        public string SessoStr { get { return _sessoStr; } }
        public int GiornoDiNascita { get { return _giornoDiNascita; } }
        public string GiornoDiNascita2Caratteri { get { return _giornoDiNascita.ToString("00"); } }
        public int MeseDiNascita { get { return _meseDiNascita; } }
        public string MeseDiNascita2Caratteri { get { return _meseDiNascita.ToString("00"); } }
        public string MeseDiNascitaTesto { get { return _meseDiNascitaTesto; } }

        public int GetAnnoDiNascita1900() { return 1900 + _annoDiNascita_2cifre; }
        public int GetAnnoDiNascita2000() { return 2000 + _annoDiNascita_2cifre; }
        public int AnnoDiNascita { get { return (_annoNascitaIsMillenial ? 2000 : 1900) + _annoDiNascita_2cifre; } }
        public string AnnoDiNascita4Caratteri { get { return AnnoDiNascita.ToString("0000"); } }

        public string CodiceBelfiore { get { return _codiceBelfiore; } }
        public char CodiceDiControllo { get { return _check; } }

        /// <summary>
        /// Imposta se anno di nascita parte da 1900 (millenial=false) o 2000 (millenial = true)
        /// Lo puoi stabilire calcolando il codice fiscale
        /// </summary>
        /// <param name="millenial">millenial = true parte da 2000, altrimenti da 1900</param>
        public void SetAnnoDiNascitaIsMillenial(bool millenial)
        {
            _annoNascitaIsMillenial = millenial;
        }

        public bool CanBeMillenial(int annoDueCifre)
        {
            DateTime dtNow = DateTime.Now;
            if (annoDueCifre + 2000 > dtNow.Year) // Cioè se siamo nel 2018 e annoDueCifre > 18 sicuro è un anno del 1900
            {
                return false;
            }
            return true;
        }

        private void SetUnknownData(string cf)
        {
            _cf = cf;

            _sesso = ' ';
            _sessoStr = " ";
            _giornoDiNascita = 0;
            _annoDiNascita_2cifre = 0;
            _meseDiNascita = 0;
            _meseDiNascitaTesto = String.Empty;
            _codiceBelfiore = String.Empty;
            _check = ' ';
        }

        // TODO: capire se l'anno è 1900 o 2000 (facile), per ora solo 1900 per fretta
        //
        // belfioreNascitaTransformer: 
        /// <summary>
        /// Dati di nascita a partire dal CF
        /// </summary>
        /// <param name="cf">CF da analizzare</param>
        /// <param name="belfioreNascitaTransformer">callback che accetta in input il codice istat nel cf e lo ritorna trasformato</param>
        /// <param name="allowWrongWithSpaces">Non lancia eccezioni se il CF inizia o termina con spazi (" "), impostando a valori di default (errati) i dati</param>
        public CodiceFiscaleToDatiNascita(string cf, Func<string, string> belfioreNascitaTransformer, bool allowWrongWithSpaces)
        {
            if (String.IsNullOrEmpty(cf))
            {
                throw new ArgumentNullException(nameof(cf));
            }

            // =========================================
            cf = cf.Trim().ToUpper();
            // =========================================

            if (cf.Length != 16)
            {
                if (allowWrongWithSpaces)
                {
                    // Sono quelli che avevano lunghezza errata e che ho aggiustato a mano: non posso ricavare i dati
                    SetUnknownData(cf);
                    return;
                }
                else
                {
                    throw new ArgumentException($"Lunghezza {nameof(cf)} non valida");
                }
            }

            _cf = cf;
            _annoNascitaIsMillenial = false; // Default 1900


            string annoNascitaDueCifreStr = StringHelpers.SafeSubstring(cf, 6, 2);
            if (!int.TryParse(annoNascitaDueCifreStr, out _annoDiNascita_2cifre))
            {
                throw new ArgumentException($"{cf} - codice fiscale non valido - impossibile stabilire anno di nascita");
            }

            _giornoDiNascita = TrovaGiornoDiNascita(cf);
            _sessoStr = TrovaSesso(_giornoDiNascita, cf);
            _sesso = (_sessoStr ?? "").Length > 0 ? _sessoStr[0] : ' ';

            string meseNascitaChar = StringHelpers.SafeSubstring(cf, 8, 1);
            switch (meseNascitaChar)
            {
                case "A": _meseDiNascita = 1; _meseDiNascitaTesto = "gennaio"; break;
                case "B": _meseDiNascita = 2; _meseDiNascitaTesto = "febbraio"; break;
                case "C": _meseDiNascita = 3; _meseDiNascitaTesto = "marzo"; break;
                case "D": _meseDiNascita = 4; _meseDiNascitaTesto = "aprile"; break;
                case "E": _meseDiNascita = 5; _meseDiNascitaTesto = "maggio"; break;
                case "H": _meseDiNascita = 6; _meseDiNascitaTesto = "giugno"; break;
                case "L": _meseDiNascita = 7; _meseDiNascitaTesto = "luglio"; break;
                case "M": _meseDiNascita = 8; _meseDiNascitaTesto = "agosto"; break;
                case "P": _meseDiNascita = 9; _meseDiNascitaTesto = "settembre"; break;
                case "R": _meseDiNascita = 10; _meseDiNascitaTesto = "ottobre"; break;
                case "S": _meseDiNascita = 11; _meseDiNascitaTesto = "novembre"; break;
                case "T": _meseDiNascita = 12; _meseDiNascitaTesto = "dicembre"; break;
                default:
                    throw new ArgumentException($"{cf} - codice fiscale non valido - impossibile stabilire il mese");
            }

            //123456789012345
            //BAOXWU81D58Z210D
            _codiceBelfiore = StringHelpers.SafeSubstring(cf, 11, 4);
            if (belfioreNascitaTransformer != null)
            {
                _codiceBelfiore = belfioreNascitaTransformer(_codiceBelfiore);
            }
            _check = StringHelpers.SafeSubstring(cf, 15, 1)[0];
        }

        public static int TrovaGiornoDiNascita(string cf)
        {
            string dayOfBirthStr = StringHelpers.SafeSubstring(cf, 9, 2);
            if (!int.TryParse(dayOfBirthStr, out int giornoDiNascita))
            {
                throw new ArgumentException($"{cf} - codice fiscale non valido - impossibile stabilire giorno di nascita");
            }
            return giornoDiNascita;
        }

        public static string TrovaSesso(int giornoDiNascita, string cf)
        {
            string sessoStr;


            if (giornoDiNascita > 0 && giornoDiNascita <= 31)
            {
                sessoStr = "M";
            }
            else if (giornoDiNascita > 40 && giornoDiNascita <= 71)
            {
                giornoDiNascita -= 40;
                sessoStr = "F";
            }
            else { throw new ArgumentException($"{cf} - codice fiscale non valido - impossibile stabilire il sesso"); }

            return sessoStr;
        }

        private CodiceFiscaleToDatiNascita()
        {
            SetUnknownData(String.Empty);
        }

        public static string CalcolaCodiceFiscale(IDatiDiNascita datiNascitaDestinatario, string nome, string cognome)
        {
            string cfCalcolato = CodiceFiscaleUtilities.CalcolaCF(
                            nome,
                            cognome,
                            datiNascitaDestinatario.GiornoDiNascita.ToString(),
                            datiNascitaDestinatario.MeseDiNascita.ToString(),
                            datiNascitaDestinatario.AnnoDiNascita.ToString(),
                            datiNascitaDestinatario.SessoStr,
                            datiNascitaDestinatario.CodiceBelfiore);
            return cfCalcolato;
        }

        public static bool VerificaCodiceFiscale(IDatiDiNascita datiNascitaDestinatario, string nome, string cognome)
        {
            string cfCalcolato = CalcolaCodiceFiscale(datiNascitaDestinatario, nome, cognome);
            return 0 == String.Compare(cfCalcolato, datiNascitaDestinatario.CF, true);
        }

    }
}
