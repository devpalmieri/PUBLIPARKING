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
    
    public partial class tab_dati_metrici_catastali
    {
        public tab_dati_metrici_catastali()
        {
            this.join_oggetto_dati_metrici = new HashSet<join_oggetto_dati_metrici>();
        }
    
        public int id_tab_dati_metrici_catastali { get; set; }
        public Nullable<int> id_categoria_catastale { get; set; }
        public Nullable<int> id_immobile_catastale { get; set; }
        public string sezione_urbana { get; set; }
        public string foglio { get; set; }
        public string numero { get; set; }
        public string particella { get; set; }
        public string subalterno { get; set; }
        public string num_protocollo_imm_non_accastato { get; set; }
        public Nullable<System.DateTime> data_protocollo_imm_non_accastato { get; set; }
        public Nullable<decimal> valore_rendita_provvisoria { get; set; }
        public string num_protocollo_ute_rendita_definitiva { get; set; }
        public Nullable<System.DateTime> data_protocollo_ute_rendita_definitiva { get; set; }
        public Nullable<System.DateTime> data_efficiacia_rendita_definitiva { get; set; }
        public Nullable<decimal> valore_rendita_definitiva { get; set; }
        public Nullable<int> id_tipologia_1 { get; set; }
        public Nullable<decimal> superfice_tipologia_1 { get; set; }
        public Nullable<int> id_tipologia_2 { get; set; }
        public Nullable<decimal> superfice_tipologia_2 { get; set; }
        public Nullable<int> id_tipologia_3 { get; set; }
        public Nullable<decimal> superfice_tipologia_3 { get; set; }
        public Nullable<int> id_tipologia_4 { get; set; }
        public Nullable<decimal> superfice_tipologia_4 { get; set; }
        public Nullable<int> id_tipologia_5 { get; set; }
        public Nullable<decimal> superfice_tipologia_5 { get; set; }
        public Nullable<int> id_tipologia_6 { get; set; }
        public Nullable<decimal> superfice_tipologia_6 { get; set; }
        public Nullable<int> id_tipologia_7 { get; set; }
        public Nullable<decimal> superfice_tipologia_7 { get; set; }
        public Nullable<int> id_tipologia_8 { get; set; }
        public Nullable<decimal> superfice_tipologia_8 { get; set; }
        public Nullable<int> id_tipologia_9 { get; set; }
        public Nullable<decimal> superfice_tipologia_9 { get; set; }
        public Nullable<int> id_tipologia_10 { get; set; }
        public Nullable<decimal> superfice_tipologia_10 { get; set; }
        public Nullable<decimal> superficie_min_tarsu { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public string descrizione_categoria_catastale { get; set; }
        public Nullable<decimal> superficie_catastale { get; set; }
        public string indirizzo_immobile_completo { get; set; }
        public string proprietario_1 { get; set; }
        public string cf_piva_1 { get; set; }
        public string proprietario_2 { get; set; }
        public string cf_piva_2 { get; set; }
        public string proprietario_3 { get; set; }
        public string cf_piva_3 { get; set; }
        public string proprietario_4 { get; set; }
        public string cf_piva_4 { get; set; }
        public string proprietario_1_nome { get; set; }
        public string proprietario_2_nome { get; set; }
        public string proprietario_3_nome { get; set; }
        public string proprietario_4_nome { get; set; }
        public string ctg { get; set; }
        public string indirizzo_frazione { get; set; }
        public Nullable<double> macroarea { get; set; }
        public string note { get; set; }
        public string amb_1 { get; set; }
        public string amb_2 { get; set; }
        public string amb_3 { get; set; }
        public string amb_4 { get; set; }
    
        ///<summary><para>Relazione: FK_tab_dati_metrici_catastali_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_dati_metrici_catastali_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_dati_metrici_catastali_tab_categorie_fabbricati</para> Chiave Origine: id_categoria_catastale</summary>
     public virtual tab_categorie_fabbricati tab_categorie_fabbricati { get; set; }
        ///<summary><para>Relazione: FK_join_oggetto_dati_metrici_tab_dati_metrici_catastali</para> Chiave Origine: id_dati_metrici_catastali</summary>
     public virtual ICollection<join_oggetto_dati_metrici> join_oggetto_dati_metrici { get; set; }
    }
}