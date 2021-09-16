using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinRisorseSerComuniLinq
    {
        public static IQueryable<join_risorse_ser_comuni> WhereByIdRisorsa(this IQueryable<join_risorse_ser_comuni> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_risorsa == p_idRisorsa);
        }

        public static IQueryable<join_risorse_ser_comuni> WhereByIdRisorsaNot(this IQueryable<join_risorse_ser_comuni> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_risorsa != p_idRisorsa);
        }

        public static IQueryable<join_risorse_ser_comuni> WhereByIdDocEntrata(this IQueryable<join_risorse_ser_comuni> p_query, int p_idDocEntrata)
        {
            return p_query.Where(d => d.id_tipo_doc_entrate == p_idDocEntrata);
        }

        public static IQueryable<join_risorse_ser_comuni> WhereByCodProvinciaOrNull(this IQueryable<join_risorse_ser_comuni> p_query, int p_idCodProvincia)
        {
            return p_query.Where(d => d.cod_provincia == p_idCodProvincia || d.cod_provincia == null);
        }

        public static IQueryable<join_risorse_ser_comuni> WhereByCodRegioneOrNull(this IQueryable<join_risorse_ser_comuni> p_query, int p_idCodRegione)
        {
            return p_query.Where(d => d.cod_regione == p_idCodRegione || d.cod_regione == null);
        }

        public static IQueryable<join_risorse_ser_comuni> WhereByRangeValidita(this IQueryable<join_risorse_ser_comuni> p_query, DateTime p_data)
        {
            return p_query.Where(d => d.data_inizio_validita <= p_data && d.data_fine_validita >= p_data);
        }
    }
}
