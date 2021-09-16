using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabSelezioneReportViewBD : EntityBD<tab_selezione_report_view>
    {
        public TabSelezioneReportViewBD()
        {

        }
        public static string GetReport(dbEnte p_dbContext, int p_id_ente, int p_id_servizio, int id_tipo_avv_pag, int p_id_stampatore, string p_tipo_foglio)
        {
            List<string> p_nomeReport = new List<string>();
            string p_nomeReport_st = string.Empty;
            try
            {
                p_nomeReport = GetList(p_dbContext).Where(w => w.id_ente == p_id_ente && w.id_tipo_servizio == p_id_servizio && w.id_tipo_avv_pag == id_tipo_avv_pag
                && w.tipo_foglio == p_tipo_foglio).Select(s => s.nome_report).ToList();
                if(p_nomeReport != null && p_nomeReport.Count() >1 )
                {
                    p_nomeReport_st = TabSelezioneReportBD.GetList(p_dbContext).Where(w => w.id_ente == p_id_ente && w.id_tipo_servizio == p_id_servizio && w.id_tipo_avv_pag == id_tipo_avv_pag
                 && w.tipo_foglio == p_tipo_foglio).Select(s => s.nome_report).FirstOrDefault();
                }
                else if(p_nomeReport != null && p_nomeReport.Count() == 1)
                {
                    p_nomeReport_st = p_nomeReport.FirstOrDefault();
                }

                if (p_nomeReport_st == null)
                {
                    p_nomeReport = GetList(p_dbContext).Where(w => w.id_ente == p_id_ente && w.id_tipo_servizio == p_id_servizio 
                && w.tipo_foglio == p_tipo_foglio).Select(s => s.nome_report).ToList();
                    if (p_nomeReport != null && p_nomeReport.Count() > 1)
                    {
                        p_nomeReport_st = TabSelezioneReportBD.GetList(p_dbContext).Where(w => w.id_ente == p_id_ente && w.id_tipo_servizio == p_id_servizio && w.id_tipo_avv_pag == id_tipo_avv_pag
                     && w.tipo_foglio == p_tipo_foglio).Select(s => s.nome_report).FirstOrDefault();
                    }
                    else if (p_nomeReport != null && p_nomeReport.Count() == 1)
                    {
                        p_nomeReport_st = p_nomeReport.FirstOrDefault();
                    }
                }
                if (p_nomeReport_st == "" || p_nomeReport_st == null  )
                {
                    p_nomeReport = GetList(p_dbContext).Where(w => w.id_tipo_servizio == p_id_servizio
             && w.tipo_foglio == p_tipo_foglio).Select(s => s.nome_report).ToList();
                    if (p_nomeReport != null && p_nomeReport.Count() > 1)
                    {
                        p_nomeReport_st = TabSelezioneReportBD.GetList(p_dbContext).Where(w => w.id_ente == p_id_ente && w.id_tipo_servizio == p_id_servizio 
                     && w.tipo_foglio == p_tipo_foglio).Select(s => s.nome_report).FirstOrDefault();
                    }
                    else if (p_nomeReport != null && p_nomeReport.Count() == 1)
                    {
                        p_nomeReport_st = p_nomeReport.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                return "error";
            }

            return p_nomeReport_st;
        }
    }
}
