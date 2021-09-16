using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;

namespace Publisoftware.Data.BD
{
    public class TabListaSpedNotificheBD : EntityBD<tab_lista_sped_notifiche>
    {
        public TabListaSpedNotificheBD()
        {

        }

        /// <summary>
        /// Restituisce le liste di spedizione Avvisi da spedire per PEC con all'interno almeno una sped_not firmata digitalmente
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_lista_sped_notifiche> GetListAvvisiDaInviarePerPec(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).WhereStatoStampaCreata()
                                       .Where(d => d.join_lst_emissione_lst_sped_not.Any(l => l.tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.FLAG_TIPO_LISTA_C))//Solo spedizioni collegate a liste di Calcolo
                                       .Where(l => l.tab_sped_not.Any(s => s.flag_firma == "1" && !s.id_doc_output.HasValue
                                                                        && s.cod_stato == anagrafica_stato_sped_not.SPE_PRE
                                                                        && s.id_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC))
                                       .OrderBy(o => o.id_lista);
        }

        /// <summary>
        /// Restituisce le liste di spedizione Avvisi da spedire per EMail
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_lista_sped_notifiche> GetListAvvisiDaInviarePerMail(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).WhereStatoStampaCreata()
                                       .Where(d => d.join_lst_emissione_lst_sped_not.Any(l => l.tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.FLAG_TIPO_LISTA_C))//Solo spedizioni collegate a liste di Calcolo
                                       .Where(l => l.tab_sped_not.Any(s => !s.id_doc_output.HasValue
                                                                        && s.cod_stato == anagrafica_stato_sped_not.SPE_PRE
                                                                        && s.id_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_EMAIL))
                                       .OrderBy(o => o.id_lista);
        }

        /// <summary>
        /// Restituisce le liste di spedizione Esiti da spedire per PEC con all'interno almeno una sped_not firmata digitalmente
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_lista_sped_notifiche> GetListEsitiDaInviarePerPec(dbEnte p_dbContext)
        {
            List<int> v_idTipoDocEsclusi = new List<int>() { tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_CANCELLAZIONE_FERMO_OUTPUT,
                                                             tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_REVOCA_FERMO_OUTPUT};

            return GetList(p_dbContext).Where(d => d.tab_tipo_lista.cod_lista == tab_tipo_lista.TIPOLISTA_COMUNICAZIONI)
                                       .Where(l => l.tab_sped_not.Any(s => s.flag_firma == "1" && s.id_doc_output.HasValue
                                                                        && s.cod_stato == anagrafica_stato_sped_not.SPE_PRE
                                                                        && s.id_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC
                                                                        && !v_idTipoDocEsclusi.Contains(s.tab_doc_output.id_tipo_doc_entrate)
                                                                        && !s.pec_destinatario.ToLower().EndsWith("@pec.aci.it")))
                                       .OrderBy(o => o.id_lista);
        }

        /// <summary>
        /// Restituisce le liste di spedizione Esiti da spedire per EMail
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_lista_sped_notifiche> GetListEsitiDaInviarePerMail(dbEnte p_dbContext)
        {
            List<int> v_idTipoDocEsclusi = new List<int>() { tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_CANCELLAZIONE_FERMO_OUTPUT,
                                                             tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_REVOCA_FERMO_OUTPUT};

            return GetList(p_dbContext).Where(d => d.tab_tipo_lista.cod_lista == tab_tipo_lista.TIPOLISTA_COMUNICAZIONI)
                                       .Where(l => l.tab_sped_not.Any(s => s.id_doc_output.HasValue
                                                                        && s.cod_stato == anagrafica_stato_sped_not.SPE_PRE
                                                                        && s.id_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_EMAIL
                                                                        && !v_idTipoDocEsclusi.Contains(s.tab_doc_output.id_tipo_doc_entrate)))
                                       .OrderBy(o => o.id_lista);
        }

        /// <summary>
        /// Restituisce le liste di spedizione Revoche/Cancellazioni ACI da spedire per PEC con all'interno almeno una sped_not firmata digitalmente
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_lista_sped_notifiche> GetListAciDaInviarePerPec(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).WhereStatoStampaCreata()
                                       .Where(d => d.join_lst_emissione_lst_sped_not.Any(l => l.tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.TIPOLISTA_COMUNICAZIONI))//Solo spedizioni collegate a liste di Calcolo
                                       .Where(l => l.tab_sped_not.Any(s => s.flag_firma == "1" && s.id_doc_output.HasValue
                                                                        && s.cod_stato == anagrafica_stato_sped_not.SPE_PRE
                                                                        && s.id_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC
                                                                        && s.tab_doc_output.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_CANCELLAZIONE_FERMO_OUTPUT))
                                       .OrderBy(o => o.id_lista);
        }

        public static IQueryable<tab_lista_sped_notifiche> GetListByIdListaEmissione(int p_idListaEmissione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(l => l.join_lst_emissione_lst_sped_not.Any(e => e.id_lista_emissione == p_idListaEmissione));
        }

        /// <summary>
        /// Restituisce le liste di spedizione avvisi a mezzo PEC da firmare digitalmente
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_lista_sped_notifiche> GetListAvvisiPecDaFirmare(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(l => l.tab_sped_not.Any(s => (s.flag_firma == null || s.flag_firma == "0")
                                                                        && s.cod_stato == anagrafica_stato_sped_not.SPE_PRE
                                                                        && s.id_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC
                                                                        && s.cod_stato.Equals(tab_lista_sped_notifiche.DEF_STA)
                                                                        && !s.id_doc_output.HasValue
                                                                        && !s.pec_destinatario.ToLower().EndsWith("@pec.aci.it")
                                                                        ));
        }

        /// <summary>
        /// Restituisce le liste di spedizione esiti a mezzo PEC da firmare digitalmente
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_lista_sped_notifiche> GetListEsitiPecDaFirmare(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(l => l.tab_sped_not.Any(s => (s.flag_firma == null || s.flag_firma == "0")
                                                                        && s.cod_stato == anagrafica_stato_sped_not.SPE_PRE
                                                                        && s.id_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC
                                                                        && s.id_doc_output.HasValue
                                                                        && !s.pec_destinatario.ToLower().EndsWith("@pec.aci.it")
                                                                        ));

        }

        public static IQueryable<tab_lista_sped_notifiche> GetListById(int p_idListaEmissione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(l => l.id_lista == p_idListaEmissione);
        }
    }
}
