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
    
    public partial class tab_rilevatore
    {
        public tab_rilevatore()
        {
            this.tab_rilevazione_acquedotto = new HashSet<tab_rilevazione_acquedotto>();
            this.tab_assegnazione_rilevazioni = new HashSet<tab_assegnazione_rilevazioni>();
            this.tab_rilevazione_acquedotto_smart = new HashSet<tab_rilevazione_acquedotto_smart>();
            this.tab_letture = new HashSet<tab_letture>();
        }
    
        public int id_rilevatore { get; set; }
        public string flag_interna_esterna { get; set; }
        public Nullable<int> id_personale { get; set; }
        public Nullable<int> id_ruolo_mansione { get; set; }
        public string flag_PF_PG { get; set; }
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
        public string cod_stato { get; set; }
        public string nome_utente { get; set; }
        public string password { get; set; }
    
        ///<summary><para>Relazione: FK_tab_rilevazione_acquedotto_tab_rilevatore</para> Chiave Origine: id_rilevatore</summary>
     public virtual ICollection<tab_rilevazione_acquedotto> tab_rilevazione_acquedotto { get; set; }
        ///<summary><para>Relazione: FK_tab_assegnazione_rilevazioni_tab_rilevatore</para> Chiave Origine: id_rilevatore</summary>
     public virtual ICollection<tab_assegnazione_rilevazioni> tab_assegnazione_rilevazioni { get; set; }
        ///<summary><para>Relazione: FK_tab_rilevazione_acquedotto_smart_tab_rilevatore</para> Chiave Origine: id_rilevatore</summary>
     public virtual ICollection<tab_rilevazione_acquedotto_smart> tab_rilevazione_acquedotto_smart { get; set; }
        ///<summary><para>Relazione: FK_tab_letture_tab_rilevatore</para> Chiave Origine: id_letturista</summary>
     public virtual ICollection<tab_letture> tab_letture { get; set; }
    }
}