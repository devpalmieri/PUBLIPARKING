using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabLocazioneImmobiliBD : EntityBD<tab_locazione_immobili>
    {
        public TabLocazioneImmobiliBD()
        {

        }

        /// <summary>
        /// Filtro per ufficio, anno, serie, numero, sottonumero, negozio, immobile, data di stipulazione
        /// </summary>
        /// <param name="p_ufficio"></param>
        /// <param name="p_anno"></param>
        /// <param name="p_serie"></param>
        /// <param name="p_numero"></param>
        /// <param name="p_sottonumero"></param>
        /// <param name="p_progNegozio"></param>
        /// <param name="p_progImmobile"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_locazione_immobili GetImmobile(string p_ufficio, int? p_anno, string p_serie, int? p_numero, int? p_sottonumero, int? p_progNegozio, int? p_progImmobile, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(r => r.ufficio != null && r.ufficio.Equals(p_ufficio) && r.anno != null && r.anno == p_anno && r.serie != null && r.serie.Equals(p_serie) && r.numero_reg != null && r.numero_reg == p_numero && r.sottonumero_reg != null && r.sottonumero_reg == p_sottonumero && r.progr_negozio != null && r.progr_negozio == p_progNegozio && r.progr_immobile != null && r.progr_immobile == p_progImmobile).FirstOrDefault();
        }

        /// <summary>
        /// restituisce la lista degli immobili per l'id contratto locazione
        /// </summary>
        /// <param name="p_idContrattoLocazione"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_locazione_immobili> GetListaImmobili(Int32 p_idContrattoLocazione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(r => r.id_locazione_contratto == p_idContrattoLocazione);
        }

        /// <summary>
        /// aggiorna lo stato
        /// </summary>
        /// <param name="p_idContrattoLocazione"></param>
        /// <param name="p_codStato"></param>
        /// <param name="p_dateMax"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static int UpdateStato(Int32 p_idContrattoLocazione, String p_codStato, DateTime p_dateMax, dbEnte p_dbContext)
        {
            int rowsUp = 0;

            string sql = "UPDATE TAB_LOCAZIONE_IMMOBILI SET COD_STATO='" + p_codStato + "' WHERE COD_STATO = 'CAR-CAR' AND ID_LOCAZIONE_CONTRATTO = " + p_idContrattoLocazione + "AND DATA_FILE < " + p_dateMax;
            rowsUp = p_dbContext.Database.ExecuteSqlCommand(sql);
            return rowsUp;
        }
    }
}
