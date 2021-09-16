using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class TipoVerbaleLinq
    {

        public static TipoVerbaleDTO toTipoVerbaleDTO(this TipiVerbale iniziale)
        {
            TipoVerbaleDTO v_tipoverbale = new TipoVerbaleDTO();
            v_tipoverbale.id = iniziale.idTipoVerbale;
            v_tipoverbale.descrizione = !string.IsNullOrEmpty(iniziale.descrizione) ? iniziale.descrizione : string.Empty;
            v_tipoverbale.isDefault = iniziale.isDefault.HasValue ? iniziale.isDefault.Value : false;

            List<int> v_lst = new List<int>();
            foreach (Int32 v_causaleId in iniziale.Causali.Select(c=> c.idCausale))
            {
                v_lst.Add(v_causaleId);                
            }
            v_tipoverbale.Causali = v_lst;
            return v_tipoverbale;
        }
    }
}
