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
    
    public partial class tab_prog_lista
    {
        public int id_prog_lista { get; set; }
        public int id_ente { get; set; }
        public Nullable<int> id_entrata { get; set; }
        public int id_tipo_lista { get; set; }
        public int anno { get; set; }
        public int progr { get; set; }
    
        ///<summary><para>Relazione: FK_tab_prog_lista_anagrafica_entrate</para> Chiave Origine: id_entrata</summary>
     public virtual anagrafica_entrate anagrafica_entrate { get; set; }
        ///<summary><para>Relazione: FK_tab_prog_lista_tab_tipo_lista</para> Chiave Origine: id_tipo_lista</summary>
     public virtual tab_tipo_lista tab_tipo_lista { get; set; }
        ///<summary><para>Relazione: FK_tab_prog_lista_anagrafica_ente</para> Chiave Origine: id_ente</summary>
     public virtual anagrafica_ente anagrafica_ente { get; set; }
    }
}