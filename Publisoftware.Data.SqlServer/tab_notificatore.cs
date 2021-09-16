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
    
    public partial class tab_notificatore
    {
        public tab_notificatore()
        {
            this.tab_sped_not_fatt_emissione = new HashSet<tab_sped_not_fatt_emissione>();
            this.tab_sped_not = new HashSet<tab_sped_not>();
            this.anagrafica_tipo_spedizione_notifica = new HashSet<anagrafica_tipo_spedizione_notifica>();
        }
    
        public int id_notificatore { get; set; }
        public int id_ente { get; set; }
        public string flag_risorsa_interna_esterna { get; set; }
        public Nullable<int> id_personale { get; set; }
        public Nullable<int> id_ruolo_mansione { get; set; }
        public string flag_persona_fisica_giuridica { get; set; }
        public string cognome { get; set; }
        public string nome { get; set; }
        public string rag_sociale { get; set; }
        public string indirizzo { get; set; }
        public string tel_cellulare { get; set; }
        public string tel_casa { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> data_nascita { get; set; }
        public string cod_fiscale { get; set; }
        public string p_iva { get; set; }
        public string fonte { get; set; }
    
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_tab_notificatore</para> Chiave Origine: id_notificatore</summary>
     public virtual ICollection<tab_sped_not_fatt_emissione> tab_sped_not_fatt_emissione { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_tab_notificatore</para> Chiave Origine: id_notificatore</summary>
     public virtual ICollection<tab_sped_not> tab_sped_not { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_tipo_spedizione_notifica_tab_notificatore</para> Chiave Origine: id_notificatore</summary>
     public virtual ICollection<anagrafica_tipo_spedizione_notifica> anagrafica_tipo_spedizione_notifica { get; set; }
        ///<summary><para>Relazione: FK_tab_notificatore_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}
