using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.BD
{
    public class TabSezioniContenutiBD : EntityBD<tab_sezioni_contenuti>
    {
        #region Costructor
        public TabSezioniContenutiBD() { }
        #endregion Costructor

        #region Public Methods
        public new static tab_sezioni_contenuti GetById(int p_id_sezione, dbEnte p_ctx)
        {
            return GetList(p_ctx).Where(d => d.Id_Sezione == p_id_sezione).FirstOrDefault();
        }
        public static tab_sezioni_contenuti GetSezioneById(int p_id_sezione, dbEnte p_ctx)
        {
            return GetList(p_ctx).Where(d => d.Id_Sezione == p_id_sezione).FirstOrDefault();
        }

        public static List<tab_sezioni_contenuti> GetSezioniByEnte(dbEnte p_ctx, string tag, int? p_id_ente = null)
        {
            List<tab_sezioni_contenuti> results = GetList(p_ctx).Where(d => d.Id_Ente == p_id_ente && d.Tag == tag ).ToList();
            if (results == null)
                results = GetList(p_ctx).Where(d => d.Id_Ente == null && d.Tag == tag).ToList();
            return results;
        }
        public static tab_sezioni_contenuti GetSezioneByEnteAndStruttura(dbEnte p_ctx, string tag, int? p_id_ente = null, int? p_id_struttura = null)
        {
            tab_sezioni_contenuti results = GetList(p_ctx).Where(d => d.Id_Ente == p_id_ente
            && d.id_struttura == p_id_struttura && d.Tag == tag).FirstOrDefault();
            if (results == null)
                results = GetList(p_ctx).Where(d => d.Id_Ente == null && d.id_struttura == null && d.Tag == tag).FirstOrDefault();
            return results;
        }
        public static List<tab_sezioni_contenuti> GetSezioniByEnteAndStruttura(dbEnte p_ctx, string tag, int? p_id_ente = null, int? p_id_struttura = null)
        {
            List<tab_sezioni_contenuti> results = GetList(p_ctx).Where(d => d.Id_Ente == p_id_ente
            && d.id_struttura == p_id_struttura && d.Tag == tag).ToList();
            if (results.Count() <= 0)
                results = GetList(p_ctx).Where(d => d.Id_Ente == p_id_ente && d.Tag == tag).ToList();
            if (results.Count() <= 0)
                results = GetList(p_ctx).Where(d =>  d.Tag == tag && d.Id_Ente == null
            && d.id_struttura == null).ToList();

            return results;
        }
        public static List<tab_sezioni_contenuti> GetSezioniOnLineByEnteAndStruttura(dbEnte p_ctx, string tag, int? p_id_ente = null, int? p_id_struttura = null)
        {
            List<tab_sezioni_contenuti> results = GetList(p_ctx).Where(d => d.Id_Ente == p_id_ente
            && d.id_struttura == p_id_struttura && d.Tag == tag).ToList();
            if (results.Count() <= 0)
                results = GetList(p_ctx).Where(d => d.Id_Ente == p_id_ente && d.Tag == tag).ToList();
            if (results.Count() <= 0)
                results = GetList(p_ctx).Where(d => d.Tag == tag && d.Id_Ente == null
            && d.id_struttura == null).ToList();

            return results;
        }
        public static List<tab_sezioni_contenuti> GetSezioniByTag(dbEnte p_ctx, string tag)
        {
            List<tab_sezioni_contenuti> results = GetList(p_ctx).Where(d => d.Tag == tag).ToList();
            return results;
        }
        #endregion Public Methods
    }
}
