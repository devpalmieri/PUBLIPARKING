using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabIspezioniCoattivoV2BD : EntityBD<tab_ispezioni_coattivo_v2>
    {
        public TabIspezioniCoattivoV2BD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_ispezioni_coattivo_v2> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_ispezioni_coattivo_v2 GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_ispezione_coattivo == p_id);
        }

        /// <summary>
        /// Filtro per flag_ispezione_veicoli e flag_fine_ispezione_veicoli
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_ispezioni_coattivo_v2> GetListRicercaVeicoli(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.flag_ispezione_veicoli == "1" && c.flag_fine_ispezione_veicoli == null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_idEnte"></param>
        /// <param name="p_idRisorsaAssegnata"></param>
        /// <returns></returns>
        public static IQueryable<tab_ispezioni_coattivo_v2> GetListAttiDaEmettere(dbEnte p_dbContext, int p_idEnte, int p_idRisorsaAssegnata = -1)
        {
            IQueryable<tab_ispezioni_coattivo_v2> res = GetList(p_dbContext)
                .Where(
                        ic => ic.id_ente == p_idEnte
                            &&
                            ic.flag_esito_ispezione_totale.CompareTo("1") == 0
                            &&
                            ic.flag_supervisione_finale.CompareTo("1") != 0
                    //TODO Davide: condizione secondo flag
                    );

            //TODO Davide: Stati ispezione?
            //TODO Davide: recupero pratica in lavorazione? così è randomica quale pratica si visualizza
            res = res
                .Where(
                        ic => !ic.id_risorsa_supervisione.HasValue
                    ||
                        ic.id_risorsa_supervisione.HasValue && ic.id_risorsa_supervisione.Value == p_idRisorsaAssegnata);

            return res;
        }

        /// <summary>
        /// Filtro per flag_ispezione_terzi e flag_fine_ispezione_terzi
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_ispezioni_coattivo_v2> GetListRicercaTerzi(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.flag_ispezione_terzi == "1" && c.flag_fine_ispezione_terzi == null);
        }
    }
}
