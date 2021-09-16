using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class AnagraficaEnteGestitoBD : EntityBD<anagrafica_ente_gestito>
    {
        public AnagraficaEnteGestitoBD()
        {

        }

        /// <summary>
        /// Ottiene la lista degli enti che possono essere collegati
        /// </summary>
        /// <param name="p_id_ente"></param>
        /// <param name="p_context"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_ente_gestito> GetListEntiCanBeLinkedWith(int p_id_ente, dbEnte p_context)
        {
            return GetList(p_context).Where(d => !d.join_ente_ente_gestito.Any(e => e.id_ente == p_id_ente));
        }

        /// <summary>
        /// Ottiene la lista degli enti collegati
        /// </summary>
        /// <param name="p_id_ente"></param>
        /// <param name="p_context"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_ente_gestito> GetListEntiAlreadyLinkedWith(int p_id_ente, dbEnte p_context)
        {
            return GetList(p_context).Where(d => d.join_ente_ente_gestito.Any(e => e.id_ente == p_id_ente));
        }


        /// <summary>
        /// Ottiene il record con ente e codice comune in input
        /// </summary>
        /// <param name="p_id_ente"></param>
        /// <param name="p_context"></param>
        /// <returns></returns>
        public static anagrafica_ente_gestito GetRecEnteByCodComune(int p_id_ente,int? p_codComune, dbEnte p_context)
        {
            return GetList(p_context).Where(d => !d.join_ente_ente_gestito.Any(e => e.id_ente == p_id_ente) && d.cod_comune==p_codComune).FirstOrDefault();
        }

        /// <summary>
        /// record ente gestito per id_ente_gestito
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_ente_gestito GetEnteGById(int p_idEnteGestito, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_ente == p_idEnteGestito).FirstOrDefault();
        }

        public static int GetNextIdEnte(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).AsNoTracking().Max(x => x.id_ente) + 1;
        }

        /*
        /// <summary>
        /// Ritorna il primo ente gestito dall'ente con id in ingresso, se non ci sono enti gestiti
        /// associati ritorna null
        /// </summary>
        /// <param name="id_ente">Identificativo dell'ente</param>
        /// <param name="ctx">Contesto</param>
        /// <returns></returns>
        public static anagrafica_ente_gestito GetFirstEnteGestitoByIdEnte(int id_ente, dbEnte ctx)
        {
            return (JoinEnteEnteGestitoBD.GetList(ctx)
                .Where(x => x.id_ente == id_ente)
                .OrderBy(x => x.id_ente_gestito)
                .FirstOrDefault())?.anagrafica_ente_gestito;
        }

        */
        public static async Task<anagrafica_ente_gestito> GetFirstEnteGestitoByIdEnteAsync(int id_ente, dbEnte ctx)
        {
            return (await JoinEnteEnteGestitoBD.GetList(ctx)
                .Where(x => x.id_ente == id_ente)
                .OrderBy(x => x.id_ente_gestito)
                .FirstOrDefaultAsync())?.anagrafica_ente_gestito;
        }

        public static IList<anagrafica_ente_gestito> GetListEnteGestitoByIdEnte(int id_ente, dbEnte ctx)
        {
            return JoinEnteEnteGestitoBD.GetList(ctx)
                .Where(x => x.id_ente == id_ente)
                .Include(x=>x.anagrafica_ente_gestito)
                .OrderBy(x => x.id_ente_gestito)
                .Select(x=>x.anagrafica_ente_gestito)
                .ToList();
        }

    }
}
