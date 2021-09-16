using Publisoftware.Data.POCOLight;
using Publisoftware.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class VerificaStatoIspezioniCoattivoBD : EntityBD<join_tab_ispezioni_coattivo_tipo_ispezione>
    {
        #region Costructor
        public VerificaStatoIspezioniCoattivoBD()
        {

        }
        #endregion Costructor

        #region Public Methods
        /// <summary>
        /// Restituisce un elenco con lo stato delle ispezioni del coattivo
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static List<Dettaglio_Verifica_Stato_Ispezioni_Coattivo_Light> GetIspezioniCoattivo(dbEnte p_dbContext)
        {
            var list = (from j in p_dbContext.join_tab_ispezioni_coattivo_tipo_ispezione
                        join i in p_dbContext.tab_ispezioni_coattivo_new on j.id_tab_ispezione_coattivo equals i.id_tab_ispezione_coattivo
                        join p in p_dbContext.tab_tipo_ispezione on j.id_tab_tipo_ispezione equals p.id_tab_tipo_ispezione
                        where j.cod_stato == tab_ispezioni_coattivo_new.VAL_VAL & i.cod_stato == tab_ispezioni_coattivo_new.VAL_VAL //&& j.flag_on_off == "1" && i.flag_on_off == "1"
                        select new { p.sigla_tipo_ispezione, j.id_tab_tipo_ispezione, p.descrizione, j.flag_esito_ispezione } into x
                        group x by new { x.sigla_tipo_ispezione, x.id_tab_tipo_ispezione, x.descrizione, x.flag_esito_ispezione } into g
                        orderby g.Key.sigla_tipo_ispezione, g.Key.id_tab_tipo_ispezione, g.Key.descrizione, g.Key.flag_esito_ispezione
                        select new Dettaglio_Verifica_Stato_Ispezioni_Coattivo_Light()
                        {
                            Riga = "  ",
                            Sigla = g.Key.sigla_tipo_ispezione.Trim(),
                            Id_Tipo_Ispezione = g.Key.id_tab_tipo_ispezione,
                            descrizione = g.Key.descrizione,
                            Esito_Ispezione = g.Key.flag_esito_ispezione == "0" ? "DA_CHIUDERE"
                                                 : g.Key.flag_esito_ispezione == "1" ? "CHIUSA_POS"
                                                 : g.Key.flag_esito_ispezione == "2" ? "CHIUSA_NEG"
                                                 : "",

                            Numero = g.Select(x => x.id_tab_tipo_ispezione).Count()
                        }).ToList();
            return list;
        }
        
        //public static List<sp_statistiche_ispezioni_Result> GetListForIspezioniCoattivo(dbEnte p_dbContext)
        //{
        //    var tempList = p_dbContext.sp_statistiche_ispezioni().ToList();
        //    if (tempList != null)
        //    {
        //        var results = (from c in tempList
        //                       select new sp_statistiche_ispezioni_Result()
        //                       {
        //                           Riga = c.Riga,
        //                           Sigla = c.Sigla.Trim(),
        //                           Id_Tipo_Ispezione = c.Id_Tipo_Ispezione,
        //                           descrizione = c.descrizione,
        //                           Esito_Ispezione = c.Esito_Ispezione == "0" ? "Da Chiudere"
        //                                                    : c.Esito_Ispezione == "1" ? "Chiusa con Esito Positivo"
        //                                                    : c.Esito_Ispezione == "2" ? "Chiusa con Esito Negativo"
        //                                                    : "",
        //                           Numero = c.Numero.HasValue ? c.Numero.Value : 0
        //                       }

        //                 ).ToList();
        //        return results;

        //    }
        //    return null;
        //}

        #endregion Public Methods
    }
}
