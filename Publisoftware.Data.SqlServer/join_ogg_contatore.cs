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
    
    public partial class join_ogg_contatore
    {
        public int id_join_oggetto_contatore { get; set; }
        public decimal id_oggetto { get; set; }
        public int id_contatore { get; set; }
    
        ///<summary><para>Relazione: FK_join_ogg_contatore_tab_oggetti</para> Chiave Origine: id_oggetto</summary>
     public virtual tab_oggetti tab_oggetti { get; set; }
        ///<summary><para>Relazione: FK_join_ogg_contatore_tab_contatore</para> Chiave Origine: id_contatore</summary>
     public virtual tab_contatore tab_contatore { get; set; }
    }
}
