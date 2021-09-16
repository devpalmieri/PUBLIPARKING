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
    
    public partial class tab_f24_a1
    {
        public tab_f24_a1()
        {
            this.tab_f24_g2 = new HashSet<tab_f24_g2>();
            this.tab_f24_g3 = new HashSet<tab_f24_g3>();
            this.tab_f24_g4 = new HashSet<tab_f24_g4>();
            this.tab_f24_g5 = new HashSet<tab_f24_g5>();
            this.tab_f24_g9 = new HashSet<tab_f24_g9>();
            this.tab_f24_g1 = new HashSet<tab_f24_g1>();
        }
    
        public int id_a1 { get; set; }
        public string tipo_record { get; set; }
        public System.DateTime data_fornitura { get; set; }
        public int progressivo_fornitura { get; set; }
        public Nullable<int> numero_trasmissione { get; set; }
        public string codice_valuta { get; set; }
        public string codice_ente_comunale { get; set; }
        public Nullable<int> codice_intermediario { get; set; }
        public string identificativo_file { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public Nullable<int> id_struttura_stato { get; set; }
        public Nullable<int> id_risorsa_stato { get; set; }
    
        ///<summary><para>Relazione: FK_tab_f24_g2_tab_f24_a1</para> Chiave Origine: id_tab_f24_a1</summary>
     public virtual ICollection<tab_f24_g2> tab_f24_g2 { get; set; }
        ///<summary><para>Relazione: FK_tab_f24_g3_tab_f24_a1</para> Chiave Origine: id_tab_f24_a1</summary>
     public virtual ICollection<tab_f24_g3> tab_f24_g3 { get; set; }
        ///<summary><para>Relazione: FK_tab_f24_g4_tab_f24_a1</para> Chiave Origine: id_tab_f24_a1</summary>
     public virtual ICollection<tab_f24_g4> tab_f24_g4 { get; set; }
        ///<summary><para>Relazione: FK_tab_f24_g5_tab_f24_a1</para> Chiave Origine: id_tab_f24_a1</summary>
     public virtual ICollection<tab_f24_g5> tab_f24_g5 { get; set; }
        ///<summary><para>Relazione: FK_tab_f24_g9_tab_f24_a1</para> Chiave Origine: id_tab_f24_a1</summary>
     public virtual ICollection<tab_f24_g9> tab_f24_g9 { get; set; }
        ///<summary><para>Relazione: FK_tab_f24_g1_tab_f24_a1</para> Chiave Origine: id_tab_f24_a1</summary>
     public virtual ICollection<tab_f24_g1> tab_f24_g1 { get; set; }
    }
}