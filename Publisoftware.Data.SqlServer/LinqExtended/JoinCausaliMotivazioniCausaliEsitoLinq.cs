using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinCausaliMotivazioniCausaliEsitoLinq
    {
        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByIdTipoDocEntrata(this IQueryable<join_causali_motivazioni_causali_esito> p_query, int p_idTipoDocEntrata)
        {
            return p_query.Where(d => d.id_tipo_doc_entrata_ricorso == p_idTipoDocEntrata);
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByIdTipoDocEntrataOrNull(this IQueryable<join_causali_motivazioni_causali_esito> p_query, int p_idTipoDocEntrata)
        {
            return p_query.Where(d => d.id_tipo_doc_entrata_ricorso == p_idTipoDocEntrata ||
                                     !d.id_tipo_doc_entrata_ricorso.HasValue);
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByIdTipoServizio(this IQueryable<join_causali_motivazioni_causali_esito> p_query, int p_idTipoServizio)
        {
            return p_query.Where(d => d.id_tipo_servizio_avvpag == p_idTipoServizio);
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByIdTipoServizioOrNull(this IQueryable<join_causali_motivazioni_causali_esito> p_query, int p_idTipoServizio)
        {
            return p_query.Where(d => d.id_tipo_servizio_avvpag == p_idTipoServizio ||
                                     !d.id_tipo_servizio_avvpag.HasValue);
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByIdEntrata(this IQueryable<join_causali_motivazioni_causali_esito> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata_avvpag == p_idEntrata);
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByIdEntrataOrNull(this IQueryable<join_causali_motivazioni_causali_esito> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata_avvpag == p_idEntrata ||
                                     !d.id_entrata_avvpag.HasValue);
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByIdCausaleMotivazione(this IQueryable<join_causali_motivazioni_causali_esito> p_query, int p_idCausale)
        {
            return p_query.Where(d => d.id_causale_motivazione == p_idCausale);
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByCodStatoCausaleMotivazione(this IQueryable<join_causali_motivazioni_causali_esito> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato_motivazione.Equals(p_codStato));
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByCodStato(this IQueryable<join_causali_motivazioni_causali_esito> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByCodStatoNot(this IQueryable<join_causali_motivazioni_causali_esito> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByFonte(this IQueryable<join_causali_motivazioni_causali_esito> p_query, string p_fonte)
        {
            return p_query.Where(d => d.fonte_avvpag.StartsWith(p_fonte));
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByRangeValiditaOdierno(this IQueryable<join_causali_motivazioni_causali_esito> p_query)
        {
            return p_query.Where(d => (d.data_inizio_validita.HasValue ? d.data_inizio_validita.Value : DateTime.MinValue) <= DateTime.Now &&
                                      (d.data_fine_validita.HasValue ? d.data_fine_validita.Value : DateTime.MaxValue) >= DateTime.Now);
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereByFlagTrattamentoEsito(this IQueryable<join_causali_motivazioni_causali_esito> p_query, string p_flagTrattamento)
        {
            return p_query.Where(d => d.anagrafica_causale1.flag_trattamento.Equals(p_flagTrattamento));
        }

        public static IQueryable<join_causali_motivazioni_causali_esito> WhereBySiglaCausaleEsito(this IQueryable<join_causali_motivazioni_causali_esito> p_query, string p_siglaCausale)
        {
            return p_query.Where(d => d.anagrafica_causale1.sigla_tipo_causale.Trim().Equals(p_siglaCausale));
        }
    }
}
