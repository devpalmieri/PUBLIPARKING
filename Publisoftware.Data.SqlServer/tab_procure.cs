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
    
    public partial class tab_procure
    {
        public tab_procure()
        {
            this.join_risorse_ser_comuni = new HashSet<join_risorse_ser_comuni>();
            this.tab_doc_input = new HashSet<tab_doc_input>();
        }
    
        public int id_procura { get; set; }
        public int id_risorsa { get; set; }
        public string titolo_procuratore { get; set; }
        public string tipo_rapporto { get; set; }
        public string desc_tipo_procura { get; set; }
        public string pec_rif_procura { get; set; }
        public string rif_legislvativo_procura { get; set; }
        public string redattore_procura { get; set; }
        public string reportorio_raccolta { get; set; }
        public System.DateTime data_procura { get; set; }
        public string domicilio_elettivo_procuratore { get; set; }
        public Nullable<System.DateTime> data_inizio_validita { get; set; }
        public Nullable<System.DateTime> data_fine_validità { get; set; }
        public string cod_stato { get; set; }
        public string flag_tipo_procura { get; set; }
    
        ///<summary><para>Relazione: FK_tab_procure_anagrafica_risorse</para> Chiave Origine: id_risorsa</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_join_risorse_ser_comuni_tab_procure</para> Chiave Origine: id_procura</summary>
     public virtual ICollection<join_risorse_ser_comuni> join_risorse_ser_comuni { get; set; }
        ///<summary><para>Relazione: FK_tab_doc_input_tab_procure</para> Chiave Origine: id_procura</summary>
     public virtual ICollection<tab_doc_input> tab_doc_input { get; set; }
    }
}