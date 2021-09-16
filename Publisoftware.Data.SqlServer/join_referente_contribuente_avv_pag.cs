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
    
    public partial class join_referente_contribuente_avv_pag
    {
        public int id_join_referente_contribuente_avv_pag { get; set; }
        public int id_join_referente_contribuente { get; set; }
        public int id_avv_pag { get; set; }
        public decimal importo_max_obbligazione { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
    
        ///<summary><para>Relazione: FK_join_referente_contribuente_avv_pag_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_join_referente_contribuente_avv_pag_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_join_referente_contribuente_avv_pag_join_referente_contribuente</para> Chiave Origine: id_join_referente_contribuente</summary>
     public virtual join_referente_contribuente join_referente_contribuente { get; set; }
        ///<summary><para>Relazione: FK_join_referente_contribuente_avv_pag_tab_avv_pag</para> Chiave Origine: id_avv_pag</summary>
     public virtual tab_avv_pag tab_avv_pag { get; set; }
    }
}
