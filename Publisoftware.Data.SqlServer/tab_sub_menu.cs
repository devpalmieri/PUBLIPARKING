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
    
    public partial class tab_sub_menu
    {
        public int IDSubMenu { get; set; }
        public Nullable<int> Id_Menu { get; set; }
        public string DescrizioneSub { get; set; }
        public string ActionSub { get; set; }
        public string ControllerSub { get; set; }
        public string Parametri_Url { get; set; }
        public Nullable<int> OrdineSub { get; set; }
        public string HasSubMenu2 { get; set; }
    
        ///<summary><para>Relazione: FK_tab_sub_menu_tab_menu_orizzontale</para> Chiave Origine: Id_Menu</summary>
     public virtual tab_menu_orizzontale tab_menu_orizzontale { get; set; }
    }
}
