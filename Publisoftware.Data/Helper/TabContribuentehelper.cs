using Publisoftware.Data.Interface;
using Publisoftware.Utilities;
using Publisoftware.Utility.StringHandlling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.Helper
{
    public class TabContribuentehelper
    {


        // private static readonly Regex _codiceCivicoCheckRegex = new Regex(tab_contribuente.codiceCivicoRegex);
        private static readonly Regex _pecCheckRegex = new Regex(tab_contribuente.emailPecRegularExpression);
        private static readonly Regex _capCheckRegex = new Regex(tab_contribuente.capRegex);
        public static Regex RegexCAP { get { return _capCheckRegex; } }

        private static void RegolaLunghezzaCampiContribuente(ref tab_contribuente tContrib)
        {
            // Campi non comuni

            tContrib.cod_stato_contribuente = RegolaLunghezzaCampo(tContrib.cod_stato_contribuente, 7);
            tContrib.data_inizio_stato = DateTimeHelpers.ClampSqlServerDatetime(tContrib.data_inizio_stato);
            if (tContrib.data_fine_stato != null)
            {
                tContrib.data_fine_stato = DateTimeHelpers.ClampSqlServerDatetime(tContrib.data_fine_stato.Value);
            }
            tContrib.indirizzo_ar = RegolaLunghezzaCampo(tContrib.indirizzo_ar, 255);
            tContrib.ccia = RegolaLunghezzaCampo(tContrib.ccia, 50);
            tContrib.flag_web = RegolaLunghezzaCampo(tContrib.flag_web, 2);
            tContrib.fax = RegolaLunghezzaCampo(tContrib.fax, 30);
            tContrib.flag_verifica_stati = RegolaLunghezzaCampo(tContrib.flag_verifica_stati, 1);

            if (tContrib.data_stato != null)
            {
                tContrib.data_stato = DateTimeHelpers.ClampSqlServerDatetime(tContrib.data_stato.Value);
            }
            tContrib.flag_verifica_pec = RegolaLunghezzaCampo(tContrib.flag_verifica_pec, 1);
            tContrib.flag_ricerca_eredi = RegolaLunghezzaCampo(tContrib.flag_ricerca_eredi, 1);
            tContrib.flag_ricerca_nuovo_referente_pg = RegolaLunghezzaCampo(tContrib.flag_ricerca_nuovo_referente_pg, 1);
        }
        private static void RegolaLunghezzaCampiReferente(ref tab_referente tRef)
        {
            // Campi non comuni

            tRef.cod_stato_referente = RegolaLunghezzaCampo(tRef.cod_stato_referente, 7);
            if (tRef.data_inizio_stato != null)
            {
                tRef.data_inizio_stato = DateTimeHelpers.ClampSqlServerDatetime(tRef.data_inizio_stato.Value);
            }
            if (tRef.data_fine_stato != null)
            {
                tRef.data_fine_stato = DateTimeHelpers.ClampSqlServerDatetime(tRef.data_fine_stato.Value);
            }
            // tRef.indirizzo_ar non c'è
            // tRef.ccia non c'è
            // tRef.flag_web non c'è
            tRef.fax = RegolaLunghezzaCampo(tRef.fax, 30);
            // tRef.flag_verifica_stati non c'è
            tRef.flag_verifica_pec = RegolaLunghezzaCampo(tRef.flag_verifica_pec, 1);
            // tRef.flag_ricerca_eredi non c'è (ovviamente)
            // tRef.flag_ricerca_nuovo_referente_pg non c'è (ovviamente)

            tRef.data_stato = DateTimeHelpers.ClampSqlServerDatetime(tRef.data_stato);
        }

        private static void RegolaLunghezzaCampiTerzo(ref tab_terzo tRef)
        {
            // Campi non comuni

            // TODO
        }

        private static string RegolaLunghezzaCampo(string fieldVal, int maxLen)
        {
            return fieldVal != null
                    ? StringHelpers.SafeSubstring(fieldVal, 0, maxLen)
                    : fieldVal;
        }

        public static void RegolaLunghezzaCampiContribuenteReferenteCC<T>(ref T tContrib) where T: IContribuenteReferenteCampiComuni
        {
            if (tContrib.rag_sociale != null)
            {
                tContrib.rag_sociale = StringHelpers.SafeSubstring(tContrib.rag_sociale, 0, 150);
            }
            if (tContrib.denominazione_commerciale != null)
            {
                tContrib.denominazione_commerciale = StringHelpers.SafeSubstring(tContrib.denominazione_commerciale, 0, 100);
            }

            tContrib.cognome = RegolaLunghezzaCampo(tContrib.cognome, 100);
            tContrib.nome = RegolaLunghezzaCampo(tContrib.nome, 100);
            tContrib.cod_fiscale = RegolaLunghezzaCampo(tContrib.cod_fiscale, 16);
            tContrib.p_iva = RegolaLunghezzaCampo(tContrib.p_iva, 11);
            
            if (tContrib.stato_nas != null)
            {
                tContrib.stato_nas = StringHelpers.SafeSubstring(tContrib.stato_nas, 0, 50);
            }
            if (tContrib.comune_nas != null)
            {
                tContrib.comune_nas = StringHelpers.SafeSubstring(tContrib.comune_nas, 0, 50);
            }
            if (tContrib.data_nas != null)
            {
                tContrib.data_nas = DateTimeHelpers.ClampSqlServerDatetime(tContrib.data_nas.Value);
            }
            if (tContrib.data_morte != null)
            {
                tContrib.data_morte = DateTimeHelpers.ClampSqlServerDatetime(tContrib.data_morte.Value);
            }

            if (tContrib.indirizzo != null)
            {
                tContrib.indirizzo = StringHelpers.SafeSubstring(tContrib.indirizzo, 0, 100);
            }
            tContrib.edificio = RegolaLunghezzaCampo(tContrib.edificio, 50);

            // Se sigla civico fatto solo di spazi, lo imposta a null
            // (altrimenti il validatore EF darebbe errore)
            if (tContrib.sigla_civico != null)
            {
                tContrib.sigla_civico = tContrib.sigla_civico.Trim();
                if (tContrib.sigla_civico.Length == 0)
                {
                    tContrib.sigla_civico = null;
                }
            }
            if (tContrib.sigla_civico != null)
            {
                tContrib.sigla_civico = StringHelpers.SafeSubstring(tContrib.sigla_civico, 0, 50);
            }
            tContrib.scala   = RegolaLunghezzaCampo(tContrib.scala  , 50);
            tContrib.piano   = RegolaLunghezzaCampo(tContrib.piano  , 50);
            tContrib.interno = RegolaLunghezzaCampo(tContrib.interno, 50);
            
            if (tContrib.condominio != null)
            {
                tContrib.condominio = StringHelpers.SafeSubstring(tContrib.condominio, 0, 50);
            }
            if (tContrib.frazione != null)
            {
                tContrib.frazione = StringHelpers.SafeSubstring(tContrib.frazione, 0, 50);
            }
            if (tContrib.stato != null)
            {
                tContrib.stato = StringHelpers.SafeSubstring(tContrib.stato, 0, 50);
            }
            if (tContrib.citta != null)
            {
                tContrib.citta = StringHelpers.SafeSubstring(tContrib.citta, 0, 50);
            }

            tContrib.cap = RegolaLunghezzaCampo(tContrib.cap, 5);
            // VALIDARE cap CON RegEx

            if (tContrib.prov != null)
            {
                tContrib.prov = StringHelpers.SafeSubstring(tContrib.prov, 0, 50);
            }
            
            tContrib.e_mail = RegolaLunghezzaCampo(tContrib.e_mail, 40000);
            tContrib.tel = RegolaLunghezzaCampo(tContrib.tel, 30);
            
            // pec max 100 nel db 4000
            if (tContrib.pec != null)
            {
                tContrib.pec = StringHelpers.SafeSubstring(tContrib.pec, 0, 4000);
            }

            tContrib.cell = RegolaLunghezzaCampo(tContrib.cell, 30);
            tContrib.fonte_stato = RegolaLunghezzaCampo(tContrib.fonte_stato, 3);
            tContrib.flag_verifica_cf_piva = RegolaLunghezzaCampo(tContrib.flag_verifica_cf_piva, 1);
            tContrib.flag_ricerca_indirizzo_emigrazione = RegolaLunghezzaCampo(tContrib.flag_ricerca_indirizzo_emigrazione, 1);
            tContrib.flag_ricerca_indirizzo_mancata_notifica = RegolaLunghezzaCampo(tContrib.flag_ricerca_indirizzo_mancata_notifica, 1);
            if (tContrib.data_inizio_validita_altri_dati != null)
            {
                tContrib.data_inizio_validita_altri_dati = DateTimeHelpers.ClampSqlServerDatetime(tContrib.data_inizio_validita_altri_dati.Value);
            }
            if (tContrib.data_fine_validita_altri_dati != null)
            {
                tContrib.data_fine_validita_altri_dati = DateTimeHelpers.ClampSqlServerDatetime(tContrib.data_fine_validita_altri_dati.Value);
            }
            tContrib.fonte_altri_dati = RegolaLunghezzaCampo(tContrib.fonte_altri_dati, 3);
            if (tContrib.data_inizio_validita_indirizzo != null)
            {
                tContrib.data_inizio_validita_indirizzo = DateTimeHelpers.ClampSqlServerDatetime(tContrib.data_inizio_validita_indirizzo.Value);
            }
            if (tContrib.data_fine_validita_indirizzo != null)
            {
                tContrib.data_fine_validita_indirizzo = DateTimeHelpers.ClampSqlServerDatetime(tContrib.data_fine_validita_indirizzo.Value);
            }
            tContrib.fonte_indirizzo = RegolaLunghezzaCampo(tContrib.fonte_indirizzo, 3);
            tContrib.flag_irreperibilita_definitiva = RegolaLunghezzaCampo(tContrib.flag_irreperibilita_definitiva, 3);
            tContrib.cod_stato = RegolaLunghezzaCampo(tContrib.cod_stato, 7);
            
            if (tContrib.colore != null)
            {
                tContrib.colore = StringHelpers.SafeSubstring(tContrib.colore, 0, 50);
            }
            
            tContrib.cod_fiscale_pg = RegolaLunghezzaCampo(tContrib.cod_fiscale_pg, 11);
            if (tContrib.data_ultima_verifica != null)
            {
                tContrib.data_ultima_verifica = DateTimeHelpers.ClampSqlServerDatetime(tContrib.data_ultima_verifica.Value);
            }

            // Campi non comuni
            tab_contribuente contribuente = tContrib as tab_contribuente;
            tab_referente referente = tContrib as tab_referente;
            tab_terzo terzo = tContrib as tab_terzo;
            if (contribuente!=null)
            {
                RegolaLunghezzaCampiContribuente(ref contribuente);
            }
            else if (referente!=null)
            {
                RegolaLunghezzaCampiReferente(ref referente);
            }
            else if (terzo!=null)
            {
                RegolaLunghezzaCampiTerzo(ref terzo);
            }
        }


        public static string GetLetteraCivicoClear(string letteraCivico)
        {
            if (letteraCivico.Length > 0)
            {
                letteraCivico = letteraCivico
                    .Replace(".", "")
                    .Replace(",", "")
                    .Replace("!", "")
                    .Replace("*", "")
                    .Replace("?", "");
            }
            return letteraCivico;
        }

        public static string GetSiglaCivicoCleared(string letteraCivico)
        {
            if (letteraCivico != null && letteraCivico.Length > 0)
            {
                letteraCivico = GetLetteraCivicoClear(letteraCivico);
                
                // var siglaCivicoMatchResult = _codiceCivicoCheckRegex.Match(letteraCivico);
                // if (!(siglaCivicoMatchResult.Success  && siglaCivicoMatchResult.Index==0))
                if(!tab_contribuente.IsCodiceCivicoValid(letteraCivico))
                {
                    if (letteraCivico == "SN")
                    {
                        letteraCivico = "SNC";
                    }
                    else
                    {
                        // 8/6/2017: abbiamo stabilito che non è errore di scarto record, ma semplicemente impostiamo la ns. lettera civico a NULL
                        //           (diverso dalla documentazione)
                        letteraCivico = null;
                    }
                }
            }
            return letteraCivico;
        }

        public static bool IsSiglaCivico(string siglaCivico)
        {
            if (siglaCivico != null && siglaCivico.Length > 0)
            {
                // var siglaCivicoMatchResult = _codiceCivicoCheckRegex.Match(siglaCivico);
                // return siglaCivicoMatchResult.Success && siglaCivicoMatchResult.Index==0;
                return tab_contribuente.IsCodiceCivicoValid(siglaCivico);
            }
            else
            {
                return true;
            }
        }


        public static bool IsPecOK(string pec)
        {
            if (pec != null && pec.Length > 0)
            {
                var pecmatch = _pecCheckRegex.Match(pec);
                return pecmatch.Success && pecmatch.Index==0;
            }
            else
            {
                return true;
            }
        }

        public static bool IsCAPOk(string cap, bool emptyIsOK = true)
        {
            if (String.IsNullOrWhiteSpace(cap))
            {
                return emptyIsOK;
            }

            //cap = cap.PadLeft(5, '0');
            //if (cap == "00000")
            //{
            //    cap = null;
            //    return emptyIsOK;
            //}
            //else
            {
                //return TabContribuentehelper.RegexCAP.IsMatch(cap);
                ////throw new Tracciato290ReadException(
                ////    cbParams.LineNumber,
                ////    cbParams.CurrTipo,
                ////    $"CAP errato ({cap})");
                var capMatcher = TabContribuentehelper.RegexCAP.Match(cap);
                // bool isCapOK = TabContribuentehelper.RegexCAP.IsMatch(cap);
                bool isCapOK = capMatcher.Success && capMatcher.Index == 0;
                return isCapOK;
            }
        }
    }
}
