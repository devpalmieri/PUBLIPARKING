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
    
    public partial class tab_rivestizione_anagrafica
    {
        public tab_rivestizione_anagrafica()
        {
            this.tab_richieste_rivestizione_liste = new HashSet<tab_richieste_rivestizione_liste>();
        }
    
        public int id_rivestizione_anagrafica { get; set; }
        public int id_ente { get; set; }
        public string tipo_rivestizione { get; set; }
        public string WS_user_identificazione_primo_livello { get; set; }
        public string WS_password_identificazione_primo_livello { get; set; }
        public string WS_uri_wsdl { get; set; }
        public string WS_user_id_accesso_utente { get; set; }
        public string WS_password_accesso_utente { get; set; }
        public string tipo_bd_esterna { get; set; }
        public string codice_servizio { get; set; }
        public string tipo_estrazione_dati { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risosrsa_stato { get; set; }
        public Nullable<int> id_risorsa_responsabile_richieste_interno { get; set; }
        public Nullable<int> id_risorsa_responsabile_richieste_ente { get; set; }
    
        ///<summary><para>Relazione: FK_tab_rivestizione_anagrafica_anagrafica_risorse</para> Chiave Origine: id_risorsa_responsabile_richieste_interno</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_rivestizione_anagrafica_anagrafica_risorse1</para> Chiave Origine: id_risorsa_responsabile_richieste_ente</summary>
     public virtual anagrafica_risorse anagrafica_risorse1 { get; set; }
        ///<summary><para>Relazione: FK_tab_richieste_rivestizione_liste_tab_rivestizione_anagrafica</para> Chiave Origine: id_rivestizione_anagrafica</summary>
     public virtual ICollection<tab_richieste_rivestizione_liste> tab_richieste_rivestizione_liste { get; set; }
        ///<summary><para>Relazione: FK_tab_rivestizione_anagrafica_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}