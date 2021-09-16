using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabVeicoliVincoliBD : EntityBD<tab_veicoli_vincoli>
    {
        public TabVeicoliVincoliBD()
        {

        }
        /// <summary>
        /// Filtro per id_veicolo
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_veicoli_vincoli> GetList(int p_idVeicolo, string p_codStatoVeicolo, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.id_veicolo == p_idVeicolo && c.tab_veicoli.cod_stato == p_codStatoVeicolo);
        }

    }
}
