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
    
    public partial class tab_tipo_nota
    {
        public tab_tipo_nota()
        {
            this.tab_note = new HashSet<tab_note>();
        }
    
        public int id_tab_tipo_nota { get; set; }
        public string codice { get; set; }
        public string descrizione { get; set; }
    
        ///<summary><para>Relazione: FK_tab_note_tab_tipo_nota</para> Chiave Origine: id_tab_tipo_nota</summary>
     public virtual ICollection<tab_note> tab_note { get; set; }
    }
}