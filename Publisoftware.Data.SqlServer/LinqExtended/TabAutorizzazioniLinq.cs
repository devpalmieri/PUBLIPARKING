using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAutorizzazioniLinq
    {
        public static IQueryable<tab_autorizzazioni> WhereByIdUtenteRichiedente(this IQueryable<tab_autorizzazioni> p_query, int p_idUtente)
        {
            return p_query.Where(d => d.id_utente_richiedente == p_idUtente);
        }

        public static IQueryable<tab_autorizzazioni> WhereByIdUtenteDelegato(this IQueryable<tab_autorizzazioni> p_query, int p_idUtente)
        {
            return p_query.Where(d => d.id_utente_delegato == p_idUtente);
        }

        public static IQueryable<tab_autorizzazioni> WhereByIdTerzoRichiestaAccesso(this IQueryable<tab_autorizzazioni> p_query, int p_idTerzo)
        {
            return p_query.Where(d => d.id_terzo_richiesta_accesso == p_idTerzo);
        }

        public static IQueryable<tab_autorizzazioni> WhereByIdContribuenteRichiestaAccesso(this IQueryable<tab_autorizzazioni> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente_richiesta_accesso == p_idContribuente);
        }

        public static IQueryable<tab_autorizzazioni> WhereByIdContribuenteRichiedente(this IQueryable<tab_autorizzazioni> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente_richiedente == p_idContribuente);
        }

        public static IQueryable<tab_autorizzazioni> WhereByIdReferenteRichiedente(this IQueryable<tab_autorizzazioni> p_query, int p_idReferente)
        {
            return p_query.Where(d => d.id_referente_richiedente == p_idReferente);
        }

        public static IQueryable<tab_autorizzazioni> WhereByIdReferenteDelegato(this IQueryable<tab_autorizzazioni> p_query, int p_idReferente)
        {
            return p_query.Where(d => d.id_referente_delegato == p_idReferente);
        }

        public static IQueryable<tab_autorizzazioni> WhereByCodStato(this IQueryable<tab_autorizzazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_autorizzazioni> WhereByCodStato(this IQueryable<tab_autorizzazioni> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_autorizzazioni> WhereByCodStatoNot(this IQueryable<tab_autorizzazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_autorizzazioni> WhereByCodStatoNot(this IQueryable<tab_autorizzazioni> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => !p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_autorizzazioni> WhereByContribuentiRichiestaAccessoCodFiscalePIVA(this IQueryable<tab_autorizzazioni> p_query, string p_codFiscalePIVA)
        {
            if (p_codFiscalePIVA.Length == 16)
            {
                return p_query.Where(d => d.tab_contribuente1.cod_fiscale.ToUpper().Equals(p_codFiscalePIVA));
            }
            else
            {
                return p_query.Where(d => d.tab_contribuente1.p_iva.ToUpper().Equals(p_codFiscalePIVA));
            }
        }

        public static IQueryable<tab_autorizzazioni> WhereByTerziRichiestaAccessoCodFiscalePIVA(this IQueryable<tab_autorizzazioni> p_query, string p_codFiscalePIVA)
        {
            if (p_codFiscalePIVA.Length == 16)
            {
                return p_query.Where(d => d.tab_terzo.cod_fiscale.ToUpper().Equals(p_codFiscalePIVA));
            }
            else
            {
                return p_query.Where(d => d.tab_terzo.p_iva.ToUpper().Equals(p_codFiscalePIVA));
            }
        }

        public static IQueryable<tab_autorizzazioni> WhereByContribuentiRichiestaAccesso(this IQueryable<tab_autorizzazioni> p_query)
        {
            return p_query.Where(d => d.id_contribuente_richiesta_accesso.HasValue);
        }

        public static IQueryable<tab_autorizzazioni> WhereByTerziRichiestaAccesso(this IQueryable<tab_autorizzazioni> p_query)
        {
            return p_query.Where(d => d.id_terzo_richiesta_accesso.HasValue);
        }

        public static IQueryable<tab_autorizzazioni> OrderByDataRichiestaDesc(this IQueryable<tab_autorizzazioni> p_query)
        {
            return p_query.OrderByDescending(d => d.data_richiesta);
        }

        public static IQueryable<tab_autorizzazioni> OrderByTipoAutorizzazione(this IQueryable<tab_autorizzazioni> p_query)
        {
            return p_query.OrderByDescending(d => d.tipo_autorizzazione == tab_autorizzazioni.TIPO_AUTORIZZAZIONE_CONTRIBUENTE)
                          .ThenByDescending(d => d.tipo_autorizzazione == tab_autorizzazioni.TIPO_AUTORIZZAZIONE_EREDE)
                          .ThenByDescending(d => d.tipo_autorizzazione == tab_autorizzazioni.TIPO_AUTORIZZAZIONE_RAPPRESENTANTE)
                          .ThenByDescending(d => d.tipo_autorizzazione == tab_autorizzazioni.TIPO_AUTORIZZAZIONE_TERZO)
                          .ThenByDescending(d => d.tipo_autorizzazione == tab_autorizzazioni.TIPO_AUTORIZZAZIONE_RAPPRESENTANTE_TERZO);
        }
    }
}
