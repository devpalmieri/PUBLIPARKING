using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabPagineBD : EntityBD<tab_pagine>
    {
        public struct Azione
        {
            public string Valore { get; set; }
            public string Descrizione { get; set; }
        }

        public TabPagineBD()
        {

        }

        /// <summary>
        /// Filtro per id
        /// </summary>
        /// <param name="p_id"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static List<Azione> GetActionsById(int p_id, dbEnte p_dbContext)
        {
            List<Azione> v_azioni = new List<Azione>();

            if (p_id != -1)
            {
                string v_azioniAsString = @GetList(p_dbContext).Where(d => d.id_tab_pagine == p_id).FirstOrDefault().actions;                

                v_azioniAsString = v_azioniAsString.Remove(0, 1);
                v_azioniAsString = v_azioniAsString.Remove(v_azioniAsString.Length - 1, 1);

                string[] v_listaElementiAzione = v_azioniAsString.Split(',');

                Azione v_azione = new Azione();
                foreach (string v_elementoListaElementiAzione in v_listaElementiAzione)
                {
                    if (v_elementoListaElementiAzione == null)
                        continue;

                    string[] v_elementoAzione = v_elementoListaElementiAzione.Split(':');

                    v_azione.Descrizione = v_elementoAzione[0].Remove(v_elementoAzione[0].Length - 1, 1).Remove(0, 1);
                    v_azione.Valore = v_elementoAzione[1].Remove(v_elementoAzione[1].Length - 1, 1).Remove(0, 1);
                    v_azioni.Add(v_azione);
                }
            }

            return v_azioni;
        }
    }
}
