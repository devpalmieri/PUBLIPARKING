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
    
    public partial class join_lista_voci_contribuzione
    {
        public int id_join_lista_voci_contribuzione { get; set; }
        public int id_lista { get; set; }
        public int id_tipo_voce_contribuzione { get; set; }
        public string numero_accertamento_contabile { get; set; }
        public Nullable<System.DateTime> data_accertamento_contabile { get; set; }
    
        ///<summary><para>Relazione: FK_join_lista_voci_contribuzione_tab_liste</para> Chiave Origine: id_lista</summary>
     public virtual tab_liste tab_liste { get; set; }
        ///<summary><para>Relazione: FK_join_lista_voci_contribuzione_tab_tipo_voce_contribuzione</para> Chiave Origine: id_tipo_voce_contribuzione</summary>
     public virtual tab_tipo_voce_contribuzione tab_tipo_voce_contribuzione { get; set; }
    }
}
