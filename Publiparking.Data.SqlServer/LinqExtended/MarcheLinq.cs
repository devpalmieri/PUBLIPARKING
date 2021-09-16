using Publiparking.Data.dto;
using Publiparking.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class MarcheLinq
    {
        public static IQueryable<Marche_light> ToLight(this IQueryable<Marche> iniziale)
        {
            return iniziale.ToList().Select(d => new Marche_light
            {
                id = d.idMarca,
                descrizione = d.descrizione,                
            }).AsQueryable();
        }
        public static IQueryable<MarcaLightDTO> ToLightDTO(this IQueryable<Marche> iniziale)
        {
            return iniziale.ToList().Select(d => new MarcaLightDTO
            {
                id = d.idMarca,
                descrizione = d.descrizione,
            }).AsQueryable();
        }
    }
}
