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
    
    public partial class tab_assegnazione_istanze
    {
        public int id_assegnazione { get; set; }
        public int id_risorsa { get; set; }
        public int num_tot_istanze_assegnate { get; set; }
        public int num_tot_istanze_lavorate { get; set; }
        public int num_tot_istanze_in_lavorazione { get; set; }
        public System.DateTime data_ultimo_aggiornamento { get; set; }
    
        ///<summary><para>Relazione: FK_tab_assegnazione_istanze_anagrafica_risorse</para> Chiave Origine: id_risorsa</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
    }
}
