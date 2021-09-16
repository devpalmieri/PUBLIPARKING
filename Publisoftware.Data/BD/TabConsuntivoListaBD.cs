using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabConsuntivoListaBD : EntityBD<tab_consuntivo_lista_carico>
    {
        public TabConsuntivoListaBD()
        { 

        }

        /// <summary>
        /// Filtro per l'id entrata, l'id ente, l'id lista e l'id tipo lista
        /// </summary>
        /// <param name="p_id_ente"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_id_entrata"></param>
        /// <param name="p_id_tipo_lista"></param>
        /// <param name="p_id_lista"></param>
        /// <returns></returns>
        public static IQueryable<tab_consuntivo_lista_carico> searchConsuntivoListe(int p_id_ente, dbEnte p_dbContext, int p_id_entrata = -1, int p_id_lista = -1, DateTime? p_data_consuntivo = null, int p_id_ente_gestito = -1)
        {
            IQueryable<tab_consuntivo_lista_carico> res = GetList(p_dbContext).Where(c => c.id_ente == p_id_ente);

            if (p_id_ente_gestito > 0)
                res = res.Where(c => c.id_ente_gestito == p_id_ente_gestito);

            if (p_data_consuntivo.HasValue)
                res = res.Where(c => c.data_consuntivo.Year == p_data_consuntivo.Value.Year && c.data_consuntivo.Month == p_data_consuntivo.Value.Month && c.data_consuntivo.Day == p_data_consuntivo.Value.Day);

            if (p_id_entrata > 0)
                res = res.Where(c => c.id_entrata == p_id_entrata);

            if (p_id_lista > 0)
                res = res.Where(c => c.id_lista == p_id_lista);

            return res;
        }
        public static IEnumerable<tab_consuntivo_lista_carico> searchConsuntivoListeWithFilters(int p_id_ente, dbEnte p_dbContext, int p_id_entrata = -1, int p_id_lista = -1, DateTime? p_data_consuntivo = null, int p_id_ente_gestito = -1)
        {
            IQueryable<tab_consuntivo_lista_carico> res = GetList(p_dbContext).Where(c => c.id_ente == p_id_ente);

            if (p_id_ente_gestito > 0)
                res = res.Where(c => c.id_ente_gestito == p_id_ente_gestito);

            if (p_data_consuntivo.HasValue)
                res = res.Where(c => c.data_consuntivo.Year == p_data_consuntivo.Value.Year && c.data_consuntivo.Month == p_data_consuntivo.Value.Month && c.data_consuntivo.Day == p_data_consuntivo.Value.Day);

            if (p_id_entrata > 0)
                res = res.Where(c => c.id_entrata == p_id_entrata);

            if (p_id_lista > 0)
                res = res.Where(c => c.id_lista == p_id_lista);

            res = res.Where(x =>
              !x.cod_stato_avv_pag.StartsWith("ANN") && !x.cod_stato_avv_pag.StartsWith("DAN") && !x.cod_stato_avv_pag.StartsWith("RET") && !x.cod_stato_avv_pag.StartsWith("DAR") && !x.cod_stato_avv_pag.StartsWith("CAR")
            );


            return res;
        }
        /// <summary>
        /// restituisce le date di storicizzazione dei consuntivi
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_id_lista"></param>
        /// <param name="p_howMany"></param>
        /// <returns></returns>
        public static IQueryable<DateTime> getDateStoricizzazione(dbEnte p_dbContext, int p_id_lista = -1, int p_id_ente = -1, int p_id_ente_gestito = -1, int p_howMany = -1)
        {
            IQueryable<tab_consuntivo_lista_carico> consuntivi = GetList(p_dbContext);

            if (p_id_lista > 0)
            {
                consuntivi = consuntivi.Where(c => c.id_lista == p_id_lista);
            }

            if (p_id_ente > 0)
            {
                consuntivi = consuntivi.Where(c => c.id_ente == p_id_ente);
            }

            if (p_id_ente_gestito > 0)
            {
                consuntivi = consuntivi.Where(c => c.id_ente_gestito == p_id_ente_gestito);
            }

            IQueryable<DateTime> dateDisp = consuntivi.Select(c => c.data_consuntivo).Distinct();

            if (p_howMany > 0)
            {
                dateDisp = dateDisp.Take(p_howMany);
            }

            return dateDisp;
        }
    }
}
