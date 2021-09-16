using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaVociContribuzioneLinq
    {
        public static IQueryable<anagrafica_voci_contribuzione> OrderByDefault(this IQueryable<anagrafica_voci_contribuzione> p_query)
        {
            return p_query.OrderBy(d => d.descr_anagrafica_voce_contribuzione);
        }
       
        public static IQueryable<anagrafica_voci_contribuzione> WhereByIdTipoVoce(this IQueryable<anagrafica_voci_contribuzione> p_query, int p_idTipoVoce)
        {
            return p_query.Where(d => d.id_tipo_voce_contribuzione == p_idTipoVoce);
        }

        public static IQueryable<anagrafica_voci_contribuzione> WhereByIdEntrata(this IQueryable<anagrafica_voci_contribuzione> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata == p_idEntrata);
        }
    }
}
