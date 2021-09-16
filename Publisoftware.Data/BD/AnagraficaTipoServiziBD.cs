using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaTipoServiziBD : EntityBD<anagrafica_tipo_servizi>
    {
        public AnagraficaTipoServiziBD()
        {

        }

        /// <summary>
        /// Restituisce il tipo servizio corrispondente al codice
        /// </summary>
        /// <param name="p_codice">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_tipo_servizi GetByDescrizione(string p_descrizione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(a => a.descr_tiposervizio.ToUpper() == p_descrizione.ToUpper());
        }

        /// <summary>
        /// Restituisce i tipi servizio di interesse per la gestione del coattivo
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_servizi> GetListForCoattivo(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.id_tipo_servizio == 3
                            || a.id_tipo_servizio == 4
                            || a.id_tipo_servizio == 6
                            || a.id_tipo_servizio == 7);
        }
    }
}
