using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class VerbaleLinq
    {

        public static VerbaleDTO toVerbaleDTO(this Verbali iniziale)
        {          
            VerbaleDTO v_verbale = new VerbaleDTO();
            v_verbale.id = iniziale.idVerbale;
            v_verbale.idOperatore = iniziale.idOperatore;
            v_verbale.data = iniziale.data;
            v_verbale.idStallo = iniziale.idStallo;
            v_verbale.ubicazione = iniziale.Stalli.ubicazione;
            v_verbale.tipoVeicolo = iniziale.tipoVeicolo;
            v_verbale.marca = iniziale.marca;
            v_verbale.modello = iniziale.modello;
            v_verbale.targa = iniziale.targa;
            v_verbale.targaEstera = iniziale.targaEstera.HasValue ? iniziale.targaEstera.Value : false;
            v_verbale.totale = iniziale.totale;
            v_verbale.assenzaTrasgressore = iniziale.assenzatrasgressore;
            v_verbale.codiceBollettino = iniziale.codiceBollettino;
            v_verbale.serie = iniziale.serie;
            v_verbale.note = iniziale.note;
            v_verbale.targa = iniziale.targa;
            foreach (FotoVerbali v_foto in iniziale.FotoVerbali)
            {
                string v_nomeFile = v_foto.fileFoto;
                v_verbale.foto.Add(v_nomeFile);
            }

            foreach (CausaliVerbali v_causaleVerbale in iniziale.CausaliVerbali)
            {                
                v_verbale.codiciViolati.Add(v_causaleVerbale.idCausale);
            }
           
            return v_verbale;
        }
    }
}
