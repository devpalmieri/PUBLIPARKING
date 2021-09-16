using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabRegistroAssegnazionePraticheLinq
    {
        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByIdTipoDoc(this IQueryable<tab_registro_assegnazione_pratiche> p_query, int p_idTipoPratica)
        {
            return p_query.Where(d => d.tab_tipo_doc_entrate.id_tipo_doc == p_idTipoPratica);
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByIdTipoDocEntrata(this IQueryable<tab_registro_assegnazione_pratiche> p_query, int p_idTipoDocEntrata)
        {
            return p_query.Where(d => d.tipo_pratica == p_idTipoDocEntrata);
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByDataIscrizioneNull(this IQueryable<tab_registro_assegnazione_pratiche> p_query)
        {
            return p_query.Where(d => d.data_iscrizione_ruolo_autorita_giudiziaria == null);
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByIdDocInput(this IQueryable<tab_registro_assegnazione_pratiche> p_query, int p_idDocInput)
        {
            return p_query.Where(d => d.id_doc_input == p_idDocInput);
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByIdAvvisoRiferimento(this IQueryable<tab_registro_assegnazione_pratiche> p_query, int p_idavviso)
        {
            return p_query.Where(d => d.id_avviso_riferimento == p_idavviso);
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByIdRisorsa(this IQueryable<tab_registro_assegnazione_pratiche> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_risorsa == p_idRisorsa);
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByIdEnte(this IQueryable<tab_registro_assegnazione_pratiche> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByCodStato(this IQueryable<tab_registro_assegnazione_pratiche> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByCodStatoNot(this IQueryable<tab_registro_assegnazione_pratiche> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByIdAutoritaGiudiziaria(this IQueryable<tab_registro_assegnazione_pratiche> p_query, int p_idAutoritaGiudiziaria)
        {
            return p_query.Where(d => d.id_autorita_giudiziaria == p_idAutoritaGiudiziaria);
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByRGR(this IQueryable<tab_registro_assegnazione_pratiche> p_query, string p_rgr)
        {
            if (!string.IsNullOrEmpty(p_rgr))
            {
                return p_query.Where(d => d.rg_iscrizione_ruolo_autorita_giudiziaria.Equals(p_rgr));
            }
            else
            {
                return p_query;
            }
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> WhereByRangeDataPresentazione(this IQueryable<tab_registro_assegnazione_pratiche> p_query, DateTime? da, DateTime? a)
        {
            if (da.HasValue)
            {
                p_query = p_query.Where(d => d.data_assegnazione_pratica >= da.Value);
            }

            if (a.HasValue)
            {
                p_query = p_query.Where(d => d.data_assegnazione_pratica <= a.Value);
            }

            return p_query;
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> OrderDescByRegistro(this IQueryable<tab_registro_assegnazione_pratiche> p_query)
        {
            return p_query.OrderByDescending(d => d.id_registro);
        }

        public static IQueryable<tab_registro_assegnazione_pratiche> OrderByDefault(this IQueryable<tab_registro_assegnazione_pratiche> p_query)
        {
            return p_query.OrderBy(d => d.anagrafica_ente.descrizione_ente).ThenBy(d => d.data_assegnazione_pratica);
        }
    }
}
