using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabAbilitazioneBD : EntityBD<tab_abilitazione>
    {
        public TabAbilitazioneBD()
        {

        }

        /// <summary>
        /// Indica se la risorsa è abilitata ad eseguire l'applicazione per la struttura ed ente di accesso
        /// </summary>
        /// <param name="p_idRisorsa">ID Risorsa da verificare</param>
        /// <param name="p_idStruttura">ID Struttura di accesso</param>
        /// <param name="p_idEnte">ID Ente di accesso</param>
        /// <param name="p_idApplicazione">ID Applicazione da verificare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool CanDo(int p_idRisorsa, int p_idStruttura, int p_idEnte, int p_idApplicazione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Any(ta => ta.id_risorsa == p_idRisorsa && ta.id_struttura_aziendale == p_idStruttura && ta.id_ente == p_idEnte && ta.id_tab_applicazioni == p_idApplicazione && ta.flag_abilitato);
        }

        /// <summary>
        /// Restituisce l'elenco delle abilitazioni (abilitate e non) di una risorsa per la struttura ed ente di accesso
        /// </summary>
        /// <param name="p_idRisorsa">ID Risorsa di ricerca</param>
        /// <param name="p_idStruttura">ID Struttura di accesso</param>
        /// <param name="p_idEnte">ID Ente di accesso</param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_loadProperties"></param>
        /// <returns></returns>
        public static IQueryable<tab_abilitazione> GetListFor(int p_idRisorsa, int p_idStruttura, int p_idEnte, dbEnte p_dbContext, bool p_loadProperties = false)
        {
            IQueryable<tab_abilitazione> abilitazioni = GetList(p_dbContext).Where(ab => ab.id_risorsa == p_idRisorsa && ab.id_struttura_aziendale == p_idStruttura && ab.id_ente == p_idEnte);

            if (p_loadProperties)
            {
                abilitazioni = abilitazioni.Include(ab => ab.tab_applicazioni);
            }

            return abilitazioni;
        }

        /// <summary>
        /// Filtro per l'id della risorsa e l'id della struttura
        /// </summary>
        /// <param name="p_idRisorsa"></param>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<tab_abilitazione> GetListByIdRisorsaIdStrutturaEnteWithDB(int p_idRisorsa, int p_idStruttura, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities)
                        .Where(d => d.anagrafica_ente.nome_db != null && d.id_risorsa == p_idRisorsa && d.id_struttura_aziendale == p_idStruttura);
        }

        /// <summary>
        /// Filtro per l'id della risorsa, l'id della struttura, l'id dell'ente e l'id dell'applicazione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_idRisorsa"></param>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_idEnte"></param>
        /// <param name="p_idApplicazione"></param>
        /// <returns></returns>
        public static IQueryable<tab_abilitazione> SearchAbilitazione(dbEnte p_dbContext, int p_idRisorsa = -1, int p_idStruttura = -1, int p_idEnte = -1, int p_idApplicazione = -1)
        {
            IQueryable<tab_abilitazione> abilitazioni = GetList(p_dbContext);

            if (p_idRisorsa > 0)
                abilitazioni = abilitazioni.Where(a => a.id_risorsa == p_idRisorsa);

            if (p_idStruttura > 0)
                abilitazioni = abilitazioni.Where(a => a.id_struttura_aziendale == p_idStruttura);

            if (p_idEnte > 0)
                abilitazioni = abilitazioni.Where(a => a.id_ente == p_idEnte);

            if (p_idApplicazione > 0)
                abilitazioni = abilitazioni.Where(a => a.id_tab_applicazioni == p_idApplicazione);

            return abilitazioni;
        }
    }
}
