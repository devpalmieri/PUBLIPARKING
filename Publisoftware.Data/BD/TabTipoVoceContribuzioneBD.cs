using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data;
using Publisoftware.Data.LinqExtended;

namespace Publisoftware.Data.BD
{
    public class TabTipoVoceContribuzioneBD : EntityBD<tab_tipo_voce_contribuzione>
    {
        public TabTipoVoceContribuzioneBD()
        {

        }

        /// <summary>
        /// Filtro per id entrata
        /// </summary>
        /// <param name="p_idEntrata"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_tipo_voce_contribuzione> GetListByIdEntrata(int p_idEntrata, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_entrata_new == p_idEntrata);
        }

        public static IQueryable<tab_tipo_voce_contribuzione> GetListByIdEntrate(IEnumerable<int> p_idEntrate, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_entrata_new.HasValue && p_idEntrate.Contains(d.id_entrata_new.Value));
        }

        public static IQueryable<tab_tipo_voce_contribuzione> WhereByCodiceTributoMinisteriale(int p_idEntrata, string p_codice, dbEnte p_dbContext)
        {
            List<tab_tipo_voce_contribuzione> v_listaTributiMinisteriali = GetList(p_dbContext)
                                                                                .Where(d => d.codice_tributo_ministeriale.Trim().Equals(p_codice))
                                                                                .Where(d => d.id_entrata == p_idEntrata)
                                                                                .ToList();

            return v_listaTributiMinisteriali.AsQueryable();
        }

        /// <summary>
        /// Restituisce id_entrata_new
        /// </summary>
        /// <param name="p_idTabMovPag">
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_tipo_voce_contribuzione GetByIdTipoVoceContribuzione(int p_idTipoVoce, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tipo_voce_contribuzione == p_idTipoVoce);
        }
    }
}
