using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabLocazioneSoggettoBD : EntityBD<tab_locazione_soggetto>
    {
        public TabLocazioneSoggettoBD()
        {

        }

        /// <summary>
        /// Filtro per ufficio, anno, serie, numero, sottonumero, negozio, soggetto, data di stipulazione
        /// </summary>
        /// <param name="p_ufficio"></param>
        /// <param name="p_anno"></param>
        /// <param name="p_serie"></param>
        /// <param name="p_numero"></param>
        /// <param name="p_sottonumero"></param>
        /// <param name="p_progNegozio"></param>
        /// <param name="p_progSoggetto"></param>
        /// <param name="p_tipoSoggetto"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_locazione_soggetto GetSoggetto(string p_ufficio, int? p_anno, string p_serie, int? p_numero, int? p_sottonumero, int? p_progNegozio, int? p_progSoggetto, string p_tipoSoggetto, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(r => r.ufficio != null && r.ufficio.Equals(p_ufficio) && r.anno != null && r.anno == p_anno && r.serie != null && r.serie.Equals(p_serie) && r.numero_reg != null && r.numero_reg == p_numero && r.sottonumero_reg != null && r.sottonumero_reg == p_sottonumero && r.progr_negozio != null && r.progr_negozio == p_progNegozio && r.progr_soggetto != null && r.progr_soggetto == p_progSoggetto && r.tipo_soggetto != null && r.tipo_soggetto == p_tipoSoggetto).FirstOrDefault();
        }

        /// <summary>
        /// restituisce la lista dei soggetti per l'id contratto locazione
        /// </summary>
        /// <param name="p_idContrattoLocazione"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_locazione_soggetto> GetListaSoggetti(Int32 p_idContrattoLocazione, dbEnte p_dbContext)
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

            string sql = "UPDATE TAB_LOCAZIONE_PROPR_INQUILINO SET COD_STATO='" + p_codStato + "' WHERE COD_STATO = 'CAR-CAR' AND ID_LOCAZIONE_CONTRATTO = " + p_idContrattoLocazione + "AND DATA_FILE < " + p_dateMax;
            rowsUp = p_dbContext.Database.ExecuteSqlCommand(sql);
            return rowsUp;
        }

        /// <summary>
        /// Filtro per codice_fiscale soggetto e tipo_soggetto
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_locazione_soggetto> GetListProprietariCfPiva(string p_cfPivaSoggetto, dbEnte p_dbContext)
        { DateTime dt_confronto = DateTime.Now.AddYears(1);
            return GetList(p_dbContext).Where(c => c.cod_stato == tab_locazione_soggetto.ATT_ATT && c.codice_fiscale == p_cfPivaSoggetto && c.tipo_soggetto.ToLower() == "d" 
            && c.tab_locazione_contratto.valuta_canone=="E" && c.tab_locazione_contratto.tipo_canone=="A" && c.tab_locazione_contratto.data_fine > dt_confronto);
        }

        /// <summary>
        /// tipo_soggetto
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_locazione_soggetto> GetListProprietari(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.cod_stato == tab_locazione_soggetto.ATT_ATT && c.tipo_soggetto == "d");
        }

        /// <summary>
        /// Filtro id contratto e tipo_soggetto
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_locazione_soggetto> GetListInquiliniIdContratto(Int32 p_idContratto, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.cod_stato == tab_locazione_soggetto.ATT_ATT && c.id_locazione_contratto == p_idContratto && c.tipo_soggetto == "a");
        }
    }
}
