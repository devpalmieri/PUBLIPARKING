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
    
    public partial class tab_veicoli
    {
        public tab_veicoli()
        {
            this.tab_profilo_contribuente_new = new HashSet<tab_profilo_contribuente_new>();
            this.tab_veicoli_vincoli = new HashSet<tab_veicoli_vincoli>();
        }
    
        public int id_veicolo { get; set; }
        public System.DateTime data_interrogazione { get; set; }
        public string targa { get; set; }
        public string cf_piva_proprietario { get; set; }
        public Nullable<int> tipo_veicolo { get; set; }
        public string telaio { get; set; }
        public string classe { get; set; }
        public string marca { get; set; }
        public string modello { get; set; }
        public Nullable<int> id_modello { get; set; }
        public string alimentazione { get; set; }
        public string catalitica { get; set; }
        public string cilindrata { get; set; }
        public Nullable<decimal> kw { get; set; }
        public Nullable<decimal> hp { get; set; }
        public Nullable<int> anno_costruzione { get; set; }
        public string carrozzeria { get; set; }
        public string specialita { get; set; }
        public string uso { get; set; }
        public Nullable<int> posti { get; set; }
        public Nullable<decimal> portata { get; set; }
        public Nullable<decimal> tara { get; set; }
        public Nullable<decimal> peso_complessivo { get; set; }
        public Nullable<int> assi { get; set; }
        public string fuoristrada { get; set; }
        public string diesel_eco { get; set; }
        public Nullable<decimal> importo_valore_veicolo { get; set; }
        public Nullable<System.DateTime> data_prima_immatricolazione { get; set; }
        public Nullable<System.DateTime> data_rilascio_carta { get; set; }
        public Nullable<System.DateTime> data_prima_iscrizione { get; set; }
        public Nullable<System.DateTime> data_atto_proprieta { get; set; }
        public Nullable<System.DateTime> data_registrazione_intestazione { get; set; }
        public Nullable<System.DateTime> data_scadenza_locazione { get; set; }
        public string ultima_formalita { get; set; }
        public Nullable<System.DateTime> data_ultima_formalita { get; set; }
        public Nullable<System.DateTime> data_perdita_possesso_radiazione { get; set; }
        public string cod_causale_perdita_possesso_radiazione { get; set; }
        public string fonte { get; set; }
        public Nullable<System.DateTime> data_fine_validita { get; set; }
        public int id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public string flag_uso_strumentale { get; set; }
    
        ///<summary><para>Relazione: FK_tab_veicoli_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_veicoli_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_veicoli_anagrafica_tipo_veicolo</para> Chiave Origine: tipo_veicolo</summary>
     public virtual anagrafica_tipo_veicolo anagrafica_tipo_veicolo { get; set; }
        ///<summary><para>Relazione: FK_tab_veicoli_tab_modello_veicolo</para> Chiave Origine: id_modello</summary>
     public virtual tab_modello_veicolo tab_modello_veicolo { get; set; }
        ///<summary><para>Relazione: FK_tab_profilo_contribuente_new_tab_veicoli</para> Chiave Origine: id_veicolo</summary>
     public virtual ICollection<tab_profilo_contribuente_new> tab_profilo_contribuente_new { get; set; }
        ///<summary><para>Relazione: FK_tab_veicoli_vincoli_tab_veicoli</para> Chiave Origine: id_veicolo</summary>
     public virtual ICollection<tab_veicoli_vincoli> tab_veicoli_vincoli { get; set; }
    }
}