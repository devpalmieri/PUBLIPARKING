//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Publisoftware.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tab_anomalie_spedizioni
    {
        public int id_tab_anomalie_spedizioni { get; set; }
        public int id_ente { get; set; }
        public string nome_file { get; set; }
        public string tipo_record_controllato { get; set; }
        public string tipo_importazione { get; set; }
        public Nullable<int> num_riga_record_file_input { get; set; }
        public int id_anagrafica_controllo { get; set; }
        public string contribuente_codfis_piva { get; set; }
        public Nullable<int> id_avv_pag { get; set; }
        public string identificativo_avvpag { get; set; }
        public string barcode_sped_not { get; set; }
        public Nullable<decimal> importo_avv_pag { get; set; }
        public string cod_stato_avv_pag { get; set; }
        public string descrizione_errore_estesa { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public string flag_on_off { get; set; }
    
        ///<summary><para>Relazione: FK_tab_anomalie_spedizioni_anagrafica_controlli_qualita_emissione_avvpag</para> Chiave Origine: id_anagrafica_controllo</summary>
     public virtual anagrafica_controlli_qualita_emissione_avvpag anagrafica_controlli_qualita_emissione_avvpag { get; set; }
        ///<summary><para>Relazione: FK_tab_anomalie_spedizioni_tab_avv_pag</para> Chiave Origine: id_avv_pag</summary>
     public virtual tab_avv_pag tab_avv_pag { get; set; }
    }
}