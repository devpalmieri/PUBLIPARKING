using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class TariffeFlatBD : EntityBD<TariffeFlat>
    {
        public TariffeFlatBD()
        {

        }
        
        public static decimal calcolaImportoFissoDaPagare(Tariffe p_tariffa, Int32 p_minuti, DateTime p_dataOraPagamento, DbParkCtx p_context)
        {
            decimal v_importo = 0;

            TariffeFlat v_tariffaFlat = GetList(p_context)
                                .Where(a => a.idTariffa.Equals(p_tariffa.idTariffa))
                                //.Where(a=> a.orainizio.Value >= p_dataOraPagamento.TimeOfDay && a.orafine.Value <= p_dataOraPagamento.TimeOfDay)

                                 //Where tariffa.minutiMinimo <= p_minuti And tariffa.minutiMassimo >= p_minuti

                                .Where(a=> a.minutiMinimo.Value <= p_minuti && a.minutiMassimo.Value >= p_minuti)
                                .OrderByDescending(a=> a.minutiMassimo)
                                .FirstOrDefault();

            if (v_tariffaFlat != null)
            {
                v_importo = v_tariffaFlat.importo.HasValue ? v_tariffaFlat.importo.Value : 0;
            }

            return v_importo;
        }
        public static int calcolaMinutiResidui(Tariffe p_tariffa, Int32 p_minuti, DateTime p_dataOraPagamento, DbParkCtx p_context)
        {
            int v_minutiresidui = p_minuti;

            TariffeFlat v_tariffaFlat = GetList(p_context)
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


        public static bool ExistsTariffaFlat(Tariffe p_tariffa, DbParkCtx p_context)
        {
            return GetList(p_context).Where(a => a.idTariffa.Equals(p_tariffa.idTariffa)).Any();            
        }

    }
}
