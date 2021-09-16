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
    
    public partial class tab_locazione_immobili
    {
        public int id_locazione_immobile { get; set; }
        public int id_locazione_contratto { get; set; }
        public Nullable<System.DateTime> data_file { get; set; }
        public string ufficio { get; set; }
        public Nullable<int> anno { get; set; }
        public string serie { get; set; }
        public Nullable<int> numero_reg { get; set; }
        public Nullable<int> sottonumero_reg { get; set; }
        public Nullable<int> progr_negozio { get; set; }
        public Nullable<int> progr_immobile { get; set; }
        public string in_accatastamento { get; set; }
        public string tipo_catasto { get; set; }
        public string fl_intero_porzione { get; set; }
        public string codice_catastale { get; set; }
        public string sez_urbana_comune_catastale { get; set; }
        public string foglio { get; set; }
        public string particella_numeratore { get; set; }
        public string particella_denominatore { get; set; }
        public string subalterno { get; set; }
        public string indirizzo { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
    
        ///<summary><para>Relazione: FK_tab_locazione_immobili_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_locazione_immobili_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_locazione_immobili_tab_locazione_contratto</para> Chiave Origine: id_locazione_contratto</summary>
     public virtual tab_locazione_contratto tab_locazione_contratto { get; set; }
    }
}
