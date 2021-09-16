using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class AnagraficaEnteBD : EntityBD<anagrafica_ente>
    {
        public AnagraficaEnteBD()
        {

        }
        ///// <summary>
        ///// Elenco di tutti gli Enti
        ///// </summary>
        ///// <param name="p_dbContext"></param>
        ///// <returns>La lista è ordinata per descrizione e tipo ente</returns>
        //public static IQueryable<anagrafica_ente> GetList(DbParkContext p_dbContext)
        //{
        //    return p_dbContext.anagrafica_ente.Where(e => e.nome_db != null && e.password_db != null && e.indirizzo_ip_db != null).OrderBy(o => o.descrizione_ente).ThenBy(a => a.id_tipo_ente).Select(c => c);

        //}
        /// <summary>
        /// Restituisce l'ente in base all'id
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_id"></param>
        /// <returns></returns>
        //public static anagrafica_ente GetById(DbParkContext p_dbContext, int p_id)
        //{
        //    return GetEntityById(p_dbContext, p_id);

        //}
        /// <summary>
        /// Elenco degli Enti che hanno valorizzato il campo nome_db ed hanno il flag_sosta alzato
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns>La lista è ordinata per descrizione e tipo ente</returns>
        public static IQueryable<anagrafica_ente> GetParkList(DbParkContext p_dbContext)
        {
            return p_dbContext.anagrafica_ente.Where(e => e.nome_db != null && e.password_db != null && e.indirizzo_ip_db != null && e.flag_sosta == "1").OrderBy(o => o.descrizione_ente).ThenBy(a => a.id_tipo_ente).Select(c => c);

        }
    }
}
