using Publiparking.Data.dto;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Publiparking.Data.BD
{
    public class TitoloBD : EntityBD<translog>
    {
        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("TitoloBD");
        public TitoloBD()
        {

        }


        /// <summary>
        /// Ricerca il codice del titolo nelle tabelle TRANSLOG, TRANSLOG_PYNG, TRANSLOG_PHONZIE, TITOLI_SMS. Non verifica abbonamenti_rinnovi
        /// </summary>
        /// <param name="p_codice"></param>
        /// <param name="p_context"></param>
        /// <returns></returns>
        public static TitoloDTO getByCodice(string p_codice, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("getByCodice ---> codice: {0}", p_codice), EnLogSeverity.Debug);

            TitoloDTO v_titolo = null;

            v_titolo = TranslogBD.GetList(p_context)
                                .Where(tll => tll.tlPDM_TicketNo_union == p_codice)
                                .Where(t => t.tlExpDateTime.HasValue)
                                .Where(t => t.tlAmount > 0)
                                .Select(t => new TitoloDTO
                                {
                                    id = t.tlRecordID,
                                    codice = t.tlPDM_TicketNo_union,
                                    idStallo = null,
                                    dataPagamento = t.tlPayDateTime,
                                    scadenza = t.tlExpDateTime.Value,
                                    importo = t.tlAmount.Value
                                }).FirstOrDefault();

            if (v_titolo == null)
            {
                v_titolo = TranslogPhonzieBD.GetList(p_context)
                                            .Where(t => (t.tlPDM.Value.ToString() + "." + t.tlTicketNo.Value.ToString()) == p_codice)
                                            .Where(t => t.tlExpDateTime.HasValue)
                                            .Select(t => new TitoloDTO
                                            {
                                                id = t.tlRecordID,
                                                codice = (t.tlPDM.Value.ToString() + "." + t.tlTicketNo.Value.ToString()),
                                                idStallo = null,
                                                dataPagamento = t.tlPayDateTime,
                                                scadenza = t.tlExpDateTime.Value,
                                                importo = t.tlAmount.Value
                                            }).FirstOrDefault();
                if (v_titolo == null)
                {
                    v_titolo = TranslogPyngBD.GetList(p_context)
                                             .Where(t => (t.tlPDM.Value.ToString() + "." + t.tlTicketNo.Value.ToString()) == p_codice)
                                             .Where(t => t.tlExpDateTime.HasValue)
                                            .Select(t => new TitoloDTO
                                            {
                                                id = t.tlRecordID,
                                                codice = (t.tlPDM.Value.ToString() + "." + t.tlTicketNo.Value.ToString()),
                                                idStallo = null,
                                                dataPagamento = t.tlPayDateTime,
                                                scadenza = t.tlExpDateTime.Value,
                                                importo = t.tlAmount.Value
                                            }).FirstOrDefault();
                    if (v_titolo == null)
                    {
                        v_titolo = TitoliSMSBD.GetList(p_context)
                                              .Where(t => t.codice.Equals(p_codice))
                                              .Where(t => t.importo > 0)
                                                .Select(t => new TitoloDTO
                                                {
                                                    id = t.idTitoloSMS,
                                                    codice = t.codice,
                                                    idStallo = t.idStallo,
                                                    dataPagamento = t.dataPagamento,
                                                    scadenza = t.scadenza,
                                                    importo = t.importo
                                                }).FirstOrDefault();
                    }
                }
            }

            return v_titolo;
        }

        public static bool existByCodice(string p_codice, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("existByCodice codice: {0}", p_codice), EnLogSeverity.Debug);
            bool risp = false;
            TitoloDTO v_titolo = getByCodice(p_codice, p_context);

            if (v_titolo != null)
            {
                risp = true;
            }

            return risp;
        }

        public static bool isExpired(string p_codice, int p_minutiTolleranza, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("isExpired ---> codice: {0}", p_codice), EnLogSeverity.Debug);
            bool risp = true;
            TitoloDTO vTitolo = TitoloBD.getByCodice(p_codice, p_context);
            if (vTitolo != null)
            {
                if (vTitolo.scadenza.AddMinutes(p_minutiTolleranza) > DateTime.Now)
                {
                    risp = false;
                }
            }
            return risp;
        }


        public static bool titoliSovrapposti(TitoloDTO p_titolo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("titoliSovrapposti"), EnLogSeverity.Debug);
            //Manca lo stallo per controllare la sovrapposizione
            if (!p_titolo.idStallo.HasValue)
            {
                //Non ha uno stallo associato
                return false;
            }

            Stalli v_stallo = StalliBD.GetById(p_titolo.idStallo.Value, p_context);
            bool v_titoloSovrapposto = false;

            //Ricerca di un titolo sovrapposto a quello indicato (per tempo di pagamento) sullo stesso stallo
            v_titoloSovrapposto = TranslogBD.GetList(p_context)
                                        .Where(tll => tll.tlLicenseNo == v_stallo.numero)
                                        .Where(tll => tll.tlRecordID != p_titolo.id)
                                        .Where(t => t.tlExpDateTime.HasValue)
                                        .Where(t => t.tlAmount > 0)
                                        .Where(t => t.tlExpDateTime >= p_titolo.dataPagamento)
                                        .Where(t => t.tlPayDateTime <= p_titolo.scadenza).Any();

            //prosegue la ricerca sulle altre tabelle dei titoli fino a trovarne uno
            if (!v_titoloSovrapposto)
            {
                v_titoloSovrapposto = TitoliSMSBD.GetList(p_context)
                                        .Where(tll => tll.idStallo == p_titolo.idStallo)
                                        .Where(tll => tll.idTitoloSMS != p_titolo.id)
                                        .Where(tll => tll.importo > 0)
                                        .Where(tll => tll.scadenza >= p_titolo.dataPagamento)
                                        .Where(tll => tll.dataPagamento <= p_titolo.scadenza).Any();

                if (!v_titoloSovrapposto)
                {
                    //Non ha trovato titoli paganti per stallo e cerca sovrapposizioni per pagamenti con targa sullo stallo indicato
                    StalliTarghe v_stalliTarga = StalliTargheBD.getLastByIdStallo(p_titolo.idStallo.Value, p_context);

                    if (v_stalliTarga != null)
                    {
                        v_titoloSovrapposto = TranslogBD.GetList(p_context)
                                                    .Where(tll => tll.tlLicenseNo == v_stalliTarga.targa)
                                                    .Where(tll => tll.tlRecordID != p_titolo.id)
                                                    .Where(t => t.tlExpDateTime.HasValue)
                                                    .Where(t => t.tlAmount > 0)
                                                    .Where(t => t.tlExpDateTime >= p_titolo.dataPagamento)
                                                    .Where(t => t.tlPayDateTime <= p_titolo.scadenza).Any();

                        if (!v_titoloSovrapposto)
                        {
                            v_titoloSovrapposto = TranslogPhonzieBD.GetList(p_context)
                                                    .Where(t => t.tlLicenseNo == v_stalliTarga.targa)
                                                    .Where(tll => tll.tlRecordID != p_titolo.id)
                                                    .Where(tll => tll.tlExpDateTime.HasValue)
                                                    .Where(tll => tll.tlAmount > 0)
                                                    .Where(tll => tll.tlExpDateTime >= p_titolo.dataPagamento)
                                                    .Where(tll => tll.tlPayDateTime <= p_titolo.scadenza).Any();

                            if (!v_titoloSovrapposto)
                            {
                                v_titoloSovrapposto = TranslogPyngBD.GetList(p_context)
                                                    .Where(t => t.tlLicenseNo == v_stalliTarga.targa)
                                                    .Where(tll => tll.tlRecordID != p_titolo.id)
                                                    .Where(tll => tll.tlExpDateTime.HasValue)
                                                    .Where(tll => tll.tlAmount > 0)
                                                    .Where(tll => tll.tlExpDateTime >= p_titolo.dataPagamento)
                                                    .Where(tll => tll.tlPayDateTime <= p_titolo.scadenza).Any();

                                //Tolto perchè se parcheggia anche solo un'auto con rinnovo trimestrale, la sovrapposizione è positiva per tutto il trimestre
                                //if (!v_titoloSovrapposto)
                                //{
                                //    //Non ha trovato pagamenti per targa sovrapposti e cerca negli abbonamenti
                                //    IList<int> v_tariffeIdList = TariffeAbbonamentiBD.GetList(p_context)
                                //                                                                    .Where(t => t.Giri.StalliGiro.idStallo == p_titolo.idStallo)
                                //                                                                    .Select(t => t.idTariffaAbbonamento)
                                //                                                                    .ToList();

                                //    IQueryable<AbbonamentiPeriodici> v_abbonamentiLst = AbbonamentiPeriodiciBD.GetList(p_context)
                                //                                                                              .Where(a => a.targa.Contains(v_stalliTarga.targa))
                                //                                                                              .Where(a => v_tariffeIdList.Contains(a.idTariffaAbbonamento));

                                //    v_titoloSovrapposto = v_abbonamentiLst.Where(a => a.AbbonamentiRinnovi.Any(r => r.dataFine >= p_titolo.dataPagamento && r.dataInizio <= p_titolo.scadenza)).Any();
                                //}
                            }
                        }
                    }
                }
            }

            return v_titoloSovrapposto;
        }

        public static TitoloDTO getUltimoPagatoByIdStallo(Int32 p_idStallo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("getUltimoPagatoByIdStallo ---> idstallo : {0}", p_idStallo), EnLogSeverity.Debug);

            Stalli v_stallo = StalliBD.GetById(p_idStallo, p_context);
            TitoloDTO v_titolo = null;
            IList<TitoloDTO> v_titoliList = new List<TitoloDTO>();

            v_titolo = TranslogBD.GetList(p_context)
                                        .Where(tll => tll.tlLicenseNo == v_stallo.numero)
                                        .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                        .Where(t => t.tlAmount > 0)
                                        .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                        .Select(t => new TitoloDTO
                                        {
                                            id = t.tlRecordID,
                                            codice = t.tlPDM_TicketNo_union,
                                            idStallo = p_idStallo,
                                            dataPagamento = t.tlPayDateTime,
                                            scadenza = t.tlExpDateTime.Value,
                                            importo = t.tlAmount.Value
                                        }).FirstOrDefault();

            if (v_titolo != null)
            {
                v_titoliList.Add(v_titolo);
            }

            v_titolo = TitoliSMSBD.GetList(p_context)
                                    .Where(tll => tll.idStallo == p_idStallo)
                                    .Where(tll => tll.importo > 0)
                                    .OrderByDescending(o => o.dataPagamento).Take(1)
                                    .Select(t => new TitoloDTO
                                    {
                                        id = t.idTitoloSMS,
                                        codice = t.codice,
                                        idStallo = p_idStallo,
                                        dataPagamento = t.dataPagamento,
                                        scadenza = t.scadenza,
                                        importo = t.importo
                                    }).FirstOrDefault();

            if (v_titolo != null)
            {
                v_titoliList.Add(v_titolo);
            }

            StalliTarghe v_stalliTarga = StalliTargheBD.getLastByIdStallo(p_idStallo, p_context);

            if (v_stalliTarga != null)
            {
                v_titolo = TranslogBD.GetList(p_context)
                                            .Where(tll => tll.tlLicenseNo == v_stalliTarga.targa)
                                            .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                            .Where(t => t.tlAmount > 0)
                                            .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                            .Select(t => new TitoloDTO
                                            {
                                                id = t.tlRecordID,
                                                codice = t.tlPDM_TicketNo_union,
                                                idStallo = p_idStallo,
                                                dataPagamento = t.tlPayDateTime.Value,
                                                scadenza = t.tlExpDateTime.Value,
                                                importo = t.tlAmount.Value
                                            }).FirstOrDefault();

                if (v_titolo != null)
                {
                    v_titoliList.Add(v_titolo);
                }

                v_titolo = TranslogPhonzieBD.GetList(p_context)
                                            .Where(tll => tll.tlLicenseNo == v_stalliTarga.targa)
                                            .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                            .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                            .Select(t => new TitoloDTO
                                            {
                                                id = t.tlRecordID,
                                                codice = (t.tlPDM.Value.ToString() + "." + t.tlTicketNo.Value.ToString()),
                                                idStallo = p_idStallo,
                                                dataPagamento = t.tlPayDateTime.Value,
                                                scadenza = t.tlExpDateTime.Value,
                                                importo = t.tlAmount.Value
                                            }).FirstOrDefault();

                if (v_titolo != null)
                {
                    v_titoliList.Add(v_titolo);
                }

                v_titolo = TranslogPyngBD.GetList(p_context)
                                            .Where(tll => tll.tlLicenseNo == v_stalliTarga.targa)
                                            .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                            .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                            .Select(t => new TitoloDTO
                                            {
                                                id = t.tlRecordID,
                                                codice = (t.tlPDM.Value.ToString() + "." + t.tlTicketNo.Value.ToString()),
                                                idStallo = p_idStallo,
                                                dataPagamento = t.tlPayDateTime.Value,
                                                scadenza = t.tlExpDateTime.Value,
                                                importo = t.tlAmount.Value
                                            }).FirstOrDefault();

                if (v_titolo != null)
                {
                    v_titoliList.Add(v_titolo);
                }
                DateTime v_oggi = DateTime.Now;
                TimeSpan v_oraoggi = v_oggi.TimeOfDay;
                IList<int> v_tariffeIdList = TariffeAbbonamentiBD.GetList(p_context)
                                                                 .Where(t => t.Giri.StalliGiro.idStallo == p_idStallo)
                                                                 .Where(t => !t.ora_validita_inizio.HasValue || (t.ora_validita_inizio.Value <= v_oraoggi && t.ora_validita_fine.Value >= v_oraoggi))
                                                                 .Select(t => t.idTariffaAbbonamento)
                                                                 .ToList();

                IList<int> v_abbonamentiLst = AbbonamentiPeriodiciBD.GetList(p_context)
                                                                    .Where(a => a.targa.Contains(v_stalliTarga.targa))
                                                                    .Where(a => v_tariffeIdList.Contains(a.idTariffaAbbonamento))
                                                                    .Where(a => a.validoAl >= v_oggi)
                                                                    .Select(a => a.idAbbonamentoPeriodico).ToList();

                v_titolo = AbbonamentiRinnoviBD.GetList(p_context)
                                               .Where(r => v_abbonamentiLst.Contains(r.idAbbonamento))
                                               .OrderByDescending(o => o.dataFine).Take(1)
                                            .Select(t => new TitoloDTO
                                            {
                                                id = t.idAbbonamentoRinnovo,
                                                codice = t.codice,
                                                idStallo = p_idStallo,
                                                dataPagamento = v_stalliTarga.data,
                                                scadenza = t.dataFine,
                                                importo = t.importo
                                            }).FirstOrDefault();

                if (v_titolo != null)
                {
                    v_titoliList.Add(v_titolo);
                }
            }

            return v_titoliList.OrderByDescending(t => t.dataPagamento).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTarga"></param>
        /// <param name="p_idStallo">utilizzato solo per verificare la validità dell'abbonamento</param>
        /// <param name="p_context"></param>
        /// <returns></returns>
        public static TitoloDTO getUltimoPagatoByTarga(string pTarga, Int32 p_idStallo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("getUltimoPagatoByTarga ---> targa : {0}", pTarga), EnLogSeverity.Debug);


            Stalli v_stallo = StalliBD.GetById(p_idStallo, p_context);
            TitoloDTO v_titolo = null;
            IList<TitoloDTO> v_titoliList = new List<TitoloDTO>();
            pTarga = pTarga.Trim();

            v_titolo = TranslogBD.GetList(p_context)
                                        .Where(tll => tll.tlLicenseNo == pTarga)
                                        .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                        .Where(t => t.tlAmount > 0)
                                        .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                        .Select(t => new TitoloDTO
                                        {
                                            id = t.tlRecordID,
                                            codice = t.tlPDM_TicketNo_union,
                                            idStallo = 0,
                                            dataPagamento = t.tlPayDateTime,
                                            scadenza = t.tlExpDateTime.Value,
                                            importo = t.tlAmount.Value
                                        }).FirstOrDefault();

            if (v_titolo != null)
            {
                v_titoliList.Add(v_titolo);
            }

            v_titolo = TranslogPhonzieBD.GetList(p_context)
                                        .Where(tll => tll.tlLicenseNo == pTarga)
                                        .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                        .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                        .Select(t => new TitoloDTO
                                        {
                                            id = t.tlRecordID,
                                            codice = (t.tlPDM.Value.ToString() + "." + t.tlTicketNo.Value.ToString()),
                                            idStallo = 0,
                                            dataPagamento = t.tlPayDateTime.Value,
                                            scadenza = t.tlExpDateTime.Value,
                                            importo = t.tlAmount.Value
                                        }).FirstOrDefault();

            if (v_titolo != null)
            {
                v_titoliList.Add(v_titolo);
            }

            v_titolo = TranslogPyngBD.GetList(p_context)
                                        .Where(tll => tll.tlLicenseNo == pTarga)
                                        .Where(t => t.tlExpDateTime.HasValue && t.tlPayDateTime.HasValue)
                                        .OrderByDescending(o => o.tlPayDateTime.Value).Take(1)
                                        .Select(t => new TitoloDTO
                                        {
                                            id = t.tlRecordID,
                                            codice = (t.tlPDM.Value.ToString() + "." + t.tlTicketNo.Value.ToString()),
                                            idStallo = 0,
                                            dataPagamento = t.tlPayDateTime.Value,
                                            scadenza = t.tlExpDateTime.Value,
                                            importo = t.tlAmount.Value
                                        }).FirstOrDefault();

            if (v_titolo != null)
            {
                v_titoliList.Add(v_titolo);
            }

            DateTime v_oggi = DateTime.Now;
            TimeSpan v_oraoggi = v_oggi.TimeOfDay;
            IList<int> v_tariffeIdList = TariffeAbbonamentiBD.GetList(p_context)
                                                             .Where(t => t.Giri.StalliGiro.idStallo == p_idStallo)
                                                             .Where(t => !t.ora_validita_inizio.HasValue || (t.ora_validita_inizio.Value <= v_oraoggi && t.ora_validita_fine.Value >= v_oraoggi))
                                                             .Select(t => t.idTariffaAbbonamento)
                                                             .ToList();

            IList<int> v_abbonamentiLst = AbbonamentiPeriodiciBD.GetList(p_context)
                                                                .Where(a => a.targa.Contains(pTarga))
                                                                .Where(a => v_tariffeIdList.Contains(a.idTariffaAbbonamento))
                                                                .Where(a => a.validoAl >= v_oggi)
                                                                .Select(a => a.idAbbonamentoPeriodico).ToList();

            v_titolo = AbbonamentiRinnoviBD.GetList(p_context)
                                           .Where(r => v_abbonamentiLst.Contains(r.idAbbonamento))
                                           .OrderByDescending(o => o.dataFine).Take(1)
                                        .Select(t => new TitoloDTO
                                        {
                                            id = t.idAbbonamentoRinnovo,
                                            codice = t.codice,
                                            idStallo = p_idStallo,
                                            dataPagamento = t.dataPagamento,
                                            scadenza = t.dataFine,
                                            importo = t.importo
                                        }).FirstOrDefault();

            if (v_titolo != null)
            {
                v_titoliList.Add(v_titolo);
            }


            return v_titoliList.OrderByDescending(t => t.dataPagamento).FirstOrDefault();
        }

        public static TitoloDTO loadByCodice(string p_codice, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("loadByCodice ---> codice: {0}", p_codice), EnLogSeverity.Debug);

            return getByCodice(p_codice, p_context);
        }

        public static TitoloDTO loadByIdStallo(Int32 p_idstallo, DbParkCtx p_context)
        {
            m_logger.LogMessage(String.Format("loadByIdStallo ---> idstallo: {0}", p_idstallo.ToString()), EnLogSeverity.Debug);
            return getUltimoPagatoByIdStallo(p_idstallo, p_context);
        }

    }
}
