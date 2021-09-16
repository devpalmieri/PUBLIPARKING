using Publiparking.Data.dto;
using Publiparking.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class GiriLinq
    {
       
        public static IQueryable<Giri_light> ToLight(this IQueryable<Giri> iniziale)
        {
            return iniziale.ToList().Select(d => new Giri_light
            {
                id = d.idGiro,
                descrizione = d.descrizione,
                ultimaModifica = d.dataUltimaModifica
            }).AsQueryable();
        }

        public static Giri_light ToGiroLight(this Giri iniziale)
        {
            Giri_light gl = new Giri_light();
            gl.id = iniziale.idGiro;
            gl.descrizione = iniziale.descrizione;
            gl.ultimaModifica = iniziale.dataUltimaModifica;

            return gl;
        }

        public static GiroLightDTO ToGiroLightDTO(this Giri iniziale)
        {
            GiroLightDTO gl = new GiroLightDTO();
            gl.id = iniziale.idGiro;
            gl.descrizione = iniziale.descrizione;
            gl.ultimaModifica = iniziale.dataUltimaModifica;
            
            return gl;
        }
    }
}
