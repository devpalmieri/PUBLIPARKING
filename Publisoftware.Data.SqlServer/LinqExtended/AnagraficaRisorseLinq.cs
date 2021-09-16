using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaRisorseLinq
    {
        public static IQueryable<anagrafica_risorse> OrderByDefault(this IQueryable<anagrafica_risorse> p_query)
        {
            return p_query.OrderBy(o => o.cognome).ThenBy(o => o.nome).ThenBy(o => o.data_nascita);
        }

        public static IQueryable<anagrafica_risorse> OrderByUsername(this IQueryable<anagrafica_risorse> p_query)
        {
            return p_query.OrderBy(o => o.username);
        }

        public static IQueryable<anagrafica_risorse> WhereByIdRuoloMansione(this IQueryable<anagrafica_risorse> p_query, int p_idRuoloMansione)
        {
            return p_query.Where(d => d.anagrafica_ruolo_mansione.id_ruolo_mansione == p_idRuoloMansione);
        }

        public static IQueryable<anagrafica_risorse> OrderByCognomeNome(this IQueryable<anagrafica_risorse> p_query)
        {
            return p_query.OrderBy(o => o.cognome).ThenBy(o => o.nome);
        }

        public static IQueryable<anagrafica_risorse> WhereByCodRuoloMansione(this IQueryable<anagrafica_risorse> p_query, string p_cod_ruolo)
        {
            return p_query.Where(ar => ar.anagrafica_ruolo_mansione.cod_ruolo_mansione.ToUpper().Contains(p_cod_ruolo.ToUpper()));
        }

        public static IQueryable<anagrafica_risorse> WhereByResponsabili(this IQueryable<anagrafica_risorse> p_query)
        {
            return p_query.Where(d => d.flag_ispettore == "1");
        }

        public static IQueryable<anagrafica_risorse> WhereByRisorsa(this IQueryable<anagrafica_risorse> p_query)
        {
            return p_query.Where(d => d.flag_ispettore == "0");
        }

        public static IQueryable<anagrafica_risorse> WhereByIdRisorsa(this IQueryable<anagrafica_risorse> p_query, int p_id)
        {
            return p_query.Where(d => d.id_risorsa == p_id);
        }

        public static IQueryable<anagrafica_risorse> WhereByNome(this IQueryable<anagrafica_risorse> p_query, string p_nome)
        {
            return p_query.Where(d => d.nome.ToLower().Equals(p_nome.ToLower()));
        }

        public static IQueryable<anagrafica_risorse> WhereByCognome(this IQueryable<anagrafica_risorse> p_query, string p_cognome)
        {
            return p_query.Where(d => d.cognome.ToLower().Equals(p_cognome.ToLower()));
        }

        public static IQueryable<anagrafica_risorse> WhereByUsername(this IQueryable<anagrafica_risorse> p_query, string p_username)
        {
            return p_query.Where(d => d.username.ToLower().Equals(p_username.ToLower()));
        }

        public static IQueryable<anagrafica_risorse> WhereByIdEnteAppartenenza(this IQueryable<anagrafica_risorse> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente_appartenenza == p_idEnte);
        }

        public static IQueryable<anagrafica_risorse_light> ToLight(this IQueryable<anagrafica_risorse> iniziale)
        {
            return iniziale.ToList().Select(d => new anagrafica_risorse_light
            {
                id_risorsa = d.id_risorsa,
                cognome = d.cognome,
                nome = d.nome,
                matricola = d.matricola,
                email = d.email,
                username = d.username,
                Strutture = string.Empty,
                idJoinRisorsaStruttura = 0
            }).AsQueryable();
        }
    }
}
