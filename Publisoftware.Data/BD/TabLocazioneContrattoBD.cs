using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabLocazioneContrattoBD : EntityBD<tab_locazione_contratto>
    {
        public TabLocazioneContrattoBD()
        {

        }

        /// <summary>
        /// Filtro per ufficio, anno, serie, numero, sottonumero, negozio, data di stipulazione
        /// </summary>
        /// <param name="p_ufficio"></param>
        /// <param name="p_anno"></param>
        /// <param name="p_serie"></param>
        /// <param name="p_numero"></param>
        /// <param name="p_sottonumero"></param>
        /// <param name="p_progNegozio"></param>
        /// <param name="p_dataStipula"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_locazione_contratto GetContratto(string p_ufficio, int? p_anno, string p_serie, int? p_numero, int? p_sottonumero, int? p_progNegozio, DateTime? p_dataStipula, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(r => r.ufficio != null && r.ufficio.Equals(p_ufficio) && r.anno != null && r.anno == p_anno && r.serie != null && r.serie.Equals(p_serie) && r.numero_reg != null && r.numero_reg == p_numero && r.sottonumero_reg != null && r.sottonumero_reg == p_sottonumero && r.progr_negozio != null && r.progr_negozio == p_progNegozio && r.data_stipula != null && r.data_stipula == p_dataStipula).SingleOrDefault();
        }

        /// <summary>
        /// Filtro per i contratti attivi
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_locazione_contratto> GetListAttivi(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.data_file != Convert.ToDateTime("01/1/1900") && c.cod_stato == "CAR-CAR");
        }
    }
}
