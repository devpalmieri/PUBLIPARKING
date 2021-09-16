using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data;

namespace Publisoftware.Data.BD
{
    public class AnagraficaEnteBD : EntityBD<anagrafica_ente>
    {
        public const int ENTE_GENERICO_ID = 14;
        public const int ENTE_GENERICO_BACKOFFICE_ID = 15;

        public AnagraficaEnteBD()
        {

        }

        /// <summary>
        /// Elenco degli Enti che hanno valorizzato il campo nome_db
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns>La lista è ordinata per descrizione e tipo ente</returns>
        public static IQueryable<anagrafica_ente> GetList(dbEnte p_dbContext)
        {
            return GetListInternal(p_dbContext).Where(e => e.nome_db != null && e.user_name_db != null && e.password_db != null && e.indirizzo_ip_db != null  && e.flag_sosta == "0").OrderBy(o => o.descrizione_ente).ThenBy(a => a.id_tipo_ente).Select(c => c);
        }

        /// <summary>
        /// Elenco degli Enti che hanno valorizzato il campo nome_db ed hanno il flag_sosta alzato
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns>La lista è ordinata per descrizione e tipo ente</returns>
        public static IQueryable<anagrafica_ente> GetParkList(dbEnte p_dbContext)
        {
            return GetListInternal(p_dbContext).Where(e => e.nome_db != null && e.password_db != null && e.indirizzo_ip_db != null && e.flag_sosta == "1").OrderBy(o => o.descrizione_ente).ThenBy(a => a.id_tipo_ente).Select(c => c);
        }

        /// <summary>
        /// Ottiene l'elenco degli enti discriminando se la struttura è connessa all'ente generico, ad un altro ente o a nessun ente
        /// </summary>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_ente> GetListCheckEnteGenerico(int p_idStruttura, dbEnte p_dbContext)
        {
            if (AnagraficaStruttureAziendaliBD.IsRelatedToEnteGenerico(p_idStruttura, p_dbContext))
            {
                return Enumerable.Empty<anagrafica_ente>().AsQueryable();
            }
            else if (AnagraficaStruttureAziendaliBD.IsRelatedToEnti(p_idStruttura, p_dbContext))
            {
                return GetList(p_dbContext).Where(d => d.id_ente != ENTE_GENERICO_ID && d.id_ente != ENTE_GENERICO_BACKOFFICE_ID && !d.join_ente_strutture.Any(x => x.id_struttura_aziendale == p_idStruttura));
            }
            else
            {
                return GetList(p_dbContext);
            }
        }

        /// <summary>
        /// Elenco degli Enti che hanno valorizzato il campo nome_db
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns>La lista è ordinata per descrizione e tipo ente</returns>
        public static IQueryable<anagrafica_ente> GetListWithPortal(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(e => e.nome_db != null).OrderBy(o => o.descrizione_ente).ThenBy(a => a.id_tipo_ente).Select(c => c);
        }

        /// <summary>
        /// Restituisce un ente ricercandolo per codice
        /// </summary>
        /// <param name="p_codiceEnte">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_ente GetByCodice(string p_codiceEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(e => e.codice_ente != null && e.codice_ente.ToUpper() == p_codiceEnte.ToUpper()).SingleOrDefault();
        }

        /// <summary>
        /// Restituisce un ente ricercandolo per cod ente
        /// </summary>
        /// <param name="p_codEnte">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_ente GetByCodEnte(string p_codEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(e => e.nome_db != null && e.user_name_db != null && e.password_db != null && e.indirizzo_ip_db != null && e.flag_sosta == "0")
                                       .Where(e => e.cod_ente != null && e.cod_ente.ToUpper() == p_codEnte.ToUpper()).SingleOrDefault();
        }
        // <summary>
        /// Restituisce un ente ricercandolo per cod ente o descrizione
        /// </summary>
        /// <param name="p_codEnte">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_ente GetByCodEnteOrDescrizione(string p_value, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(e => e.nome_db != null && e.user_name_db != null && e.password_db != null && e.indirizzo_ip_db != null && e.flag_sosta == "0")
                                       .Where(e => e.cod_ente != null && (e.cod_ente.ToUpper().Contains( p_value .ToUpper()) || e.descrizione_ente.ToUpper().Contains(p_value.ToUpper()))
                                       ).FirstOrDefault();
        }

        /// <summary>
        /// Elenco degli Enti ai quali la struttura è collegata
        /// </summary>
        /// <param name="p_idStrutturaAziendale">ID Struttura su cui cercare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_ente> GetListByIdStrutturaAziendale(int p_idStrutturaAziendale, dbEnte p_dbContext)
        {
            return JoinEnteStruttureBD.GetList(p_dbContext).Where(d => d.id_struttura_aziendale == p_idStrutturaAziendale).Select(c => c.anagrafica_ente);
        }

        /// <summary>
        /// Restituisce la lista degli Enti su cui l'operatore/struttura possono lavorare
        /// </summary>
        /// <param name="p_idOperatore">ID Operatore ricercato</param>
        /// <param name="p_idStruttura">ID Struttura ricercata</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_ente> GetListByIdOperatoreIdStruttura(int p_idOperatore, int p_idStruttura, dbEnte p_dbContext)
        {
            IQueryable<anagrafica_ente> v_enteListAttivi = new List<anagrafica_ente>().AsQueryable();
            int idEnte = 0;

            // Verifica se la struttura è collegata almeno ad un ente
            if (JoinEnteStruttureBD.HasIdStruttura(p_idStruttura, p_dbContext))
            {
                // Prende il primo per verificare se la struttura è collegata all'ente generico
                idEnte = JoinEnteStruttureBD.GetListByIdStruttura(p_idStruttura, p_dbContext).FirstOrDefault().id_ente;
            }

            if (idEnte > 0)
            {
                if (idEnte == anagrafica_ente.ID_ENTE_GENERICO)
                {
                    // Se la struttura appartiene all'ente generico, restituisce la lista degli enti su cui la risorsa ha almeno un'abilitazione
                    v_enteListAttivi = TabAbilitazioneBD.GetListByIdRisorsaIdStrutturaEnteWithDB(p_idOperatore, p_idStruttura, p_dbContext, new String[] { "anagrafica_ente" })
                                                         .Select(e => e.anagrafica_ente).Distinct();
                }
                else
                {
                    // La struttura è collegata a degli enti specifici
                    v_enteListAttivi = TabAbilitazioneBD.GetListByIdRisorsaIdStrutturaEnteWithDB(p_idOperatore, p_idStruttura, p_dbContext, new String[] { "anagrafica_ente" })
                                                         .Select(d => d.anagrafica_ente).Distinct();
                }
            }

            return v_enteListAttivi;
        }


        public static anagrafica_ente GetEnteGenerico(dbEnte p_dbContext)
        {
            return AnagraficaEnteBD.GetList(p_dbContext).Where(e => e.codice_ente.Equals("GEN")).FirstOrDefault();
        }
    }
}
