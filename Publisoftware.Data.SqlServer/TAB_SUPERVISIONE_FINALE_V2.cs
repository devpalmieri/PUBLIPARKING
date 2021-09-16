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
    
    public partial class TAB_SUPERVISIONE_FINALE_V2
    {
        public TAB_SUPERVISIONE_FINALE_V2()
        {
            this.join_ingfis_supervisione = new HashSet<join_ingfis_supervisione>();
            this.tab_avv_pag_fatt_emissione = new HashSet<tab_avv_pag_fatt_emissione>();
            this.join_tab_supervisione_profili = new HashSet<join_tab_supervisione_profili>();
            this.tab_avv_pag1 = new HashSet<tab_avv_pag>();
            this.tab_ispezioni_coattivo_new1 = new HashSet<tab_ispezioni_coattivo_new>();
            this.join_dichiarazione_terzo_contribuente = new HashSet<join_dichiarazione_terzo_contribuente>();
        }
    
        public int ID_TAB_SUPERVISIONE_FINALE { get; set; }
        public Nullable<int> ID_TAB_ISPEZIONE_COATTIVO { get; set; }
        public Nullable<int> ID_ENTE { get; set; }
        public Nullable<decimal> ID_CONTRIBUENTE { get; set; }
        public Nullable<int> ID_TIPO_AVVPAG_DA_EMETTERE { get; set; }
        public Nullable<int> ID_SUPERVISORE_FINALE { get; set; }
        public Nullable<System.DateTime> DATA_SUPERVISIONE_FINALE { get; set; }
        public Nullable<int> ID_TIPO_AVVPAG_EMESSO { get; set; }
        public Nullable<int> ID_AVVPAG_EMESSO { get; set; }
        public Nullable<int> ID_TAB_PROFILO_CONTRIBUENTE { get; set; }
        public Nullable<int> ID_STATO { get; set; }
        public string COD_STATO { get; set; }
        public int ID_RISORSA_STATO { get; set; }
        public int ID_STRUTTURA_STATO { get; set; }
        public System.DateTime DATA_STATO { get; set; }
        public Nullable<int> id_avvpag_preavviso_collegato { get; set; }
    
        ///<summary><para>Relazione: FK_join_ingfis_supervisione_TAB_SUPERVISIONE_FINALE_V2</para> Chiave Origine: id_tab_supervisione_finale</summary>
     public virtual ICollection<join_ingfis_supervisione> join_ingfis_supervisione { get; set; }
        ///<summary><para>Relazione: FK_TAB_SUPERVISIONE_FINALE_V2_tab_avv_pag</para> Chiave Origine: ID_AVVPAG_EMESSO</summary>
     public virtual tab_avv_pag tab_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_fatt_emissione_TAB_SUPERVISIONE_FINALE_V2</para> Chiave Origine: id_tab_supervisione_finale</summary>
     public virtual ICollection<tab_avv_pag_fatt_emissione> tab_avv_pag_fatt_emissione { get; set; }
        ///<summary><para>Relazione: FK_TAB_SUPERVISIONE_FINALE_V2_tab_ispezioni_coattivo_new</para> Chiave Origine: ID_TAB_ISPEZIONE_COATTIVO</summary>
     public virtual tab_ispezioni_coattivo_new tab_ispezioni_coattivo_new { get; set; }
        ///<summary><para>Relazione: FK_TAB_SUPERVISIONE_FINALE_V2_tab_profilo_contribuente_new</para> Chiave Origine: ID_TAB_PROFILO_CONTRIBUENTE</summary>
     public virtual tab_profilo_contribuente_new tab_profilo_contribuente_new { get; set; }
        ///<summary><para>Relazione: FK_TAB_SUPERVISIONE_FINALE_V2_anagrafica_tipo_avv_pag_da_emettere</para> Chiave Origine: ID_TIPO_AVVPAG_DA_EMETTERE</summary>
     public virtual anagrafica_tipo_avv_pag anagrafica_tipo_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_TAB_SUPERVISIONE_FINALE_V2_anagrafica_tipo_avv_pag_emesso</para> Chiave Origine: ID_TIPO_AVVPAG_EMESSO</summary>
     public virtual anagrafica_tipo_avv_pag anagrafica_tipo_avv_pag1 { get; set; }
        ///<summary><para>Relazione: FK_join_tab_supervisione_profili_TAB_SUPERVISIONE_FINALE_V2</para> Chiave Origine: id_tab_supervisione_finale</summary>
     public virtual ICollection<join_tab_supervisione_profili> join_tab_supervisione_profili { get; set; }
        ///<summary><para>Relazione: FK_tab_avv_pag_TAB_SUPERVISIONE_FINALE_V2</para> Chiave Origine: id_tab_supervisione_finale</summary>
     public virtual ICollection<tab_avv_pag> tab_avv_pag1 { get; set; }
        ///<summary><para>Relazione: FK_TAB_SUPERVISIONE_FINALE_V2_tab_avv_pag1</para> Chiave Origine: id_avvpag_preavviso_collegato</summary>
     public virtual tab_avv_pag tab_avv_pag2 { get; set; }
        ///<summary><para>Relazione: FK_tab_ispezioni_coattivo_new_TAB_SUPERVISIONE_FINALE_V2</para> Chiave Origine: id_tab_supervisione</summary>
     public virtual ICollection<tab_ispezioni_coattivo_new> tab_ispezioni_coattivo_new1 { get; set; }
        ///<summary><para>Relazione: FK_TAB_SUPERVISIONE_FINALE_V2_tab_contribuente</para> Chiave Origine: ID_CONTRIBUENTE</summary>
     public virtual tab_contribuente tab_contribuente { get; set; }
        ///<summary><para>Relazione: FK_join_dichiarazione_terzo_contribuente_TAB_SUPERVISIONE_FINALE_V2</para> Chiave Origine: id_tab_supervizione_finale</summary>
     public virtual ICollection<join_dichiarazione_terzo_contribuente> join_dichiarazione_terzo_contribuente { get; set; }
        ///<summary><para>Relazione: FK_TAB_SUPERVISIONE_FINALE_V2_anagrafica_ente</para> Chiave Origine: ID_ENTE</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}