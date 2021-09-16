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
    
    public partial class tab_corrispettivi
    {
        public int id_corrispettivi { get; set; }
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public System.DateTime dt_inizio_rediconto { get; set; }
        public System.DateTime dt_fine_rediconto { get; set; }
        public Nullable<int> anno_rif { get; set; }
        public int id_tab_cc_riscossione { get; set; }
        public int id_tipo_avv_pag { get; set; }
        public int id_entrata { get; set; }
        public int id_tipo_voce_contribuzione { get; set; }
        public string flag_fonte { get; set; }
        public string flag_ente { get; set; }
        public decimal totale_riscosso_voce { get; set; }
        public decimal totale_imponibile_riscosso { get; set; }
        public decimal totale_esente_iva_riscosso { get; set; }
        public decimal totale_iva_riscosso { get; set; }
        public string flag_segno { get; set; }
        public string tipo_riversamento { get; set; }
        public string flag_spesa { get; set; }
        public int id_voce_contrattuale { get; set; }
        public Nullable<decimal> aggio_contrattuale { get; set; }
        public decimal totale_corrispettivo { get; set; }
        public decimal imponibile_corrispettivo { get; set; }
        public string flag_iva { get; set; }
        public decimal imponibile_esente_corrispettivo { get; set; }
        public decimal iva_corrispettiva { get; set; }
        public int id_rendiconto { get; set; }
        public int id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public string flag_on_off { get; set; }
    
        ///<summary><para>Relazione: FK_tab_corrispettivi_anagrafica_ente_gestito</para> Chiave Origine: id_ente_gestito</summary>
     public virtual anagrafica_ente_gestito anagrafica_ente_gestito { get; set; }
        ///<summary><para>Relazione: FK_tab_corrispettivi_anagrafica_entrate</para> Chiave Origine: id_entrata</summary>
     public virtual anagrafica_entrate anagrafica_entrate { get; set; }
        ///<summary><para>Relazione: FK_tab_corrispettivi_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_corrispettivi_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_corrispettivi_anagrafica_tipo_avv_pag</para> Chiave Origine: id_tipo_avv_pag</summary>
     public virtual anagrafica_tipo_avv_pag anagrafica_tipo_avv_pag { get; set; }
        ///<summary><para>Relazione: FK_tab_corrispettivi_tab_rendiconto</para> Chiave Origine: id_rendiconto</summary>
     public virtual tab_rendiconto tab_rendiconto { get; set; }
        ///<summary><para>Relazione: FK_tab_corrispettivi_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}
