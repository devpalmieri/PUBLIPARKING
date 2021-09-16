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
    
    public partial class tab_esecuzione_applicazioni
    {
        public int id_esecuzione_applicazioni { get; set; }
        public int id_pianificazione_ciclo { get; set; }
        public int id_pianificazione_ciclo_dettaglio { get; set; }
        public int id_ente { get; set; }
        public int priorita { get; set; }
        public Nullable<System.DateTime> inizio_esecuzione { get; set; }
        public Nullable<System.DateTime> fine_esecuzione { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public string cod_stato { get; set; }
        public string file_name { get; set; }
        public string file_folder { get; set; }
        public string file_params { get; set; }
        public string cod_stato_prec { get; set; }
        public Nullable<System.DateTime> inizio_esecuzione_prec { get; set; }
        public Nullable<System.DateTime> fine_esecuzione_prec { get; set; }
        public Nullable<int> id_struttura_stato_prec { get; set; }
        public Nullable<int> id_risorsa_stato_prec { get; set; }
        public string cron_cfg { get; set; }
    
        ///<summary><para>Relazione: FK_tab_esecuzione_applicazioni_anagrafica_cod_stato_base</para> Chiave Origine: cod_stato</summary>
     public virtual anagrafica_cod_stato_base anagrafica_cod_stato_base { get; set; }
        ///<summary><para>Relazione: FK_tab_esecuzione_applicazioni_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_esecuzione_applicazioni_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_esecuzione_applicazioni_tab_pianificazione_cicli</para> Chiave Origine: id_pianificazione_ciclo</summary>
     public virtual tab_pianificazione_cicli tab_pianificazione_cicli { get; set; }
        ///<summary><para>Relazione: FK_tab_esecuzione_applicazioni_tab_pianificazione_cicli_dettaglio</para> Chiave Origine: id_pianificazione_ciclo_dettaglio</summary>
     public virtual tab_pianificazione_cicli_dettaglio tab_pianificazione_cicli_dettaglio { get; set; }
        ///<summary><para>Relazione: FK_tab_esecuzione_applicazioni_anagrafica_risorse1</para> Chiave Origine: id_risorsa_stato_prec</summary>
     public virtual anagrafica_risorse anagrafica_risorse1 { get; set; }
        ///<summary><para>Relazione: FK_tab_esecuzione_applicazioni_anagrafica_strutture_aziendali_prec</para> Chiave Origine: id_struttura_stato_prec</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali1 { get; set; }
        ///<summary><para>Relazione: FK_tab_esecuzione_applicazioni_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}