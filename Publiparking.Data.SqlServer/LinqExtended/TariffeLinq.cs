using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class TariffeLinq
    {

        public static IQueryable<TariffaLightDTO> ToDTO(this IQueryable<Tariffe> iniziale)
        {

            return iniziale.ToList().Select(d => new TariffaLightDTO
            {
                id = d.idTariffa,
                descrizione = d.descrizione
            }).AsQueryable();
        }

        public static TariffaLightDTO toTariffaLightDTO(this Tariffe iniziale)
        {
            TariffaLightDTO v_tariffa = new TariffaLightDTO();
            v_tariffa.id = iniziale.idTariffa;
            v_tariffa.descrizione = iniziale.descrizione;
           
            return v_tariffa;
        }

    }
}
