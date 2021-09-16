using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class TariffeFlatBD : EntityBD<TariffeFlat>
    {
        public TariffeFlatBD()
        {

        }

        public static decimal calcolaImportoFissoDaPagare(Tariffe p_tariffa, Int32 p_minuti, DateTime p_dataOraPagamento, DbParkContext p_dbContext)
        {
            decimal v_importo = 0;

            TariffeFlat v_tariffaFlat = GetList(p_dbContext)
                                .Where(a => a.idTariffa.Equals(p_tariffa.idTariffa))
                                //.Where(a=> a.orainizio.Value >= p_dataOraPagamento.TimeOfDay && a.orafine.Value <= p_dataOraPagamento.TimeOfDay)

                                //Where tariffa.minutiMinimo <= p_minuti And tariffa.minutiMassimo >= p_minuti

                                .Where(a => a.minutiMinimo.Value <= p_minuti && a.minutiMassimo.Value >= p_minuti)
                                .OrderByDescending(a => a.minutiMassimo)
                                .FirstOrDefault();

            if (v_tariffaFlat != null)
            {
                v_importo = v_tariffaFlat.importo.HasValue ? v_tariffaFlat.importo.Value : 0;
            }

            return v_importo;
        }
        public static int calcolaMinutiResidui(Tariffe p_tariffa, Int32 p_minuti, DateTime p_dataOraPagamento, DbParkContext p_dbContext)
        {
            int v_minutiresidui = p_minuti;

            TariffeFlat v_tariffaFlat = GetList(p_dbContext)
                                .Where(a => a.idTariffa.Equals(p_tariffa.idTariffa))
                                 //.Where(a => a.orainizio.Value >= p_dataOraPagamento.TimeOfDay && a.orafine.Value <= p_dataOraPagamento.TimeOfDay)
                                 .Where(a => a.minutiMinimo.Value <= p_minuti && a.minutiMassimo.Value >= p_minuti)
                                .OrderByDescending(a => a.minutiMassimo)
                                .FirstOrDefault();

            if (v_tariffaFlat != null)
            {
                v_minutiresidui = v_minutiresidui - v_tariffaFlat.minutiMassimo.Value;

                if (v_minutiresidui < 0)
                    v_minutiresidui = 0;

            }

            return v_minutiresidui;
        }


        public static bool ExistsTariffaFlat(Tariffe p_tariffa, DbParkContext p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.idTariffa.Equals(p_tariffa.idTariffa)).Any();
        }

    }
}
