using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabModalitaRateAvvPagViewBD : EntityBD<tab_modalita_rate_avvpag_view>
    {
        public TabModalitaRateAvvPagViewBD()
        {

        }

        public static tab_modalita_rate_avvpag_view GetByIdServizioAndIdEnte(int p_idServizio, int p_idEnte, dbEnte dbContext)
        {
            return GetList(dbContext).Where(j => j.id_tipo_servizio == p_idServizio)
                                     .Where(j => j.id_ente == p_idEnte)
                                     .WhereByRangeValiditaOdierno()
                                     .FirstOrDefault();
        }

        public static IQueryable<tab_modalita_rate_avvpag_view> QueryByIdTipoAvvPagAndIdEnte(int p_idTipoAvvPag, int p_idEnte, dbEnte dbContext)
        {
            return GetList(dbContext).Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                                     .Where(j => j.id_ente == p_idEnte)
                                     .WhereByRangeValiditaOdierno();
        }

        // TODO: Pietro: Controllare dove viene usata "GetByIdTipoAvvPagAndIdEnte"
        // TODO: Cambiare nome: questa funzione controlla anche range validità, non fa select solo per p_idTipoAvvPag, p_idEnte
        // TODO: Luigi aggiungere indici
        public static tab_modalita_rate_avvpag_view GetByIdTipoAvvPagAndIdEnte(int p_idTipoAvvPag, int p_idEnte, dbEnte dbContext)
        {
            return GetList(dbContext).Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                                     .Where(j => j.id_ente == p_idEnte)
                                     .WhereByRangeValiditaOdierno()
                                     .FirstOrDefault();
        }

        /// <summary>
        /// Filtro per l'id del tipo avviso, l'id dell'ente (filtra anche per l'id_servizio?)
        /// </summary>        
        /// <param name="p_idTipoAvvPag"></param>
        /// <param name="p_idEnteGestito"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static tab_modalita_rate_avvpag_view GetByIdTipoAvvPagAndIdEnteAndRange(int p_idTipoAvvPag, int p_idEnte, dbEnte dbContext)
        {
            return GetList(dbContext).Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                                     .Where(j => j.id_ente == p_idEnte)
                                     .WhereByRangeValiditaOdierno()
                                     .FirstOrDefault();
        }

        /// <summary>
        /// Filtro per l'id del tipo avviso, l'id dell'ente e l'id della voce di contribuzione
        /// </summary>        
        /// <param name="p_idTipoAvvPag"></param>
        /// <param name="p_idEnteGestito"></param>
        /// <param name="p_ValidInDate">Data in cui le modalità sono da considerarsi valide</param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static tab_modalita_rate_avvpag_view GetByIdTipoAvvPagAndIdEnteGestitoForSpecificDate(int p_idTipoAvvPag, int p_idEnte, DateTime p_ValidInDate, dbEnte dbContext)
        {
            DateTime p_ValidInDateTruncated = p_ValidInDate.Date;

            return GetList(dbContext).Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                                     .Where(j => j.id_ente == p_idEnte)
                                     .Where(j => DbFunctions.TruncateTime(j.periodo_validita_da) <= p_ValidInDateTruncated && p_ValidInDateTruncated <= DbFunctions.TruncateTime(j.periodo_validita_a))
                                     .FirstOrDefault();
        }

        public static IQueryable<tab_modalita_rate_avvpag_view> GetByIdTipoAvvPagAndIdEnteGestito(int p_idTipoAvvPag, int p_idEnte, int p_idEnteGestito, dbEnte dbContext)
        {
            return GetList(dbContext).Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                                     .Where(j => j.id_ente == p_idEnte)
                                     .Where(j => j.id_ente_gestito == p_idEnteGestito);
        }

        public static string CalcolaDecorrenzaInteressi(int p_idTipoAvviso, dbEnte dbContext)
        {
            tab_modalita_rate_avvpag_view v_modalita = GetList(dbContext).WhereByIdTipoAvvPag(p_idTipoAvviso).FirstOrDefault();

            string v_decorrenza = string.Empty;

            if (v_modalita != null && v_modalita.flag_decorrenza_interessi.HasValue && v_modalita.flag_decorrenza_interessi.Value)
            {
                if (v_modalita.GG_decorrenza_interessi == 1)
                {
                    v_decorrenza = "Dal giorno successivo alla data di notifica.";
                }
                else
                {
                    v_decorrenza = v_modalita.GG_decorrenza_interessi + " giorni a decorrere dalla data di notifica.";
                }
            }
            else if (v_modalita != null)
            {
                if (v_modalita.GG_decorrenza_interessi == 1)
                {
                    v_decorrenza = "Dal giorno successivo alla data di emissione.";
                }
                else
                {
                    v_decorrenza = v_modalita.GG_decorrenza_interessi + " giorni a decorrere dalla data di emissione.";
                }
            }

            return v_decorrenza;
        }

        public static decimal GetImportoLimiteSollecitoBonarioMax(dbEnte dbContext)
        {
            return GetList(dbContext).WhereByRangeValiditaOdierno().WhereByIdServizio(anagrafica_tipo_servizi.SOLL_PRECOA).Select(m => m.importo_limite_sollecito_bonario).Max().GetValueOrDefault();
        }

        public static bool EmissioneSecondoSollecito(dbEnte dbContext)
        {
            return GetList(dbContext).Any(m => m.id_tipo_avv_pag == anagrafica_tipo_avv_pag.SOLLECITO_POST_INGIUNZIONE_G);
        }
    }
}
