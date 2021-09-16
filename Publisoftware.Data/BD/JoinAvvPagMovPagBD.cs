using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data;

namespace Publisoftware.Data.BD
{
    public class JoinAvvPagMovPagBD : EntityBD<join_avv_pag_mov_pag>
    {
        public JoinAvvPagMovPagBD()
        {

        }

        public static IQueryable<join_avv_pag_mov_pag> GetAvvisiByRicercaAvvisoPagato(int? IdEntrata, int? IdTipoAvviso, string codiceAvvisoRicerca, DateTime? daAvvisoRicerca, DateTime? aAvvisoRicerca, dbEnte p_dbContext)
        {
            IQueryable<join_avv_pag_mov_pag> v_avvisiList = GetListInternal(p_dbContext);

            if (!string.IsNullOrEmpty(codiceAvvisoRicerca))
            {
                string p_identificativo2 = !string.IsNullOrEmpty(codiceAvvisoRicerca) ? codiceAvvisoRicerca.Replace("/", string.Empty).Replace("-", string.Empty).Trim() : string.Empty;
                string v_codice = string.Empty;
                string v_anno = string.Empty;
                string v_progressivo = string.Empty;

                if (!string.IsNullOrEmpty(p_identificativo2))
                {
                    v_codice = p_identificativo2.Substring(0, 4);
                    v_anno = p_identificativo2.Substring(4, 4);
                    if (p_identificativo2.Substring(8).All(char.IsDigit))
                    {
                        v_progressivo = Convert.ToInt32(p_identificativo2.Substring(8)).ToString();
                    }
                }

                v_avvisiList = v_avvisiList.Where(d => d.tab_avv_pag.identificativo_avv_pag.Trim() == codiceAvvisoRicerca || (d.tab_avv_pag.anagrafica_tipo_avv_pag.cod_tipo_avv_pag == v_codice &&
                                                                                                                              d.tab_avv_pag.anno_riferimento == v_anno &&
                                                                                                                              d.tab_avv_pag.numero_avv_pag == v_progressivo));
            }
            else
            {
                if (daAvvisoRicerca.HasValue)
                {
                    v_avvisiList = v_avvisiList.Where(d => d.data_oper_acc >= daAvvisoRicerca);
                }

                if (aAvvisoRicerca.HasValue)
                {
                    v_avvisiList = v_avvisiList.Where(d => d.data_oper_acc <= aAvvisoRicerca);
                }

                if (IdTipoAvviso.HasValue)
                {
                    v_avvisiList = v_avvisiList.Where(d => d.tab_avv_pag.id_tipo_avvpag == IdTipoAvviso.Value);
                }
            }

            return v_avvisiList;
        }
    }
}
