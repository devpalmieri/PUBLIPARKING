using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaStruttureAziendaliBD : EntityBD<anagrafica_strutture_aziendali>
    {
        public AnagraficaStruttureAziendaliBD()
        {

        }

        /// <summary>
        /// Restituisce la lista delle strutture che hanno il responsabile indicato
        /// </summary>
        /// <param name="p_idResponsabile">ID Responsabile ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_strutture_aziendali> GetListByIdResponsabile(int p_idResponsabile, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(s => s.id_risorsa == p_idResponsabile).OrderBy(o => o.descr_struttura);
        }

        /// <summary>
        /// Restituisce la struttura corrispondente al codice
        /// </summary>
        /// <param name="p_codStruttura">Codice struttura ricercata</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_strutture_aziendali GetByCodStruttura(string p_codStruttura, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(s => s.codice_struttura_aziendale.ToUpper() == p_codStruttura.ToUpper()).SingleOrDefault();
        }

        /// <summary>
        /// Verifica se la struttura è connessa all'ente generico
        /// </summary>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool IsRelatedToEnteGenerico(int p_idStruttura, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_struttura_aziendale == p_idStruttura).Any(d => d.join_ente_strutture.FirstOrDefault().id_ente == AnagraficaEnteBD.ENTE_GENERICO_ID || d.join_ente_strutture.FirstOrDefault().id_ente == AnagraficaEnteBD.ENTE_GENERICO_BACKOFFICE_ID);
        }

        /// <summary>
        /// Verifica se la struttura è connessa a qualche ente (escluso l'ente generico)
        /// </summary>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool IsRelatedToEnti(int p_idStruttura, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_struttura_aziendale == p_idStruttura).Any(d => d.join_ente_strutture.FirstOrDefault().id_ente != AnagraficaEnteBD.ENTE_GENERICO_ID && d.join_ente_strutture.FirstOrDefault().id_ente != AnagraficaEnteBD.ENTE_GENERICO_BACKOFFICE_ID && d.join_ente_strutture.Count > 0);
        }
    }
}
