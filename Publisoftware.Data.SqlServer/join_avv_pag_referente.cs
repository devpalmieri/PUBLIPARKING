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
    
    public partial class join_avv_pag_referente
    {
        public int id_join_avv_pag_referente { get; set; }
        public int id_tab_avv_pag { get; set; }
        public int id_referente { get; set; }
        public System.DateTime data_stato { get; set; }
        public string cod_stato { get; set; }
    
        ///<summary><para>Relazione: FK_join_avv_pag_referente_tab_avv_pag</para> Chiave Origine: id_tab_avv_pag</summary>
     public virtual tab_avv_pag tab_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_join_avv_pag_referente_tab_referente</para> Chiave Origine: id_referente</summary>
     public virtual tab_referente tab_referente { get; set; }
    }
}