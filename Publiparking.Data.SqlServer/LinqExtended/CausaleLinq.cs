using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class CausaleLinq
    {
        public static IQueryable<CausaleDTO> ToDTO(this IQueryable<Causali> iniziale)
        {
           
            return iniziale.ToList().Select(d => new CausaleDTO
            {
                id = d.idCausale,
                articolo = d.articolo,
                codice = d.codice,
                subCodice = d.subCodice,
                descrizione = d.descBreve,
                descrizioneStampa = d.descStampa,
                importo = d.importo,
                attivo = d.attivo

            }).AsQueryable();
        }

        public static CausaleDTO ToCausaleDTO(this Causali iniziale)
        {
            CausaleDTO v_causale = new CausaleDTO();
            v_causale.id = iniziale.idCausale;
            v_causale.articolo = iniziale.articolo;
            v_causale.codice = iniziale.codice;
            v_causale.subCodice = iniziale.subCodice;
            v_causale.descrizione = iniziale.descBreve;
            v_causale.descrizioneStampa = iniziale.descStampa;
            v_causale.importo = iniziale.importo;
            v_causale.attivo = iniziale.attivo;
            return v_causale;


        }
    }
}
