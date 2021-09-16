using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabPianoContiEconomiciBD : EntityBD<tab_piano_conti_economici>
    {
        public TabPianoContiEconomiciBD()
        {

        }

        public static IQueryable<tab_piano_conti_economici> GetPianoContiEconomiciForConsuntiviEconomici(IEnumerable<int> p_idTipoVociLst, int p_idTipoServizio, dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                .Where(pc => pc.join_conto_economico_tipo_voce_contribuzione
                    .Any(j => j.id_tipo_servizio == p_idTipoServizio 
                    && j.flag_consuntivi_economici
                    && p_idTipoVociLst.Contains(j.id_tipo_voce_contribuzione)));
        }

        public static IQueryable<tab_piano_conti_economici> GetPianoContiEconomiciForConsuntiviRiscossionione(IEnumerable<int> p_idTipoVociLst, int p_idTipoServizio, dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                .Where(pc => pc.join_conto_economico_tipo_voce_contribuzione
                    .Any(j => j.id_tipo_servizio == p_idTipoServizio
                    && j.flag_consuntivi_riscossioni
                    && p_idTipoVociLst.Contains(j.id_tipo_voce_contribuzione)));
        }
    }
}
