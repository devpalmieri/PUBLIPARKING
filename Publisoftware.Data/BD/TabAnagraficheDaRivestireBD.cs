using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabAnagraficheDaRivestireBD : EntityBD<tab_anagrafiche_da_rivestire>
    {
#if false
        public enum TipoRivestimento
        {
            Contribuenti,
            Referenti,
            Terzi
        }

        // Deve essere uguale a AndIsReferenteConditions
        public static readonly string REFERENTI_DA_RIVESTIRE_BATCH_QUERY_STD_WHERE =
            $" {nameof(tab_anagrafiche_da_rivestire.id_anag_contribuente_collegato)} IS NOT NULL " +
            $"AND {nameof(tab_anagrafiche_da_rivestire.flag_tipo_soggetto_debitore)} = '{tab_anagrafiche_da_rivestire.FLAG_TIPO_SOGGETTO_DEBITORE_REFERENTE}' " +
            $"AND ({nameof(tab_anagrafiche_da_rivestire.cod_fiscale)} IS NOT NULL OR {nameof(tab_anagrafiche_da_rivestire.cod_fiscale_pg)} IS NOT NULL)" +
            $"AND {nameof(tab_anagrafiche_da_rivestire.id_anagrafica_tipo_relazione_contr_coll)} IS NOT NULL ";


        // Deve essere uguale a AndIsTerzoConditions
        public static readonly string TERZI_DA_RIVESTIRE_BATCH_QUERY_STD_WHERE =
            $" {nameof(tab_anagrafiche_da_rivestire.flag_tipo_soggetto_debitore)} = '{tab_anagrafiche_da_rivestire.FLAG_TIPO_SOGGETTO_DEBITORE_TERZO}' " +
            $"AND ({nameof(tab_anagrafiche_da_rivestire.cod_fiscale)} IS NOT NULL OR {nameof(tab_anagrafiche_da_rivestire.cod_fiscale_pg)} IS NOT NULL) ";

        // Deve essere uguale a AndIsContribuenteConditions
        public static readonly string CONTRIBUENTI_DA_RIVESTIRE_BATCH_QUERY_STD_WHERE =
            $" {nameof(tab_anagrafiche_da_rivestire.flag_tipo_soggetto_debitore)} = '{tab_anagrafiche_da_rivestire.FLAG_TIPO_SOGGETTO_DEBITORE_TERZO}' " +
            $"AND ({nameof(tab_anagrafiche_da_rivestire.cod_fiscale)} IS NOT NULL OR {nameof(tab_anagrafiche_da_rivestire.cod_fiscale_pg)} IS NOT NULL) ";

        // Deve essere uguale a TAB_REFERENTI_DA_RIVESTIRE_BATCH_QUERY_STD_WHERE 
        public static IQueryable<tab_anagrafiche_da_rivestire> AndIsReferenteConditions(IQueryable<tab_anagrafiche_da_rivestire> query)
        {
            return query.Where(x =>
                x.id_anag_contribuente_collegato != null
                && x.flag_tipo_soggetto_debitore == tab_anagrafiche_da_rivestire.FLAG_TIPO_SOGGETTO_DEBITORE_REFERENTE
                && (x.cod_fiscale != null || x.cod_fiscale_pg!=null)
                && x.id_anagrafica_tipo_relazione_contr_coll != null);
        }

        // deve essere uguale a TAB_TERZI_DA_RIVESTIRE_BATCH_QUERY_STD_WHERE
        public static IQueryable<tab_anagrafiche_da_rivestire> AndIsTerzoConditions(IQueryable<tab_anagrafiche_da_rivestire> query)
        {
            return query.Where(x =>
                x.flag_tipo_soggetto_debitore == tab_anagrafiche_da_rivestire.FLAG_TIPO_SOGGETTO_DEBITORE_TERZO
                & (x.cod_fiscale != null || x.cod_fiscale_pg != null));
        }

        // deve essere uguale a TAB_CONTRIBUENTI_DA_RIVESTIRE_BATCH_QUERY_STD_WHERE
        public static IQueryable<tab_anagrafiche_da_rivestire> AndIsContribuenteConditions(IQueryable<tab_anagrafiche_da_rivestire> query)
        {
            return query.Where(x =>
                x.flag_tipo_soggetto_debitore == tab_anagrafiche_da_rivestire.FLAG_TIPO_SOGGETTO_DEBITORE_CONTRIBUENTE
                & (x.cod_fiscale != null || x.cod_fiscale_pg != null));
        }
#endif
        public static readonly string PERSONE_FISICHE_DA_RIVESTIRE_BATCH_QUERY_STD_WHERE =
            $" ({nameof(tab_anagrafiche_da_rivestire.cod_fiscale)} IS NOT NULL AND {nameof(tab_anagrafiche_da_rivestire.cod_fiscale_pg)} IS NULL) ";
        
        public static IQueryable<tab_anagrafiche_da_rivestire> AndIsPersonaFisicaConditions(IQueryable<tab_anagrafiche_da_rivestire> query)
        {
            return query.Where(x => x.cod_fiscale != null && x.cod_fiscale_pg == null);
        }

        public static readonly string PERSONE_GIURIDICHE_DA_RIVESTIRE_BATCH_QUERY_STD_WHERE =
            $" ({nameof(tab_anagrafiche_da_rivestire.cod_fiscale)} IS NOT NULL AND {nameof(tab_anagrafiche_da_rivestire.cod_fiscale_pg)} IS NULL) " +
            $"AND {nameof(tab_anagrafiche_da_rivestire.err_piva_no_cf)}<>1";

        public static IQueryable<tab_anagrafiche_da_rivestire> AndIsPersonaGiuridicaConditions(IQueryable<tab_anagrafiche_da_rivestire> query)
        {
            return query.Where(x => x.cod_fiscale == null && x.cod_fiscale_pg != null && x.err_piva_no_cf!=true);
        }
    } // class
}