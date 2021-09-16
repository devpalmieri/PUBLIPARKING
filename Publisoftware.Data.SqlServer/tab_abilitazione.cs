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
    
    public partial class tab_abilitazione
    {
        public int id_tab_abilitazione { get; set; }
        public int id_risorsa { get; set; }
        public int id_struttura_aziendale { get; set; }
        public int id_ente { get; set; }
        public int id_tab_applicazioni { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public bool flag_abilitato { get; set; }
    
        ///<summary><para>Relazione: FK_tab_abilitazione_anagrafica_risorse</para> Chiave Origine: id_risorsa</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_abilitazione_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_aziendale</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_abilitazione_tab_applicazioni</para> Chiave Origine: id_tab_applicazioni</summary>
     public virtual tab_applicazioni tab_applicazioni { get; set; }
        ///<summary><para>Relazione: FK_tab_abilitazione_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}