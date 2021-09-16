using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class TerminaliBD :EntityBD<Terminali>
    {
        public TerminaliBD()
        {

        }

        public static bool verbalizzazioneAttiva(Int32 id,DbParkCtx p_context)
        {
            bool risp = false;
            Configurazione v_configurazione = ConfigurazioneBD.GetList(p_context).FirstOrDefault();
            DateTime vDataDB = DateTime.Now;
            //TOTO; getultimotitolo
            DateTime vDataUltimoTitolo = TranslogBD.GetList(p_context).OrderByDescending(t => t.tlPayDateTime.Value).FirstOrDefault().tlPayDateTime.Value;

            TimeSpan vDiff = vDataDB.Subtract(vDataUltimoTitolo);

            if (vDiff.TotalMinutes <= v_configurazione.bloccoInattivitaTotem & (!v_configurazione.bloccoVerbali))
                risp = true;

            return risp;
        }

    }
}
