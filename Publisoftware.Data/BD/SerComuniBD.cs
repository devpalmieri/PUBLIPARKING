using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class SerComuniBD : EntityCacheBD<ser_comuni>
    {
        public SerComuniBD()
        {

        }

        public static IQueryable<ser_comuni> GetListAttivi(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(a => a.f_comune_sto == 0);
        }

        public static IQueryable<ser_comuni> GetListStoricizzati(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(a => a.f_comune_sto == 1);
        }

        /// <summary>
        /// Ottiene la lista filtrata per parola chiave
        /// </summary>
        /// <param name="p_text"></param>
        /// <param name="p_dbContext"></param>
        /// <param name="p_includeEntities"></param>
        /// <returns></returns>
        public static IQueryable<ser_comuni> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListAttivi(p_dbContext, p_includeEntities).Where(a => a.des_comune.Contains(p_text) || a.cap_comune.Contains(p_text) || a.ser_province.des_provincia.Contains(p_text) || a.ser_regioni.des_regione.Contains(p_text))
                                                          .OrderBy(o => o.des_comune);
        }

        /// <summary>
        /// Restituisce un comune a partire dal codice amministrativo
        /// </summary>
        /// <param name="p_codiceAmministrativo">Codice Amministraivo ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static ser_comuni GetByCodiceAmministrativo(string p_codiceAmministrativo, dbEnte p_dbContext)
        {
            return GetListAttivi(p_dbContext).Where(c => c.cod_catasto.Equals(p_codiceAmministrativo)).SingleOrDefault();
        }

        /// <summary>
        /// Restituisce un comune a partire dal codice amministrativo
        /// </summary>
        /// <param name="p_codiceAmministrativo">Codice Amministraivo ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        [Obsolete("Non usare, puo dare errore con i comuni storici - usare GetByCodiceAmministrativoNew")]
        public static ser_comuni GetByCodiceAmministrativo(string p_codiceAmministrativo, dbEnte p_dbContext, bool bIncludeSerProvince)
        {
            var qry = GetListAttivi(p_dbContext).Where(c => c.cod_catasto.Equals(p_codiceAmministrativo));
            if (bIncludeSerProvince)
            {
                qry = qry.Include(x => x.ser_province);
            }
            return qry.SingleOrDefault();
        }


        /// <summary>
        /// Restituisce un comune a partire dal codice amministrativo
        /// </summary>
        /// <param name="p_codiceAmministrativo">Codice Amministraivo ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static ser_comuni GetByCodiceAmministrativoNew(string p_codiceAmministrativo, dbEnte p_dbContext, bool bIncludeSerProvince)
        {
            var qry = GetListAttivi(p_dbContext).Where(c => c.cod_catasto.Equals(p_codiceAmministrativo));
            if (bIncludeSerProvince)
            {
                qry = qry.Include(x => x.ser_province);
            }
            var sList = qry.ToList(); // qry.OrderBy(x=> x.cod_comune).ToList();
            return sList.FirstOrDefault(x => x.f_comune_sto == 0) ?? sList.FirstOrDefault(x => x.f_comune_sto == 1);
        }

        /// <summary>
        /// Restituisce un comune a partire dal codice amministrativo
        /// </summary>
        /// <param name="p_codiceAmministrativo">Codice Amministraivo ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IList<ser_comuni> GetByCodiceAmministrativoStoricizzati(dbEnte p_dbContext, string p_codiceAmministrativo, bool bIncludeSerProvince)
        {
            var qry = GetListStoricizzati(p_dbContext).Where(c => c.cod_catasto.Equals(p_codiceAmministrativo));
            if (bIncludeSerProvince)
            {
                qry = qry.Include(x => x.ser_province);
            }
            // L'ultimo deve essere il primo, che se son ripetuti prenderemo l'ultimo (cioè il primo della lista)
            return qry.OrderByDescending(x=>x.cod_comune).ToList();
        }

        /// <summary>
        /// Restituisce l'elenco dei comuni appartenenti alla provincia indicata dal Codice
        /// </summary>
        /// <param name="p_codProvincia">Codice Provincia di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<ser_comuni> GetByCodProvincia(int p_codProvincia, dbEnte p_dbContext)
        {
            return GetListAttivi(p_dbContext).Where(c => c.cod_provincia == p_codProvincia).OrderBy(o => o.des_comune);
        }

        /// <summary>
        /// Restituisce il codice catastale del comune corrispondente all'ID indicato
        /// </summary>
        /// <param name="p_id">ID Comune di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static string GetCodiceCatastoById(Int32 p_id, dbEnte p_dbContext)
        {
            if (GetListAttivi(p_dbContext).Any(d => d.cod_comune == p_id))
            {
                return GetListAttivi(p_dbContext).Where(d => d.cod_comune == p_id).SingleOrDefault().cod_catasto;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Restituisce il comune con il codice ISTAT indicato
        /// </summary>
        /// <param name="p_codIstat">Codice ISTAT di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static ser_comuni GetByCodIstat(string p_codIstat, dbEnte p_dbContext)
        {
            return GetListAttivi(p_dbContext).Where(c => c.cod_istat == p_codIstat).FirstOrDefault();
        }

        /// <summary>
        /// Restituisce il comune con il codice Comune indicato
        /// </summary>
        /// <param name="p_codComune">Codice Comune di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static ser_comuni GetByCodComune(string p_codComune, dbEnte p_dbContext)
        {
            int v_codice = Int32.Parse(p_codComune);

            return GetListAttivi(p_dbContext).Where(c => c.cod_comune == v_codice).FirstOrDefault();
        }

        public static ser_comuni GetByCodComune(int v_codice, dbEnte p_dbContext)
        {
            return GetListAttivi(p_dbContext).Where(c => c.cod_comune == v_codice).FirstOrDefault();
        }

        public static ser_comuni GetByCodCatasto(string p_codCatasto, dbEnte p_dbContext)
        {
            string v_codice = p_codCatasto;

            return GetListAttivi(p_dbContext).Where(c => c.cod_catasto == v_codice).FirstOrDefault();
        }
        /// <summary>
        /// Restituisce il comune con il codice Comune indicato ricercando anche tra gli storicizzati
        /// </summary>
        /// <param name="p_codComune">Codice Comune di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static ser_comuni GetByCodComuneAll(string p_codComune, dbEnte p_dbContext)
        {
            int v_codice = Int32.Parse(p_codComune);

            return GetList(p_dbContext).Where(c => c.cod_comune == v_codice).FirstOrDefault();
        }

        /// <summary>
        /// Controlla se il comune è presente tra quelli validi
        /// </summary>
        /// <param name="p_codComune"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool isComuneValido(int p_codCitta, dbEnte p_dbContext)
        {
            return GetListAttivi(p_dbContext).ToList().Exists(d => d.cod_comune == p_codCitta);
        }
        /// <summary>
        /// Restituisce la sigla provincia del comune corrispondente all'ID indicato
        /// </summary>
        /// <param name="p_id">ID Comune di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static string GetSiglaProvinciaById(Int32 p_id, dbEnte p_dbContext)
        {
            if (GetListAttivi(p_dbContext).Any(d => d.cod_comune == p_id))
            {
                return GetListAttivi(p_dbContext).Where(d => d.cod_comune == p_id).SingleOrDefault().ser_province.sig_provincia;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
