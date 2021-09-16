using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class PenaliLinq
    {     
        public static PenaleDTO ToPenaleDTO(this Penali iniziale)
        {
            PenaleDTO v_penale = new PenaleDTO();
            v_penale.id = iniziale.idPenale;
            v_penale.data = iniziale.data;
            v_penale.idOperatore = iniziale.idOperatore;
            v_penale.idStallo = iniziale.idStallo;
            v_penale.tipoVeicolo = iniziale.tipoVeicolo;
            v_penale.marca = iniziale.marca;
            v_penale.modello = iniziale.modello;
            v_penale.targa = iniziale.targa;
            v_penale.targaEstera = iniziale.targaEstera.HasValue ? iniziale.targaEstera.Value : false;
            v_penale.ubicazione = iniziale.via;
            v_penale.totale = iniziale.totale;
            v_penale.assenzaTrasgressore = iniziale.assenzatrasgressore;
            v_penale.codice = iniziale.codice;
            v_penale.note = iniziale.note;
            v_penale.pagata = iniziale.pagata;
            v_penale.dataElaborazione = iniziale.dataElaborazione;
            v_penale.idVerbale = iniziale.idVerbale;
            v_penale.dataPagamento = iniziale.dataPagamento;
            v_penale.codiceTitoloPagante = iniziale.codiceTitoloPagante;           
            foreach (FotoPenali v_foto in iniziale.FotoPenali)
            {
                string v_nomeFile = v_foto.fileFoto.Substring(0, 4) + @"\" + v_foto.fileFoto.Substring(4, 2) + @"\" + v_foto.fileFoto;
                v_penale.foto.Add(v_nomeFile);
            }

                return v_penale;
        }
    }
}
