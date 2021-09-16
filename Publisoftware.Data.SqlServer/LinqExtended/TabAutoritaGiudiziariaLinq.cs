using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAutoritaGiudiziariaLinq
    {
        public static IQueryable<tab_autorita_giudiziaria> OrderByDefault(this IQueryable<tab_autorita_giudiziaria> p_query)
        {
            return p_query.OrderBy(d => d.tab_tipo_doc_entrate.descr_doc).ThenBy(d => d.descrizione_autorita);
        }

        public static IQueryable<tab_autorita_giudiziaria> WhereByIdAutoritaGiudiziaria(this IQueryable<tab_autorita_giudiziaria> p_query, int p_idAutorita)
        {
            return p_query.Where(d => d.id_tab_autorita_giudiziaria == p_idAutorita);
        }

        public static IQueryable<tab_autorita_giudiziaria> WhereByDescrizioneAutoritaGiudiziaria(this IQueryable<tab_autorita_giudiziaria> p_query, string p_descrizione)
        {
            return p_query.Where(d => d.descrizione_autorita.ToLower().Contains(p_descrizione.ToLower()));
        }

        public static IQueryable<tab_autorita_giudiziaria> WhereByIdTipoDocEntrata(this IQueryable<tab_autorita_giudiziaria> p_query, int p_idTipoDocEntrata)
        {
            return p_query.Where(d => d.id_tipo_doc_entrata.Value == p_idTipoDocEntrata);
        }

        public static IQueryable<tab_autorita_giudiziaria> WhereBySiglaAutorita(this IQueryable<tab_autorita_giudiziaria> p_query, string p_siglaAutorita)
        {
            return p_query.Where(d => d.sigla_autorita.Equals(p_siglaAutorita));
        }

        public static IQueryable<tab_autorita_giudiziaria> WhereTerritorioCompetenza(this IQueryable<tab_autorita_giudiziaria> p_query, string p_territorioCompetenza)
        {
            if (string.IsNullOrEmpty(p_territorioCompetenza))
            {
                return p_query.Where(d => d.territorio_competenza == null);
            }
            else
            {
                return p_query.Where(d => d.territorio_competenza.ToUpper() == p_territorioCompetenza.ToUpper());
            }
        }

        public static IQueryable<tab_autorita_giudiziaria> WhereByComune(this IQueryable<tab_autorita_giudiziaria> p_query, string p_comune)
        {
            return p_query.Where(d => d.comune_ubicazione.ToUpper().CompareTo(p_comune.ToUpper()) == 0);
        }

        public static IQueryable<tab_autorita_giudiziaria> WhereBySiglaContains(this IQueryable<tab_autorita_giudiziaria> p_query, string p_sigla)
        {
            return p_query.Where(d => d.sigla_autorita.ToUpper().Contains(p_sigla.ToUpper()));
        }
    }
}
