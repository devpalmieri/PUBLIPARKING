using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaAbiCabBD : EntityBD<ANAGRAFICA_ABI_CAB>
    {
        public AnagraficaAbiCabBD()
        {

        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>    
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<ANAGRAFICA_ABI_CAB> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(a => a.ABI.Contains(p_text) || a.CAB.Contains(p_text) || a.BANCA.Contains(p_text))
                                                          .OrderBy(o => o.BANCA).ThenBy(o => o.AGENZIA);
        }

        public static IQueryable<ANAGRAFICA_ABI_CAB> GetBancaSedePrincipalePerProvincia(String p_provincia, dbEnte p_context)
        {         
            return GetList(p_context)
                   .Where(a=> a.ID_ABI_CAB == a.ID_ABI_CAB_RIFERIMENTO)
                   .Where(a => a.PROVINCIA.Equals(p_provincia));
        }
        public static IQueryable<ANAGRAFICA_ABI_CAB> GetBancaSedePrincipale(dbEnte p_context)
        {            
            return GetList(p_context).Where(a => a.ID_ABI_CAB == a.ID_ABI_CAB_RIFERIMENTO);
        }
    }
}
