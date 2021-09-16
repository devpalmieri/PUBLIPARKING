using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabModalitaSpednotDocOutputBD : EntityBD<tab_modalita_spednot_doc_output>
    {
        public TabModalitaSpednotDocOutputBD()
        {

        }

        public static tab_modalita_spednot_doc_output GetModalitaSpedizione(int? p_Idente, int p_TipoDoc, string p_Stato, dbEnte p_DbContext)
        {
            tab_modalita_spednot_doc_output risp = null;
            
            if (!String.IsNullOrEmpty(p_Stato) && p_Stato.ToUpper() == "IT")
            {
                p_Stato = "ITALIA";
            }

            risp = GetList(p_DbContext).WhereByIdEnte(p_Idente)
                                       .WhereByIdTipoDocEntrata(p_TipoDoc)
                                       .WhereByFlagEstero(p_Stato.ToUpper() == "ITALIA" ? "0" : "1")
                                       .WhereByRangeValidita(DateTime.Now)
                                       .SingleOrDefault();


            if (risp == null)
            {
                risp = GetList(p_DbContext).WhereByIdEnteNull()
                                           .WhereByIdTipoDocEntrata(p_TipoDoc)
                                           .WhereByFlagEstero(p_Stato.ToUpper() == "ITALIA" ? "0" : "1")
                                           .WhereByRangeValidita(DateTime.Now)
                                           .SingleOrDefault();
            }

            return risp;
        }
    }
}
