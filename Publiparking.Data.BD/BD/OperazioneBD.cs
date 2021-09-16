using Publiparking.Data.dto;
using Publiparking.Data.LinqExtended;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Publiparking.Data.BD
{
    public class OperazioneBD : EntityBD<translog>
    {
        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("OperazioneBD");

        public OperazioneBD()
        {

        }
        
        public static OperazioneDTO getUltimaOperazioneByIdStallo(Int32 pIdStallo, DbParkCtx p_context, DateTime? p_dataTaglio = null, bool p_withCodice = false)
        {
            //in operazioni local troveremo sempre almeno una operazione
            m_logger.LogMessage(String.Format("getUltimaOperazioneByIdStallo"), EnLogSeverity.Debug);
            OperazioneDTO v_operazioneLocal = OperazioniLocalBD.GetList(p_context)
                                                               .Where(o => o.idStallo.Equals(pIdStallo))
                                                               .Where(o => !p_dataTaglio.HasValue || o.data < p_dataTaglio.Value)
                                                               .Where(o => !p_withCodice || (o.codiceTitolo != null && o.codiceTitolo.Length > 0))
                                                               .OrderByDescending(o => o.data).FirstOrDefault().ToOperazioneDTO();

            OperazioneDTO v_operazioneTotem = getUltimaOperazioneTotemByIdStallo(pIdStallo, p_context, p_dataTaglio);



            DateTime v_operazioniLocalData = v_operazioneLocal != null ? v_operazioneLocal.data : DateTime.Now.AddYears(-1);
            DateTime v_operazioniTotelData = v_operazioneTotem != null ? v_operazioneTotem.data : DateTime.Now.AddYears(-1);

            //if (v_operazioneTotem.data > v_operazioneTotem.scadenza && v_operazioneLocal.data > v_operazioneTotem.scadenza)
            ////nel caso di un abbonamento scambio i campi data/scadenza per gestire correttamente l'ordinamento per data
            //{
            //    return v_operazioneLocal;
            //}

            return (v_operazioniLocalData > v_operazioniTotelData) ? v_operazioneLocal : v_operazioneTotem;
        }


        private static OperazioneDTO getUltimaOperazioneTotemByIdStallo(Int32 pIdStallo, DbParkCtx p_context, DateTime? p_dataTaglio = null)
        {
            m_logger.LogMessage(String.Format("getUltimaOperazioneTotemByIdStallo"), EnLogSeverity.Debug);
            Stalli v_Stallo = StalliBD.GetById(pIdStallo, p_context);
            OperazioneDTO v_operazione = null;
            IList<OperazioneDTO> v_operazioneList = new List<OperazioneDTO>();

            //prima query sui pagamenti avvenuti per stallo
            v_operazione = TranslogBD.GetList(p_context)
                                     .Where(tll => tll.tlLicenseNo == v_Stallo.numero)
                                     .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                     .Where(t => t.tlAmount > 0)
                                     .Where(tll => tll.tlRecordType.HasValue && tll.tlRecordType.Value == 0)
                                     .Where(tll => tll.tlPayType.HasValue && tll.tlPayType.Value > 0)
                                     .Where(tll => !p_dataTaglio.HasValue || tll.tlPayDateTime < p_dataTaglio.Value)
                                     .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                     .Select(ope => new OperazioneDTO
                                     {
                                         id = ope.tlRecordID,
                                         idStallo = v_Stallo.idStallo,
                                         data = ope.tlPayDateTime.Value,
                                         codiceTitolo = ope.tlPDM_TicketNo_union,
                                         targa = null,
                                         scadenza = ope.tlExpDateTime.Value,
                                         stato = OperazioneDTO.statoRegolareConNumero
                                     }).FirstOrDefault();

            if (v_operazione != null)
            {
                v_operazioneList.Add(v_operazione);
            }

            //Cerca un pagamento per SMS
            v_operazione = TitoliSMSBD.GetList(p_context)
                                    .Where(tll => tll.idStallo == pIdStallo)
                                    .Where(tll => tll.importo > 0)
                                    .Where(tll => !p_dataTaglio.HasValue || tll.dataPagamento < p_dataTaglio.Value)
                                    .OrderByDescending(o => o.dataPagamento).Take(1)
                                    .Select(ope => new OperazioneDTO
                                     {
                                         id = ope.idTitoloSMS,
                                         idStallo = v_Stallo.idStallo,
                                         data = ope.dataPagamento,
                                         codiceTitolo = ope.codice,
                                         targa = null,
                                         scadenza = ope.scadenza,
                                         stato = OperazioneDTO.statoRegolareConNumero
                                    }).FirstOrDefault();

            if (v_operazione != null)
            {
                v_operazioneList.Add(v_operazione);
            }

            StalliTarghe v_stalliTarga = StalliTargheBD.getLastByIdStallo(pIdStallo, p_context);

            DateTime v_oggi = DateTime.Now.Date;
            TimeSpan v_oraoggi = v_oggi.TimeOfDay;

            if (v_stalliTarga != null)
            {
                v_operazione = TranslogBD.GetList(p_context)
                                            .Where(tll => tll.tlLicenseNo == v_stalliTarga.targa)
                                            .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                            .Where(t => t.tlAmount > 0)
                                            .Where(t => t.tlExpDateTime >= v_oggi)
                                            .Where(tll => !p_dataTaglio.HasValue || tll.tlPayDateTime < p_dataTaglio.Value)
                                            .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                            .Select(ope => new OperazioneDTO
                                            {
                                                id = ope.tlRecordID,
                                                idStallo = v_Stallo.idStallo,
                                                data = v_stalliTarga.data,//ope.tlPayDateTime.Value,
                                                codiceTitolo = ope.tlPDM_TicketNo_union,
                                                targa = v_stalliTarga.targa,
                                                scadenza = ope.tlExpDateTime.Value,
                                                stato = OperazioneDTO.statoRegolareConNumero
                                            }).FirstOrDefault();

                if (v_operazione != null)
                {
                    v_operazioneList.Add(v_operazione);
                }

                //Cerca in Phonzie per targa
                v_operazione = TranslogPhonzieBD.GetList(p_context)
                                            .Where(tll => tll.tlLicenseNo == v_stalliTarga.targa)
                                            .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                            .Where(tll => !p_dataTaglio.HasValue || tll.tlPayDateTime < p_dataTaglio.Value)
                                            .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                            .Select(ope => new OperazioneDTO
                                            {
                                                id = ope.tlRecordID,
                                                idStallo = v_Stallo.idStallo,
                                                data = ope.tlPayDateTime.Value,
                                                codiceTitolo = (ope.tlPDM.Value.ToString() + "." + ope.tlTicketNo.Value.ToString()),
                                                targa = v_stalliTarga.targa,
                                                scadenza = ope.tlExpDateTime.Value,
                                                stato = OperazioneDTO.statoRegolareConNumero
                                            }).FirstOrDefault();

                if (v_operazione != null)
                {
                    v_operazioneList.Add(v_operazione);
                }

                //Cerca in Pyng per targa
                v_operazione = TranslogPyngBD.GetList(p_context)
                                            .Where(tll => tll.tlLicenseNo == v_stalliTarga.targa)
                                            .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                            .Where(tll => !p_dataTaglio.HasValue || tll.tlPayDateTime < p_dataTaglio.Value)
                                            .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                            .Select(ope => new OperazioneDTO
                                            {
                                                id = ope.tlRecordID,
                                                idStallo = v_Stallo.idStallo,
                                                data = v_stalliTarga.data,//ope.tlPayDateTime.Value,
                                                codiceTitolo = (ope.tlPDM.Value.ToString() + "." + ope.tlTicketNo.Value.ToString()),
                                                targa = v_stalliTarga.targa,
                                                scadenza = ope.tlExpDateTime.Value,
                                                stato = OperazioneDTO.statoRegolareConNumero
                                            }).FirstOrDefault();

                if (v_operazione != null)
                {
                    v_operazioneList.Add(v_operazione);
                }

                //Cerca negli abbonamenti
                IList<int> v_tariffeIdList = TariffeAbbonamentiBD.GetList(p_context)
                                                                .Where(t => t.Giri.StalliGiro.idStallo == pIdStallo)
                                                                .Where(t=> !t.ora_validita_inizio.HasValue || (t.ora_validita_inizio.Value <= v_oraoggi && t.ora_validita_fine.Value >= v_oraoggi))                                            
                                                                .Select(t => t.idTariffaAbbonamento)
                                                                .ToList();
                
                IList<int> v_abbonamentiLst = AbbonamentiPeriodiciBD.GetList(p_context)
                                                                    .Where(a => a.targa.Contains(v_stalliTarga.targa))
                                                                    .Where(a => v_tariffeIdList.Contains(a.idTariffaAbbonamento))
                                                                    .Where(a=> a.validoAl >= v_oggi)
                                                                    .Select(a => a.idAbbonamentoPeriodico).ToList();

                v_operazione = AbbonamentiRinnoviBD.GetList(p_context)
                                            .Where(r => v_abbonamentiLst.Contains(r.idAbbonamento))
                                            .Where(r => r.dataFine > v_oggi)
                                            .OrderByDescending(o => o.dataFine).Take(1)
                                            .Select(t => new OperazioneDTO
                                            {
                                                id = t.idAbbonamentoRinnovo,
                                                idStallo = v_stalliTarga.idStallo,
                                                data = v_stalliTarga.data,
                                                codiceTitolo = null,
                                                targa = v_stalliTarga.targa,
                                                scadenza = t.dataFine,
                                                stato = OperazioneDTO.statoRegolareConNumero
                                            }).FirstOrDefault();

                if (v_operazione != null && v_operazione.data >= v_operazioneList.Max(o=> o.data))
                {
                    return v_operazione;
                    //v_operazioneList.Add(v_operazione);
                }               
            }           

            return v_operazioneList.OrderByDescending(t => t.data).OrderByDescending(t=>t.scadenza).FirstOrDefault();          
        }
      
        public static OperazioneDTO loadLast(Int32 pIdStallo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("loadLast"), EnLogSeverity.Debug);
            return getUltimaOperazioneByIdStallo(pIdStallo, p_context);
        }

        public static OperazioneDTO loadLastPreavviso(Int32 pIdStallo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("loadLastPreavviso"), EnLogSeverity.Debug);
            return OperazioniLocalBD.GetList(p_context)
                                    .Where(o => o.idStallo.Equals(pIdStallo))
                                    .Where(o => o.stato.Equals(OperazioneDTO.statoPreavviso))
                                    .OrderByDescending(o => o.data)
                                    .FirstOrDefault().ToOperazioneDTO();
        }

        public static OperazioneDTO loadLastStatoBeforePreavviso(Int32 pIdStallo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("loadLastStatoBeforePreavviso"), EnLogSeverity.Debug);
            DateTime dataAppoggio = OperazioniLocalBD.GetList(p_context)
                                                     .Where(o => o.idStallo.Equals(pIdStallo)).Where(o => o.stato.Equals(OperazioneDTO.statoPreavviso))
                                                     .OrderByDescending(o => o.data).FirstOrDefault().data;

            return getUltimaOperazioneByIdStallo(pIdStallo, p_context, dataAppoggio);
        }

        public static OperazioneDTO loadLastBeforeOperazione(OperazioneDTO p_operazione, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("loadLastBeforeOperazione"), EnLogSeverity.Debug);
            return getUltimaOperazioneByIdStallo(p_operazione.idStallo, p_context, p_operazione.data);
        }

        public static OperazioneDTO loadLastWithTitolo(Int32 p_idStallo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("loadLastWithTitolo"), EnLogSeverity.Debug);
            return getUltimaOperazioneByIdStallo(p_idStallo, p_context, null, true);
        }

    }


}
