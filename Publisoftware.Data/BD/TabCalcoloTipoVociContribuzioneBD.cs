using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabCalcoloTipoVociContribuzioneBD : EntityBD<tab_calcolo_tipo_voci_contribuzione>
    {
        public TabCalcoloTipoVociContribuzioneBD()
        {

        }

        public static IQueryable<tab_calcolo_tipo_voci_contribuzione> GetListModalitaCalcoloFromTipoVoce(Int32 p_idEnte, Int32 p_idEnteGestito, Int32 p_idTipoVoceContribuzione, DateTime? p_periodoDa, DateTime? p_periodoA, dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                       .Where(c => c.id_ente == p_idEnte)                       
                       .Where(c => c.id_tipo_voce_contribuzione == p_idTipoVoceContribuzione)
                       //.Where(c => c.modalita_calcolo.Equals(tab_calcolo_tipo_voci_contribuzione.MODALITA_CALCOLO_ALIQUOTA_ANNUALE))
                       .Where(c => c.periodo_validita_da.HasValue && c.periodo_validita_da.Value <= p_periodoA)
                       .Where(c => c.periodo_validita_a.HasValue && c.periodo_validita_a.Value > p_periodoDa)
                       .Where(c => !c.cod_stato.StartsWith(CodStato.ANN));

        }

        public static IQueryable<tab_calcolo_tipo_voci_contribuzione> GetCalcoloOnereAccEsecutivo(Int32 p_idEnte, Int32 p_idEnteGestito, Int32 p_idEntrata, Int32 p_idTipoVoceContribuzione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                       .Where(c => c.id_ente == p_idEnte)
                       .Where(c => c.id_ente_gestito == p_idEnteGestito)
                       .Where(c => c.id_entrata == p_idEntrata)
                       .Where(c => c.id_tipo_voce_contribuzione == p_idTipoVoceContribuzione)
                       .Where(c => !c.cod_stato.StartsWith(CodStato.ANN));
        }

        public static IQueryable<tab_calcolo_tipo_voci_contribuzione> GetListModalitaCalcoloFromAnagVoceServizio(Int32 p_idEnte, Int32 p_idEnteGestito, Int32 p_idAnagraficaVoceVoceContribuzione, Int32 p_idTipoServizio, DateTime? p_periodoDa, DateTime? p_periodoA, dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                       .Where(c => c.id_ente == p_idEnte)
                       .Where(c => c.id_ente_gestito == p_idEnteGestito)
                       .Where(c => c.id_tipo_servizio == p_idTipoServizio)
                       .Where(c => c.id_anagrafica_voce_contribuzione == p_idAnagraficaVoceVoceContribuzione)
                       .Where(c => c.modalita_calcolo.Equals(tab_calcolo_tipo_voci_contribuzione.MODALITA_CALCOLO_ALIQUOTA_ANNUALE))
                       .Where(c => c.periodo_validita_da.HasValue && c.periodo_validita_da.Value <= p_periodoA)
                       .Where(c => c.periodo_validita_a.HasValue && c.periodo_validita_a.Value > p_periodoDa)
                       .Where(c => !c.cod_stato.StartsWith(CodStato.ANN));

        }
        public static tab_calcolo_tipo_voci_contribuzione getSpeseSpedizione(Int32 p_idEnte, Int32 p_id_TipoServizio, Int32 p_tipo_sped_not_contribuente, DateTime? p_oggi, dbEnte p_dbContext)
        {
            tab_calcolo_tipo_voci_contribuzione calcolo_tvc = GetList(p_dbContext)
                        .Where(c=> c.id_ente == p_idEnte)
                        .Where(c=> c.id_tipo_servizio == p_id_TipoServizio)
                        .Where(c=> c.id_tipo_spedizione_notifica.HasValue && c.id_tipo_spedizione_notifica.Value == p_tipo_sped_not_contribuente)
                        .Where(c => c.modalita_calcolo == tab_calcolo_tipo_voci_contribuzione.MODALITA_CALCOLO_FISSA)
                        .Where(c=> c.periodo_validita_da <= p_oggi && c.periodo_validita_a >= p_oggi)
                        .Where(c=> !c.cod_stato.StartsWith(CodStato.ANN))
                .FirstOrDefault();

            if(calcolo_tvc == null)
            {
                calcolo_tvc = GetList(p_dbContext)
                        .Where(c => c.id_ente == p_idEnte)                      
                        .Where(c => c.id_tipo_spedizione_notifica.HasValue && c.id_tipo_spedizione_notifica.Value == p_tipo_sped_not_contribuente)
                        .Where(c => c.modalita_calcolo == tab_calcolo_tipo_voci_contribuzione.MODALITA_CALCOLO_FISSA)
                        .Where(c => c.periodo_validita_da <= p_oggi && c.periodo_validita_a >= p_oggi)
                        .Where(c => !c.cod_stato.StartsWith(CodStato.ANN))
                .FirstOrDefault();
            }

            return calcolo_tvc;
           
        }


    }
}
