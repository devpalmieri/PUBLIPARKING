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
    
    public partial class join_oggetto_dati_metrici
    {
        public join_oggetto_dati_metrici()
        {
            this.join_oggetto_dati_metrici1 = new HashSet<join_oggetto_dati_metrici>();
        }
    
        public int id_join_oggetto_catasto { get; set; }
        public Nullable<int> id_denuncia { get; set; }
        public Nullable<decimal> id_oggetto { get; set; }
        public Nullable<int> id_dati_metrici_dichiarati { get; set; }
        public Nullable<int> id_dati_metrici_catastali { get; set; }
        public Nullable<int> percentuale { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public Nullable<decimal> mq_occupazione_dichiarati { get; set; }
        public Nullable<decimal> mq_occupazione_catastali_data_denuncia { get; set; }
        public Nullable<decimal> mq_occupazione_non_tassabili { get; set; }
        public string flag_errore_catasto { get; set; }
        public Nullable<decimal> id_contribuente { get; set; }
        public Nullable<int> id_join_oggetto_dati_metrici_old { get; set; }
        public string flag_immobile_non_accatastato { get; set; }
        public string annotazioni { get; set; }
    
        ///<summary><para>Relazione: fk_A9B0B256_3AB0_4869_B07C_179A8DDEAF3B</para> Chiave Origine: id_contribuente</summary>
     public virtual tab_contribuente tab_contribuente { get; set; }
        ///<summary><para>Relazione: FK_D00175AC_245C_4CA9_8E54_1CD405312729</para> Chiave Origine: id_join_oggetto_dati_metrici_old</summary>
     public virtual ICollection<join_oggetto_dati_metrici> join_oggetto_dati_metrici1 { get; set; }
        ///<summary><para>Relazione: FK_D00175AC_245C_4CA9_8E54_1CD405312729</para> Chiave Origine: id_join_oggetto_dati_metrici_old</summary>
     public virtual join_oggetto_dati_metrici join_oggetto_dati_metrici2 { get; set; }
        ///<summary><para>Relazione: FK_join_oggetto_dati_metrici_tab_dati_metrici_catastali</para> Chiave Origine: id_dati_metrici_catastali</summary>
     public virtual tab_dati_metrici_catastali tab_dati_metrici_catastali { get; set; }
        ///<summary><para>Relazione: FK_join_oggetto_dati_metrici_tab_dati_metrici_dichiarati</para> Chiave Origine: id_dati_metrici_dichiarati</summary>
     public virtual tab_dati_metrici_dichiarati tab_dati_metrici_dichiarati { get; set; }
        ///<summary><para>Relazione: FK_join_oggetto_dati_metrici_tab_oggetti</para> Chiave Origine: id_oggetto</summary>
     public virtual tab_oggetti tab_oggetti { get; set; }
    }
}
