using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabContribuenteLinq
    {
        /// <summary>
        /// Ordine di default per le persone fisiche
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_contribuente> OrderByDefaultPersoneFisiche(this IQueryable<tab_contribuente> p_query)
        {
            return p_query.OrderBy(o => o.cognome).ThenBy(o => o.nome).ThenBy(o => o.data_nas);
        }

        /// <summary>
        /// Ordine di default per le persone giuridiche
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_contribuente> OrderByDefaultPersoneGiuridiche(this IQueryable<tab_contribuente> p_query)
        {
            return p_query.OrderBy(o => o.rag_sociale).ThenBy(o => o.denominazione_commerciale);
        }

        /// <summary>
        /// Ordine di default per le persone giuridiche
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_contribuente> OrderByDefaultAllGroupTipoPersona(this IQueryable<tab_contribuente> p_query)
        {
            return p_query.OrderBy(o => o.id_tipo_contribuente).ThenBy(o => o.cognome).ThenBy(o => o.nome).ThenBy(o => o.data_nas).ThenBy(o => o.rag_sociale).ThenBy(o => o.denominazione_commerciale);
        }

        public static IQueryable<tab_contribuente> WhereByCodStato(this IQueryable<tab_contribuente> p_query, string p_codstato)
        {
            return p_query.Where(c => c.cod_stato_contribuente == p_codstato);
        }

        public static IQueryable<tab_contribuente> WhereByCodiceFiscalePIVA(this IQueryable<tab_contribuente> p_query, string p_codiceFiscalePIVA)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                return p_query.Where(d => d.cod_fiscale.Equals(p_codiceFiscalePIVA));
            }
            else //if (p_codiceFiscalePIVA.Length == 11)
            {
                return p_query.Where(d => d.p_iva.Equals(p_codiceFiscalePIVA));
            }
        }

        public static IQueryable<tab_contribuente> WhereToponimoDaNormalizzare(this IQueryable<tab_contribuente> p_query)
        {
            return p_query.Where(c => c.id_toponimo_normalizzato.HasValue);
        }

        public static IQueryable<tab_contribuente> WhereByIdTipoContribuente(this IQueryable<tab_contribuente> p_query, int p_idTipoContribuente)
        {
            return p_query.Where(c => c.id_tipo_contribuente == p_idTipoContribuente);
        }

        public static IQueryable<tab_contribuente> WhereByFlagWeb(this IQueryable<tab_contribuente> p_query, string p_flagweb)
        {
            return p_query.Where(c => c.flag_web == p_flagweb);
        }

        public static IQueryable<tab_contribuente> WhereByflag_ricerca_indirizzo_migrazione(this IQueryable<tab_contribuente> p_query, string p_flag_ricerca_indirizzo_migrazione)
        {
            return p_query.Where(c => c.flag_ricerca_indirizzo_emigrazione == p_flag_ricerca_indirizzo_migrazione);
        }

        public static IQueryable<tab_contribuente> WhereByflag_ricerca_indirizzo_mancata_notifica(this IQueryable<tab_contribuente> p_query, string p_flag_ricerca_indirizzo_mancata_notifica)
        {
            return p_query.Where(c => c.flag_ricerca_indirizzo_mancata_notifica == p_flag_ricerca_indirizzo_mancata_notifica);
        }

        public static IQueryable<tab_contribuente> WhereByflag_ricerca_eredi(this IQueryable<tab_contribuente> p_query, string p_flag_ricerca_eredi)
        {
            return p_query.Where(c => c.flag_ricerca_eredi == p_flag_ricerca_eredi);
        }

        public static IQueryable<tab_contribuente> WhereByflag_ricerca_nuovo_referente_pg(this IQueryable<tab_contribuente> p_query, string p_flag_ricerca_nuovo_referente_pg)
        {
            return p_query.Where(c => c.flag_ricerca_nuovo_referente_pg == p_flag_ricerca_nuovo_referente_pg);
        }

        public static IQueryable<tab_contribuente> OrderByDefault(this IQueryable<tab_contribuente> p_query)
        {
            return p_query.OrderBy(o => o.id_anag_contribuente);
        }

        /// <summary>
        /// Filtro per ID Contribuente
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idAnagContribuente"></param>
        /// <returns></returns>
        public static IQueryable<tab_contribuente> WhereById(this IQueryable<tab_contribuente> p_query, decimal p_idAnagContribuente)
        {
            return p_query.Where(c => c.id_anag_contribuente == p_idAnagContribuente);
        }
    }
}
