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
    
    public partial class calcolo_voci_contribuzione
    {
        public int id_calcolo_voci_contrib { get; set; }
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public Nullable<int> id_entrata { get; set; }
        public Nullable<int> Anno { get; set; }
        public System.DateTime periodo_rif_da { get; set; }
        public System.DateTime periodo_rif_a { get; set; }
        public int id_anagrafica_voce_contribuzione { get; set; }
        public string tipo_base_calcolo { get; set; }
        public Nullable<decimal> aliquota_base_calcolo { get; set; }
        public Nullable<decimal> imp_base_calcolo { get; set; }
        public string UM_Voce_Contribuzione { get; set; }
        public string tipo_maggiorazione_1 { get; set; }
        public Nullable<decimal> aliquota_maggiorazione_1 { get; set; }
        public Nullable<decimal> imp_maggiorazione_1 { get; set; }
        public Nullable<decimal> fascia_da_1 { get; set; }
        public string fascia_a_1 { get; set; }
        public string tipo_maggiorazione_2 { get; set; }
        public Nullable<decimal> aliquota_maggiorazione_2 { get; set; }
        public Nullable<decimal> imp_maggiorazione_2 { get; set; }
        public Nullable<decimal> fascia_da_2 { get; set; }
        public string fascia_a_2 { get; set; }
        public string tipo_maggiorazione_3 { get; set; }
        public Nullable<decimal> aliquota_maggiorazione_3 { get; set; }
        public Nullable<decimal> imp_maggiorazione_3 { get; set; }
        public Nullable<decimal> fascia_da_3 { get; set; }
        public string fascia_a_3 { get; set; }
        public string tipo_maggiorazione_4 { get; set; }
        public Nullable<decimal> aliquota_maggiorazione_4 { get; set; }
        public Nullable<decimal> imp_maggiorazione_4 { get; set; }
        public Nullable<decimal> fascia_da_4 { get; set; }
        public string fascia_a_4 { get; set; }
        public Nullable<int> id_provvedimento { get; set; }
        public string flag_segno { get; set; }
        public string flag_iva { get; set; }
        public Nullable<decimal> aliquota_iva { get; set; }
        public string flag_rateizzazione { get; set; }
    
        ///<summary><para>Relazione: FK_calcolo_voci_contribuzione_anagrafica_entrate</para> Chiave Origine: id_entrata</summary>
     public virtual anagrafica_entrate anagrafica_entrate { get; set; }
        ///<summary><para>Relazione: FK_calcolo_voci_contribuzione_anagrafica_voci_contribuzione</para> Chiave Origine: id_anagrafica_voce_contribuzione</summary>
     public virtual anagrafica_voci_contribuzione anagrafica_voci_contribuzione { get; set; }
        ///<summary><para>Relazione: FK_calcolo_voci_contribuzione_anagrafica_ente_gestito</para> Chiave Origine: id_ente_gestito</summary>
     public virtual anagrafica_ente_gestito anagrafica_ente_gestito { get; set; }
        ///<summary><para>Relazione: FK_calcolo_voci_contribuzione_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}