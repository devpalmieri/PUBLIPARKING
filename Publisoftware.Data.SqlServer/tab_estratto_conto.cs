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
    
    public partial class tab_estratto_conto
    {
        public tab_estratto_conto()
        {
            this.tab_mov_pag_fatt_emissione = new HashSet<tab_mov_pag_fatt_emissione>();
            this.tab_mov_pag_ici = new HashSet<tab_mov_pag_ici>();
        }
    
        public int id_estr_conto { get; set; }
        public int id_cc_riscossione { get; set; }
        public decimal saldo_iniziale { get; set; }
        public decimal saldo_finale { get; set; }
        public int tot_mov_dare { get; set; }
        public int tot_mov_avere { get; set; }
        public string fl_quadrato { get; set; }
        public int mese { get; set; }
        public int anno { get; set; }
        public System.DateTime data_inizio { get; set; }
        public System.DateTime data_fine { get; set; }
        public string flag_on_off { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
    
        ///<summary><para>Relazione: FK_tab_estratto_conto_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_estratto_conto_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_mov_pag_fatt_emissione_tab_estratto_conto</para> Chiave Origine: id_tab_estrattoconto</summary>
     public virtual ICollection<tab_mov_pag_fatt_emissione> tab_mov_pag_fatt_emissione { get; set; }
        ///<summary><para>Relazione: FK_tab_mov_pag_ici_tab_estratto_conto</para> Chiave Origine: id_tab_estrattoconto</summary>
     public virtual ICollection<tab_mov_pag_ici> tab_mov_pag_ici { get; set; }
    }
}