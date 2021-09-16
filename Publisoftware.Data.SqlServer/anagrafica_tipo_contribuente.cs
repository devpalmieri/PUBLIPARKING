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
    
    public partial class anagrafica_tipo_contribuente
    {
        public anagrafica_tipo_contribuente()
        {
            this.tab_ispezioni_coattivo_new = new HashSet<tab_ispezioni_coattivo_new>();
            this.tab_domicilio = new HashSet<tab_domicilio>();
            this.tab_terzo = new HashSet<tab_terzo>();
            this.join_tipo_stato_referenza = new HashSet<join_tipo_stato_referenza>();
            this.tab_contribuente_sto_new = new HashSet<tab_contribuente_sto_new>();
            this.tab_contribuente = new HashSet<tab_contribuente>();
            this.tab_referente = new HashSet<tab_referente>();
        }
    
        public int id_tipo_contribuente { get; set; }
        public string desc_tipo_contribuente { get; set; }
        public string sigla_tipo_contribuente { get; set; }
        public int ordinamento { get; set; }
        public string descr_sigla { get; set; }
    
        ///<summary><para>Relazione: FK_tab_ispezioni_coattivo_new_anagrafica_tipo_contribuente</para> Chiave Origine: id_tipo_contribuente</summary>
     public virtual ICollection<tab_ispezioni_coattivo_new> tab_ispezioni_coattivo_new { get; set; }
        ///<summary><para>Relazione: FK_tab_domicilio_anagrafica_tipo_contribuente</para> Chiave Origine: id_tipo</summary>
     public virtual ICollection<tab_domicilio> tab_domicilio { get; set; }
        ///<summary><para>Relazione: FK_tab_terzo_anagrafica_tipo_contribuente</para> Chiave Origine: id_tipo_terzo</summary>
     public virtual ICollection<tab_terzo> tab_terzo { get; set; }
        ///<summary><para>Relazione: FK_join_tipo_stato_referenza_anagrafica_tipo_contribuente</para> Chiave Origine: id_tipo_contribuente</summary>
     public virtual ICollection<join_tipo_stato_referenza> join_tipo_stato_referenza { get; set; }
        ///<summary><para>Relazione: FK_tab_contribuente_sto_new_anagrafica_tipo_contribuente</para> Chiave Origine: id_tipo_contribuente</summary>
     public virtual ICollection<tab_contribuente_sto_new> tab_contribuente_sto_new { get; set; }
        ///<summary><para>Relazione: FK_tab_contribuente_anagrafica_tipo_contribuente</para> Chiave Origine: id_tipo_contribuente</summary>
     public virtual ICollection<tab_contribuente> tab_contribuente { get; set; }
        ///<summary><para>Relazione: FK_tab_referente_anagrafica_tipo_relazione</para> Chiave Origine: id_tipo_referente</summary>
     public virtual ICollection<tab_referente> tab_referente { get; set; }
    }
}
