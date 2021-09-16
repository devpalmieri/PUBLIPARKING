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
    
    public partial class tab_lista_riscossione
    {
        public tab_lista_riscossione()
        {
            this.tab_dett_riscossione = new HashSet<tab_dett_riscossione>();
            this.tab_dett_riscossione1 = new HashSet<tab_dett_riscossione>();
        }
    
        public int id_lista_riscossione { get; set; }
        public int id_ente { get; set; }
        public int id_tipo_lista { get; set; }
        public int id_tab_cc_riscossione { get; set; }
        public string abi_cc { get; set; }
        public string cab_cc { get; set; }
        public string num_cc { get; set; }
        public int anno_lista { get; set; }
        public string numero_lista { get; set; }
        public System.DateTime data_lista { get; set; }
        public Nullable<int> anno_riscossione { get; set; }
        public Nullable<int> mese_riscossione { get; set; }
        public Nullable<System.DateTime> periodo_rif_da { get; set; }
        public System.DateTime periodo_rif_a { get; set; }
        public decimal importo_totale_riscosso { get; set; }
        public decimal imponibile_totale_riscosso { get; set; }
        public decimal iva_totale_riscossa { get; set; }
        public string desc_aggiuntiva_lista { get; set; }
        public int id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public string flag_on_off { get; set; }
    
        ///<summary><para>Relazione: FK_tab_lista_riscossione_anagrafica_risorse</para> Chiave Origine: id_risorsa_stato</summary>
     public virtual anagrafica_risorse anagrafica_risorse { get; set; }
        ///<summary><para>Relazione: FK_tab_lista_riscossione_anagrafica_strutture_aziendali</para> Chiave Origine: id_struttura_stato</summary>
     public virtual anagrafica_strutture_aziendali anagrafica_strutture_aziendali { get; set; }
        ///<summary><para>Relazione: FK_tab_dett_riscossione_tab_lista_riscossione</para> Chiave Origine: id_lista_riscossione</summary>
     public virtual ICollection<tab_dett_riscossione> tab_dett_riscossione { get; set; }
        ///<summary><para>Relazione: FK_tab_dett_riscossione_tab_lista_riscossione1</para> Chiave Origine: id_lista_rettifica_riscossione</summary>
     public virtual ICollection<tab_dett_riscossione> tab_dett_riscossione1 { get; set; }
        ///<summary><para>Relazione: FK_tab_lista_riscossione_tab_tipo_lista</para> Chiave Origine: id_tipo_lista</summary>
     public virtual tab_tipo_lista tab_tipo_lista { get; set; }
        ///<summary><para>Relazione: FK_tab_lista_riscossione_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}
