using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaTipoAvvPagBD : EntityBD<anagrafica_tipo_avv_pag>
    {
        public AnagraficaTipoAvvPagBD()
        {

        }

        /// <summary>
        /// Restituisce la lista dei tipi avvisi collegati alla lista degli ID Entrata indicate
        /// </summary>
        /// <param name="p_idEntrateList">Elenco ID Entrate di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_avv_pag> GetListByListIdEntrate(List<int> p_idEntrateList, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => p_idEntrateList.Contains(d.id_entrata)).OrderBy(d => d.id_entrata).ThenBy(d => d.id_tipo_avvpag);
        }

        /// <summary>
        /// Restituisce la lista dei tipi avvisi collegati all'ID ebtrata indicato
        /// </summary>
        /// <param name="p_idEntrata">ID Entrata</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_avv_pag> GetListByIdEntrata(int p_idEntrata, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_entrata == p_idEntrata).OrderBy(d => d.id_entrata).ThenBy(d => d.id_tipo_avvpag);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> GetListByListOfIdServizio(int[] p_idServizioList, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => p_idServizioList.Contains(d.id_servizio)).OrderBy(d => d.id_entrata).ThenBy(d => d.id_tipo_avvpag);
        }


        /// <summary>
        /// Restituisce la lista dei tipi avvisi validi per il coattivo
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_avv_pag> GetListForCoattivo(dbEnte p_dbContext)
        {
            IQueryable<anagrafica_tipo_servizi> tipiServizioCoattivo = AnagraficaTipoServiziBD.GetListForCoattivo(p_dbContext);

            IQueryable<anagrafica_tipo_avv_pag> tipiAvv = GetList(p_dbContext);

            IQueryable<anagrafica_tipo_avv_pag> ret = tipiAvv.Where(ta => tipiServizioCoattivo.Select(tsc => tsc.id_tipo_servizio).Contains(ta.id_servizio));

            return ret;
        }

        public static anagrafica_tipo_avv_pag GetTipoAvvisoForProfiloBeneCoattivo(dbEnte p_dbContext, int id_tab_profilo)
        {
            anagrafica_tipo_avv_pag selTipoAvv = null;

            tab_profilo_contribuente_new selProfilo = TabProfiloContribuenteNewBD.GetById(id_tab_profilo, p_dbContext);
            if (selProfilo != null)
            {
                switch (selProfilo.tab_tipo_bene.codice_bene)
                {
                    case tab_tipo_bene.SIGLA_VEICOLI:
                        selTipoAvv = GetById(anagrafica_tipo_avv_pag.PRE_FERMO_AMM, p_dbContext);
                        break;

                    case tab_tipo_bene.SIGLA_STIPENDI:
                    case tab_tipo_bene.SIGLA_LOCAZIONE:
                    case tab_tipo_bene.SIGLA_BANCHE:
                        selTipoAvv = GetById(anagrafica_tipo_avv_pag.PIGN_TERZI_ORD, p_dbContext);
                        break;

                    case tab_tipo_bene.SIGLA_PENSIONI:
                        selTipoAvv = GetById(anagrafica_tipo_avv_pag.PIGN_TERZI_CIT, p_dbContext);
                        break;
                    case tab_tipo_bene.SIGLA_IMMOBILI:
                        selTipoAvv = GetById(anagrafica_tipo_avv_pag.PRE_IPOTECA, p_dbContext);
                        break;
                    case tab_tipo_bene.SIGLA_MOBILI:
                        selTipoAvv = GetById(anagrafica_tipo_avv_pag.PIGN_MOB, p_dbContext);
                        break;
                }
            }

            return selTipoAvv;
        }

        /// <summary>
        /// Restituisce id_entrata
        /// </summary>
        /// <param name="p_idTabMovPag">
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new anagrafica_tipo_avv_pag GetByIdTipoAvvPag(int p_idTipoAvvPag, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tipo_avvpag == p_idTipoAvvPag);
        }
    }
}
