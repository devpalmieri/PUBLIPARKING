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
    
    public partial class tab_f24_g4
    {
        public int id_g4 { get; set; }
        public int id_tab_f24_a1 { get; set; }
        public string tipo_record { get; set; }
        public System.DateTime data_fornitura { get; set; }
        public int progressivo_fornitura { get; set; }
        public Nullable<System.DateTime> data_ripartizione { get; set; }
        public Nullable<int> progressivo_ripartizione { get; set; }
        public Nullable<System.DateTime> data_bonifico { get; set; }
        public string codice_ente_comunale { get; set; }
        public string codice_valuta { get; set; }
        public Nullable<decimal> importo_anticipazione { get; set; }
        public string tipo_imposta { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public Nullable<int> id_struttura_stato { get; set; }
        public Nullable<int> id_risorsa_stato { get; set; }
    
        ///<summary><para>Relazione: FK_tab_f24_g4_tab_f24_a1</para> Chiave Origine: id_tab_f24_a1</summary>
     public virtual tab_f24_a1 tab_f24_a1 { get; set; }
    }
}
