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
    
    public partial class tab_toponimi_normalizzati_2
    {
        public int id_tab_toponimi_normalizzati { get; set; }
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public string cod_comune { get; set; }
        public string cap_comune { get; set; }
        public Nullable<int> id_toponimo_originale { get; set; }
        public string indirizzo_originale { get; set; }
        public string frazione_originale { get; set; }
        public string tipo_toponimo_originale { get; set; }
        public string toponimo_originale { get; set; }
        public string tipo_toponimo_corretto_automaticamente { get; set; }
        public string toponimo_corretto_automaticamente { get; set; }
        public string toponimo_rimanente { get; set; }
        public string tipo_toponimo_corretto_manualmente { get; set; }
        public string toponimo_corretto_manualmente { get; set; }
        public string tipo_toponimo_normalizzato { get; set; }
        public string toponimo_normalizzato { get; set; }
        public Nullable<int> numero_civico { get; set; }
        public string sigla_civico { get; set; }
        public string frazione { get; set; }
        public string condominio { get; set; }
        public string edificio { get; set; }
        public string piano { get; set; }
        public string scala { get; set; }
        public string interno { get; set; }
        public string fonte { get; set; }
        public int id_toponimo_assegnato { get; set; }
        public string note { get; set; }
        public string cod_stato { get; set; }
        public string flag_on_off { get; set; }
    }
}
