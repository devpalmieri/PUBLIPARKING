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
    
    public partial class tab_f24_tipo_r3_regione
    {
        public int id_r3 { get; set; }
        public int id_aa { get; set; }
        public string tipo_record { get; set; }
        public string codice_regione { get; set; }
        public Nullable<System.DateTime> data_ripartizione { get; set; }
        public Nullable<int> progressivo_ripartizione { get; set; }
        public Nullable<System.DateTime> data_bonifico_principale { get; set; }
        public string codice_divisa { get; set; }
        public string tipo_imposta { get; set; }
        public Nullable<decimal> importo_accreditato { get; set; }
        public string sezione_cs { get; set; }
        public string numero_conto { get; set; }
        public Nullable<System.DateTime> data_finalizzazione { get; set; }
    
        ///<summary><para>Relazione: FK_tab_f24_tipo_r3_regione_tab_f24_tipo_aa_regione</para> Chiave Origine: id_aa</summary>
     public virtual tab_f24_tipo_aa_regione tab_f24_tipo_aa_regione { get; set; }
    }
}
