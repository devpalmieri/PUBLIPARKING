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
    
    public partial class tab_pianificazione_cicli_input_importazione_file
    {
        public int id_pianificazione_ciclo_input_importazione_file { get; set; }
        public int id_pianificazione_ciclo { get; set; }
        public string path_f24 { get; set; }
    
        ///<summary><para>Relazione: FK_tab_pianificazione_cicli_input_importazione_file_tab_pianificazione_cicli</para> Chiave Origine: id_pianificazione_ciclo</summary>
     public virtual tab_pianificazione_cicli tab_pianificazione_cicli { get; set; }
    }
}
