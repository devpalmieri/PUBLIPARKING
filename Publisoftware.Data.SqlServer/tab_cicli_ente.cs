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
    
    public partial class tab_cicli_ente
    {
        public tab_cicli_ente()
        {
            this.tab_cicli_ente_dettaglio = new HashSet<tab_cicli_ente_dettaglio>();
            this.tab_pianificazione_cicli = new HashSet<tab_pianificazione_cicli>();
        }
    
        public int id_ciclo_ente { get; set; }
        public int id_ente { get; set; }
        public int id_ciclo { get; set; }
        public System.TimeSpan esecuzione_da { get; set; }
        public System.TimeSpan esecuzione_a { get; set; }
        public string descrizione_ciclo { get; set; }
    
        ///<summary><para>Relazione: FK_tab_cicli_ente_anagrafica_cicli</para> Chiave Origine: id_ciclo</summary>
     public virtual anagrafica_cicli anagrafica_cicli { get; set; }
        ///<summary><para>Relazione: FK_tab_cicli_ente_dettaglio_tab_cicli_ente</para> Chiave Origine: id_ciclo_ente</summary>
     public virtual ICollection<tab_cicli_ente_dettaglio> tab_cicli_ente_dettaglio { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_tab_cicli_ente</para> Chiave Origine: id_ciclo_ente</summary>
     public virtual ICollection<tab_pianificazione_cicli> tab_pianificazione_cicli { get; set; }
        ///<summary><para>Relazione: FK_tab_cicli_ente_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}