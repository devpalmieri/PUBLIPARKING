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
    
    public partial class tab_rata_avv_pag
    {
        public tab_rata_avv_pag()
        {
            this.join_rate_avvpag_movpag = new HashSet<join_rate_avvpag_movpag>();
            this.tab_boll_pag = new HashSet<tab_boll_pag>();
            this.join_tab_carrello_tab_rate = new HashSet<join_tab_carrello_tab_rate>();
        }
    
        public int id_rata_avv_pag { get; set; }
        public int id_tab_avv_pag { get; set; }
        public int num_rata { get; set; }
        public string bar_code { get; set; }
        public Nullable<System.DateTime> dt_scadenza_rata { get; set; }
        public decimal imp_tot_rata { get; set; }
        public Nullable<decimal> imp_spese_notifica { get; set; }
        public decimal imp_pagato { get; set; }
        public string flag_pagamento { get; set; }
        public Nullable<System.DateTime> dt_pagamento { get; set; }
        public Nullable<int> id_stato { get; set; }
        public string cod_stato { get; set; }
        public Nullable<System.DateTime> data_stato { get; set; }
        public Nullable<int> id_struttura_stato { get; set; }
        public Nullable<int> id_risorsa_stato { get; set; }
        public Nullable<decimal> imp_spese_coattive { get; set; }
        public Nullable<int> id_cc_riscossione { get; set; }
        public string Iuv_identificativo_pagamento { get; set; }
        public string codice_pagamento_pagopa { get; set; }
        public string codice_cbill { get; set; }
        public string codice_tassonomia_pagopa { get; set; }
        public Nullable<int> giorni_scadenza_da { get; set; }
        public Nullable<int> giorni_scadenza_a { get; set; }
    
        ///<summary><para>Relazione: FK_tab_rata_avv_pag_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_rata_avv_pag_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_join_rate_avvpag_movpag_tab_rata_avv_pag</para> Chiave Origine: id_rata</summary>
     public virtual ICollection<join_rate_avvpag_movpag> join_rate_avvpag_movpag { get; set; }
        ///<summary><para>Relazione: FK_tab_rata_avv_pag_tab_avv_pag</para> Chiave Origine: id_tab_avv_pag</summary>
     public virtual tab_avv_pag tab_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_boll_pag_tab_rata_avv_pag</para> Chiave Origine: id_rata_avv_pag</summary>
     public virtual ICollection<tab_boll_pag> tab_boll_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_rata_avv_pag_tab_cc_riscossione</para> Chiave Origine: id_cc_riscossione</summary>
     public virtual tab_cc_riscossione tab_cc_riscossione { get; set; }
        ///<summary><para>Relazione: FK_join_tab_carrello_tab_rate_tab_rata_avv_pag</para> Chiave Origine: id_rata</summary>
     public virtual ICollection<join_tab_carrello_tab_rate> join_tab_carrello_tab_rate { get; set; }
    }
}