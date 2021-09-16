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
    
    public partial class tab_unita_contribuzione_fatt_rettifica
    {
        public int id_unita_contribuzione { get; set; }
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public int id_entrata { get; set; }
        public int id_tipo_avv_pag_generato { get; set; }
        public int id_avv_pag_generato { get; set; }
        public int num_riga_avv_pag_generato { get; set; }
        public int id_anagrafica_voce_contribuzione { get; set; }
        public int id_tipo_voce_contribuzione { get; set; }
        public string flag_tipo_addebito { get; set; }
        public string anno_rif { get; set; }
        public System.DateTime periodo_rif_da { get; set; }
        public System.DateTime periodo_rif_a { get; set; }
        public Nullable<int> num_giorni_contribuzione { get; set; }
        public System.DateTime periodo_contribuzione_da { get; set; }
        public System.DateTime periodo_contribuzione_a { get; set; }
        public decimal id_contribuente { get; set; }
        public Nullable<decimal> id_oggetto { get; set; }
        public Nullable<decimal> id_oggetto_contribuzione { get; set; }
        public Nullable<int> id_fatt_consumi { get; set; }
        public Nullable<int> id_intervento { get; set; }
        public Nullable<int> id_avv_pag_collegato { get; set; }
        public Nullable<int> id_spesa { get; set; }
        public string flag_collegamento_unita_contribuzione { get; set; }
        public Nullable<int> id_unita_contribuzione_collegato { get; set; }
        public string um_unita { get; set; }
        public string flag_segno { get; set; }
        public decimal quantita_unita_contribuzione { get; set; }
        public decimal importo_unitario_contribuzione { get; set; }
        public decimal importo_unita_contribuzione { get; set; }
        public Nullable<decimal> importo_ridotto { get; set; }
        public Nullable<decimal> importo_tributo { get; set; }
        public Nullable<int> id_agevolazione1 { get; set; }
        public Nullable<decimal> imp_agevolazione1 { get; set; }
        public Nullable<int> durata_agevolazione1 { get; set; }
        public string cod_agevolazione1 { get; set; }
        public Nullable<int> id_agevolazione2 { get; set; }
        public Nullable<decimal> imp_agevolazione2 { get; set; }
        public Nullable<int> durata_agevolazione2 { get; set; }
        public string cod_agevolazione2 { get; set; }
        public Nullable<int> id_agevolazione3 { get; set; }
        public Nullable<decimal> imp_agevolazione3 { get; set; }
        public Nullable<int> durata_agevolazione3 { get; set; }
        public string cod_agevolazione3 { get; set; }
        public Nullable<int> id_agevolazione4 { get; set; }
        public Nullable<decimal> imp_agevolazione4 { get; set; }
        public Nullable<int> durata_agevolazione4 { get; set; }
        public string cod_agevolazione4 { get; set; }
        public decimal imponibile_unita_contribuzione { get; set; }
        public decimal aliquota_iva { get; set; }
        public decimal iva_unita_contribuzione { get; set; }
        public string flag_val { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public string num_avv_pag { get; set; }
    
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_anagrafica_entrate</para> Chiave Origine: id_entrata</summary>
     public virtual anagrafica_entrate anagrafica_entrate { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_anagrafica_voci_contribuzione</para> Chiave Origine: id_anagrafica_voce_contribuzione</summary>
     public virtual anagrafica_voci_contribuzione anagrafica_voci_contribuzione { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_tab_avv_pag_fatt_rettifica</para> Chiave Origine: id_avv_pag_generato</summary>
     public virtual tab_avv_pag_fatt_rettifica tab_avv_pag_fatt_rettifica { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_tab_oggetti_contribuzione</para> Chiave Origine: id_oggetto_contribuzione</summary>
     public virtual tab_oggetti_contribuzione tab_oggetti_contribuzione { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_anagrafica_ente_gestito</para> Chiave Origine: id_ente_gestito</summary>
     public virtual anagrafica_ente_gestito anagrafica_ente_gestito { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_tab_oggetti</para> Chiave Origine: id_oggetto</summary>
     public virtual tab_oggetti tab_oggetti { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_anagrafica_tipo_avv_pag</para> Chiave Origine: id_tipo_avv_pag_generato</summary>
     public virtual anagrafica_tipo_avv_pag anagrafica_tipo_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_tab_contribuente</para> Chiave Origine: id_contribuente</summary>
     public virtual tab_contribuente tab_contribuente { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_tab_agevolazioni</para> Chiave Origine: id_agevolazione1</summary>
     public virtual tab_agevolazioni tab_agevolazioni { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_tab_agevolazioni1</para> Chiave Origine: id_agevolazione2</summary>
     public virtual tab_agevolazioni tab_agevolazioni1 { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_tab_agevolazioni2</para> Chiave Origine: id_agevolazione3</summary>
     public virtual tab_agevolazioni tab_agevolazioni2 { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_tab_agevolazioni3</para> Chiave Origine: id_agevolazione4</summary>
     public virtual tab_agevolazioni tab_agevolazioni3 { get; set; }
        ///<summary><para>Relazione: FK_tab_unita_contribuzione_fatt_rettifica_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}
