using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabDocOutputBD : EntityBD<tab_doc_output>
    {
        public TabDocOutputBD()
        {

        }

        public static IQueryable<tab_doc_output> GetEsitiByRicercaEsiti(string codiceIstanzaRicerca, DateTime? daIstanzaRicerca, DateTime? aIstanzaRicerca, int idTipoDocEntrate, dbEnte p_dbContext)
        {
            IQueryable<tab_doc_output> v_istanzeList = GetListInternal(p_dbContext).Where(d => d.id_tipo_doc_entrate == idTipoDocEntrate);

            if (!string.IsNullOrEmpty(codiceIstanzaRicerca))
            {
                v_istanzeList = v_istanzeList.Where(d => d.barcode.Trim() == codiceIstanzaRicerca);
            }
            else
            {
                if (daIstanzaRicerca.HasValue)
                {
                    v_istanzeList = v_istanzeList.Where(d => d.data_emissione_doc >= daIstanzaRicerca);
                }

                if (aIstanzaRicerca.HasValue)
                {
                    v_istanzeList = v_istanzeList.Where(d => d.data_emissione_doc <= aIstanzaRicerca);
                }
            }

            return v_istanzeList;
        }

        public static new IQueryable<tab_doc_output> GetList(dbEnte dbContext)
        {
            return dbContext.tab_doc_output.Where(d => dbContext.idContribuenteDefaultList.Count == 0 || dbContext.idContribuenteDefaultList.Contains(d.id_contribuente));
        }

        public static new tab_doc_output GetById(Int32 id, dbEnte dbContext)
        {
            return GetList(dbContext).SingleOrDefault(c => c.id_tab_doc_output == id);
        }
    }
}
