using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabApplicazioniBD : EntityBD<tab_applicazioni>
    {
        public const int MaxLivelloAutorizzativo = 3;

        public TabApplicazioniBD()
        {

        }

        /// <summary>
        /// Restituisce l'elenco delle applicazioni che la risorsa puo'eseguire per la struttura, ente, modalità operativa e livello di autorizzazione di accesso
        /// </summary>
        /// <param name="p_idRisorsa">ID Risorsa di ricerca</param>
        /// <param name="p_idStruttura">ID Struttura di accesso</param>
        /// <param name="p_idEnte">ID Ente di accesso</param>
        /// <param name="p_modalitaOperativa">Modalità operativa</param>
        /// <param name="p_livelloAutorizzativo">Livello di autorizzazione</param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_withInclude">Includi tabelle secondarie nel caricamento dei dati</param>
        /// <returns></returns>
        public static IQueryable<tab_applicazioni> GetEnableApplicationList(int p_idRisorsa, int p_idStruttura, int p_idEnte, ModalitaOperativaEnum? p_modalitaOperativa, int p_livelloAutorizzativo, dbEnte p_dbContext, bool p_withInclude = true)
        {
            string v_modOperativa = p_modalitaOperativa != null ? String.Format("{0}", (char)p_modalitaOperativa) : String.Empty;
            string v_modOperativaAll = ((char)ModalitaOperativaEnum.ALL).ToString();

            IQueryable<tab_applicazioni> v_applicazioniList;

            IQueryable<tab_applicazioni> v_applicazioniAbilitateList = TabAbilitazioneBD.GetList(p_dbContext)
                                                                            .Where(ap => ap.id_risorsa == p_idRisorsa && ap.id_struttura_aziendale == p_idStruttura
                                                                                        && ap.id_ente == p_idEnte && ap.flag_abilitato
                                                                                        && (v_modOperativa == "" || ap.tab_applicazioni.tipo_applicazione == "G" || ap.tab_applicazioni.tipo_applicazione == v_modOperativa || ap.tab_applicazioni.tipo_applicazione == v_modOperativaAll)
                                                                                        && ap.tab_applicazioni.livello_autorizzazione_interno <= p_livelloAutorizzativo)
                                                                                        .Select(ap => ap.tab_applicazioni);

            IQueryable<tab_applicazioni> v_appSistemaList = TabApplicazioniBD.getApplicazioniDiSistema(p_dbContext);

            IQueryable<tab_applicazioni> v_appSecondarieList = v_applicazioniAbilitateList.SelectMany(j => j.join_applicazioni_secondarie).Select(s => s.tab_applicazioni1).Where(a => a.tipo_applicazione == v_modOperativa || a.tipo_applicazione == v_modOperativaAll).Distinct();

            v_applicazioniList = v_applicazioniAbilitateList.Union(v_appSistemaList);
            v_applicazioniList = v_applicazioniList.Union(v_appSecondarieList);

            IQueryable<tab_applicazioni> v_applicazioniListInclude = v_applicazioniList.Union(v_appSistemaList)
                                                                .Include(x => x.tab_funzionalita)
                                                                .Include(y => y.tab_funzionalita.tab_procedure);

            if (p_withInclude)
                return v_applicazioniListInclude;
            else
                return v_applicazioniList;

        }

        /// <summary>
        /// restituisce le applicazioni di sistema
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_applicazioni> getApplicazioniDiSistema(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.flag_sistema);
        }

        /// <summary>
        /// restituisce le applicazioni abilitabili dal responsabile
        /// </summary>
        /// <param name="p_idRisorsa"></param>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_idEnte"></param>
        /// <param name="p_idEntrata"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_idFunzione"></param>
        /// <returns></returns>
        public static IQueryable<tab_applicazioni> getCanBeEnabledApplicationListForResponsabile(int p_idRisorsa, int p_idStruttura, int p_idEnte, int p_idEntrata, dbEnte p_dbContext, int p_idFunzione = 0)
        {
            anagrafica_strutture_aziendali struttura = AnagraficaStruttureAziendaliBD.GetById(p_idStruttura, p_dbContext);

            if (struttura == null)
                throw new Exception(String.Format("Struttura con id {0} non trovata", p_idStruttura));

            if (!struttura.id_risorsa.HasValue)
                throw new Exception(String.Format("Struttura '{0}' non ha responsabile.", struttura.descr_struttura));

            //Lista applicazioni di partenza: quella abilitate al responsabile della struttura
            IQueryable<tab_applicazioni> appsAbilitabili = TabApplicazioniBD.GetEnableApplicationList(struttura.id_risorsa.Value, p_idStruttura, p_idEnte, null, Int32.MaxValue, p_dbContext, false);

            if (p_idFunzione > 0)
                appsAbilitabili = appsAbilitabili.Where(a => a.id_tab_funzionalita == p_idFunzione);

            //Elimino le applicazioni di sistema
            appsAbilitabili = appsAbilitabili.Where(a => !a.flag_sistema);

            //Elimino le applicazioni già assegnate alla terna (risorsa , struttura , Ente)
            //appsAbilitabili = appsAbilitabili.Where(a => !a.tab_abilitazione.Any(ab => ab.id_risorsa == p_idRisorsa && ab.id_struttura_aziendale == p_idStruttura && ab.id_ente == p_idEnte));

            return appsAbilitabili;
        }

        /// <summary>
        /// restituisce le applicazioni abilitabili
        /// </summary>
        /// <param name="p_idRisorsa"></param>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_idEnte"></param>
        /// <param name="p_idEntrata"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_idFunzione"></param>
        /// <returns></returns>
        public static IQueryable<tab_applicazioni> getCanBeEnabledApplicationList(int p_idRisorsa, int p_idStruttura, int p_idEnte, int p_idEntrata, dbEnte p_dbContext, int p_idFunzione = 0)
        {
            IQueryable<tab_applicazioni> appsAbilitabili = GetList(p_dbContext);

            if (p_idFunzione > 0)
                appsAbilitabili = appsAbilitabili.Where(a => a.id_tab_funzionalita == p_idFunzione);

            //Elimino le applicazioni di sistema
            appsAbilitabili = appsAbilitabili.Where(a => !a.flag_sistema);

            //Elimino le applicazioni già assegnate alla terna (risorsa, struttura, ente)
            //appsAbilitabili = appsAbilitabili.Where(a => !a.tab_abilitazione.Any(ab => ab.id_risorsa == p_idRisorsa && ab.id_struttura_aziendale == p_idStruttura && ab.id_ente == p_idEnte));

            return appsAbilitabili;
        }

        /// <summary>
        /// Restituisce l'applicazione associata al FullCode
        /// </summary>
        /// <param name="p_fullCode">FullCode di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_applicazioni GetByFullCode(string p_fullCode, dbEnte p_dbContext)
        {
            if (p_fullCode.Length >= 15)
            {
                string v_procCode = p_fullCode.Substring(0, 5);
                string v_funzCode = p_fullCode.Substring(5, 5);
                string v_appCode = p_fullCode.Substring(10, 5);

                tab_applicazioni v_applicazione = GetList(p_dbContext)
                                                  .Where(ap => ap.codice.Equals(v_appCode) && ap.tab_funzionalita.codice.Equals(v_funzCode) && ap.tab_funzionalita.tab_procedure.codice.Equals(v_procCode))
                                                  .Select(ap => ap).SingleOrDefault();

                return v_applicazione;
            }
            else
            {
                return null;
            }
        }
    }
}
