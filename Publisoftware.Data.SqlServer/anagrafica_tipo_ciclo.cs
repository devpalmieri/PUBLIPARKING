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
    
    public partial class anagrafica_tipo_ciclo
    {
        public anagrafica_tipo_ciclo()
        {
            this.anagrafica_cicli = new HashSet<anagrafica_cicli>();
            this.tab_pianificazione_cicli = new HashSet<tab_pianificazione_cicli>();
            this.tab_applicazioni = new HashSet<tab_applicazioni>();
            this.tab_pianificazione_cicli_input_emissione = new HashSet<tab_pianificazione_cicli_input_emissione>();
        }
    
        public int id_tipo_ciclo { get; set; }
        public string descr_tipociclo { get; set; }
    
        ///<summary><para>Relazione: FK_anagrafica_cicli_anagrafica_tipo_ciclo</para> Chiave Origine: id_tipo_ciclo</summary>
     public virtual ICollection<anagrafica_cicli> anagrafica_cicli { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_anagrafica_tipo_ciclo</para> Chiave Origine: id_tipo_ciclo</summary>
     public virtual ICollection<tab_pianificazione_cicli> tab_pianificazione_cicli { get; set; }
        ///<summary><para>Relazione: FK_tab_applicazioni_anagrafica_tipo_ciclo</para> Chiave Origine: id_tipo_ciclo</summary>
     public virtual ICollection<tab_applicazioni> tab_applicazioni { get; set; }
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_input_emissione_anagrafica_tipo_ciclo</para> Chiave Origine: id_tipo_ciclo</summary>
     public virtual ICollection<tab_pianificazione_cicli_input_emissione> tab_pianificazione_cicli_input_emissione { get; set; }
    }
}