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
    
    public partial class tab_categoria_tariffaria
    {
        public tab_categoria_tariffaria()
        {
            this.tab_oggetti_contribuzione = new HashSet<tab_oggetti_contribuzione>();
        }
    
        public int id_categoria_tariffaria { get; set; }
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public int id_entrata { get; set; }
        public int anno { get; set; }
        public int id_anagrafica_categoria { get; set; }
        public string flag_impegno_base_std { get; set; }
        public string tipo_base_std { get; set; }
        public string um_base_std { get; set; }
        public Nullable<decimal> quantita_base_std { get; set; }
        public Nullable<decimal> aliquota_base_std { get; set; }
        public decimal imp_unitario_base_std { get; set; }
        public Nullable<decimal> perc_rivalutazione { get; set; }
        public Nullable<int> moltiplicatore { get; set; }
        public string flag_essenziali { get; set; }
        public string tipo_essenziali { get; set; }
        public string um_essenziali { get; set; }
        public Nullable<int> quantita_essenziali { get; set; }
        public Nullable<decimal> aliquota_essenziali { get; set; }
        public Nullable<decimal> imp_unitario_essenziali { get; set; }
        public string flag_agevolata { get; set; }
        public string tipo_agevolata { get; set; }
        public string um_agevolata { get; set; }
        public Nullable<int> quantita_agevolata { get; set; }
        public Nullable<decimal> aliquota_agevolata { get; set; }
        public Nullable<decimal> imp_unitario_agevolata { get; set; }
        public string tipo_ecc1_std { get; set; }
        public string um_ecc1_std { get; set; }
        public string tipo_quantita_ecc1_std { get; set; }
        public Nullable<int> quantita_ecc1_std { get; set; }
        public Nullable<decimal> aliquota_ecc1_std { get; set; }
        public Nullable<decimal> imp_unitario_ecc1_std { get; set; }
        public string tipo_ecc2_std { get; set; }
        public string um_ecc2_std { get; set; }
        public string tipo_quantita_ecc2_std { get; set; }
        public Nullable<int> quantita_ecc2_std { get; set; }
        public Nullable<decimal> aliquota_ecc2_std { get; set; }
        public Nullable<decimal> imp_unitario_ecc2_std { get; set; }
        public string tipo_ecc3_std { get; set; }
        public string um_ecc3_std { get; set; }
        public string tipo_quantita_ecc3_std { get; set; }
        public Nullable<int> quantita_ecc3_std { get; set; }
        public Nullable<decimal> aliquota_ecc3_std { get; set; }
        public Nullable<decimal> imp_unitario_ecc3_std { get; set; }
        public System.DateTime periodo_rif_da { get; set; }
        public System.DateTime periodo_rif_a { get; set; }
        public Nullable<int> id_provvedimento { get; set; }
        public Nullable<decimal> nolo_contatore_forfettario { get; set; }
        public Nullable<decimal> manutenzione_contatore_forfettario { get; set; }
        public Nullable<decimal> quota_fissa_fascia1 { get; set; }
        public Nullable<decimal> imp_annuo_quota_fissa_fascia1 { get; set; }
        public Nullable<decimal> quota_fissa_fascia2 { get; set; }
        public Nullable<decimal> imp_annuo_quota_fissa_fascia2 { get; set; }
        public Nullable<decimal> quota_fissa_fascia3 { get; set; }
        public Nullable<decimal> imp_annuo_quota_fissa_fascia3 { get; set; }
        public Nullable<decimal> quota_fissa_fascia4 { get; set; }
        public Nullable<decimal> imp_annuo_quota_fissa_fascia4 { get; set; }
        public string um_fognatura_depurazione { get; set; }
        public Nullable<decimal> imp_unitario_fognatura { get; set; }
        public Nullable<decimal> imp_unitario_depurazione { get; set; }
        public string UM_oneri_1 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_idrico_1 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_fognatura_1 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_depurazione_1 { get; set; }
        public string UM_oneri_2 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_idrico_2 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_fognatura_2 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_depurazione_2 { get; set; }
        public string UM_oneri_3 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_idrico_3 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_fognatura_3 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_depurazione_3 { get; set; }
        public string UM_oneri_4 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_idrico_4 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_fognatura_4 { get; set; }
        public Nullable<decimal> imp_unitario_oneri_depurazione_4 { get; set; }
        public string tipo_ecc4_std { get; set; }
        public string um_ecc4_std { get; set; }
        public string tipo_quantita_ecc4_std { get; set; }
        public Nullable<int> quantita_ecc4_std { get; set; }
        public Nullable<decimal> aliquota_ecc4_std { get; set; }
        public Nullable<decimal> imp_unitario_ecc4_std { get; set; }
    
        ///<summary><para>Relazione: FK_tab_categoria_tariffaria_anagrafica_categoria</para> Chiave Origine: id_anagrafica_categoria</summary>
     public virtual anagrafica_categoria anagrafica_categoria { get; set; }
        ///<summary><para>Relazione: FK_tab_categoria_tariffaria_anagrafica_ente_gestito</para> Chiave Origine: id_ente_gestito</summary>
     public virtual anagrafica_ente_gestito anagrafica_ente_gestito { get; set; }
        ///<summary><para>Relazione: FK_tab_categoria_tariffaria_tab_macroentrate</para> Chiave Origine: id_entrata</summary>
     public virtual tab_macroentrate tab_macroentrate { get; set; }
        ///<summary><para>Relazione: FK_tab_oggetti_contribuzione_tab_categoria_tariffaria</para> Chiave Origine: id_categoria_tariffaria</summary>
     public virtual ICollection<tab_oggetti_contribuzione> tab_oggetti_contribuzione { get; set; }
        ///<summary><para>Relazione: FK_tab_categoria_tariffaria_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}
