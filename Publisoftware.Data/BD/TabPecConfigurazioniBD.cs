using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;

namespace Publisoftware.Data.BD
{
    public class TabPecConfigurazioniBD : EntityBD<tab_pec_configurazioni>   
    {
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT_CES = "ATT-CES";

        public TabPecConfigurazioniBD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_pec_configurazioni> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext,p_includeEntities);
        }

        public static tab_pec_configurazioni GetByIdEnteIdEntrataIdServizio(dbEnte p_dbContext, string p_tipoInvio, int p_idEnte, int p_idEntrata = -1, int p_idServizio = -1)
        {
            tab_pec_configurazioni v_risp = null;

            v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == p_idEnte && c.id_entrata == p_idEntrata && c.id_tipo_servizio == p_idServizio).SingleOrDefault();

            if (v_risp == null)
            {
                v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == p_idEnte && c.id_entrata == p_idEntrata && c.id_tipo_servizio == null).SingleOrDefault();
            }

            if (v_risp == null)
            {
                v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == p_idEnte && c.id_entrata == null && c.id_tipo_servizio == null).SingleOrDefault();
            }

            if (v_risp == null)
            {
                v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == null && c.id_entrata == p_idEntrata && c.id_tipo_servizio == null).SingleOrDefault();
            }

            if (v_risp == null)
            {
                v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == null && c.id_entrata == null && c.id_tipo_servizio == null).SingleOrDefault();
            }

            return v_risp;
        }
        public static tab_pec_configurazioni GetByIdEnteIdEntrataIdTipoDocEntrate(dbEnte p_dbContext, string p_tipoInvio, int p_idEnte, string p_flagIO ="E", int p_idtipoDoc = -1)
        {
            tab_pec_configurazioni v_risp = null;

            v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == p_idEnte && c.flag_input_output == p_flagIO && c.id_tipo_doc_entrate == p_idtipoDoc).SingleOrDefault();

            if (v_risp == null)
            {
                v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == p_idEnte && c.flag_input_output == p_flagIO && c.id_tipo_doc_entrate == null).SingleOrDefault();
            }

            if (v_risp == null)
            {
                v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == p_idEnte && c.flag_input_output == p_flagIO && c.id_tipo_doc_entrate == null).SingleOrDefault();
            }

            if (v_risp == null)
            {
                v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == null && c.flag_input_output == p_flagIO && c.id_tipo_doc_entrate == null).SingleOrDefault();
            }

            return v_risp;
        }
        public static tab_pec_configurazioni GetFirstByIdEnteIndirizzoAttivo(dbEnte p_dbContext, string p_tipoInvio, int p_idEnte, string p_indirizzoPec, bool p_ancheSeCessata = false)
        {
            tab_pec_configurazioni v_risp = null;

            v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == p_idEnte && c.indirizzo_email == p_indirizzoPec).FirstOrDefault();

            if (v_risp == null)
            {
                v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_ATT && c.tipo == p_tipoInvio && c.id_ente == null && c.indirizzo_email == p_indirizzoPec).FirstOrDefault();
            }

            if (v_risp == null && p_ancheSeCessata)
            {
                v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_CES && c.tipo == p_tipoInvio && c.id_ente == p_idEnte && c.indirizzo_email == p_indirizzoPec).FirstOrDefault();
            }

            if (v_risp == null && p_ancheSeCessata)
            {
                v_risp = GetList(p_dbContext).Where(c => c.cod_stato == ATT_CES && c.tipo == p_tipoInvio && c.id_ente == null && c.indirizzo_email == p_indirizzoPec).FirstOrDefault();
            }

            return v_risp;
        }
    }
}
