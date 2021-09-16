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
    
    public partial class tab_selezione_report
    {
        public int id_selezione_report { get; set; }
        public Nullable<int> id_ente { get; set; }
        public int id_tipo_avv_pag { get; set; }
        public string nome_report { get; set; }
        public int id_stampatore { get; set; }
        public string flag_on_off { get; set; }
        public Nullable<System.DateTime> data_stato { get; set; }
        public string tipo_foglio { get; set; }
        public Nullable<int> id_tipo_servizio { get; set; }
    
        ///<summary><para>Relazione: FK_tab_selezione_report_anagrafica_stampatori</para> Chiave Origine: id_stampatore</summary>
     public virtual anagrafica_stampatori anagrafica_stampatori { get; set; }
        ///<summary><para>Relazione: FK_tab_selezione_report_anagrafica_tipo_avv_pag</para> Chiave Origine: id_tipo_avv_pag</summary>
     public virtual anagrafica_tipo_avv_pag anagrafica_tipo_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_selezione_report_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}
