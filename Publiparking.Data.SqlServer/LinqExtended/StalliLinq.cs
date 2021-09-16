using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class StalliLinq
    {
        public static IQueryable<StalloLightDTO> ToDTO(this IQueryable<Stalli> iniziale)
        {

            return iniziale.ToList().Select(d => new StalloLightDTO
            {
                id = d.idStallo,
                numero = d.numero,
                ubicazione = d.ubicazione,
                X = decimal.ToDouble(d.X),
                Y = decimal.ToDouble(d.Y),
                fotoRichiesta = d.fotoRichiesta,
                idToponimo = d.idToponimo.HasValue ? d.idToponimo.Value : 0,
                idTariffa = d.idTariffa.HasValue ? d.idTariffa.Value : 0

            }).AsQueryable();
        }
        public static StalloLightDTO toStalloLightDTO(this Stalli iniziale)
        {
            StalloLightDTO v_stallo = new StalloLightDTO();
            v_stallo.id = iniziale.idStallo;
            v_stallo.numero = iniziale.numero;
            v_stallo.ubicazione = iniziale.ubicazione;
            v_stallo.X = decimal.ToDouble(iniziale.X);
            v_stallo.Y = decimal.ToDouble(iniziale.Y);
            v_stallo.fotoRichiesta = iniziale.fotoRichiesta;
            v_stallo.idToponimo = iniziale.idToponimo.HasValue ? iniziale.idToponimo.Value : 0 ;
            v_stallo.idTariffa = iniziale.idTariffa.HasValue ? iniziale.idTariffa.Value : 0;

            return v_stallo;
        }
    }
}
