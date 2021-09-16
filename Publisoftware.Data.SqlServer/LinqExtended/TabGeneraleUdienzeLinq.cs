using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabGeneraleUdienzeLinq
    {
        public static IQueryable<tab_generale_udienze> WhereByDataUdienza(this IQueryable<tab_generale_udienze> p_query, DateTime p_data)
        {
            return p_query.Where(d => d.data_udienza.HasValue &&
                                      DbFunctions.TruncateTime(d.data_udienza.Value) == DbFunctions.TruncateTime(p_data));
        }

        public static IQueryable<tab_generale_udienze> WhereByDataUdienzaMaggiore(this IQueryable<tab_generale_udienze> p_query, DateTime p_data)
        {
            return p_query.Where(d => d.data_udienza.HasValue &&
                                      DbFunctions.TruncateTime(d.data_udienza.Value) >= DbFunctions.TruncateTime(p_data));
        }

        public static IQueryable<tab_generale_udienze> WhereByIdAutoritaGiudiziaria(this IQueryable<tab_generale_udienze> p_query, int p_idAutoritaGiudiziaria)
        {
            return p_query.Where(d => d.id_autorita_giudiziaria == p_idAutoritaGiudiziaria);
        }

        public static IQueryable<tab_generale_udienze> WhereByIdRisorsaAssegnazioneList(this IQueryable<tab_generale_udienze> p_query, List<int> p_listIdRisorsa)
        {
            return p_query.Where(d => p_listIdRisorsa.Contains(d.id_risorsa_assegnazione.Value));
        }

        public static IQueryable<tab_generale_udienze> WhereByIdRisorsaDelegaList(this IQueryable<tab_generale_udienze> p_query, List<int> p_listIdRisorsa)
        {
            return p_query.Where(d => p_listIdRisorsa.Contains(d.id_risorsa_delegata.Value));
        }

        public static IQueryable<tab_generale_udienze> WhereByIdTipoDocEntrata(this IQueryable<tab_generale_udienze> p_query, int p_idTipoDocEntrate)
        {
            return p_query.Where(d => d.tab_autorita_giudiziaria.id_tipo_doc_entrata == p_idTipoDocEntrate);
        }

        public static IQueryable<tab_generale_udienze> WhereByIdRegistro(this IQueryable<tab_generale_udienze> p_query, int p_idRegistro)
        {
            return p_query.Where(d => d.id_registro == p_idRegistro);
        }

        public static IQueryable<tab_generale_udienze> WhereByIdRisorsaAssegnazione(this IQueryable<tab_generale_udienze> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_risorsa_assegnazione == p_idRisorsa);
        }

        public static IQueryable<tab_generale_udienze> WhereByIdDelegatoNull(this IQueryable<tab_generale_udienze> p_query)
        {
            return p_query.Where(d => d.id_risorsa_delegata == null);
        }

        public static IQueryable<tab_generale_udienze> WhereByIdDelegatoNotNull(this IQueryable<tab_generale_udienze> p_query)
        {
            return p_query.Where(d => d.id_risorsa_delegata != null);
        }

        public static IQueryable<tab_generale_udienze> WhereByIdRisorsaDelegata(this IQueryable<tab_generale_udienze> p_query, int p_idRisorsaDelega)
        {
            return p_query.Where(d => d.id_risorsa_delegata == p_idRisorsaDelega);
        }

        public static IQueryable<tab_generale_udienze> WhereByIdRisorsaDelegataOrIdRisorsaAssegnataria(this IQueryable<tab_generale_udienze> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_risorsa_delegata == p_idRisorsa || (d.id_risorsa_assegnazione == p_idRisorsa && d.id_risorsa_delegata == null));
        }

        public static IQueryable<tab_generale_udienze> WhereByFlagEsitoUdienza(this IQueryable<tab_generale_udienze> p_query, string p_flag)
        {
            return p_query.Where(d => d.flag_esito_udienza.Equals(p_flag));
        }

        public static IQueryable<tab_generale_udienze> WhereByRGR(this IQueryable<tab_generale_udienze> p_query, string p_rgr)
        {
            if (!string.IsNullOrEmpty(p_rgr))
            {
                return p_query.Where(d => d.tab_registro_assegnazione_pratiche.rg_iscrizione_ruolo_autorita_giudiziaria.Equals(p_rgr));
            }
            else
            {
                return p_query;
            }
        }

        public static IQueryable<tab_generale_udienze> WhereByCodStato(this IQueryable<tab_generale_udienze> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_generale_udienze> WhereByCodStatoNot(this IQueryable<tab_generale_udienze> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_generale_udienze> WhereByDataIscrizioneNull(this IQueryable<tab_generale_udienze> p_query)
        {
            return p_query.Where(d => d.tab_registro_assegnazione_pratiche.data_iscrizione_ruolo_autorita_giudiziaria == null);
        }

        public static IQueryable<tab_generale_udienze> WhereByRangeDataPresentazione(this IQueryable<tab_generale_udienze> p_query, DateTime? da, DateTime? a)
        {
            if (da.HasValue)
            {
                p_query = p_query.Where(d => d.data_udienza >= da.Value);
            }

            if (a.HasValue)
            {
                p_query = p_query.Where(d => d.data_udienza <= a.Value);
            }

            return p_query;
        }

        public static IQueryable<tab_generale_udienze> OrderByDataUdienza(this IQueryable<tab_generale_udienze> p_query)
        {
            return p_query.OrderBy(d => d.data_udienza);
        }

        public static IQueryable<tab_generale_udienze> OrderDescByDataUdienza(this IQueryable<tab_generale_udienze> p_query)
        {
            return p_query.OrderByDescending(d => d.data_udienza);
        }
    }
}
