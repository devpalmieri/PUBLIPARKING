using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class AbbonamentiRinnoviBD : EntityBD<AbbonamentiRinnovi>
    {
        public AbbonamentiRinnoviBD()
        {

        }
        public static bool rinnova(int p_idAbbonamento, Int32 p_idRicarica, DbParkContext p_dbContext)
        {
            bool risp = false;
            translog v_ricarica = TranslogBD.GetById(p_dbContext, p_idRicarica);
            AbbonamentiPeriodici v_abbonamento = AbbonamentiPeriodiciBD.GetById(p_dbContext, p_idAbbonamento);
            if (v_ricarica != null & v_abbonamento != null)
            {
                AbbonamentiRinnovi v_rinnovo = new AbbonamentiRinnovi();
                decimal v_resto = v_ricarica.tlAmount.Value; //importo
                v_rinnovo.idAbbonamento = v_abbonamento.idAbbonamentoPeriodico;
                v_rinnovo.dataPagamento = v_ricarica.tlPayDateTime.Value; //datapagamento
                v_rinnovo.codice = v_ricarica.tlPDM_TicketNo_union; //codicetitolo
                v_rinnovo.importo = v_ricarica.tlAmount.Value; //importo
                AbbonamentiRinnovi v_ultimoRinnovo = AbbonamentiRinnoviBD.GetList(p_dbContext)
                                                                         .Where(a => a.idAbbonamento.Equals(p_idAbbonamento))
                                                                         .OrderByDescending(o => o.idAbbonamentoRinnovo).FirstOrDefault();
                if (v_ultimoRinnovo == null)
                    v_rinnovo.dataInizio = v_ricarica.tlPayDateTime.Value;
                else if (v_ultimoRinnovo.dataFine < v_ricarica.tlPayDateTime.Value)
                    v_rinnovo.dataInizio = v_ricarica.tlPayDateTime.Value;
                else
                    v_rinnovo.dataInizio = v_ultimoRinnovo.dataFine;

                TariffeAbbonamenti v_tariffa = TariffeAbbonamentiBD.GetById(p_dbContext, v_abbonamento.idTariffaAbbonamento);
                bool v_fasciaTrovata = false;

                IQueryable<FasceTariffeAbbonamenti> v_fasciePerAbbonamento = FasceTariffeAbbonamentiBD.GetList(p_dbContext).Where(f => f.idTariffaAbbonamento == v_tariffa.idTariffaAbbonamento);
                foreach (FasceTariffeAbbonamenti v_fascia in v_fasciePerAbbonamento)
                {
                    if (v_resto >= v_fascia.importo)
                    {
                        v_fasciaTrovata = true;
                        int v_numUnità = v_ricarica.tlAmount.Value / (int)v_fascia.importo;
                        v_resto = v_ricarica.tlAmount.Value - (v_fascia.importo * v_numUnità);
                        switch (v_fascia.unitaMisura)
                        {
                            case FasceTariffeAbbonamentiBD.GIORNI:
                                {
                                    v_rinnovo.dataFine = v_rinnovo.dataInizio.AddDays(v_numUnità * v_fascia.durata);

                                    // Gestisce il resto su base giornaliera
                                    int v_giorniResto = (int)decimal.Round((v_resto / v_fascia.importo) * v_fascia.durata, 0);
                                    v_rinnovo.dataFine = v_rinnovo.dataFine.AddDays(v_giorniResto);
                                    break;
                                }

                            case FasceTariffeAbbonamentiBD.SETTIMANE:
                                {
                                    v_rinnovo.dataFine = v_rinnovo.dataInizio.AddDays(7 * v_numUnità * v_fascia.durata);

                                    // Gestisce il resto su base settimanale
                                    Int32 v_giorniResto = (int)decimal.Round((v_resto / v_fascia.importo) * (v_fascia.durata * 7), 0);
                                    v_rinnovo.dataFine = v_rinnovo.dataFine.AddDays(v_giorniResto);
                                    break;
                                }

                            case FasceTariffeAbbonamentiBD.MESI:
                                {
                                    v_rinnovo.dataFine = v_rinnovo.dataInizio.AddMonths(v_numUnità * v_fascia.durata);

                                    // Gestisce il resto su base mensile
                                    decimal v_costoMensile = v_fascia.importo / v_fascia.durata;
                                    int v_mesiResto = (int)decimal.Round(v_resto / v_costoMensile, 0);
                                    v_rinnovo.dataFine = v_rinnovo.dataFine.AddMonths(v_mesiResto);

                                    // Gestisce il resto su base giornaliera
                                    v_resto = v_resto - (v_mesiResto * v_costoMensile);
                                    int v_giorniResto = (int)decimal.Round(v_resto / (v_costoMensile / 30), 0);
                                    v_rinnovo.dataFine = v_rinnovo.dataFine.AddDays(v_giorniResto);
                                    break;
                                }

                            case FasceTariffeAbbonamentiBD.ANNI:
                                {
                                    v_rinnovo.dataFine = v_rinnovo.dataInizio.AddYears(v_numUnità * v_fascia.durata);

                                    // Gestisce il resto su base mensile
                                    decimal v_costoMensile = v_fascia.importo / (v_fascia.durata * 12);
                                    int v_mesiResto = (int)decimal.Round(v_resto / v_costoMensile, 0);
                                    v_rinnovo.dataFine = v_rinnovo.dataFine.AddMonths(v_mesiResto);

                                    // Gestisce il resto su base giornaliera
                                    v_resto = v_resto - (v_mesiResto * v_costoMensile);
                                    int v_giorniResto = 0;

                                    if ((v_costoMensile / 30) >= 1)
                                        v_giorniResto = (int)decimal.Round(v_resto / (v_costoMensile / 30), 0);


                                    v_rinnovo.dataFine = v_rinnovo.dataFine.AddDays(v_giorniResto);
                                    break;
                                }
                            default:
                                {
                                    // Formato sconosciuto                                   
                                    break;
                                }
                        }
                    }
                }
                if (v_fasciaTrovata)
                {
                    p_dbContext.AbbonamentiRinnovi.Add(v_rinnovo);
                }


                risp = true;
            }

            return risp;
        }


        //public static bool rinnovaAbbonamentoFromWebService(Int64 p_idTicket, DBInfos p_dbinfo)
        //{
        //    bool risp = false;
        //    DbParkCtx v_parkcontext = p_dbinfo.GetParkCtx(false);
        //    translog v_ricarica = TranslogBD.GetById(p_idTicket, v_parkcontext);
        //    AbbonamentiPeriodici v_abbonamento = AbbonamentiPeriodiciBD.getAbbonamentoByCodice(v_ricarica.tlLicenseNo, v_parkcontext);

        //    if (v_ricarica != null & v_abbonamento != null)
        //    {
        //        AbbonamentiRinnovi v_rinnovo = new AbbonamentiRinnovi();
        //        decimal v_resto = v_ricarica.tlAmount.Value; //importo
        //        v_rinnovo.idAbbonamento = v_abbonamento.idAbbonamentoPeriodico;
        //        v_rinnovo.dataPagamento = v_ricarica.tlPayDateTime.Value; //datapagamento
        //        v_rinnovo.codice = v_ricarica.tlPDM_TicketNo_union; //codicetitolo
        //        v_rinnovo.importo = v_ricarica.tlAmount.Value; //importo
        //        AbbonamentiRinnovi v_ultimoRinnovo = AbbonamentiRinnoviBD.GetList(v_parkcontext)
        //                                                                 .Where(a => a.idAbbonamento.Equals(v_abbonamento.idAbbonamentoPeriodico))
        //                                                                 .OrderByDescending(o => o.idAbbonamentoRinnovo).FirstOrDefault();
        //        if (v_ultimoRinnovo == null)
        //            v_rinnovo.dataInizio = v_ricarica.tlPayDateTime.Value;
        //        else if (v_ultimoRinnovo.dataFine < v_ricarica.tlPayDateTime.Value)
        //            v_rinnovo.dataInizio = v_ricarica.tlPayDateTime.Value;
        //        else
        //            v_rinnovo.dataInizio = v_ultimoRinnovo.dataFine;

        //        TariffeAbbonamenti v_tariffa = TariffeAbbonamentiBD.GetById(v_abbonamento.idTariffaAbbonamento, v_parkcontext);
        //        bool v_fasciaTrovata = false;

        //        IQueryable<FasceTariffeAbbonamenti> v_fasciePerAbbonamento = FasceTariffeAbbonamentiBD.GetList(v_parkcontext).Where(f => f.idTariffaAbbonamento == v_tariffa.idTariffaAbbonamento)
        //                                                                        .OrderByDescending(f=> f.importo);

        //        foreach (FasceTariffeAbbonamenti v_fascia in v_fasciePerAbbonamento)
        //        {
        //            if (v_resto >= v_fascia.importo)
        //            {
        //                v_fasciaTrovata = true;
        //                int v_numUnità = v_ricarica.tlAmount.Value / (int)v_fascia.importo;
        //                v_resto = v_ricarica.tlAmount.Value - (v_fascia.importo * v_numUnità);
        //                switch (v_fascia.unitaMisura)
        //                {
        //                    case FasceTariffeAbbonamentiBD.GIORNI:
        //                        {
        //                            v_rinnovo.dataFine = v_rinnovo.dataInizio.AddDays(v_numUnità * v_fascia.durata);

        //                            // Gestisce il resto su base giornaliera
        //                            int v_giorniResto = (int)decimal.Round((v_resto / v_fascia.importo) * v_fascia.durata, 0);
        //                            v_rinnovo.dataFine = v_rinnovo.dataFine.AddDays(v_giorniResto);
        //                            break;
        //                        }

        //                    case FasceTariffeAbbonamentiBD.SETTIMANE:
        //                        {
        //                            v_rinnovo.dataFine = v_rinnovo.dataInizio.AddDays(7 * v_numUnità * v_fascia.durata);

        //                            // Gestisce il resto su base settimanale
        //                            Int32 v_giorniResto = (int)decimal.Round((v_resto / v_fascia.importo) * (v_fascia.durata * 7), 0);
        //                            v_rinnovo.dataFine = v_rinnovo.dataFine.AddDays(v_giorniResto);
        //                            break;
        //                        }

        //                    case FasceTariffeAbbonamentiBD.MESI:
        //                        {
        //                            v_rinnovo.dataFine = v_rinnovo.dataInizio.AddMonths(v_numUnità * v_fascia.durata);

        //                            // Gestisce il resto su base mensile
        //                            decimal v_costoMensile = v_fascia.importo / v_fascia.durata;
        //                            int v_mesiResto = (int)decimal.Round(v_resto / v_costoMensile, 0);
        //                            v_rinnovo.dataFine = v_rinnovo.dataFine.AddMonths(v_mesiResto);

        //                            // Gestisce il resto su base giornaliera
        //                            v_resto = v_resto - (v_mesiResto * v_costoMensile);
        //                            int v_giorniResto = (int)decimal.Round(v_resto / (v_costoMensile / 30), 0);
        //                            v_rinnovo.dataFine = v_rinnovo.dataFine.AddDays(v_giorniResto);
        //                            break;
        //                        }

        //                    case FasceTariffeAbbonamentiBD.ANNI:
        //                        {
        //                            v_rinnovo.dataFine = v_rinnovo.dataInizio.AddYears(v_numUnità * v_fascia.durata);

        //                            // Gestisce il resto su base mensile
        //                            decimal v_costoMensile = v_fascia.importo / (v_fascia.durata * 12);
        //                            int v_mesiResto = (int)decimal.Round(v_resto / v_costoMensile, 0);
        //                            v_rinnovo.dataFine = v_rinnovo.dataFine.AddMonths(v_mesiResto);

        //                            // Gestisce il resto su base giornaliera
        //                            v_resto = v_resto - (v_mesiResto * v_costoMensile);
        //                            int v_giorniResto = 0;

        //                            if ((v_costoMensile / 30) >= 1)
        //                                v_giorniResto = (int)decimal.Round(v_resto / (v_costoMensile / 30), 0);


        //                            v_rinnovo.dataFine = v_rinnovo.dataFine.AddDays(v_giorniResto);
        //                            break;
        //                        }
        //                    default:
        //                        {
        //                            // Formato sconosciuto                                   
        //                            break;
        //                        }
        //                }
        //            }
        //        }
        //        if (v_fasciaTrovata)
        //        {
        //            v_parkcontext.AbbonamentiRinnovi.Add(v_rinnovo);
        //            v_parkcontext.SaveChanges();
        //        }


        //        risp = true;
        //    }

        //    return risp;
        //}
    }
}
