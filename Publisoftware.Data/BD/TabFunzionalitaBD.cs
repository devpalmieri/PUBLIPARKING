using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabFunzionalitaBD : EntityBD<tab_funzionalita>
    {
        public TabFunzionalitaBD()
        {

        }
        
        /// <summary>
        /// Restituisce la funzionalità corrispondente al codice indicato
        /// </summary>
        /// <param name="p_fullCode">Codice funzionalità ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_funzionalita GetByFullCode(string p_fullCode, dbEnte p_dbContext)
        {
            try
            {
                string v_procCode = p_fullCode.Substring(0, 5);
                string v_funzCode = p_fullCode.Substring(5, 5);

                return GetList(p_dbContext).Where(f => f.codice.Equals(v_funzCode) && f.tab_procedure.codice.Equals(v_procCode)).SingleOrDefault();
            }
            catch (Exception e) { return null; }
        }

        /// <summary>
        /// Restituisce la lista di tutte le funzionalità appartenenti alla procedura indicata
        /// </summary>
        /// <param name="p_idProcedura">ID Procedura ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_funzionalita> GetByIdProcedura(int p_idProcedura, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(f => f.id_tab_procedure == p_idProcedura).OrderBy(o => o.ordine);
        }
    }
}
