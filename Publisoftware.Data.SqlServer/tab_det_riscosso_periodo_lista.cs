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
    
    public partial class tab_det_riscosso_periodo_lista
    {
        public int id_det_riscosso_periodo_lista { get; set; }
        public int id_ente { get; set; }
        public Nullable<int> id_ente_gestito { get; set; }
        public int id_entrata { get; set; }
        public System.DateTime periodo_riscosso_da { get; set; }
        public System.DateTime periodo_riscosso_a { get; set; }
        public Nullable<int> id_lista_avviso_riscosso { get; set; }
        public Nullable<int> id_tipo_avvpag_riscosso { get; set; }
        public Nullable<int> id_servizio_avvpag_riscosso { get; set; }
        public Nullable<int> anno_riscossione { get; set; }
        public Nullable<int> id_lista_riferimento { get; set; }
        public Nullable<int> id_entrata_tipo_avvpag_lista_riferimento { get; set; }
        public Nullable<int> id_tipo_avvpag_lista_riferimento { get; set; }
        public Nullable<int> anno_emissione_avvisi_lista_riferimento { get; set; }
        public Nullable<int> id_entrata_tipo_voce_contribuzione { get; set; }
        public int id_tipo_voce_contribuzione { get; set; }
        public string descrizione_gruppo { get; set; }
        public Nullable<int> anno_riferimento_voce_contribuzione { get; set; }
        public Nullable<decimal> riscosso_tipo_voce_contribuzione { get; set; }
        public Nullable<decimal> imponibile_riscosso_tipo_voce_lista_pagata { get; set; }
        public Nullable<decimal> iva_riscosso_tipo_voce_lista_pagata { get; set; }
        public string cod_stato { get; set; }
        public Nullable<System.DateTime> data_stato { get; set; }
        public Nullable<int> id_struttura_stato { get; set; }
        public Nullable<int> id_risorsa_stato { get; set; }
        public Nullable<System.Guid> id_gen { get; set; }
        public string numero_accertamento_contabile { get; set; }
        public Nullable<System.DateTime> data_accertamento_contabile { get; set; }
    }
}
