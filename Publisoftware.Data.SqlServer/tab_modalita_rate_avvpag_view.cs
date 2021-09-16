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
    
    public partial class tab_modalita_rate_avvpag_view
    {
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public int id_tipo_avv_pag { get; set; }
        public Nullable<int> modalita_sped_not_fuori_comune_PF { get; set; }
        public Nullable<int> modalita_sped_not_fuori_comune_PG { get; set; }
        public Nullable<int> modalita_sped_not_nel_comune_PF { get; set; }
        public Nullable<int> modalita_sped_not_nel_comune_PG { get; set; }
        public decimal importo_minimo_emissione_avvpag { get; set; }
        public decimal importo_minimo_avvpag_collegati { get; set; }
        public int GG_minimi_notifica_avvpag_collegati { get; set; }
        public int numero_rate { get; set; }
        public int periodicita_rate { get; set; }
        public bool flag_rate_importo_uguale { get; set; }
        public bool flag_spese_sped_not_prima_rata { get; set; }
        public bool flag_spese_coattive_prima_rata { get; set; }
        public Nullable<int> GG_definitivita_avviso { get; set; }
        public Nullable<int> GG_scadenza_pagamento { get; set; }
        public Nullable<int> GG_minimi_data_notifica { get; set; }
        public Nullable<int> GG_massimi_data_notifica { get; set; }
        public Nullable<int> GG_massimi_data_emissione { get; set; }
        public Nullable<int> AA_prescrizione_avviso { get; set; }
        public Nullable<decimal> importo_minimo_da_pagare { get; set; }
        public Nullable<decimal> importo_limite_sollecito_bonario { get; set; }
        public int num_rate_insolute { get; set; }
        public int num_gg_max_scadenza_rata_insoluta { get; set; }
        public System.DateTime periodo_validita_da { get; set; }
        public System.DateTime periodo_validita_a { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public string testo1 { get; set; }
        public string testo2 { get; set; }
        public string testo3 { get; set; }
        public string testo4 { get; set; }
        public Nullable<bool> flag_notifica_amministratori_PG { get; set; }
        public Nullable<bool> flag_rinotifica_amministratori_PG { get; set; }
        public Nullable<int> modalita_sped_not_estero { get; set; }
        public Nullable<int> id_cc_riscossione { get; set; }
        public Nullable<bool> flag_IUV { get; set; }
        public Nullable<int> id_risorsa_resp_emissione_tipo_avvpag { get; set; }
        public Nullable<int> id_risorsa_resp_notifica_tipo_avvpag { get; set; }
        public Nullable<bool> flag_decorrenza_interessi { get; set; }
        public Nullable<int> id_struttura_restituzione_notifiche { get; set; }
        public Nullable<int> num_max_spedizioni { get; set; }
        public Nullable<int> num_pagine_stampa_testo_atto { get; set; }
        public string descr_personalizzata_tipo_avvpag { get; set; }
        public Nullable<int> GG_massimi_notifica_avvpag_collegati { get; set; }
        public Nullable<int> id_tipo_servizio { get; set; }
        public Nullable<int> GG_decorrenza_interessi { get; set; }
        public Nullable<decimal> percentuale_inc_iscrizione_ruolo { get; set; }
        public string aux_digit_pagopa { get; set; }
        public string application_code_pagopa { get; set; }
        public string codice_segregazione_pagopa { get; set; }
        public string codice_struttura_ente_pagopa { get; set; }
        public string flag_stampa_F24 { get; set; }
    
        ///<summary><para>Relazione: anagrafica_tipo_avv_pagtab_modalita_rate_avvpag_view</para> Chiave Origine: id_tipo_avv_pag</summary>
     public virtual anagrafica_tipo_avv_pag anagrafica_tipo_avv_pag { get; set; }
        ///<summary><para>Relazione: tab_cc_riscossionetab_modalita_rate_avvpag_view1</para> Chiave Origine: id_cc_riscossione</summary>
     public virtual tab_cc_riscossione tab_cc_riscossione { get; set; }
        ///<summary><para>Relazione: anagrafica_risorsetab_modalita_rate_avvpag_view</para> Chiave Origine: id_risorsa_resp_emissione_tipo_avvpag</summary>
     public virtual anagrafica_risorse anagrafica_risorse1 { get; set; }
    }
}