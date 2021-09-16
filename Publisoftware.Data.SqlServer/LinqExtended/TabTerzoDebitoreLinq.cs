using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTerzoDebitoreLinq
    {
        public static IQueryable<tab_terzo_debitore> WhereByTerzo(this IQueryable<tab_terzo_debitore> p_query, int p_idTerzo)
        {
            return p_query.Where(w => w.id_terzo == p_idTerzo);
        }

        /// <summary>
        /// Filtro per Codice Fiscale/Partita IVA soggetto Ispezione
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_CfPivaSoggettoIspezione">Codice Fiscale/Partita IVA soggetto Ispezione</param>
        /// <returns></returns>
        public static IQueryable<tab_terzo_debitore> WhereByCfPivaSoggettoIspezione(this IQueryable<tab_terzo_debitore> p_query, string p_CfPivaSoggettoIspezione)
        {
            return p_query.Where(w => w.cf_piva_soggetto_ispezione == p_CfPivaSoggettoIspezione);
        }

        //public static IQueryable<tab_terzo_debitore> WhereSiatel(this IQueryable<tab_terzo_debitore> p_query)
        //{
        //    return p_query.Where(w => w.tab_tipo_bene.tab_tipo_ispezione.sigla_tipo_ispezione == tab_tipo_ispezione.SIATEL);
        //}

        //public static IQueryable<tab_terzo_debitore> WhereDisponibilitaFinanziaria(this IQueryable<tab_terzo_debitore> p_query)
        //{
        //    return p_query.Where(w => w.tab_tipo_bene.tab_tipo_ispezione.sigla_tipo_ispezione == tab_tipo_ispezione.DISPONIBILITA_FINANZIARIA);
        //}

        public static IQueryable<tab_terzo_debitore> WhereTipoBene(this IQueryable<tab_terzo_debitore> p_query, int p_idTipoBene)
        {
            return p_query.Where(w => w.id_tipo_bene == p_idTipoBene);
        }

        public static IQueryable<tab_terzo_debitore> WhereSiglaTipoBene(this IQueryable<tab_terzo_debitore> p_query, string p_siglaTipoBene)
        {
            return p_query.Where(w => w.tab_tipo_bene.codice_bene == p_siglaTipoBene);
        }

        public static IQueryable<tab_terzo_debitore> WhereSiglaTipoBene(this IQueryable<tab_terzo_debitore> p_query, IList<string> p_siglaTipoBene)
        {
            return p_query.Where(w => p_siglaTipoBene.Contains(w.tab_tipo_bene.codice_bene));
        }

        /// <summary>
        /// Seleziona quelli da ricercare sul portale SIATEL
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_terzo_debitore> WhereSiglaTipoIspezione(this IQueryable<tab_terzo_debitore> p_query, string p_siglaTipoIspezione)
        {
            return p_query.Where(w => w.tab_tipo_bene.tab_tipo_ispezione.sigla_tipo_ispezione.Trim() == p_siglaTipoIspezione);
        }

        /// <summary>
        /// Seleziona quelli Attivi
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_terzo_debitore> WhereAttivi(this IQueryable<tab_terzo_debitore> p_query)
        {
            return p_query.Where(w => w.cod_stato == tab_terzo_debitore.ATT_ATT);
        }

        public static IQueryable<tab_terzo_debitore> WhereStatiDaMostrare(this IQueryable<tab_terzo_debitore> p_query)
        {
            return p_query.Where(w => w.cod_stato == tab_terzo_debitore.ATT_ATT || w.cod_stato == tab_terzo_debitore.ATT_CES || w.cod_stato.StartsWith(tab_terzo_debitore.ANN));
        }

        /// <summary>
        /// Trasforma nella versione light
        /// </summary>
        /// <param name="iniziale"></param>
        /// <returns></returns>
        public static IList<tab_terzo_debitore_light> ToLight(this IList<tab_terzo_debitore> iniziale)
        {
            return iniziale.Select(l => new tab_terzo_debitore_light
                                                                    {
                                                                        id_trz_debitore = l.id_trz_debitore,
                                                                        tipo_bene_descrizione = l.tab_tipo_bene.descrizione,
                                                                        codiceFiscalePartitaIva = (l.tab_terzo.id_tipo_terzo == 1 ? l.tab_terzo.cod_fiscale : l.tab_terzo.p_iva),
                                                                        nominativoRagioneSociale = (l.tab_terzo.id_tipo_terzo == 1 ? l.tab_terzo.cognome + " " + l.tab_terzo.nome : l.tab_terzo.rag_sociale),
                                                                        citta = l.tab_terzo != null ? l.tab_terzo.citta : string.Empty,
                                                                        indirizzo = l.tab_terzo.indirizzo + (l.tab_terzo.nr_civico != null && l.tab_terzo.nr_civico.ToString() != "" ? ", " + l.tab_terzo.nr_civico.ToString() : ""),
                                                                        dataAggiornamento = l.dataAggiornamentoString,
                                                                        codStato = l.cod_stato
                                                                    }).ToList();
        }
    }
}
