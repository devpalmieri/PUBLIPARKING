using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinReferenteContribuenteBD : EntityBD<join_referente_contribuente>
    {
        public JoinReferenteContribuenteBD()
        {
        }

        public static new IQueryable<join_referente_contribuente> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext, p_includeEntities).Where(d =>
                p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_anag_contribuente));
        }

        public static new join_referente_contribuente GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_join_referente_contribuente == p_id);
        }

        public static IQueryable<join_referente_contribuente> QueryById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.id_join_referente_contribuente == p_id);
        }

        public static IQueryable<join_referente_contribuente> GetListByIdContribuenteIdReferenteIdRelazione(decimal p_idContribuente, int p_idReferente,int p_idRelazione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_anag_contribuente == p_idContribuente && 
                                                   d.id_tab_referente == p_idReferente &&
                                                   d.id_tipo_relazione == p_idRelazione)
                                       .OrderByDescending(d => d.data_fine_validita);
        }

        public static join_referente_contribuente GetLastRelazioneByIdContribuenteIdReferenteIdRelazione(decimal p_idContribuente, int p_idReferente,
            int p_idRelazione, dbEnte p_dbContext)
        {
            if (GetList(p_dbContext).Any(d =>
                d.id_anag_contribuente == p_idContribuente && d.id_tab_referente == p_idReferente && d.id_tipo_relazione == p_idRelazione))
            {
                return GetList(p_dbContext)
                    .Where(d => d.id_anag_contribuente == p_idContribuente && d.id_tab_referente == p_idReferente && d.id_tipo_relazione == p_idRelazione)
                    .OrderByDescending(d => d.data_fine_validita ?? DateTime.MaxValue).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static join_referente_contribuente GetLastOpenRelazioneByIdContribuenteIdReferenteIdRelazione(Decimal p_idContribuente, int p_idReferente,
            int p_idRelazione, dbEnte p_dbContext)
        {
            if (GetList(p_dbContext).Any(d =>
                d.id_anag_contribuente == p_idContribuente && d.id_tab_referente == p_idReferente && d.id_tipo_relazione == p_idRelazione &&
                d.cod_stato.Equals(join_referente_contribuente.ATT_ATT) && d.data_fine_validita == null))
            {
                return GetList(p_dbContext).Where(d =>
                    d.id_anag_contribuente == p_idContribuente && d.id_tab_referente == p_idReferente && d.id_tipo_relazione == p_idRelazione &&
                    d.cod_stato.Equals(join_referente_contribuente.ATT_ATT) && d.data_fine_validita == null).SingleOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static join_referente_contribuente GetLastRelazioneByIdContribuenteIdReferente(decimal p_idContribuente, int p_idReferente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_anag_contribuente == p_idContribuente && d.id_tab_referente == p_idReferente)
                .OrderBy(d => d.data_inizio_validita).FirstOrDefault();
        }

        public static int GetLivelloAutorizzativoByIdContribuente(decimal p_idContribuente, int p_idReferente, dbEnte p_dbContext)
        {
            int v_livelloAut = TabApplicazioniBD.MaxLivelloAutorizzativo;

            if (p_idReferente != -1)
            {
                if (GetList(p_dbContext).Any(d => d.id_anag_contribuente == p_idContribuente && d.id_tab_referente == p_idReferente))
                {
                    v_livelloAut = GetList(p_dbContext).Where(d => d.id_anag_contribuente == p_idContribuente && d.id_tab_referente == p_idReferente)
                                                       .OrderByDescending(d => d.data_inizio_validita)
                                                       .FirstOrDefault()
                                                       .livello_autorizzazione_interno;
                }
            }

            return v_livelloAut;
        }

        public static IQueryable<join_referente_contribuente> GetElencoReferentiByIdContribuente(Decimal p_idContribuente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_anag_contribuente == p_idContribuente);
        }

        public static IQueryable<join_referente_contribuente> GetElencoReferentiAttiviByIdContribuente(decimal idContribuente, dbEnte ctx)
        {
            return GetElencoReferentiByIdContribuente(idContribuente, ctx)
                .Where(x => x.data_fine_validita == null || x.data_fine_validita >= DateTime.Now);
        }

        public static IQueryable<join_referente_contribuente> GetElencoCoobligatiByIdContribuente(Decimal p_idContribuente, dbEnte p_dbContext,
            string p_codStato = "")
        {
            IQueryable<join_referente_contribuente> ret = GetList(p_dbContext).Where(d => d.id_anag_contribuente == p_idContribuente &&
                                                                                          d.flag_coobbligato == tab_referente.COOBBLIGATO &&
                                                                                          (d.data_inizio_validita == null ||
                                                                                           d.data_inizio_validita <= DateTime.Now) &&
                                                                                          (d.data_fine_validita == null ||
                                                                                           d.data_fine_validita >= DateTime.Now));

            if (!string.IsNullOrWhiteSpace(p_codStato))
            {
                ret = ret.Where(d => d.cod_stato.ToUpper() == p_codStato.ToUpper());
            }

            return ret;
        }

        public static IQueryable<join_referente_contribuente> GetElencoGarantiByIdContribuente(Decimal p_idContribuente, dbEnte p_dbContext,
            string p_codStato = "")
        {
            IQueryable<join_referente_contribuente> ret = GetList(p_dbContext)
                .Where(d => d.id_anag_contribuente == p_idContribuente && (d.flag_coobbligato == tab_referente.GARANTE));

            if (!string.IsNullOrWhiteSpace(p_codStato))
            {
                ret = ret.Where(d => d.cod_stato.ToUpper() == p_codStato.ToUpper());
            }

            return ret;
        }

        public static IQueryable<join_referente_contribuente> GetListRecsByIdRef(int p_idReferente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.id_tab_referente == p_idReferente && c.cod_stato.StartsWith(join_referente_contribuente.ATT));
        }

        public static IQueryable<join_referente_contribuente> GetListAttByIdRefIdContr(int p_idReferente, int p_idContribuente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c =>
                c.id_tab_referente == p_idReferente && c.id_anag_contribuente == p_idContribuente && c.cod_stato.StartsWith(join_referente_contribuente.ATT) &&
                c.data_inizio_validita <= DateTime.Now && (c.data_fine_validita == null || c.data_fine_validita >= DateTime.Now));
        }
    }
}