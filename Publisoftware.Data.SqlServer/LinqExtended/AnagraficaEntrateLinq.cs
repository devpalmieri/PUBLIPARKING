using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaEntrateLinq
    {
        public static IQueryable<anagrafica_entrate> WhereByNotIMUICIeTASI(this IQueryable<anagrafica_entrate> p_query)
        {
            return p_query.Where(d => d.id_entrata != anagrafica_entrate.ICI && 
                                      d.id_entrata != anagrafica_entrate.IMU && 
                                      d.id_entrata != anagrafica_entrate.TASI);
        }

        public static IQueryable<anagrafica_entrate> OrderByDescrizione(this IQueryable<anagrafica_entrate> p_query)
        {
            return p_query.OrderBy(e => e.descrizione_entrata);
        }

        public static IQueryable<anagrafica_entrate> OrderByDefault(this IQueryable<anagrafica_entrate> p_query)
        {
            return p_query.OrderBy(e => e.ordine);
        }
        
        public static IQueryable<anagrafica_entrate> WhereByIdEntrataList(this IQueryable<anagrafica_entrate> p_query, IList<int> p_idEntrataList)
        {
            return p_query.Where(d => p_idEntrataList.Contains(d.id_entrata));
        }

        public static IQueryable<anagrafica_entrate> WhereByIdEntrata(this IQueryable<anagrafica_entrate> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata == p_idEntrata);
        }
    }
}
