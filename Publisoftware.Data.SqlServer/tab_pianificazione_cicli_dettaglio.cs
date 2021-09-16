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
    
    public partial class tab_pianificazione_cicli_dettaglio
    {
        public tab_pianificazione_cicli_dettaglio()
        {
            this.tab_esecuzione_applicazioni = new HashSet<tab_esecuzione_applicazioni>();
        }
    
        public int id_pianificazione_ciclo_dettaglio { get; set; }
        public int id_pianificazione_ciclo { get; set; }
        public int id_tab_applicazioni { get; set; }
        public int sequenza { get; set; }
        public string parametri_lancio { get; set; }
        public System.DateTime data_scadenza { get; set; }
        public Nullable<System.TimeSpan> previsione_durata { get; set; }
        public Nullable<System.TimeSpan> durata_esecuzione { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public string cod_stato { get; set; }
        public int id_ente { get; set; }
        public int id_ciclo_ente_dettaglio { get; set; }
        public Nullable<int> id_lista { get; set; }
    
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_dettaglio_anagrafica_cod_stato_base</para> Chiave Origine: cod_stato</summary>
     public virtual anagrafica_cod_stato_base anagrafica_cod_stato_base { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_dettaglio_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_dettaglio_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_dettaglio_tab_pianificazione_cicli</para> Chiave Origine: id_pianificazione_ciclo</summary>
     public virtual tab_pianificazione_cicli tab_pianificazione_cicli { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_dettaglio_tab_cicli_ente_dettaglio</para> Chiave Origine: id_ciclo_ente_dettaglio</summary>
     public virtual tab_cicli_ente_dettaglio tab_cicli_ente_dettaglio { get; set; }
        ///<summary><para>Relazione: FK_tab_esecuzione_applicazioni_tab_pianificazione_cicli_dettaglio</para> Chiave Origine: id_pianificazione_ciclo_dettaglio</summary>
     public virtual ICollection<tab_esecuzione_applicazioni> tab_esecuzione_applicazioni { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_dettaglio_tab_applicazioni</para> Chiave Origine: id_tab_applicazioni</summary>
     public virtual tab_applicazioni tab_applicazioni { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_dettaglio_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}