using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaVociContribuzioneBD : EntityBD<anagrafica_voci_contribuzione>
    {
        public AnagraficaVociContribuzioneBD()
        { 

        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_voci_contribuzione> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            var p = GetList(p_dbContext, p_includeEntities).Where(a => p_text == "" || 
                a.descr_anagrafica_voce_contribuzione.ToUpper().Contains(p_text.ToUpper()) ||
                a.cod_tributo_ministeriale.ToUpper().Contains(p_text.ToUpper()) ||
                a.cod_anagrafica_voce_contribuzione.ToUpper().Contains(p_text.ToUpper())
                );
            return p;
        }

        /// <summary>
        /// Ricerca l'anagrafica in funzione del codice tributo ministeriale in input
        /// </summary>
        /// <param name="p_codTributo"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_voci_contribuzione GetByCodTributoMinisteriale(string p_codTributo, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).FirstOrDefault(avc => avc.cod_tributo_ministeriale == p_codTributo);
        }
    }
}
