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
    
    public partial class anagrafica_tipo_esito_notifica
    {
        public anagrafica_tipo_esito_notifica()
        {
            this.tab_sped_not_fatt_emissione = new HashSet<tab_sped_not_fatt_emissione>();
            this.tab_sped_not_fatt_emissione1 = new HashSet<tab_sped_not_fatt_emissione>();
            this.tab_sped_not = new HashSet<tab_sped_not>();
            this.tab_sped_not1 = new HashSet<tab_sped_not>();
        }
    
        public int id_tipo_esito_notifica { get; set; }
        public string descr_tipo_esito_notifica { get; set; }
        public string flag_esito { get; set; }
        public string fl_rr_messo { get; set; }
        public string fl_not_ok { get; set; }
        public string fl_not_nok { get; set; }
        public string cod_postel { get; set; }
        public string cod_nexive { get; set; }
    
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_tipo_esito_notifica</para> Chiave Origine: id_tipo_esito_notifica</summary>
     public virtual ICollection<tab_sped_not_fatt_emissione> tab_sped_not_fatt_emissione { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_fatt_emissione_anagrafica_tipo_esito_notifica1</para> Chiave Origine: id_tipo_esito_avvdep</summary>
     public virtual ICollection<tab_sped_not_fatt_emissione> tab_sped_not_fatt_emissione1 { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_anagrafica_tipo_esito_notifica</para> Chiave Origine: id_tipo_esito_notifica</summary>
     public virtual ICollection<tab_sped_not> tab_sped_not { get; set; }
        ///<summary><para>Relazione: FK_tab_sped_not_anagrafica_tipo_esito_notifica1</para> Chiave Origine: id_tipo_esito_avvdep</summary>
     public virtual ICollection<tab_sped_not> tab_sped_not1 { get; set; }
    }
}