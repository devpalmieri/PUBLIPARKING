using Publisoftware.Data.LinqExtended;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Publisoftware.Data.BD
{
    public class TabSentenzeBD : EntityBD<tab_sentenze>
    {
        public TabSentenzeBD()
        {

        }

        public static IQueryable<tab_sentenze> GetSentenzeAppello(string pNumeroSentenza, DateTime? daIstanzaRicerca, DateTime? aIstanzaRicerca, int IdAutoritaSentenza, string codStato, dbEnte p_dbContext)
        {
            IQueryable<tab_sentenze> v_sentenzeList = GetListInternal(p_dbContext).Where(d => d.cod_stato.StartsWith(codStato) &&
                                                                                              //!d.id_tab_appello.HasValue &&
                                                                                              //d.tab_ricorsi1.Count == 0 &&
                                                                                              //d.data_scadenza_appello > DateTime.Now && //Il dottore hanno voluto togliere questo filtro
                                                                                              d.tab_ricorsi.id_tab_autorita_giudiziaria_ricorso == IdAutoritaSentenza);

            if (!string.IsNullOrEmpty(pNumeroSentenza))
            {
                v_sentenzeList = v_sentenzeList.Where(d => d.numero_sentenza == pNumeroSentenza);
            }

            if (daIstanzaRicerca.HasValue)
            {
                daIstanzaRicerca = daIstanzaRicerca.Value;
                v_sentenzeList = v_sentenzeList.Where(d => d.data_deposito_sentenza >= daIstanzaRicerca);
            }

            if (aIstanzaRicerca.HasValue)
            {
                aIstanzaRicerca = aIstanzaRicerca.Value.AddHours(23).AddMinutes(59);
                v_sentenzeList = v_sentenzeList.Where(d => d.data_deposito_sentenza <= aIstanzaRicerca);
            }

            return v_sentenzeList;
        }
    }
}
