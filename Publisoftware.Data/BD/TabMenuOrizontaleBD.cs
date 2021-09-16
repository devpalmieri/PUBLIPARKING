using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.BD
{
    public class TabMenuOrizontaleBD : EntityBD<tab_menu_orizzontale>
    {
        #region Costructor
        public TabMenuOrizontaleBD() { }
        #endregion Costructor

        #region Public Methods
        public static List<tab_menu_orizzontale> GetMenu(dbEnte p_ctx, string modOp, int? p_id_ente = null, int? p_id_struttura = null)
        {

            List<tab_menu_orizzontale> results = GetList(p_ctx).Where(d => d.Id_Ente == p_id_ente
            && d.ModOp == modOp).ToList();
            if (results != null && results.Count()>0)
            {
                if (p_id_ente.HasValue)
                    results = results.Where(x => x.Id_Ente == p_id_ente).ToList();
                else
                    results = results.Where(x => x.Id_Ente == null).ToList();

                if (p_id_struttura.HasValue)
                    results = results.Where(x => x.Id_Struttura == p_id_struttura).ToList();
                else
                    results = results.Where(x => x.Id_Struttura == null).ToList();

            }
            if (results.Count ()<=0)
            {
                results = GetList(p_ctx).Where(d => d.Id_Ente == null
                && d.ModOp == modOp).ToList();
            }

            return results;
        }
        #endregion Public Methods
    }
}
