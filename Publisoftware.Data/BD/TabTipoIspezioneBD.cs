using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabTipoIspezioneBD : EntityBD<tab_tipo_ispezione> 
    {
        public TabTipoIspezioneBD()
        {

        }

        /// <summary>
        /// Ritorna i giorni di validita dell'ispezione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static int GetValiditaByTipo(string p_tipoIspezione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(t => t.sigla_tipo_ispezione.Equals(p_tipoIspezione.Trim())).Select(s => s.giorni_validita_ispezione).Single();
        }
        public static tab_tipo_ispezione GetTipoIspezione(int p_tipoIspezione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(t => t.id_tab_tipo_ispezione == p_tipoIspezione).FirstOrDefault();
        }
    }
}
