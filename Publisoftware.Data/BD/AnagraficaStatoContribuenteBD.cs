using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaStatoContribuenteBD : EntityBD<anagrafica_stato_contribuente>
    {
        public AnagraficaStatoContribuenteBD()
        {

        }

        /// <summary>
        /// Restituisce lo stato corrispondente al codice ed al tipo persona
        /// </summary>
        /// <param name="p_codice">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        //public static anagrafica_stato_contribuente GetByCodiceTipoPersona(string p_codice, int p_tipoContribuente, dbEnte p_dbContext)
        //{
        //    return GetList(p_dbContext).Where(sc => sc.cod_stato_contribuente.ToUpper() == p_codice.ToUpper() && (sc.flag_fisica_giuridica == p_tipoContribuente.ToString() || sc.flag_fisica_giuridica == "0"))
        //                               .SingleOrDefault();
        //}

        /// <summary>
        /// Filtra per Stato
        /// </summary>
        /// <param name="p_idStato">p_idStato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_stato_contribuente GetByIdStato(int p_idStato, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_stato_contribuente == p_idStato).SingleOrDefault();
        }
    }
}
