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
    
    public partial class tab_co1151_rappresentanti_legali
    {
        public int id_record { get; set; }
        public Nullable<int> id_co1151 { get; set; }
        public string tipo_record { get; set; }
        public string dati_richiesta_ente { get; set; }
        public string codice_ritorno { get; set; }
        public string codice_fiscale_at { get; set; }
        public Nullable<int> numero_rappresentanti_legali { get; set; }
        public string codice_fiscale_rappresentante_legale { get; set; }
        public string codice_carica { get; set; }
        public Nullable<System.DateTime> data_decorrenza { get; set; }
        public Nullable<System.DateTime> data_fine_carica { get; set; }
        public string carattere_controllo { get; set; }
    
        ///<summary><para>Relazione: FK_tab_co1151_rappresentanti_legali_tab_co1151</para> Chiave Origine: id_co1151</summary>
     public virtual tab_co1151 tab_co1151 { get; set; }
    }
}
