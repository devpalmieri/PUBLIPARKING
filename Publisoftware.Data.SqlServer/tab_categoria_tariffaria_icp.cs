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
    
    public partial class tab_categoria_tariffaria_icp
    {
        public int id_categoria_tariffaria { get; set; }
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public int id_entrata { get; set; }
        public int anno { get; set; }
        public int id_anagrafica_categoria { get; set; }
        public string um_base { get; set; }
        public decimal quantita_base_da { get; set; }
        public decimal quantita_base_a { get; set; }
        public decimal imp_unitario_base { get; set; }
        public System.DateTime periodo_rif_da { get; set; }
        public System.DateTime periodo_rif_a { get; set; }
        public string tipo_calc_tmp { get; set; }
        public Nullable<decimal> imp_unitario_tmp { get; set; }
        public Nullable<decimal> aliquota_tmp { get; set; }
        public Nullable<int> num_min_giorni { get; set; }
        public Nullable<int> num_max_giorni { get; set; }
    
        ///<summary><para>Relazione: FK_tab_categoria_tariffaria_icp_anagrafica_entrate</para> Chiave Origine: id_entrata</summary>
     public virtual anagrafica_entrate anagrafica_entrate { get; set; }
        ///<summary><para>Relazione: FK_tab_categoria_tariffaria_icp_anagrafica_categoria</para> Chiave Origine: id_anagrafica_categoria</summary>
     public virtual anagrafica_categoria anagrafica_categoria { get; set; }
        ///<summary><para>Relazione: FK_tab_categoria_tariffaria_icp_anagrafica_ente_gestito</para> Chiave Origine: id_ente_gestito</summary>
     public virtual anagrafica_ente_gestito anagrafica_ente_gestito { get; set; }
        ///<summary><para>Relazione: FK_tab_categoria_tariffaria_icp_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}