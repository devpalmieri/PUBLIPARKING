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
    
    public partial class join_file
    {
        public join_file()
        {
            this.join_contenitori_join_file = new HashSet<join_contenitori_join_file>();
        }
    
        public int id_join_file { get; set; }
        public int id_entrata { get; set; }
        public int id_riferimento { get; set; }
        public string nome_file { get; set; }
        public string estensione_file { get; set; }
        public Nullable<int> id_tipo_file { get; set; }
        public string cod_tipo_file { get; set; }
        public int id_tipo_record { get; set; }
        public string cod_tipo_record { get; set; }
        public System.DateTime data_creazione_file { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura { get; set; }
        public int id_risorsa_stato { get; set; }
    
        ///<summary><para>Relazione: FK_join_file_anagrafica_entrate</para> Chiave Origine: id_entrata</summary>
     public virtual anagrafica_entrate anagrafica_entrate { get; set; }
        ///<summary><para>Relazione: FK_join_file_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_join_file_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_join_file_anagrafica_tipo_record</para> Chiave Origine: id_tipo_record</summary>
     public virtual anagrafica_tipo_record anagrafica_tipo_record { get; set; }
        ///<summary><para>Relazione: FK_join_contenitori_join_file_join_file</para> Chiave Origine: id_join_file</summary>
     public virtual ICollection<join_contenitori_join_file> join_contenitori_join_file { get; set; }
    }
}