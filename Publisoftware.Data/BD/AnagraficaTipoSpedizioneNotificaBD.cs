using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaTipoSpedizioneNotificaBD : EntityBD<anagrafica_tipo_spedizione_notifica>
    {
        public AnagraficaTipoSpedizioneNotificaBD()
        {

        }

        public static IQueryable<anagrafica_tipo_spedizione_notifica> GetTipoSpedizioneForTipoAvvPag(int p_idTipoAvv, dbEnte p_dbContext)
        {
            anagrafica_tipo_avv_pag selAnaTipoAvv = AnagraficaTipoAvvPagBD.GetById(p_idTipoAvv, p_dbContext);
            if (selAnaTipoAvv.flag_notifica == "0")
                return GetList(p_dbContext).Where(tsn => tsn.flag_sped_not == "S");

            if (selAnaTipoAvv.flag_notifica == "1")
                return GetList(p_dbContext).Where(tsn => tsn.flag_sped_not == "N");

            return new List<anagrafica_tipo_spedizione_notifica>().AsQueryable();
        }
        public static int? GetIdTipoNotificatoreForTipoSpedizione(int p_id_tipo_spedizione_notifica, dbEnte p_dbContext)
        {
            return GetById(p_id_tipo_spedizione_notifica, p_dbContext).id_notificatore;

        }
        public static int? GetIdTipoSpedizioneNotificaBySigla(string p_code, dbEnte p_dbContext)
        {
            return GetList( p_dbContext).Where(d=>d.sigla_tipo_spedizione_notifica==p_code).FirstOrDefault().id_tipo_spedizione_notifica;

        }

        public static int? GetIdTipoSpedizioneNotificaBySiglaIdStampatore(string p_code, int p_idStampatore, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.sigla_tipo_spedizione_notifica == p_code && d.id_stampatore == p_idStampatore).FirstOrDefault().id_tipo_spedizione_notifica;

        }
    }
}
