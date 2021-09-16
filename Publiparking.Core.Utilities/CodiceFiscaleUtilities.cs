using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Utilities
{
    public static class CodiceFiscaleUtilities
    {
        // Aggiungo funzione calcolo CF da usare in caso già si abbia il belfiore del comune o dello stato estero
        // (utile per verifiche di correttezza CF)
        public static string CalcolaCF(string nome, string cognome, string giorno, string mese, string anno, string sesso, string belfioreComune_o_CodiceStatoEstero)
        {
            string codDataSesso = CodiceFiscaleHelperFunctions.trovaCodiceDataSesso(mese.ToUpper(), anno, giorno, sesso);
            string CF = string.Empty;

            // Controlla che nuovo codice funzione e leva "|| true"
#if ORIGINALE || true 
            string StringCognomeCons, StringCognomeVoc;
            string StringNomeCons, StringNomeVoc;

            nome = (nome ?? "").Trim().Replace(" ", "");
            cognome = (cognome ?? "").Trim().Replace(" ", "");

            nome = replaceLettereAccentate(nome);
            cognome = replaceLettereAccentate(cognome);

            CodiceFiscaleHelperFunctions.isolaLettere(cognome, out StringCognomeCons, out StringCognomeVoc);
            CodiceFiscaleHelperFunctions.isolaLettere(nome, out StringNomeCons, out StringNomeVoc);

            string codCognome = CodiceFiscaleHelperFunctions.trovaCodiceCognome(StringCognomeCons, StringCognomeVoc);
            string codNome = CodiceFiscaleHelperFunctions.trovaCodiceNome(StringNomeCons, StringNomeVoc);

            CF = codCognome + codNome + codDataSesso + belfioreComune_o_CodiceStatoEstero;
            CF = CF + CodiceFiscaleHelperFunctions.trovaCodiceControllo(CF);
#else
            string p6 = CalcolaPrimie6LettereCf(nome, cognome);
            string p15 = string.Concat( p6, codDataSesso, belfioreComune_o_CodiceStatoEstero);
            CF = string.Concat(p15, CodiceFiscaleHelperFunctions.trovaCodiceControllo(CF));
#endif

            return CF;
        }

        public static string CalcolaPrimie6LettereCf(string nome, string cognome)
        {
            string StringCognomeCons, StringCognomeVoc;
            string StringNomeCons, StringNomeVoc;

            nome = (nome ?? "").Trim().Replace(" ", "");
            cognome = (cognome ?? "").Trim().Replace(" ", "");

            nome = replaceLettereAccentate(nome);
            cognome = replaceLettereAccentate(cognome);

            CodiceFiscaleHelperFunctions.isolaLettere(cognome, out StringCognomeCons, out StringCognomeVoc);
            CodiceFiscaleHelperFunctions.isolaLettere(nome, out StringNomeCons, out StringNomeVoc);

            string codCognome = CodiceFiscaleHelperFunctions.trovaCodiceCognome(StringCognomeCons, StringCognomeVoc);
            string codNome = CodiceFiscaleHelperFunctions.trovaCodiceNome(StringNomeCons, StringNomeVoc);

            return string.Concat(codCognome, codNome);
        }

        private static string replaceLettereAccentate(string v_stringa)
        {
            v_stringa = v_stringa.ToLower().Replace("è", "e")
                                           .Replace("é", "e")
                                           .Replace("à", "a")
                                           .Replace("ì", "i")
                                           .Replace("ù", "u")
                                           .Replace("ò", "o");

            return v_stringa;
        }

        private static char replaceLetter(char p_letter)
        {
            switch (p_letter)
            {
                case 'L':
                    return '0';
                case 'M':
                    return '1';
                case 'N':
                    return '2';
                case 'P':
                    return '3';
                case 'Q':
                    return '4';
                case 'R':
                    return '5';
                case 'S':
                    return '6';
                case 'T':
                    return '7';
                case 'U':
                    return '8';
                case 'V':
                    return '9';
                default:
                    return 'X';
            }
        }

        //Per gestire l'omocodia
        public static string transformCodiceFiscale(string p_codiceFiscale)
        {
            if (p_codiceFiscale.Length < 16)
            {
                return p_codiceFiscale;
            }
            p_codiceFiscale = p_codiceFiscale.ToUpper();

            StringBuilder v_codiceFiscale = new StringBuilder(p_codiceFiscale);

            if (char.IsLetter(p_codiceFiscale[6]))
            {
                v_codiceFiscale[6] = replaceLetter(p_codiceFiscale[6]);
            }

            if (char.IsLetter(p_codiceFiscale[7]))
            {
                v_codiceFiscale[7] = replaceLetter(p_codiceFiscale[7]);
            }

            if (char.IsLetter(p_codiceFiscale[9]))
            {
                v_codiceFiscale[9] = replaceLetter(p_codiceFiscale[9]);
            }

            if (char.IsLetter(p_codiceFiscale[10]))
            {
                v_codiceFiscale[10] = replaceLetter(p_codiceFiscale[10]);
            }

            if (char.IsLetter(p_codiceFiscale[12]))
            {
                v_codiceFiscale[12] = replaceLetter(p_codiceFiscale[12]);
            }

            if (char.IsLetter(p_codiceFiscale[13]))
            {
                v_codiceFiscale[13] = replaceLetter(p_codiceFiscale[13]);
            }

            if (char.IsLetter(p_codiceFiscale[14]))
            {
                v_codiceFiscale[14] = replaceLetter(p_codiceFiscale[14]);
            }

            p_codiceFiscale = v_codiceFiscale.ToString();

            p_codiceFiscale = p_codiceFiscale.Substring(0, 15) + CodiceFiscaleHelperFunctions.trovaCodiceControllo(p_codiceFiscale.Substring(0, 15));

            return p_codiceFiscale;
        }
    }
}
