using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class OperatoriLinq
    {
        public static IQueryable<OperatoreDTO> ToDTO(this IQueryable<Operatori> iniziale)
        {
          
            return iniziale.ToList().Select(d => new OperatoreDTO
            {
                id = d.idOperatore,
                userName = d.username,
                hashPassword = d.password,
                cognome = d.cognome,
                nome = d.nome,
                matricola = d.matricola,
                CambioPassword = d.dataCambioPassword,
                noGpsOperazioni = d.noGpsOperazioni,
                attivo = d.attivo

            }).AsQueryable();
        }

        public static OperatoreDTO ToOperatoreDTO(this Operatori iniziale)
        {
            OperatoreDTO v_operatore = new OperatoreDTO();
            v_operatore.id = iniziale.idOperatore;
            v_operatore.userName = iniziale.username;
            v_operatore.hashPassword = iniziale.password;
            v_operatore.cognome = iniziale.cognome;
            v_operatore.nome = iniziale.nome;
            v_operatore.matricola = iniziale.matricola;
            v_operatore.CambioPassword = iniziale.dataCambioPassword;
            v_operatore.noGpsOperazioni = iniziale.noGpsOperazioni;
            v_operatore.attivo = iniziale.attivo;

            return v_operatore;
        }
    }
}
