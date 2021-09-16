using Publiparking.Data.dto;
using Publisoftware.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class TitoliOperatoriBD : EntityBD<TitoliOperatori>
    {
        public TitoliOperatoriBD()
        {

        }

        public static TitoliOperatori getLastTitoloOperatoreByIdOperatore(Int32 p_IdOperatore, DbParkCtx p_context)
        {
            return GetList(p_context).Where(t => t.idOperatore.Equals(p_IdOperatore))
                                     .OrderByDescending(t => t.idTitoloOperatore).FirstOrDefault();
        }

        public static IQueryable<TitoliOperatori> getListByIdOperatore(Int32 p_IdOperatore,Int32 p_numGiorni,  DbParkCtx p_context)
        {
            DateTime v_datariferimento = DateTime.Now.AddDays(-p_numGiorni);            
            return GetList(p_context).Where(t => t.idOperatore.Equals(p_IdOperatore))
                                     .Where(t=> t.dataPagamento >= v_datariferimento);
        }

        public static TitoliOperatori calcolaByTariffa(Tariffe p_tariffa, Int32 p_minuti, DateTime p_dataOraPagamento, DbParkCtx p_context)
        {
            TitoliOperatori risp = null;
            decimal v_importo = 0;
            Int32 v_minutiDaPagare = 0;
            Int32 v_minutiSosta = 0;
            DateTime v_scadenza;
            bool v_flat = false;
            // Calcola i minuti da pagare
            v_minutiDaPagare = TariffeBD.getMinutiDaPagare(p_tariffa, p_minuti, p_dataOraPagamento);

            // Calcola i minuti da pagare
            v_minutiSosta = TariffeBD.getMinutiDiSosta(p_tariffa, p_minuti, p_dataOraPagamento);


            if (TariffeFlatBD.ExistsTariffaFlat(p_tariffa, p_context))
            {
                v_importo = TariffeFlatBD.calcolaImportoFissoDaPagare(p_tariffa, p_minuti, p_dataOraPagamento, p_context);
                v_minutiDaPagare = TariffeFlatBD.calcolaMinutiResidui(p_tariffa, p_minuti, p_dataOraPagamento, p_context);
                if (v_importo > 0) v_flat = true;
            }

            v_importo = v_importo + TariffeBD.calcolaImportoDaPagare(p_tariffa, v_minutiDaPagare, p_dataOraPagamento, v_flat);

            // Calcola l'importo da pagare in base ai minuti dovuti
           // v_importo = TariffeBD.calcolaImportoDaPagare(p_tariffa, v_minutiDaPagare, p_dataOraPagamento);


            if (DateTimeHelpers.isHoliday(p_dataOraPagamento.Date))
            {
                if (v_importo == p_tariffa.festivoImportoMassimo)
                {
                    if (p_tariffa.festivoOraFineSecondaFascia.HasValue)
                        v_scadenza = (new DateTime(p_dataOraPagamento.Year, p_dataOraPagamento.Month, p_dataOraPagamento.Day, 0, 0, 0)).Add(p_tariffa.festivoOraFineSecondaFascia.Value);
                    else
                        v_scadenza = (new DateTime(p_dataOraPagamento.Year, p_dataOraPagamento.Month, p_dataOraPagamento.Day, 0, 0, 0)).Add(p_tariffa.festivoOraFinePrimaFascia);
                }
                else
                    v_scadenza = p_dataOraPagamento.AddMinutes(v_minutiSosta);
            }
            else if (v_importo == p_tariffa.ferialeImportoMassimo)
            {
                if (p_tariffa.ferialeOraFineSecondaFascia.HasValue)
                    v_scadenza = (new DateTime(p_dataOraPagamento.Year, p_dataOraPagamento.Month, p_dataOraPagamento.Day, 0, 0, 0)).Add(p_tariffa.ferialeOraFineSecondaFascia.Value);
                else
                    v_scadenza = (new DateTime(p_dataOraPagamento.Year, p_dataOraPagamento.Month, p_dataOraPagamento.Day, 0, 0, 0)).Add(p_tariffa.ferialeOraFinePrimaFascia);
            }
            else
                v_scadenza = p_dataOraPagamento.AddMinutes(v_minutiSosta);

           
            risp = new TitoliOperatori();
            risp.dataPagamento = p_dataOraPagamento;
            risp.scadenza = v_scadenza;
            risp.importo = v_importo;

            return risp;
        }

        public static TitoliOperatori salvaTitoloPreventivo(TitoloOperatoreDTO p_titoloOperatore, string p_idParcometro, DbParkCtx p_context)
        {
            TitoliOperatori risp = null;

            if ((p_titoloOperatore.codice == null || p_titoloOperatore.codice.Length == 0) && 
                p_titoloOperatore.dataPagamento != null && p_titoloOperatore.id == 0 && 
                p_titoloOperatore.idOperatore > 0 && 
                (p_titoloOperatore.idStallo != null || p_titoloOperatore.targa != null || p_titoloOperatore.targa.Length > 0) && 
                p_titoloOperatore.importo > 0)
            {
                               
                risp = new TitoliOperatori();
                
                risp.dataPagamento = p_titoloOperatore.dataPagamento.Value;                
                risp.idOperatore = p_titoloOperatore.idOperatore;
                risp.idStallo = p_titoloOperatore.idStallo;
                risp.targa = p_titoloOperatore.targa;
                risp.importo = (decimal)p_titoloOperatore.importo;
                risp.scadenza = p_titoloOperatore.scadenza;
                p_context.TitoliOperatori.Add(risp);
                int res = p_context.SaveChanges();
                if (res == 0)
                {
                    risp = null;
                }
                else
                {
                    risp.codice = p_idParcometro + "." + risp.idOperatore;
                    p_context.SaveChanges();
                }
            }

            return risp;
        }
    }
}
