using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabAnagrafeBD : EntityBD<tab_anagrafe>
    {
        public TabAnagrafeBD()
        {

        }

        /// <summary>
        /// Record della tabella per CF e max(data_aggiornamento) 
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_anagrafe GetLastAnagrafeByCf(string p_codFiscale, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.codice_fiscale == p_codFiscale).OrderByDescending(d => d.data_aggiornamento_anagrafe).FirstOrDefault();
        }

        /// <summary>
        /// Lista degli eredi potenziali quando il contribuente è CF
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_anagrafe> GetListErediDiCF(int p_idEnteG, int p_idEnte, DateTime p_dtAggioAna, int? p_numFam, string p_cfDeceduto, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteG && c.data_aggiornamento_anagrafe == p_dtAggioAna && c.num_fam == p_numFam && c.codice_fiscale != p_cfDeceduto && c.stato_anagrafe == "RES" && (c.sigla_parentela == "MR" || c.sigla_parentela == "MG" || c.sigla_parentela == "FO" || c.sigla_parentela == "FA") && c.data_nascita.Value.AddYears(18) < DateTime.Now);
        }

        /// <summary>
        /// Lista degli eredi potenziali quando il contribuente è MR o MG
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_anagrafe> GetListErediDiMRoMG(int p_idEnteG, int p_idEnte, DateTime p_dtAggioAna, int? p_numFam, string p_cfDeceduto, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteG && c.data_aggiornamento_anagrafe == p_dtAggioAna && c.num_fam == p_numFam && c.codice_fiscale != p_cfDeceduto && c.stato_anagrafe == "RES" && (c.sigla_parentela == "CF" || c.sigla_parentela == "FO" || c.sigla_parentela == "FA") && c.data_nascita.Value.AddYears(18) < DateTime.Now);
        }

        /// <summary>
        /// Lista degli eredi potenziali quando il contribuente è MR o MG
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_anagrafe> GetListErediDiFAoFO(int p_idEnteG, int p_idEnte, DateTime p_dtAggioAna, int? p_numFam, string p_cfDeceduto, dbEnte p_dbContext)
        {
            try
            {
                return GetList(p_dbContext).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteG && c.data_aggiornamento_anagrafe == p_dtAggioAna && c.num_fam == p_numFam && c.codice_fiscale != p_cfDeceduto && c.stato_anagrafe == "RES" && (c.sigla_parentela == "NU" || c.sigla_parentela == "GE") && c.data_nascita.Value.AddYears(18) < DateTime.Now);
            }
            catch (Exception e) { return null; }
        }

        /// <summary>
        /// Record della tabella per CF e max(data_aggiornamento) 
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_anagrafe GetRowMaxDtAggiornamentoByIdEnte(int p_idEnteG, int p_idEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_ente == p_idEnte && d.id_ente_gestito == p_idEnteG).OrderByDescending(d => d.data_aggiornamento_anagrafe).FirstOrDefault();
        }
    }
}
