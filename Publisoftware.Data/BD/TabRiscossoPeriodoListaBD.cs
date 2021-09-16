using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabRiscossoPeriodoListaBD : EntityBD<tab_riscosso_periodo_lista>
    {
        public TabRiscossoPeriodoListaBD() { }

        public static IQueryable<DateTime> getDateAStoricizzazione(dbEnte p_dbContext, int p_idLista, int p_idEnte)
        {
            return GetList(p_dbContext).Where(c => c.id_lista_riferimento == p_idLista && c.id_ente == p_idEnte).Select(c => c.periodo_riscosso_a).Distinct();
        }

        public static IQueryable<DateTime> getDateDaStoricizzazione(dbEnte p_dbContext, int p_idLista, int p_idEnte)
        {
            //return GetList(p_dbContext).Where(c => c.id_lista_riferimento == p_idLista && c.id_ente == p_idEnte && c.periodo_riscosso_da.HasValue).Select(c => c.periodo_riscosso_da.Value).Distinct();
            return GetList(p_dbContext).Where(c => c.id_lista_riferimento == p_idLista && c.id_ente == p_idEnte && c.periodo_riscosso_da != null).Select(c => c.periodo_riscosso_da).Distinct();
        }

        public static IQueryable<tab_riscosso_periodo_lista> searchConsuntivoListe(int p_id_ente, dbEnte p_dbContext, int p_id_entrata = -1, int p_id_lista_rif = -1, DateTime? p_data_periodo_a = null, DateTime? p_data_periodo_da = null, int p_id_ente_gestito = -1, bool nonAccoppiati = false)
        {
            IQueryable<tab_riscosso_periodo_lista> res = GetList(p_dbContext).Where(c => c.id_ente == p_id_ente);

            if (p_id_ente_gestito > 0)
                res = res.Where(c => c.id_ente_gestito == p_id_ente_gestito);

            if (p_data_periodo_a.HasValue)
                res = res.Where(c => c.periodo_riscosso_a.Year == p_data_periodo_a.Value.Year && c.periodo_riscosso_a.Month == p_data_periodo_a.Value.Month && c.periodo_riscosso_a.Day == p_data_periodo_a.Value.Day);

            if (p_data_periodo_da.HasValue)
                res = res.Where(c => c.periodo_riscosso_a.Year == p_data_periodo_da.Value.Year && c.periodo_riscosso_a.Month == p_data_periodo_da.Value.Month && c.periodo_riscosso_a.Day == p_data_periodo_da.Value.Day);

            if (p_id_entrata > 0)
                res = res.Where(c => c.id_entrata == p_id_entrata);

            if (p_id_lista_rif > 0)
            {
                if (nonAccoppiati)
                {
                    throw new Exception($"Per cercare i non accoppiati impostare {nameof(p_id_lista_rif)}=-1");
                }
                res = res.Where(c => c.id_lista_riferimento == p_id_lista_rif);
            }
            else
            {
                if (nonAccoppiati)
                {
                    if (p_id_lista_rif != -1)
                    {
                        throw new Exception($"Per cercare i non accoppiati impostare {nameof(p_id_lista_rif)}=-1");
                    }
                    res = res.Where(c => c.id_lista_riferimento == null);
                }
            }
            return res;
        }
    }
}
