using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinOggettoAgevolazioniBD : EntityBD<join_oggetto_agevolazioni>
    {
        public JoinOggettoAgevolazioniBD()
        {

        }

        [Obsolete("Usare GetListByIdOggettoInOggettoDiContribuzione")]
        public static IQueryable<join_oggetto_agevolazioni> GetListByIdOggetto(Decimal p_idOggettoContribuzione, Decimal p_idContribuente, dbEnte p_dbContext)
        {
            return GetListByIdOggettoInOggettoDiContribuzione(p_idOggettoContribuzione, p_idContribuente, p_dbContext);
        }

        public static IQueryable<join_oggetto_agevolazioni> GetListByIdOggettoInOggettoDiContribuzione(Decimal p_idOggettoContribuzione, Decimal p_idContribuente, dbEnte p_dbContext)
        {
            tab_oggetti_contribuzione v_oggettoContribuzione = TabOggettiContribuzioneBD.GetById(p_idOggettoContribuzione, p_dbContext);
            return GetListByIdOggettoInOggettoDiContribuzione(v_oggettoContribuzione, p_idContribuente, p_dbContext);
        }

        public static IQueryable<join_oggetto_agevolazioni> GetListByIdOggettoInOggettoDiContribuzione(tab_oggetti_contribuzione oggettoContribuzione, Decimal p_idContribuente, dbEnte p_dbContext)
        {
            if (p_idContribuente != -1)
            {
                return GetList(p_dbContext).Where(d => d.id_oggetto == oggettoContribuzione.id_oggetto &&
                                                       d.id_contribuente == p_idContribuente &&
                                                       oggettoContribuzione.data_inizio_contribuzione <= (d.tab_agevolazioni.data_fine_validita.HasValue ? d.tab_agevolazioni.data_fine_validita.Value : DateTime.MaxValue) &&
                                                       d.tab_agevolazioni!=null &&
                                                       d.tab_agevolazioni.data_inizio_validita <= (oggettoContribuzione.data_fine_contribuzione.HasValue ? oggettoContribuzione.data_fine_contribuzione.Value : DateTime.MaxValue));
            }
            else
            {
                return GetList(p_dbContext).Where(d => d.id_oggetto == oggettoContribuzione.id_oggetto &&
                                                       oggettoContribuzione.data_inizio_contribuzione <= (d.tab_agevolazioni.data_fine_validita.HasValue ? d.tab_agevolazioni.data_fine_validita.Value : DateTime.MaxValue) &&
                                                       d.tab_agevolazioni!=null &&
                                                       d.tab_agevolazioni.data_inizio_validita <= (oggettoContribuzione.data_fine_contribuzione.HasValue ? oggettoContribuzione.data_fine_contribuzione.Value : DateTime.MaxValue));
            }
        }

        public static IQueryable<join_oggetto_agevolazioni> GetListByIdOggettoInOggettoDiContribuzioneAndCodStato(
            tab_oggetti_contribuzione oggettoContribuzione,
            decimal id_anag_contribuente,
            // ----------------------------------------------------------------------------------
            // Sati da usare, tipicamente:
            //    anagrafica_stato_agevolazione.ATT_ATT,
            //    anagrafica_stato_agevolazione.SSP_ATT,
            //    anagrafica_stato_agevolazione.SSP_CES,
            //    anagrafica_stato_agevolazione.SSP_IST,
            //    // ---
            //    // Non in specifiche, ma forse meglio vederlo sebbene senza azioni abilitate!
            //    anagrafica_stato_agevolazione.ATT_CES
            IList<string> codStatoDaUsare,
            // ----------------------------------------------------------------------------------
            // Stato da non usare tipicamente
            //     anagrafica_stato_agevolazione.ANN
            string codStatoDiJoinDaNotLike,
            dbEnte p_dbContext)
        {
            return JoinOggettoAgevolazioniBD.GetListByIdOggettoInOggettoDiContribuzione(oggettoContribuzione, id_anag_contribuente, p_dbContext)
                // ------------------------------------------------------------------------------------------------------------------------
                // N.B.: il cod_stato da prendere si seleziona su "tab_agevolazioni", invece si escludono gli "ANN%" della join!
                // ------------------------------------------------------------------------------------------------------------------------
                .Where(x => codStatoDaUsare.Contains(x.tab_agevolazioni.cod_stato) && !x.cod_stato.StartsWith(codStatoDiJoinDaNotLike));
        }
    }
}
