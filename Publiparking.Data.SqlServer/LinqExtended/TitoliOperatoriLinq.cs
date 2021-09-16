using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static  class TitoliOperatoriLinq
    {
        public static IQueryable<TitoloOperatoreDTO> ToDTO(this IQueryable<TitoliOperatori> iniziale)
        {

            return iniziale.ToList().Select(d => new TitoloOperatoreDTO
            {
                id = d.idTitoloOperatore,
                targa = d.targa,
                importo = d.importo,
                idOperatore = d.idOperatore,
                idStallo = d.idStallo,
                scadenza = d.scadenza,
                codice = d.codice,
                dataPagamento = d.dataPagamento
        }).AsQueryable();
        }

        public static TitoloOperatoreDTO toTitoloOperatoreDTO(this TitoliOperatori iniziale)
        {
            TitoloOperatoreDTO v_titoliOperatore = new TitoloOperatoreDTO();
            v_titoliOperatore.id = iniziale.idTitoloOperatore;
            v_titoliOperatore.targa = iniziale.targa;
            v_titoliOperatore.importo = iniziale.importo;            
            v_titoliOperatore.idOperatore = iniziale.idOperatore;
            v_titoliOperatore.idStallo = iniziale.idStallo;
            v_titoliOperatore.scadenza = iniziale.scadenza;
            v_titoliOperatore.codice = iniziale.codice;
            v_titoliOperatore.dataPagamento = iniziale.dataPagamento;
            return v_titoliOperatore;
        }
    }
}
