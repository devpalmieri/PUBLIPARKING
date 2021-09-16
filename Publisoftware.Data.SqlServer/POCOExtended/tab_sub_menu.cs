using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_sub_menu.Metadata))]
    public partial class tab_sub_menu : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }
        
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID SubMenu")]
            public int IdSubMenu { get; set; }
            [DisplayName("ID Main Menu")]
            public int Id_menu { get; set; }
           
            [DisplayName("Voce Menù")]
            public string DescrizioneSub { get; set; }
            
            public string ActionSub { get; set; }
            public string ControllerSub { get; set; }
            public string Parametri_Url { get; set; }
            public int OrdineSub { get; set; }
            public string HasSubMenu2 { get; set; }


        }
    }
}
