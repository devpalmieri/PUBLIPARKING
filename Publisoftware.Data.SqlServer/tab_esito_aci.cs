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
    
    public partial class tab_esito_aci
    {
        public tab_esito_aci()
        {
            this.tab_esito_aci_messaggi = new HashSet<tab_esito_aci_messaggi>();
        }
    
        public int id_tab_esito_aci { get; set; }
        public int id_join_tab_supervisione_profili { get; set; }
        public Nullable<int> identificativo_pratica_interno { get; set; }
        public string flag_presentazione { get; set; }
        public Nullable<System.DateTime> data_presentazione { get; set; }
        public string protocollo_aci_presentazione { get; set; }
        public Nullable<System.DateTime> data_protocollo_aci_presentazione { get; set; }
        public string codice_risposta_presentazione { get; set; }
        public string codice_errore_presentazione { get; set; }
        public string descrizione_presentazione { get; set; }
        public string response_presentazione { get; set; }
        public string flag_accettazione { get; set; }
        public Nullable<System.DateTime> data_accettazione { get; set; }
        public string codice_risposta_accettazione { get; set; }
        public string codice_errore_accettazione { get; set; }
        public string descrizione_accettazione { get; set; }
        public string response_accettazione { get; set; }
        public string cod_stato { get; set; }
        public string procedura { get; set; }
        public string procedura_return_code { get; set; }
        public string procedura_return_message { get; set; }
        public string procedura_codice_formalita { get; set; }
        public string procedura_tipo_provvedimento { get; set; }
        public string xml_req { get; set; }
        public string xml_resp { get; set; }
    
        ///<summary><para>Relazione: FK_tab_esito_aci_join_tab_supervisione_profili</para> Chiave Origine: id_join_tab_supervisione_profili</summary>
     public virtual join_tab_supervisione_profili join_tab_supervisione_profili { get; set; }
        ///<summary><para>Relazione: FK_tab_esito_aci_messaggi_tab_esito_aci</para> Chiave Origine: id_tab_esito_aci</summary>
     public virtual ICollection<tab_esito_aci_messaggi> tab_esito_aci_messaggi { get; set; }
    }
}