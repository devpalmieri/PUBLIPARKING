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
    
    public partial class anagrafica_stato_mov_pag
    {
        public anagrafica_stato_mov_pag()
        {
            this.tab_mov_pag = new HashSet<tab_mov_pag>();
        }
    
        public int id_stato_mov_pag { get; set; }
        public string descr_stato_mov_pag { get; set; }
        public string descr_programma { get; set; }
        public string cod_stato_mov_pag { get; set; }
        public string flag_contabilizzabile { get; set; }
        public string flag_mov_daverificare { get; set; }
    
        ///<summary><para>Relazione: FK_tab_mov_pag_anagrafica_stato_mov_pag1</para> Chiave Origine: id_stato</summary>
     public virtual ICollection<tab_mov_pag> tab_mov_pag { get; set; }
    }
}