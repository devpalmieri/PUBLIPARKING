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
    
    public partial class anagrafica_procedure_concorsuali
    {
        public anagrafica_procedure_concorsuali()
        {
            this.tab_procedure_concorsuali = new HashSet<tab_procedure_concorsuali>();
        }
    
        public int id_anagrafica_proc_concorsuale { get; set; }
        public string cod_tipo_proc_concorsuale { get; set; }
        public string descr_proc_concorsuale { get; set; }
        public Nullable<int> id_stato_contribuente_apertura_proc_concorsuale { get; set; }
        public string cod_stato_contribuente_apertura_proc_concorsuale { get; set; }
        public Nullable<int> id_stato_contribuente_chiusura_proc_concorsuale { get; set; }
        public string cod_stato_contribuente_chiusura_proc_concorsuale { get; set; }
        public Nullable<int> id_tipo_doc_entrate { get; set; }
        public string tipo_autorita_competente { get; set; }
        public string flag_pf_pg_assoggettabili { get; set; }
        public Nullable<int> id_tipo_relazione_curatore_commissario { get; set; }
    
        ///<summary><para>Relazione: FK_anagrafica_procedure_concorsuali_anagrafica_stato_contribuente_apertura</para> Chiave Origine: id_stato_contribuente_apertura_proc_concorsuale</summary>
     public virtual anagrafica_stato_contribuente anagrafica_stato_contribuente { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_procedure_concorsuali_anagrafica_stato_contribuente_chiusura</para> Chiave Origine: id_stato_contribuente_chiusura_proc_concorsuale</summary>
     public virtual anagrafica_stato_contribuente anagrafica_stato_contribuente1 { get; set; }
        ///<summary><para>Relazione: FK_anagrafica_procedure_concorsuali_anagrafica_tipo_relazione</para> Chiave Origine: id_tipo_relazione_curatore_commissario</summary>
     public virtual anagrafica_tipo_relazione anagrafica_tipo_relazione { get; set; }
        ///<summary><para>Relazione: FK_tab_procedure_concorsuali_anagrafica_procedure_concorsuali</para> Chiave Origine: id_anagrafica_proc_concorsuale</summary>
     public virtual ICollection<tab_procedure_concorsuali> tab_procedure_concorsuali { get; set; }
    }
}