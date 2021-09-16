using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabDenunceContrattiBD : EntityBD<tab_denunce_contratti>
    {
        public TabDenunceContrattiBD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_denunce_contratti> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_denunce_contratti GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_denunce_contratti == p_id);
        }

        public static tab_denunce_contratti creaDenunciaContratto(decimal p_idContribuente, Int32 p_idTipoDocEntrata, string p_codiceBollettino, string p_idCodDoc, DateTime p_dataPresentazione, Int32 p_idStrutturaStato, Int32? p_CausaleTrasgressoreAssente, Int32 p_idOperatore, string p_annotazioni, bool p_trasgressoreAssente, Int32 p_idStato, string p_codStato, Int32 p_idEnte, Int32 p_idEntrata, dbEnte p_context)
        {

            tab_denunce_contratti v_DenunciaContratto = new tab_denunce_contratti();

            v_DenunciaContratto.id_ente = p_idEnte;
            v_DenunciaContratto.id_ente_gestito = p_idEnte;
            v_DenunciaContratto.id_entrata = p_idEntrata;
            v_DenunciaContratto.id_contribuente = p_idContribuente;
            v_DenunciaContratto.id_tipo_doc_entrate = p_idTipoDocEntrata;
            v_DenunciaContratto.anno = p_dataPresentazione.Year;
            v_DenunciaContratto.prog_tipo_doc_entrata = Int32.Parse(p_codiceBollettino);
            v_DenunciaContratto.cod_doc = p_idCodDoc;
            v_DenunciaContratto.data_presentazione = p_dataPresentazione;
            v_DenunciaContratto.id_risorsa_acquisizione = p_idOperatore;

            if (p_annotazioni != null && p_annotazioni.Length > 0)
                v_DenunciaContratto.annotazioni = p_annotazioni;

            if (p_trasgressoreAssente)
                v_DenunciaContratto.id_causale = p_CausaleTrasgressoreAssente;

            v_DenunciaContratto.id_stato = p_idStato;
            v_DenunciaContratto.cod_stato = p_codStato;
            v_DenunciaContratto.data_stato = p_dataPresentazione;
            v_DenunciaContratto.id_struttura_stato = p_idStrutturaStato;
            v_DenunciaContratto.id_risorsa_stato = p_idOperatore;

            return v_DenunciaContratto;
        }
    }
}
