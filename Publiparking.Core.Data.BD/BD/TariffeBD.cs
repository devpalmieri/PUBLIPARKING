using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Core.Utilities;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class TariffeBD : EntityBD<Tariffe>
    {
        public TariffeBD()
        {

        }

        public static Int32 getMinutiDaPagare(Tariffe p_tariffa, Int32 p_minuti, DateTime p_dataOraPagamento)
        {
            Int32 risp = -1;
            TimeSpan v_inizioPrimaFascia;
            TimeSpan v_finePrimaFascia;
            TimeSpan? v_inizioSecondaFascia;
            TimeSpan? v_fineSecondaFascia;
            Int32 v_minutiDaPagare = p_minuti;
            Int32 v_minutiMinimo = 0;

            // Verifica se il giorno è festivo
            if (DateTimeHelpers.isHoliday(p_dataOraPagamento.Date))
            {
                // Fasce del giorno festivo
                v_inizioPrimaFascia = p_tariffa.festivoOraInizioPrimaFascia;
                v_finePrimaFascia = p_tariffa.festivoOraFinePrimaFascia;
                v_inizioSecondaFascia = p_tariffa.festivoOraInizioSecondaFascia;
                v_fineSecondaFascia = p_tariffa.festivoOraFineSecondaFascia;
                v_minutiMinimo = p_tariffa.festivoMinutiMinimo;
            }
            else
            {
                // Fasce del giorno feriale
                v_inizioPrimaFascia = p_tariffa.ferialeOraInizioPrimaFascia;
                v_finePrimaFascia = p_tariffa.ferialeOraFinePrimaFascia;
                v_inizioSecondaFascia = p_tariffa.ferialeOraInizioSecondaFascia;
                v_fineSecondaFascia = p_tariffa.ferialeOraFineSecondaFascia;
                v_minutiMinimo = p_tariffa.ferialeMinutiMinimo;
            }

            if (v_inizioSecondaFascia != null)
            {
                // La tariffa ha 2 fasce
                if (p_dataOraPagamento.TimeOfDay < v_finePrimaFascia)
                {
                    // Il rinnovo inizia in prima fascia
                    Int32 v_minutiRimanentiFascia = (Int32)v_finePrimaFascia.Subtract(p_dataOraPagamento.TimeOfDay).TotalMinutes;
                    if (v_minutiRimanentiFascia > v_minutiDaPagare)
                        // Il rinnovo rientra per intero nella fascia
                        v_minutiDaPagare = p_minuti;
                    else
                    {
                        // Il rinnovo super la prima fascia di pagamento
                        TimeSpan v_oraFineSecondaFascia = v_fineSecondaFascia.Value;
                        TimeSpan v_oraInizioSecondaFascia = v_inizioSecondaFascia.Value;
                        Int32 v_MinutiSecondaFascia = (Int32)v_oraFineSecondaFascia.Subtract(v_oraInizioSecondaFascia).TotalMinutes;

                        if ((p_minuti - v_minutiRimanentiFascia) > v_MinutiSecondaFascia)
                            // I minuti di rinnovo eccedono la seconda fascia
                            v_minutiDaPagare = v_minutiRimanentiFascia + v_MinutiSecondaFascia;
                        else
                            // I minuti di rinnovo non eccedono la seconda fascia
                            v_minutiDaPagare = p_minuti;
                    }
                }
                else if (p_dataOraPagamento.TimeOfDay < v_fineSecondaFascia)
                {
                    // Il rinnovo inizia in seconda fascia
                    TimeSpan v_oraFineSecondaFascia = v_fineSecondaFascia.Value;
                    Int32 v_minutiRimanentiFascia = (Int32)v_oraFineSecondaFascia.Subtract(p_dataOraPagamento.TimeOfDay).TotalMinutes;
                    if (v_minutiRimanentiFascia > v_minutiDaPagare)
                        // Il rinnovo rientra per intero nella fascia
                        v_minutiDaPagare = p_minuti;
                    else
                        // Il rinnovo super la fascia di pagamento
                        v_minutiDaPagare = v_minutiRimanentiFascia;
                }
                else
                    // Il rinnovo si trova fuori dalla fascia di pagamento
                    v_minutiDaPagare = 0;
            }
            else
                // La tariffa ha una fascia
                if (p_dataOraPagamento.TimeOfDay < v_finePrimaFascia)
            {
                // Il rinnovo si trova all'interno della fascia di pagamento
                Int32 v_minutiRimanentiFascia = (Int32)v_finePrimaFascia.Subtract(p_dataOraPagamento.TimeOfDay).TotalMinutes;
                if (v_minutiRimanentiFascia > v_minutiDaPagare)
                    // Il rinnovo rientra per intero nella fascia
                    v_minutiDaPagare = p_minuti;
                else
                    // Il rinnovo super la fascia di pagamento
                    v_minutiDaPagare = v_minutiRimanentiFascia;
            }
            else
            {
                // Il rinnovo si trova fuori dalla fascia di pagamento
                v_minutiDaPagare = 0;
            }

            if (v_minutiDaPagare > 0 & v_minutiDaPagare < v_minutiMinimo)
                v_minutiDaPagare = v_minutiMinimo;

            risp = v_minutiDaPagare;
            return risp;
        }

        public static Int32 getMinutiDiSosta(Tariffe p_tariffa, Int32 p_minuti, DateTime p_dataOraPagamento)
        {
            Int32 risp = -1;
            TimeSpan v_inizioPrimaFascia;
            TimeSpan v_finePrimaFascia;
            TimeSpan? v_inizioSecondaFascia;
            TimeSpan? v_fineSecondaFascia;
            Int32 v_minutiDiSosta = p_minuti;

            // Verifica se il giorno è festivo
            if (DateTimeHelpers.isHoliday(p_dataOraPagamento.Date))
            {
                // Fasce del giorno festivo
                v_inizioPrimaFascia = p_tariffa.festivoOraInizioPrimaFascia;
                v_finePrimaFascia = p_tariffa.festivoOraFinePrimaFascia;
                v_inizioSecondaFascia = p_tariffa.festivoOraInizioSecondaFascia;
                v_fineSecondaFascia = p_tariffa.festivoOraFineSecondaFascia;
            }
            else
            {
                // Fasce del giorno feriale
                v_inizioPrimaFascia = p_tariffa.ferialeOraInizioPrimaFascia;
                v_finePrimaFascia = p_tariffa.ferialeOraFinePrimaFascia;
                v_inizioSecondaFascia = p_tariffa.ferialeOraInizioSecondaFascia;
                v_fineSecondaFascia = p_tariffa.ferialeOraFineSecondaFascia;
            }

            if (v_inizioSecondaFascia != null)
            {
                // La tariffa ha 2 fasce
                if (p_dataOraPagamento.TimeOfDay < v_finePrimaFascia)
                {
                    // Il rinnovo inizia in prima fascia
                    Int32 v_minutiRimanentiFascia = (Int32)v_finePrimaFascia.Subtract(p_dataOraPagamento.TimeOfDay).TotalMinutes;
                    if (v_minutiRimanentiFascia > v_minutiDiSosta)
                        // Il rinnovo rientra per intero nella fascia
                        v_minutiDiSosta = p_minuti;
                    else
                    {
                        // Il rinnovo super la prima fascia di pagamento
                        TimeSpan v_oraFineSecondaFascia = v_fineSecondaFascia.Value;
                        TimeSpan v_oraInizioSecondaFascia = v_inizioSecondaFascia.Value;
                        Int32 v_MinutiSecondaFascia = (Int32)v_oraFineSecondaFascia.Subtract(v_oraInizioSecondaFascia).TotalMinutes;

                        if ((p_minuti - v_minutiRimanentiFascia) > v_MinutiSecondaFascia)
                            // I minuti di rinnovo eccedono la seconda fascia
                            v_minutiDiSosta = v_minutiRimanentiFascia + v_MinutiSecondaFascia;
                        else
                            // I minuti di rinnovo non eccedono la seconda fascia
                            v_minutiDiSosta = p_minuti;

                        v_minutiDiSosta = v_minutiDiSosta + (Int32)v_oraInizioSecondaFascia.Subtract(v_finePrimaFascia).TotalMinutes;
                    }

                    if (p_dataOraPagamento.TimeOfDay < v_inizioPrimaFascia)
                        v_minutiDiSosta = v_minutiDiSosta + (Int32)v_inizioPrimaFascia.Subtract(p_dataOraPagamento.TimeOfDay).TotalMinutes;
                }
                else if (p_dataOraPagamento.TimeOfDay < v_fineSecondaFascia)
                {
                    // Il rinnovo inizia in seconda fascia
                    TimeSpan v_oraFineSecondaFascia = v_fineSecondaFascia.Value;
                    TimeSpan v_oraInizioSecondaFascia = v_inizioSecondaFascia.Value;
                    Int32 v_minutiRimanentiFascia = (Int32)v_oraFineSecondaFascia.Subtract(p_dataOraPagamento.TimeOfDay).TotalMinutes;
                    if (v_minutiRimanentiFascia > v_minutiDiSosta)
                        // Il rinnovo rientra per intero nella fascia
                        v_minutiDiSosta = p_minuti;
                    else
                        // Il rinnovo super la fascia di pagamento
                        v_minutiDiSosta = v_minutiRimanentiFascia;

                    if (p_dataOraPagamento.TimeOfDay < v_inizioSecondaFascia)
                        v_minutiDiSosta = v_minutiDiSosta + (Int32)v_oraInizioSecondaFascia.Subtract(p_dataOraPagamento.TimeOfDay).TotalMinutes;
                }
                else
                    // Il rinnovo si trova fuori dalla fascia di pagamento
                    v_minutiDiSosta = 0;
            }
            else
                // La tariffa ha una fascia
                if (p_dataOraPagamento.TimeOfDay < v_finePrimaFascia)
            {
                // Il rinnovo si trova all'interno della fascia di pagamento
                Int32 v_minutiRimanentiFascia = (Int32)v_finePrimaFascia.Subtract(p_dataOraPagamento.TimeOfDay).TotalMinutes;
                if (v_minutiRimanentiFascia > v_minutiDiSosta)
                    // Il rinnovo rientra per intero nella fascia
                    v_minutiDiSosta = p_minuti;
                else
                    // Il rinnovo super la fascia di pagamento
                    v_minutiDiSosta = v_minutiRimanentiFascia;

                if (p_dataOraPagamento.TimeOfDay < v_inizioPrimaFascia)
                    v_minutiDiSosta = v_minutiDiSosta + (Int32)v_inizioPrimaFascia.Subtract(p_dataOraPagamento.TimeOfDay).TotalMinutes;
            }
            else
            {
                // Il rinnovo si trova fuori dalla fascia di pagamento
                v_minutiDiSosta = 0;
            }
            risp = v_minutiDiSosta;
            return risp;
        }

        public static decimal calcolaImportoDaPagare(Tariffe p_tariffa, Int32 p_minuti, DateTime p_dataOraPagamento, bool p_saltaPrimaOra = false)
        {
            decimal risp = 0;
            decimal v_importo = 0;
            if (DateTimeHelpers.isHoliday(p_dataOraPagamento.Date))
            {
                if (p_tariffa.festivoFasciaInizialeMinuti <= p_minuti)
                {
                    if (!p_saltaPrimaOra)
                    {
                        // Importo per la prima ora
                        v_importo = (p_tariffa.festivoFasciaInizialeEuroOra / 60) * p_tariffa.festivoFasciaInizialeMinuti;

                        // Importo per le ore successive
                        v_importo = v_importo + ((p_minuti - p_tariffa.festivoFasciaInizialeMinuti) * (p_tariffa.festivoFasciaSuccessivaEuroOra / 60));
                    }
                    else
                    {
                        v_importo = v_importo + (p_minuti * p_tariffa.festivoFasciaSuccessivaEuroOra / 60);
                    }
                    // Controlla che l'importo non superi l'importo massimo
                    if (v_importo > p_tariffa.festivoImportoMassimo)
                    {
                        v_importo = p_tariffa.festivoImportoMassimo;
                    }
                }
                else
                {
                    v_importo = p_minuti * (p_tariffa.festivoFasciaInizialeEuroOra / 60);
                }
            }
            else
            {
                if (p_tariffa.ferialeFasciaInizialeMinuti <= p_minuti)
                {
                    if (!p_saltaPrimaOra)
                    {
                        // Importo per la prima ora
                        v_importo = (p_tariffa.ferialeFasciaInizialeEuroOra / 60) * p_tariffa.ferialeFasciaInizialeMinuti;

                        // Importo per le ore successive
                        v_importo = v_importo + ((p_minuti - p_tariffa.ferialeFasciaInizialeMinuti) * (p_tariffa.ferialeFasciaSuccessivaEuroOra / 60));
                    }
                    else
                    {
                        v_importo = v_importo + (p_minuti * (p_tariffa.ferialeFasciaSuccessivaEuroOra / 60));
                    }
                    // Controlla che l'importo non superi l'importo massimo
                    if (v_importo > p_tariffa.ferialeImportoMassimo)
                    {
                        v_importo = p_tariffa.ferialeImportoMassimo;
                    }
                }
                else
                {
                    v_importo = p_minuti * (p_tariffa.ferialeFasciaInizialeEuroOra / 60);
                }
            }

            //v_importo = Math.Round(v_importo / 5 * 5, 2); // Arrotonda sempre ai 5 centesimi per difetto
            v_importo = Math.Round((decimal)((long)v_importo / (long)5 * 5), 2); // Arrotonda sempre ai 5 centesimi per difetto
            risp = v_importo;

            return risp;
        }


    }
}
