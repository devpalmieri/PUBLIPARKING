using Publisoftware.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class TitoliSMSBD : EntityBD<TitoliSMS>
    {
        public TitoliSMSBD()
        {

        }

        public static TitoliSMS calcolaTitolo(Stalli p_stallo, Int32 p_minuti, DateTime p_dataOraPagamento, DbParkCtx p_context)
        {
            TitoliSMS risp = null;
            decimal v_importo = 0;
            Int32 v_minutiDaPagare = 0;
            Int32 v_minutiSosta = 0;
            DateTime v_scadenza;
            Tariffe v_tariffa = TariffeBD.GetById(p_stallo.idTariffa.Value, p_context);
            bool v_flat = false;

            // Calcola i minuti da pagare
            v_minutiDaPagare = TariffeBD.getMinutiDaPagare(v_tariffa, p_minuti, p_dataOraPagamento);

            // Calcola i minuti da pagare
            v_minutiSosta = TariffeBD.getMinutiDiSosta(v_tariffa, p_minuti, p_dataOraPagamento);


            if (TariffeFlatBD.ExistsTariffaFlat(v_tariffa, p_context))
            {
                v_importo = TariffeFlatBD.calcolaImportoFissoDaPagare(v_tariffa, p_minuti, p_dataOraPagamento, p_context);
                v_minutiDaPagare = TariffeFlatBD.calcolaMinutiResidui(v_tariffa, p_minuti, p_dataOraPagamento, p_context);
                if (v_importo > 0) v_flat = true;
            }

            v_importo = v_importo + TariffeBD.calcolaImportoDaPagare(v_tariffa, v_minutiDaPagare, p_dataOraPagamento,v_flat);

            // Calcola l'importo da pagare in base ai minuti dovuti
            //v_importo = TariffeBD.calcolaImportoDaPagare(v_tariffa, v_minutiDaPagare, p_dataOraPagamento);

            if (DateTimeHelpers.isHoliday(p_dataOraPagamento.Date))
            {
                if (v_importo == v_tariffa.festivoImportoMassimo)
                {
                    if (v_tariffa.festivoOraFineSecondaFascia.HasValue)
                        v_scadenza = (new DateTime(p_dataOraPagamento.Year, p_dataOraPagamento.Month, p_dataOraPagamento.Day, 0, 0, 0)).Add(v_tariffa.festivoOraFineSecondaFascia.Value);
                    else
                        v_scadenza = (new DateTime(p_dataOraPagamento.Year, p_dataOraPagamento.Month, p_dataOraPagamento.Day, 0, 0, 0)).Add(v_tariffa.festivoOraFinePrimaFascia);
                }
                else
                    v_scadenza = p_dataOraPagamento.AddMinutes(v_minutiSosta);
            }
            else if (v_importo == v_tariffa.ferialeImportoMassimo)
            {
                if (v_tariffa.ferialeOraFineSecondaFascia.HasValue)
                    v_scadenza = (new DateTime(p_dataOraPagamento.Year, p_dataOraPagamento.Month, p_dataOraPagamento.Day, 0, 0, 0)).Add(v_tariffa.ferialeOraFineSecondaFascia.Value);
                else
                    v_scadenza = (new DateTime(p_dataOraPagamento.Year, p_dataOraPagamento.Month, p_dataOraPagamento.Day, 0, 0, 0)).Add(v_tariffa.ferialeOraFinePrimaFascia);
            }
            else
                v_scadenza = p_dataOraPagamento.AddMinutes(v_minutiSosta);

            risp = new TitoliSMS();
            risp.dataPagamento = p_dataOraPagamento;
            risp.idStallo = p_stallo.idStallo;
            risp.scadenza = v_scadenza;
            risp.importo = v_importo;          
            return risp;
        }


        public static decimal getTotaleConsumi(int p_id_abbonamento, DbParkCtx p_context)
        {
            return GetList(p_context).Where(a => a.Cellulari.idAbbonamento.Equals(p_id_abbonamento)).Select(a => a.importo).DefaultIfEmpty(0).Sum();
        }
    }
}
