using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.BD
{    
    public class OperazioniLocalBD : EntityBD<OperazioniLocal>
    {
        public const string  STATOREGOLARECONNUMERO = "O";

        public OperazioniLocalBD()
        {

        }

        public static bool CreaOperazioneDaDTO(OperazioneDTO p_operazione, DbParkCtx p_context)
        {
            bool risp = false;
            OperazioniLocal v_operazione = new OperazioniLocal();
            v_operazione.idOperatore = p_operazione.idOperatore;
            v_operazione.idStallo = p_operazione.idStallo;
            v_operazione.data = DateTime.Now;
            v_operazione.stato = p_operazione.stato;
            v_operazione.X = 0;// (decimal)p_operazione.X;
            v_operazione.Y = 0;// (decimal)p_operazione.Y;
            v_operazione.fileFoto = p_operazione.fileFoto.Replace(".jpg.jpg.jpg", ".jpg").Replace(".jpg.jpg", ".jpg");
            v_operazione.codiceTitolo = p_operazione.codiceTitolo;
            v_operazione.idVerbale = p_operazione.idVerbale;
            v_operazione.idPenale = p_operazione.idPenale;
            v_operazione.targa = p_operazione.targa;
            p_context.OperazioniLocal.Add(v_operazione);
            int res = p_context.SaveChanges();
            if (res > 0)
            {
                risp = true;
            }
            return risp;

        }
    }

    
}
