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
    
    public partial class tab_cc_tesoreria
    {
        public tab_cc_tesoreria()
        {
            this.tab_riversamenti_contrattuali = new HashSet<tab_riversamenti_contrattuali>();
        }
    
        public int ID_TAB_CC_TESORERIA { get; set; }
        public Nullable<int> ID_ENTE_RIVERSAMENTO { get; set; }
        public Nullable<int> id_tipo_cc { get; set; }
        public string intestazione_cc { get; set; }
        public string intestazione_cc1_bollettino { get; set; }
        public string intestazione_cc2_bollettino { get; set; }
        public string ABI_CC { get; set; }
        public string CAB_CC { get; set; }
        public string num_cc { get; set; }
        public string aut_cc { get; set; }
        public string IBAN { get; set; }
        public Nullable<System.DateTime> data_apertura_cc { get; set; }
        public Nullable<System.DateTime> data_chiusura_cc { get; set; }
        public string tipo_riversamento { get; set; }
        public Nullable<System.DateTime> data_ultimo_riversamento { get; set; }
    
        ///<summary><para>Relazione: FK_tab_riversamenti_contrattuali_tab_cc_tesoreria</para> Chiave Origine: id_cc_tesoreria</summary>
     public virtual ICollection<tab_riversamenti_contrattuali> tab_riversamenti_contrattuali { get; set; }
    }
}
