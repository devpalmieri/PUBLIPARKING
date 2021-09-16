using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{

    public class riepilogoAgevolazione
    {
        public int idTabAgevolazione { get; set; }
        public decimal? aliquotaAgevolazione { get; set; }
        public DateTime? dataInizioAgevolazione { get; set; }
        public DateTime? dataFineAgevolazione { get; set; }
        public int? numMesiAgevolazione { get; set; }
        public string codAgevolazione { get; set; }
        public string flag_quota_fissa_variabile { get; set; }
    }

    public class TabAgevolazioniBD : EntityBD<tab_agevolazioni>
    {
        public TabAgevolazioniBD()
        {

        }

        /// <summary>
        /// Restituisce la classe "riepilogoAgevolazione" per le agevolazioni valide legate all' idOggetto nel periodo indicato
        /// </summary>
        /// <param name="p_idOggetto">Id Oggetto</param>
        /// <param name="p_idEntrata">Id Entrata</param>
        /// <param name="p_dataInizioVal">Data Inizio Validità</param>
        /// <param name="p_dataInizioVal">Data Fine Validità</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static IQueryable<riepilogoAgevolazione> GetAgevolazione(int p_idOggetto, int p_idEntrata, DateTime? p_dataInizioVal, DateTime? p_dataFineVal, dbEnte p_dbContext)
        {

            IQueryable<riepilogoAgevolazione> v_agevolazione = GetList(p_dbContext)
                                                            .Where(ag => ag.join_oggetto_agevolazioni.Any(o => o.id_oggetto == p_idOggetto))
                                                            .Where(ag => ag.id_entrata == p_idEntrata)
                                                            .Where(ag => ag.cod_stato.StartsWith(CodStato.ATT))
                                                            .Where(ag => ag.data_inizio_validita <= p_dataFineVal) 
                                                            .Where(ag => ag.data_fine_validita == null || ag.data_fine_validita >= p_dataInizioVal)
                                                            .OrderBy (ag => ag.data_inizio_validita)
                                                            .Select(ag => new riepilogoAgevolazione()
                                                                        {
                                                                            idTabAgevolazione = ag.id_tab_agevolazioni,
                                                                            aliquotaAgevolazione = ag.anagrafica_agevolazione.aliquota_base_calcolo,
                                                                            dataInizioAgevolazione = ag.data_inizio_validita,
                                                                            dataFineAgevolazione = ag.data_fine_validita,
                                                                            numMesiAgevolazione = null,
                                                                            codAgevolazione = ag.anagrafica_agevolazione.cod_agevolazione,
                                                                            flag_quota_fissa_variabile = ag.anagrafica_agevolazione.flag_quota_fissa_variabile 
                                                            } 
                                                                   );

            return v_agevolazione; 
        }

    }
}
