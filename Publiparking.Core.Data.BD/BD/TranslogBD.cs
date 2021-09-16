using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.dto;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Core.Data.SqlServer.LinqExtended;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class TranslogBD : EntityBD<translog>
    {

        public const int SERVIZIO_TESSERA = 346;

        public TranslogBD()
        {

        }

        /// <summary>
        /// Elenco dei pagamenti fatti per targa
        /// </summary>
        /// <param name="p_context"></param>
        /// <returns></returns>
        //public static IQueryable<TransLogDTO> getListTranslogLocalTargheByTarga(string p_targa, DbParkContext p_dbContext)
        //{

        //    IQueryable<TransLogDTO> v_listTransLog = GetList(p_dbContext).Where(t => t.tlLicenseNo_isNumeric.HasValue && t.tlLicenseNo_isNumeric.Value == 0)
        //                                                               .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
        //                                                               .Where(t => t.tlAmount > 0)
        //                                                               .Where(t => t.tlLicenseNo.Trim() == p_targa)
        //                                                               .OrderByDescending(o => o.tlPayDateTime.Value)
        //                                                               .Take(1)
        //                                                               .ToDTO();

        //    IQueryable<TransLogDTO> v_listpyng = TranslogPyngBD.GetList(p_dbContext).Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
        //                                                                          .Where(t => t.tlLicenseNo.Trim() == p_targa)
        //                                                                          .OrderByDescending(o => o.tlPayDateTime.Value)
        //                                                                          .Take(1)
        //                                                                          .ToTranslogDTO();

        //    IQueryable<TransLogDTO> v_listphonzie = TranslogPhonzieBD.GetList(p_dbContext).Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
        //                                                                               .Where(t => t.tlLicenseNo.Trim() == p_targa)
        //                                                                               .OrderByDescending(o => o.tlPayDateTime.Value)
        //                                                                               .Take(1)
        //                                                                               .ToTranslogDTO();
        //    return v_listTransLog.Union(v_listpyng).Union(v_listphonzie);
        //}

        public static decimal getTotaleRicariche(string p_codice_abbonamento, DbParkContext p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.tlLicenseNo.Equals(p_codice_abbonamento)).Sum(a => a.tlAmount) ?? 0;
        }

        public static IQueryable<translog> getPagamentoByCodiceAbbonamento(string p_codice_abbonamento, DbParkContext p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.tlLicenseNo.Equals(p_codice_abbonamento));
        }

        public static IQueryable<translog> getListRicaricheNonConfermate(DbParkContext p_dbContext)
        {
            // List<int> rcIdsList = RicaricheConfermateBD.GetList(p_context).Select(rc=> rc.idRicaricaConfermata).ToList();
            List<string> codiceabbonamentiList = AbbonamentiBD.GetList(p_dbContext).Select(rc => rc.codice).Distinct().ToList();

            return GetList(p_dbContext).Where(r => !string.IsNullOrEmpty(r.tlLicenseNo) && r.tlLicenseNo.Length == 12)
                                     .Where(r => r.tlPayType.HasValue && r.tlPayType.Value > 0)
                                     .Where(r => r.tlAmount.HasValue && r.tlAmount.Value > 0)
                                     .Where(r => !r.RicaricheConfermate.Any())
                                     //.Where(r => r.RicaricheConfermate.Count() == 0)
                                     //.Where(r=> !rcIdsList.Contains(r.tlRecordID))
                                     .Where(r => codiceabbonamentiList.Contains(r.tlLicenseNo));
        }


        public static IQueryable<translog> getListRicaricheAbbonamentiDaElaborare(DbParkContext p_dbContext)
        {
            List<string> codiceabbonamentiPeriodiciList = AbbonamentiPeriodiciBD.GetList(p_dbContext).Select(rc => rc.codice).Distinct().ToList();
            List<string> codiceabbonamentiRinnoviList = AbbonamentiRinnoviBD.GetList(p_dbContext).Select(rc => rc.codice).Distinct().ToList();

            return getListRicaricheAbbonamenti(p_dbContext).Where(r => codiceabbonamentiPeriodiciList.Contains(r.tlLicenseNo))
                                                         .Where(r => !codiceabbonamentiRinnoviList.Contains(r.tlPDM_TicketNo_union));
        }

        public static IQueryable<translog> getListRicaricheAbbonamenti(DbParkContext p_dbContext)
        {
            return GetList(p_dbContext).Where(r => !string.IsNullOrEmpty(r.tlLicenseNo) && r.tlLicenseNo.Length == 12)
                                     .Where(r => r.tlPayType.HasValue && r.tlPayType.Value > 0)
                                     .Where(r => r.tlAmount.HasValue && r.tlAmount.Value > 0)
                                     .Where(r => r.RicaricheConfermate.Count() == 0);
        }



        //public static void confermaRicaricaAbbonamento(translog p_ricarica, string p_numeroMittente, DbParkContext p_dbContext)
        //{

        //    SMSOut v_SMSOut = new SMSOut();
        //    string v_codiceAbbonamento = p_ricarica.tlLicenseNo;

        //    Abbonamenti v_abbonamento = AbbonamentiBD.getAbbonamentoByCodice(v_codiceAbbonamento, p_dbContext);

        //    if (v_abbonamento != null)
        //    {
        //        Cellulari v_cellulare = CellulariBD.getCellulareByIdAbbonamentoAndMaster(v_abbonamento.idAbbonamento, p_dbContext);

        //        if (v_cellulare != null)
        //        {
        //            string v_numero = v_cellulare.numero.Replace("+", "");
        //            RicaricheConfermate v_ricaricaConfermata = new RicaricheConfermate();
        //            v_ricaricaConfermata.translog = p_ricarica;
        //            v_ricaricaConfermata.SMSOut = v_SMSOut;
        //            p_dbContext.RicaricheConfermate.Add(v_ricaricaConfermata);


        //            decimal v_saldo = AbbonamentiBD.getCreditoResiduo(v_abbonamento, p_dbContext);
        //            string v_testo = v_abbonamento.codice + Environment.NewLine + "Sono stati accreditati " + (p_ricarica.tlAmount.Value / 100).ToString("0.00") + " Euro sul suo abbonamento." + "\r\n" + "Credito residuo: " + (v_saldo / 100).ToString("0.00") + " Euro";

        //            v_SMSOut.numeroDestinatario = v_numero;
        //            v_SMSOut.numeroMittente = p_numeroMittente;
        //            v_SMSOut.dataElaborazione = DateTime.Now;
        //            v_SMSOut.testo = v_testo;
        //            v_SMSOut.idSMSIn = 1;
        //            p_dbContext.SMSOut.Add(v_SMSOut);
        //            //v_ricaricaConfermata.idSMSOut = v_SMSOut.idSMSOut;

        //        }
        //    }
        //}


        //public static translog SaveTicketFromWebService(translog p_ticket, DBInfos p_dbinfo)
        //{
        //    DbParkCtx v_context = p_dbinfo.GetParkCtx(false); //viene creato il contesto con pooling a false per creare una singola connessione

        //    //modifica del recordid ricevuto dall'esterno 
        //    // Int64 v_recordId = p_ticket.tlUserType.Value * 10000000000 + p_ticket.tlRecordID;
        //    //translog v_titolo = TranslogBD.GetById(v_recordId, v_context);
        //    translog v_titolo = TranslogBD.GetById(p_ticket.tlUserType.Value, v_context);
        //    if (v_titolo != null && v_titolo.tlUserType == TranslogBD.SERVIZIO_TESSERA)
        //    {
        //        //UPDATE
        //        v_titolo.tlPDM_Sender = p_ticket.tlPDM_Sender;
        //        v_titolo.tlUserType = p_ticket.tlUserType;
        //        v_titolo.tlAmount = p_ticket.tlAmount;
        //        v_titolo.tlExpDateTime = p_ticket.tlExpDateTime;
        //        v_titolo.tlPSAM = p_ticket.tlPSAM;
        //        v_titolo.tlSpecialId = p_ticket.tlSpecialId;
        //        v_titolo.tlParkingSpaceNo = p_ticket.tlParkingSpaceNo;
        //        v_titolo.tlServiceCarSender = p_ticket.tlServiceCarSender;
        //        v_context.SaveChanges();
        //    }
        //    else
        //    {
        //        //CREATE
        //        v_titolo = new translog();

        //        v_titolo.tlRecordID = p_ticket.tlRecordID; //56343240
        //        v_titolo.tlTracer = null;
        //        v_titolo.tlPDM = p_ticket.tlPDM;
        //        v_titolo.tlPDM_Sender = p_ticket.tlPDM;
        //        v_titolo.tlDatePC = p_ticket.tlPayDateTime;
        //        v_titolo.tlRecordType = 0;
        //        v_titolo.tlPayType = p_ticket.tlPayType;
        //        v_titolo.tlUserType = p_ticket.tlUserType;
        //        v_titolo.tlPayDateTime = p_ticket.tlPayDateTime;
        //        v_titolo.tlAmount = p_ticket.tlAmount;
        //        v_titolo.tlExpDateTime = p_ticket.tlExpDateTime;
        //        v_titolo.tlTicketNo = Convert.ToInt32(p_ticket.tlRecordID);
        //        v_titolo.tlID = null;
        //        v_titolo.tlCurrency = "EUR";
        //        v_titolo.tlOk = null;
        //        v_titolo.tlSityControlID = null;
        //        v_titolo.tlSityControlLine = null;
        //        v_titolo.tlLicenseNo = p_ticket.tlLicenseNo.Trim();
        //        v_titolo.tlPDM_Id = null;
        //        v_titolo.tlCreditCardFee = null;
        //        v_titolo.tlIsOnline = null;
        //        v_titolo.tlServiceCar = null;
        //        v_titolo.tlMoneyCar = null;
        //        v_titolo.tlServiceCarSender = p_ticket.tlServiceCarSender;
        //        v_titolo.tlMoneyCarSender = null;
        //        v_titolo.tlPSAM = p_ticket.tlPSAM;
        //        v_titolo.tlSpecialId = p_ticket.tlSpecialId;
        //        v_titolo.tlParkingSpaceNo = p_ticket.tlParkingSpaceNo;
        //        v_titolo.tlValidThru = null;
        //        v_context.translog.Add(v_titolo);
        //        v_context.SaveChanges();
        //    }


        //    return v_titolo;



        //}

    }
}
