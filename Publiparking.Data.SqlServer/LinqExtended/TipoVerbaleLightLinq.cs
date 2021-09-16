using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class TipoVerbaleLightLinq
    {
        public static IQueryable<TipoVerbaleLightDTO> ToLigthDTO(this IQueryable<TipiVerbale> iniziale)
        {

            return iniziale.ToList().Select(d => new TipoVerbaleLightDTO
            {
                id = d.idTipoVerbale,
                descrizione = !string.IsNullOrEmpty(d.descrizione) ? d.descrizione : string.Empty,
                isDefault = d.isDefault.HasValue ? d.isDefault.Value : false
            }).AsQueryable();
        }
    }
}
