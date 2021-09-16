using Publisoftware.Data.LinqExtended;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Publisoftware.Data.BD
{
    public class TabSpedNotBD : EntityBD<tab_sped_not>
    {
        public TabSpedNotBD()
        {

        }

        public static string generateBarcodeAnnorif(tab_avv_pag avviso, dbEnte ctx)
        {
            int progToUse = TabProgAvvPagBD.IncrementaProgressivoSpedNotCorrente(avviso.id_tipo_avvpag, DateTime.Now.Year, null, ctx);

            anagrafica_tipo_avv_pag anaTipoAvv = AnagraficaTipoAvvPagBD.GetById(avviso.id_tipo_avvpag, ctx);

            anagrafica_ente anaEnte = AnagraficaEnteBD.GetById(avviso.id_ente, ctx);

            string barcodeTouse = String.Empty;
            barcodeTouse = anaEnte.cod_ente + anaTipoAvv.cod_tipo_avv_pag.Substring(0, 4) + avviso.anno_riferimento.Substring(Math.Max(0, avviso.anno_riferimento.Length - 2)) + progToUse.ToString("D8");

            return barcodeTouse;
        }

        /// <summary>
        /// Genera barcode per avviso in input
        /// </summary>
        /// <param name="avviso"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static string generateBarcode(tab_avv_pag avviso, dbEnte ctx)
        {
            int progToUse = TabProgAvvPagBD.IncrementaProgressivoSpedNotCorrente(avviso.id_tipo_avvpag, avviso.dt_emissione.Value.Year, null, ctx);

            anagrafica_tipo_avv_pag anaTipoAvv = AnagraficaTipoAvvPagBD.GetById(avviso.id_tipo_avvpag, ctx);

            anagrafica_ente anaEnte = AnagraficaEnteBD.GetById(avviso.id_ente, ctx);

            string barcodeTouse = String.Empty;
            barcodeTouse = anaEnte.cod_ente + anaTipoAvv.cod_tipo_avv_pag.Substring(0, 4) + avviso.dt_emissione.Value.ToString("yy") + progToUse.ToString("D8");

            return barcodeTouse;
        }


        public static string generateBarcodeAvvisoTrasmessi(tab_avv_pag avviso, dbEnte ctx)
        {
            int progToUse = TabProgAvvPagBD.IncrementaProgressivoSpedNotCorrente(avviso.id_tipo_avvpag, avviso.dt_emissione.Value.Year, null, ctx);
            anagrafica_ente anaEnte = AnagraficaEnteBD.GetById(avviso.id_ente, ctx);
            string barcodeTouse = String.Empty;
            barcodeTouse = anaEnte.cod_ente + "TRA_" + avviso.dt_emissione.Value.ToString("yy") + progToUse.ToString("D8");

            return barcodeTouse;
        }


        /// <summary>
        /// Genera barcode per avviso in input
        /// </summary>
        /// <param name="avviso"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static string generateBarcodeFromIdTipoAvvPagAndIdEnte(int v_id_tipo_avvpag, int v_id_ente, dbEnte ctx)
        {
            int progToUse = TabProgAvvPagBD.IncrementaProgressivoSpedNotCorrente(v_id_tipo_avvpag, DateTime.Now.Year, null, ctx);

            anagrafica_tipo_avv_pag anaTipoAvv = AnagraficaTipoAvvPagBD.GetById(v_id_tipo_avvpag, ctx);

            anagrafica_ente anaEnte = AnagraficaEnteBD.GetById(v_id_ente, ctx);

            string barcodeTouse = String.Empty;
            barcodeTouse = anaEnte.cod_ente + anaTipoAvv.cod_tipo_avv_pag.Substring(0, 4) + DateTime.Now.ToString("yy") + progToUse.ToString("D8");

            return barcodeTouse;
        }

        /// <summary>
        /// Genera barcode per avviso in input
        /// </summary>
        /// <param name="avviso"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static string generateBarcode(int id_avviso, dbEnte ctx)
        {
            tab_avv_pag selAvv = TabAvvPagBD.GetById(id_avviso, ctx);

            int progToUse = TabProgAvvPagBD.IncrementaProgressivoCorrente(selAvv.id_tab_avv_pag, DateTime.Now.Year, null, ctx);

            anagrafica_tipo_avv_pag anaTipoAvv = AnagraficaTipoAvvPagBD.GetById(selAvv.id_tipo_avvpag, ctx);

            string barcodeTouse = String.Empty;
            barcodeTouse = anaTipoAvv.cod_tipo_avv_pag.Substring(0, 4) + DateTime.Now.Year.ToString() + progToUse.ToString("D8");

            return barcodeTouse;
        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_sped_not> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || (d.id_contribuente != null && p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente.Value)));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_sped_not GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_sped_not == p_id);
        }

        public static tab_sped_not getByLastBarcode(string p_barcode, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(w => w.barcode == p_barcode || w.barcode_avvdep == p_barcode).OrderByDescending(o => o.id_sped_not).FirstOrDefault();
        }

        public static tab_sped_not getByBarcode(string p_barcode, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(w => w.barcode == p_barcode).FirstOrDefault();
        }

        public static tab_sped_not getPECByBarcode(string p_barcode, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(w => w.barcode == p_barcode
                                                && w.id_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC
                                                && w.flag_firma == "1").FirstOrDefault();
        }

        /// <summary>
        /// Elenco atti da inviare via sms
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_sped_not> GetListPerInvioSms(dbEnte p_dbContext)
        {//bisogna definire i seguenti campi er utilizzare la funzione
         //c.id_tipo_lista==0 && c.cod_stato=="da definire" && c.id_stato == 0 && c.id_stato_sped_not == 0
            return GetList(p_dbContext).Where(c => c.cod_stato == anagrafica_stato_sped_not.SPE_PRE && c.id_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_SMS && c.id_stato == anagrafica_stato_sped_not.SPE_PRE_ID && c.id_stato_sped_not == anagrafica_stato_sped_not.SPE_PRE_ID);
        }

        /// <summary>
        /// Elenco avvisi da inviare via Pec
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_sped_not> GetListInvioAvvisiPecByIdListaSpedizione(Int32 p_IdListaSpedizione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).WhereByIdTipoSpedizione(anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC)
                                       .WhereByIdListaSpedizione(p_IdListaSpedizione)
                                       .WhereByNonSpedita()
                                       .Where(w => w.flag_firma == "1" && !w.id_doc_output.HasValue);
        }

        /// <summary>
        /// Elenco esiti da inviare via Pec
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_sped_not> GetListInvioEsitiPecByIdListaSpedizione(Int32 p_IdListaSpedizione, dbEnte p_dbContext)
        {
            List<int> v_idTipoDocEsclusi = new List<int>() { tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_CANCELLAZIONE_FERMO_OUTPUT,
                                                             tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_REVOCA_FERMO_OUTPUT};

            return GetList(p_dbContext).WhereByIdTipoSpedizione(anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC)
                                       .WhereByIdListaSpedizione(p_IdListaSpedizione)
                                       .WhereByNonSpedita()
                                       .Where(w => w.flag_firma == "1" && w.id_doc_output.HasValue && !w.pec_destinatario.ToLower().EndsWith("@pec.aci.it")
                                       && !v_idTipoDocEsclusi.Contains(w.tab_doc_output.id_tipo_doc_entrate));
        }

        /// <summary>
        /// Elenco avvisi a mezzo PEC da firmare da una singola lista di spedizione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_sped_not> GetListAvvisiPecDaFirmareByIdListaSpedizione(Int32 p_IdListaSpedizione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).WhereByIdTipoSpedizione(anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC)
                                       .WhereByIdListaSpedizione(p_IdListaSpedizione)
                                       .WhereByNonSpedita()
                                       .Where(s => !s.id_doc_output.HasValue)
                                       .Where(s => s.flag_firma == null || s.flag_firma == "0");
        }

        /// <summary>
        /// Elenco esiti a mezzo PEC da firmare da una singola lista di spedizione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_sped_not> GetListEsitiPecDaFirmareByIdListaSpedizione(Int32 p_IdListaSpedizione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).WhereByIdTipoSpedizione(anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC)
                                       .WhereByIdListaSpedizione(p_IdListaSpedizione)
                                       .WhereByNonSpedita()
                                       .Where(s => !s.pec_destinatario.ToLower().EndsWith("@pec.aci.it"))
                                       .Where(s => s.id_doc_output.HasValue)
                                       .Where(s => s.flag_firma == null || s.flag_firma == "0");
        }

        /// <summary>
        /// Elenco avvisi da inviare via email
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_sped_not> GetListInvioAvvisiMailByIdListaSpedizione(Int32 p_IdListaSpedizione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).WhereByIdTipoSpedizione(anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_EMAIL)
                                       .WhereByIdListaSpedizione(p_IdListaSpedizione)
                                       .WhereByNonSpedita()
                                       .Where(w => !w.id_doc_output.HasValue);
        }

        /// <summary>
        /// Elenco esiti da inviare via email
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_sped_not> GetListInvioEsitiPerEmail(dbEnte p_dbContext)
        {
            List<int> v_idTipoDocEsclusi = new List<int>() { tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_CANCELLAZIONE_FERMO_OUTPUT,
                                                             tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_REVOCA_FERMO_OUTPUT};

            return GetList(p_dbContext).WhereByIdTipoSpedizione(anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_EMAIL)
                                       .WhereByNonSpedita()
                                       .Where(w => w.id_doc_output.HasValue && !v_idTipoDocEsclusi.Contains(w.tab_doc_output.id_tipo_doc_entrate));
        }

        /// <summary>
        /// Elenco esiti da inviare via email
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_sped_not> GetListInvioEsitiEmailByIdListaSpedizione(Int32 p_IdListaSpedizione, dbEnte p_dbContext)
        {
            return GetListInvioEsitiPerEmail(p_dbContext).WhereByIdListaSpedizione(p_IdListaSpedizione);
        }

        /// <summary>
        /// Elenco Pec con esito da scaricare
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_sped_not> GetListPecDaEsitare(dbEnte p_dbContext, Int32? p_idTipoAvviso = null)
        {
            IQueryable<tab_sped_not> risp = GetList(p_dbContext).WhereByIdTipoSpedizione(anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC)
                                                                .Where(w => w.flag_firma == "1")
                                                                .Where(w => w.cod_stato == anagrafica_stato_sped_not.SPE_OKK);

            if (p_idTipoAvviso != null)
            {
                risp = risp.Where(s => s.id_tipo_avv_pag == p_idTipoAvviso);
            }

            return risp;
        }

        /// <summary>
        /// Minima data da esitare per l'indirizzo PEC mittente
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static DateTime? GetMinDataDaEsitarePec(dbEnte p_dbContext, String p_indirizzoPec)
        {
            DateTime? risp = GetList(p_dbContext).Where(s => s.pec_mittente == p_indirizzoPec)
                                                .WhereByIdTipoSpedizione(anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC)
                                                .Where(w => w.flag_firma == "1")
                                                .Where(w => w.cod_stato == anagrafica_stato_sped_not.SPE_OKK)
                                                .Select(s => s.dt_spedizione_notifica.Value).Min();

            return risp;
        }

        public static bool UopdateCodStatoAvvSped(string p_nome_lotto, string p_dataSpedizione, dbEnte p_dbContext)
        {
            string v_sqlCommand = string.Empty;
            if (p_nome_lotto.Length > 0)
            {
                TimeSpan v_timeOut = new TimeSpan(1, 0, 0);
                using (TransactionScope v_trans = new TransactionScope(TransactionScopeOption.Required, v_timeOut))
                {
                    try
                    {

                        if (TabSpedNotBD.GetList(p_dbContext).Where(w => w.nome_lotto_spedizione == p_nome_lotto).First().anagrafica_tipo_spedizione_notifica.sigla_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.SIGLA_NME_MESSO_NOTIFICATORE_SPECIALE)
                        {
                            //Nel caso dei messi non deve cambiare lo stato alle sped not, ma solo alla lista di spedizione
                        }
                        else if (TabSpedNotBD.GetList(p_dbContext).Where(w => w.nome_lotto_spedizione == p_nome_lotto).First().anagrafica_tipo_spedizione_notifica.flag_sped_not != anagrafica_tipo_spedizione_notifica.POSTA_ORDINARIA)
                        {
                            //AGGIORNO LE SPED_NOT
                            v_sqlCommand = string.Concat("UPDATE ", nameof(tab_sped_not), " SET ", nameof(tab_sped_not.cod_stato), " = @COD_STATO, ",
                                                    nameof(tab_sped_not.id_stato), " = @ID_STATO, ",
                                                    nameof(tab_sped_not.id_stato_sped_not), " = @ID_STATO_SPED_NOT ",
                                                    " WHERE ", nameof(tab_sped_not.nome_lotto_spedizione), " = @nome_lotto");

                            p_dbContext.Database.ExecuteSqlCommand(v_sqlCommand
                               , new SqlParameter("@COD_STATO", anagrafica_stato_sped_not.SPE_OKK)
                               , new SqlParameter("@ID_STATO", anagrafica_stato_sped_not.SPE_OKK_ID)
                               , new SqlParameter("@ID_STATO_SPED_NOT", anagrafica_stato_sped_not.SPE_OKK_ID)
                               , new SqlParameter("@nome_lotto", p_nome_lotto));
                        }
                        else
                        {
                            //AGGIORNO LE SPED_NOT
                            v_sqlCommand = string.Concat("UPDATE ", nameof(tab_sped_not), " SET ", nameof(tab_sped_not.cod_stato), " = @COD_STATO, ",
                                                    nameof(tab_sped_not.id_stato), " = @ID_STATO, ",
                                                    nameof(tab_sped_not.id_stato_sped_not), " = @ID_STATO_SPED_NOT, ",
                                                    nameof(tab_sped_not.dt_spedizione_notifica), " = @DATA_SPEDIZIONE ",
                                                    " WHERE ", nameof(tab_sped_not.nome_lotto_spedizione), " = @nome_lotto");

                            p_dbContext.Database.ExecuteSqlCommand(v_sqlCommand
                               , new SqlParameter("@COD_STATO", anagrafica_stato_sped_not.SPE_OKK)
                               , new SqlParameter("@ID_STATO", anagrafica_stato_sped_not.SPE_OKK_ID)
                               , new SqlParameter("@ID_STATO_SPED_NOT", anagrafica_stato_sped_not.SPE_OKK_ID)
                               , new SqlParameter("@DATA_SPEDIZIONE", DateTime.ParseExact(p_dataSpedizione, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                               , new SqlParameter("@nome_lotto", p_nome_lotto));
                        }


                        //AGGIORNO GLI AVVISI
                        v_sqlCommand = string.Concat("UPDATE ", nameof(tab_avv_pag), " SET ", nameof(tab_avv_pag.cod_stato), " = @COD_STATO, ",
                                            nameof(tab_avv_pag.cod_stato_avv_pag), " = @COD_STATO_AVV_PAG, ",
                                            nameof(tab_avv_pag.id_stato), " =  @ID_STATO, ",
                                            nameof(tab_avv_pag.id_stato_avv_pag), " = @ID_STATO_AVV_PAG ",
                            " WHERE ", nameof(tab_avv_pag.cod_stato), " = '", anagrafica_stato_avv_pag.VALIDO_DA_SPEDIRE_NOTIFICARE, "' AND ",
                                       nameof(tab_avv_pag.id_tab_avv_pag), " in (Select ", nameof(tab_sped_not.id_avv_pag),
                                                                                 " FROM ", nameof(tab_sped_not),
                                                                                 " WHERE ", nameof(tab_sped_not.nome_lotto_spedizione), " =  @nome_lotto)");

                        p_dbContext.Database.ExecuteSqlCommand(v_sqlCommand
                            , new SqlParameter("@COD_STATO", anagrafica_stato_avv_pag.VAL_EME)
                            , new SqlParameter("@COD_STATO_AVV_PAG", anagrafica_stato_avv_pag.VAL_EME)
                            , new SqlParameter("@ID_STATO", anagrafica_stato_avv_pag.VAL_EME_ID)
                            , new SqlParameter("@ID_STATO_AVV_PAG", anagrafica_stato_avv_pag.VAL_EME_ID)
                            , new SqlParameter("@nome_lotto", p_nome_lotto));

                        ////AGGIORNO lo stato della/e Liste Di Spedizione
                        //List<int> v_lstSpedNotID = TabSpedNotBD.GetList(p_dbContext).Where(w => w.nome_lotto_spedizione == p_nome_lotto).Select(s => s.id_lista_sped_not.Value).Distinct().ToList();
                        int v_id_lista = TabSpedNotBD.GetList(p_dbContext).Where(w => w.nome_lotto_spedizione == p_nome_lotto).Select(s => s.id_lista_sped_not.Value).FirstOrDefault();
                        //foreach (int v_id_lista in v_lstSpedNotID)
                        //{
                        bool v_tuttoSpedito = !TabSpedNotBD.GetList(p_dbContext)
                                                          .WhereByIdListaSpedizione(v_id_lista)
                                                          .WhereByNonSpedita()
                                                          .Where(s => s.nome_lotto_spedizione != p_nome_lotto).Any();

                        if (v_tuttoSpedito)
                        {
                            TabListaSpedNotificheBD.GetById(v_id_lista, p_dbContext).cod_stato = tab_lista_sped_notifiche.SPE_DEF;
                        }

                        tab_liste v_listaEmissione = JoinLstEmissioneLstSpedNotBD.GetList(p_dbContext).Where(l => l.id_lista_sped_not == v_id_lista).FirstOrDefault().tab_liste;

                        bool v_tuttoListeSpedite = !v_listaEmissione.join_lst_emissione_lst_sped_not.Any(s => s.tab_lista_sped_notifiche.cod_stato != tab_lista_sped_notifiche.SPE_DEF);

                        if (v_tuttoListeSpedite)
                        {
                            v_listaEmissione.cod_stato = tab_liste.DEF_SPE;
                        }

                        p_dbContext.SaveChanges();
                        v_trans.Complete();
                    }
                    catch (Exception ex)
                    {
                        //loggare eventualmente
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool UopdateCodStatoAvvSpedComunicazioni(string p_nome_lotto, string p_dataSpedizione, dbEnte p_dbContext)
        {
            string v_sqlCommand = string.Empty;
            if (p_nome_lotto.Length > 0)
            {
                TimeSpan v_timeOut = new TimeSpan(1, 0, 0);
                using (TransactionScope v_trans = new TransactionScope(TransactionScopeOption.Required, v_timeOut))
                {
                    try
                    {

                        List<tab_sped_not> lstSpedNot = TabSpedNotBD.GetList(p_dbContext).Where(w => w.nome_lotto_spedizione == p_nome_lotto).ToList();
                        if (lstSpedNot.First().anagrafica_tipo_spedizione_notifica.flag_sped_not != anagrafica_tipo_spedizione_notifica.POSTA_ORDINARIA)
                        {
                            //AGGIORNO LE SPED_NOT
                            foreach (var v_sped in lstSpedNot)
                            {
                                v_sped.cod_stato = anagrafica_stato_sped_not.SPE_OKK;
                                v_sped.id_stato = anagrafica_stato_sped_not.SPE_OKK_ID;
                                v_sped.id_stato_sped_not = anagrafica_stato_sped_not.SPE_OKK_ID;
                                v_sped.nome_lotto_spedizione = p_nome_lotto;
                            }
                            p_dbContext.SaveChanges();
                            //v_sqlCommand = string.Concat("UPDATE ", nameof(tab_sped_not), " SET ", nameof(tab_sped_not.cod_stato), " = @COD_STATO, ",
                            //                        nameof(tab_sped_not.id_stato), " = @ID_STATO, ",
                            //                        nameof(tab_sped_not.id_stato_sped_not), " = @ID_STATO_SPED_NOT ",
                            //                        " WHERE ", nameof(tab_sped_not.nome_lotto_spedizione), " = @nome_lotto");

                            //p_dbContext.Database.ExecuteSqlCommand(v_sqlCommand
                            //   , new SqlParameter("@COD_STATO", anagrafica_stato_sped_not.SPE_OKK)
                            //   , new SqlParameter("@ID_STATO", anagrafica_stato_sped_not.SPE_OKK_ID)
                            //   , new SqlParameter("@ID_STATO_SPED_NOT", anagrafica_stato_sped_not.SPE_OKK_ID)
                            //   , new SqlParameter("@nome_lotto", p_nome_lotto));
                        }
                        else
                        {
                            //AGGIORNO LE SPED_NOT
                            foreach (var v_sped in lstSpedNot)
                            {
                                v_sped.cod_stato = anagrafica_stato_sped_not.SPE_OKK;
                                v_sped.id_stato = anagrafica_stato_sped_not.SPE_OKK_ID;
                                v_sped.id_stato_sped_not = anagrafica_stato_sped_not.SPE_OKK_ID;
                                v_sped.nome_lotto_spedizione = p_nome_lotto;
                                v_sped.dt_spedizione_notifica = DateTime.ParseExact(p_dataSpedizione, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            }
                            p_dbContext.SaveChanges();
                            //v_sqlCommand = string.Concat("UPDATE ", nameof(tab_sped_not), " SET ", nameof(tab_sped_not.cod_stato), " = @COD_STATO, ",
                            //                        nameof(tab_sped_not.id_stato), " = @ID_STATO, ",
                            //                        nameof(tab_sped_not.id_stato_sped_not), " = @ID_STATO_SPED_NOT, ",
                            //                        nameof(tab_sped_not.dt_spedizione_notifica), " = @DATA_SPEDIZIONE ",
                            //                        " WHERE ", nameof(tab_sped_not.nome_lotto_spedizione), " = @nome_lotto");

                            //p_dbContext.Database.ExecuteSqlCommand(v_sqlCommand
                            //   , new SqlParameter("@COD_STATO", anagrafica_stato_sped_not.SPE_OKK)
                            //   , new SqlParameter("@ID_STATO", anagrafica_stato_sped_not.SPE_OKK_ID)
                            //   , new SqlParameter("@ID_STATO_SPED_NOT", anagrafica_stato_sped_not.SPE_OKK_ID)
                            //   , new SqlParameter("@DATA_SPEDIZIONE", DateTime.ParseExact(p_dataSpedizione, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                            //   , new SqlParameter("@nome_lotto", p_nome_lotto));
                        }


                        ////AGGIORNO lo stato della/e Liste Di Spedizione
                        List<int> v_lstSpedNotID = TabSpedNotBD.GetList(p_dbContext).Where(w => w.nome_lotto_spedizione == p_nome_lotto).Select(s => s.id_lista_sped_not.Value).Distinct().ToList();
                        foreach (int v_id_lista in v_lstSpedNotID)
                        {
                            bool v_tuttoSpedito = !TabSpedNotBD.GetList(p_dbContext)
                                                              .WhereByIdListaSpedizione(v_id_lista)
                                                              .Where(s => s.nome_lotto_spedizione != p_nome_lotto).Any();

                            if (v_tuttoSpedito)
                            {
                                TabListaSpedNotificheBD.GetById(v_id_lista, p_dbContext).cod_stato = tab_lista_sped_notifiche.SPE_DEF;
                            }
                        }
                        p_dbContext.SaveChanges();
                        v_trans.Complete();
                    }
                    catch (Exception ex)
                    {
                        //loggare eventualmente
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
