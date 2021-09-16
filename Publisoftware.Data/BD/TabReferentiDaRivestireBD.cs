#if false
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    // NON CANCELLARE QUESTO FILEs
    [Obsolete("Usare TabAnagraficheDaRivestireBD", true)]
    public class TabReferentiDaRivestireBD : EntityBD<tab_referenti_da_rivestire>
    {
        public enum TipoRivestimento
        {
            Referenti,
            Terzi
        }

        public static readonly string TAB_REFERENTI_DA_RIVESTIRE_BATCH_QUERY_STD_WHERE = 
            $"{nameof(tab_referenti_da_rivestire.id_anag_contribuente)} IS NOT NULL " +
            $"AND {nameof(tab_referenti_da_rivestire.flag_tipo_soggetto_debitore)} = '{tab_referenti_da_rivestire.FLAG_TIPO_SOGGETTO_DEBITORE_REFERENTE}' " +
            $"AND {nameof(tab_referenti_da_rivestire.cod_fiscale)} IS NOT NULL " +
            $"AND {nameof(tab_referenti_da_rivestire.id_anagrafica_tipo_relazione)} IS NOT NULL ";
        
        public static readonly string TAB_TERZI_DA_RIVESTIRE_BATCH_QUERY_STD_WHERE = 
            $"{nameof(tab_referenti_da_rivestire.id_anag_contribuente)} IS NULL " +
            $"AND {nameof(tab_referenti_da_rivestire.flag_tipo_soggetto_debitore)} = '{tab_referenti_da_rivestire.FLAG_TIPO_SOGGETTO_DEBITORE_TERZO}' " +
            $"AND {nameof(tab_referenti_da_rivestire.p_iva)} IS NOT NULL ";

        public static IQueryable<tab_referenti_da_rivestire> AndIsReferenteConditions(IQueryable<tab_referenti_da_rivestire> query)
        {
            return query.Where(x =>
                x.id_anag_contribuente != null
                && x.flag_tipo_soggetto_debitore == tab_referenti_da_rivestire.FLAG_TIPO_SOGGETTO_DEBITORE_REFERENTE
                && x.cod_fiscale != null
                && x.id_anagrafica_tipo_relazione != null);
        }

        public static IQueryable<tab_referenti_da_rivestire> AndIsTerzoConditions(IQueryable<tab_referenti_da_rivestire> query)
        {
            return query.Where(x =>
                x.id_anag_contribuente == null
                && x.flag_tipo_soggetto_debitore == tab_referenti_da_rivestire.FLAG_TIPO_SOGGETTO_DEBITORE_TERZO
                && x.p_iva != null);
        }
    }
}
#endif
