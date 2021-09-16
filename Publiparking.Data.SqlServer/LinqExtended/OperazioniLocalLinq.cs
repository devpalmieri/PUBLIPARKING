using Publiparking.Data.dto;
using Publiparking.Data.dto.type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class OperazioniLocalLinq
    {
        public static IQueryable<OperazioneDTO> ToDTO(this IQueryable<OperazioniLocal> iniziale)
        {
            return iniziale.ToList().Select(d => new OperazioneDTO
            {
            id = d.idOperazione,
            idOperatore = d.idOperatore,
            idStallo = d.idStallo,
            data = d.data,
            stato = d.stato,
            X = d.X.HasValue ? Decimal.ToDouble(d.X.Value) : 0,
            Y = d.Y.HasValue ? Decimal.ToDouble(d.Y.Value) : 0,
            fileFoto = d.fileFoto,
            codiceTitolo = d.codiceTitolo,
            idVerbale = d.idVerbale.HasValue ? d.idVerbale.Value : -1,
            idPenale = d.idPenale.HasValue ? d.idPenale.Value : -1,
            targa = d.targa          
        }).AsQueryable();
        }

        public static OperazioneDTO ToOperazioneDTO(this OperazioniLocal iniziale)
        {
            OperazioneDTO v_operazione = new OperazioneDTO();
            v_operazione.id = iniziale.idOperazione;
            v_operazione.idOperatore = iniziale.idOperatore;
            v_operazione.idStallo = iniziale.idStallo;
            v_operazione.data = iniziale.data;
            v_operazione.stato = iniziale.stato;
            v_operazione.X = iniziale.X.HasValue ?  Decimal.ToDouble(iniziale.X.Value) : 0;
            v_operazione.Y = iniziale.Y.HasValue ? Decimal.ToDouble(iniziale.Y.Value) : 0;
            v_operazione.fileFoto = iniziale.fileFoto;
            v_operazione.codiceTitolo = iniziale.codiceTitolo;
            v_operazione.idVerbale = iniziale.idVerbale.HasValue ? iniziale.idVerbale.Value : -1;
            v_operazione.idPenale = iniziale.idPenale.HasValue ? iniziale.idPenale.Value : -1;
            v_operazione.targa = iniziale.targa;
            
            return v_operazione;
        }
    }
}
