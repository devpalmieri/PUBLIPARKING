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
    
    public partial class join_documenti_fascicolo_avvpag_allegati
    {
        public int id_join_documenti_fascicolo_avvpag_allegati { get; set; }
        public int id_tab_documenti { get; set; }
        public int id_fascicolo_allegati_avvpag { get; set; }
    
        ///<summary><para>Relazione: FK_join_documenti_fascicolo_avvpag_allegati_tab_documenti</para> Chiave Origine: id_tab_documenti</summary>
     public virtual tab_documenti tab_documenti { get; set; }
        ///<summary><para>Relazione: FK_join_documenti_fascicolo_avvpag_allegati_tab_fascicolo_avvpag_allegati</para> Chiave Origine: id_fascicolo_allegati_avvpag</summary>
     public virtual tab_fascicolo_avvpag_allegati tab_fascicolo_avvpag_allegati { get; set; }
    }
}
