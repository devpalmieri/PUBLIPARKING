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
    
    public partial class tab_cicli_ente_dettaglio
    {
        public tab_cicli_ente_dettaglio()
        {
            this.tab_pianificazione_cicli_dettaglio = new HashSet<tab_pianificazione_cicli_dettaglio>();
        }
    
        public int id_ciclo_ente_dettaglio { get; set; }
        public int id_ciclo_ente { get; set; }
        public Nullable<System.TimeSpan> durata { get; set; }
        public int id_ciclo_dettaglio { get; set; }
    
        ///<summary><para>Relazione: FK_tab_cicli_ente_dettaglio_tab_cicli_ente</para> Chiave Origine: id_ciclo_ente</summary>
     public virtual tab_cicli_ente tab_cicli_ente { get; set; }
        ///<summary><para>Relazione: FK_tab_cicli_ente_dettaglio_anagrafica_cicli_dettaglio</para> Chiave Origine: id_ciclo_dettaglio</summary>
     public virtual anagrafica_cicli_dettaglio anagrafica_cicli_dettaglio { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_dettaglio_tab_cicli_ente_dettaglio</para> Chiave Origine: id_ciclo_ente_dettaglio</summary>
     public virtual ICollection<tab_pianificazione_cicli_dettaglio> tab_pianificazione_cicli_dettaglio { get; set; }
    }
}