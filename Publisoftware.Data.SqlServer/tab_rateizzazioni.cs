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
    
    public partial class tab_rateizzazioni
    {
        public tab_rateizzazioni()
        {
            this.join_tab_avv_pag_tab_doc_input = new HashSet<join_tab_avv_pag_tab_doc_input>();
        }
    
        public int id_rateizzazioni { get; set; }
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public Nullable<int> id_tipo_avvpag { get; set; }
        public Nullable<int> id_tipo_rateizzazione { get; set; }
        public string cod_tipo_rateizzazione { get; set; }
        public string desc_tipo_rateizzazione { get; set; }
        public Nullable<int> numero_rate_min { get; set; }
        public int numero_rate_max { get; set; }
        public int periodicita_consentita { get; set; }
        public string desc_periodicita_consentita { get; set; }
        public decimal importo_min_rata { get; set; }
        public Nullable<decimal> importo_max_rata { get; set; }
        public decimal importo_min_da_pagare { get; set; }
        public Nullable<decimal> importo_max_da_pagare { get; set; }
        public Nullable<decimal> importo_min_reddito { get; set; }
        public Nullable<decimal> importo_max_reddito { get; set; }
        public Nullable<int> id_entrata { get; set; }
        public Nullable<int> id_tipo_servizio { get; set; }
        public string tipo_contribuente { get; set; }
        public Nullable<int> id_delibera_determina { get; set; }
        public Nullable<System.DateTime> data_inizio_validita_rateizzazione { get; set; }
        public Nullable<System.DateTime> data_fine_validita_rateizzazione { get; set; }
        public Nullable<decimal> percentuale_incremento_rate { get; set; }
        public Nullable<int> periodicita_incremento_rate { get; set; }
        public Nullable<int> numero_rate_min_interessi { get; set; }
        public Nullable<int> id_anagrafica_causale { get; set; }
        public Nullable<int> id_tipo_servizio_atto_rateizzato { get; set; }
        public string flag_fideiussione { get; set; }
    
        ///<summary><para>Relazione: FK_tab_rateizzazioni_anagrafica_entrate</para> Chiave Origine: id_entrata</summary>
     public virtual anagrafica_entrate anagrafica_entrate { get; set; }
        ///<summary><para>Relazione: FK_tab_rateizzazioni_anagrafica_tipo_avv_pag</para> Chiave Origine: id_tipo_avvpag</summary>
     public virtual anagrafica_tipo_avv_pag anagrafica_tipo_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_rateizzazioni_anagrafica_tipo_servizi</para> Chiave Origine: id_tipo_servizio</summary>
     public virtual anagrafica_tipo_servizi anagrafica_tipo_servizi { get; set; }
        ///<summary><para>Relazione: FK_tab_rateizzazioni_tab_delibere_determine_tariffarie</para> Chiave Origine: id_delibera_determina</summary>
     public virtual tab_delibere_determine_tariffarie tab_delibere_determine_tariffarie { get; set; }
        ///<summary><para>Relazione: FK_tab_rateizzazioni_anagrafica_causale</para> Chiave Origine: id_anagrafica_causale</summary>
     public virtual anagrafica_causale anagrafica_causale { get; set; }
        ///<summary><para>Relazione: FK_tab_rateizzazioni_anagrafica_tipo_servizi1</para> Chiave Origine: id_tipo_servizio_atto_rateizzato</summary>
     public virtual anagrafica_tipo_servizi anagrafica_tipo_servizi1 { get; set; }
        ///<summary><para>Relazione: FK_join_tab_avv_pag_tab_doc_input_tab_rateizzazioni</para> Chiave Origine: id_rateizzazione</summary>
     public virtual ICollection<join_tab_avv_pag_tab_doc_input> join_tab_avv_pag_tab_doc_input { get; set; }
        ///<summary><para>Relazione: FK_tab_rateizzazioni_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
        ///<summary><para>Relazione: FK_tab_rateizzazioni_anagrafica_ente1</para> Chiave Origine: id_ente_gestito</summary>
     public virtual anagrafica_ente anagrafica_ente1 { get; set; }
    }
}