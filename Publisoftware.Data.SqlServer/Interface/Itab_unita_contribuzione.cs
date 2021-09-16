using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public interface Itab_unita_contribuzione
    {
        int id_unita_contribuzione { get; set; }
        int id_ente { get; set; }
        //Nullable<int> id_ente_gestito { get; set; }
        //int id_entrata { get; set; }
        //int id_tipo_avv_pag_generato { get; set; }
        //Nullable<int> id_avv_pag_generato { get; set; }
        //Nullable<int> num_riga_avv_pag_generato { get; set; }
        //int id_anagrafica_voce_contribuzione { get; set; }
        //int id_tipo_voce_contribuzione { get; set; }
        //string flag_tipo_addebito { get; set; }
        //string anno_rif { get; set; }
        //Nullable<System.DateTime> periodo_rif_da { get; set; }
        //Nullable<System.DateTime> periodo_rif_a { get; set; }
        //Nullable<int> num_giorni_contribuzione { get; set; }
        //Nullable<System.DateTime> periodo_contribuzione_da { get; set; }
        //Nullable<System.DateTime> periodo_contribuzione_a { get; set; }
        //Nullable<decimal> id_contribuente { get; set; }
        //Nullable<decimal> id_oggetto { get; set; }
        //Nullable<decimal> id_oggetto_contribuzione { get; set; }
        //Nullable<int> id_fatt_consumi { get; set; }
        //Nullable<int> id_intervento { get; set; }
        //Nullable<int> id_avv_pag_collegato { get; set; }
        //Nullable<int> id_spesa { get; set; }
        //string flag_collegamento_unita_contribuzione { get; set; }
        //Nullable<int> id_unita_contribuzione_collegato { get; set; }
        //string um_unita { get; set; }
        //string flag_segno { get; set; }
        //Nullable<decimal> quantita_unita_contribuzione { get; set; }
        //Nullable<decimal> importo_unitario_contribuzione { get; set; }
        //Nullable<decimal> importo_unita_contribuzione { get; set; }
        //Nullable<decimal> importo_ridotto { get; set; }
        //Nullable<decimal> importo_tributo { get; set; }
        //Nullable<int> id_agevolazione1 { get; set; }
        //Nullable<int> durata_agevolazione1 { get; set; }
        //Nullable<decimal> imp_agevolazione1 { get; set; }
        //string cod_agevolazione1 { get; set; }
        //Nullable<int> id_agevolazione2 { get; set; }
        //Nullable<int> durata_agevolazione2 { get; set; }
        //Nullable<decimal> imp_agevolazione2 { get; set; }
        //string cod_agevolazione2 { get; set; }
        //Nullable<int> id_agevolazione3 { get; set; }
        //Nullable<int> durata_agevolazione3 { get; set; }
        //Nullable<decimal> imp_agevolazione3 { get; set; }
        //string cod_agevolazione3 { get; set; }
        //Nullable<int> id_agevolazione4 { get; set; }
        //Nullable<int> durata_agevolazione4 { get; set; }
        //Nullable<decimal> imp_agevolazione4 { get; set; }
        //string cod_agevolazione4 { get; set; }
        //Nullable<decimal> imponibile_unita_contribuzione { get; set; }
        //Nullable<decimal> aliquota_iva { get; set; }
        //Nullable<decimal> iva_unita_contribuzione { get; set; }
        //string flag_val { get; set; }
        //Nullable<int> id_stato { get; set; }
        //string cod_stato { get; set; }
        //System.DateTime data_stato { get; set; }
        //int id_struttura_stato { get; set; }
        //int id_risorsa_stato { get; set; }
        //string num_avv_pag { get; set; }
        //Nullable<int> anno_origine { get; set; }
        //string testo1 { get; set; }
        //string testo2 { get; set; }
    }

    public interface ISumImportoUnita
    {
        decimal? idContribuente { get; set; }
        decimal? idOggetto { get; set; }
        decimal SumImpTotEmesso { get; set; }
        int? NumGiorniContribuzione { get; set; }
        DateTime? dataInizioContribuzione { get; set; }
        DateTime? dataFineContribuzione { get; set; }
        string AnnoRif { get; set; }
    }

    public interface IUnitaAccertamento
    {
        decimal? idContribuente { get; set; }
        decimal? idOggetto { get; set; }
        int idEntrata { get; set; }
        string AnnoRif { get; set; }
        decimal? importoUnita { get; set; }
    }
}
