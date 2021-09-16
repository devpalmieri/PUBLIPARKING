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
    
    public partial class tab_generale_udienze
    {
        public int id_tab_generale_udienze { get; set; }
        public int id_registro { get; set; }
        public Nullable<int> id_risorsa_assegnazione { get; set; }
        public Nullable<int> id_autorita_giudiziaria { get; set; }
        public Nullable<int> id_risorsa_delegata { get; set; }
        public Nullable<System.DateTime> data_delega { get; set; }
        public string numero_udienza { get; set; }
        public string sez_giudicante_udienza { get; set; }
        public string nominativo_giudice_udienza { get; set; }
        public Nullable<System.DateTime> data_udienza { get; set; }
        public Nullable<System.DateTime> data_rinvio_udienza { get; set; }
        public string flag_esito_udienza { get; set; }
        public string annotazioni_udienza { get; set; }
        public int id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public Nullable<System.DateTime> data_udienza_atto_citazione { get; set; }
    
        ///<summary><para>Relazione: FK_tab_generale_udienze_anagrafica_risorse</para> Chiave Origine: id_risorsa_assegnazione</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_generale_udienze_anagrafica_risorse1</para> Chiave Origine: id_risorsa_delegata</summary>
     public virtual anagrafica_risorse anagrafica_risorse1 { get; set; }
        ///<summary><para>Relazione: FK_tab_generale_udienze_tab_autorita_giudiziaria</para> Chiave Origine: id_autorita_giudiziaria</summary>
     public virtual tab_autorita_giudiziaria tab_autorita_giudiziaria { get; set; }
        ///<summary><para>Relazione: FK_tab_generale_udienze_tab_registro_assegnazione_pratiche</para> Chiave Origine: id_registro</summary>
     public virtual tab_registro_assegnazione_pratiche tab_registro_assegnazione_pratiche { get; set; }
    }
}
