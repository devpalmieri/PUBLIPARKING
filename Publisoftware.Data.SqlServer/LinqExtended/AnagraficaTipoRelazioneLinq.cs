using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoRelazioneLinq
    {
        public static IQueryable<anagrafica_tipo_relazione> WhereByIdTipoContribuente(this IQueryable<anagrafica_tipo_relazione> p_query, String p_idTipoContribuente)
        {
            return p_query.Where(d => d.cod_tipo_relazione != null && 
                                      d.flag_fisica_giuridica == p_idTipoContribuente);
        }

        public static IQueryable<anagrafica_tipo_relazione> OrderByDefault(this IQueryable<anagrafica_tipo_relazione> p_query)
        {
            return p_query.OrderBy(o => o.desc_tipo_relazione);
        }
    }
}
