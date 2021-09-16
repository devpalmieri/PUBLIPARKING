using Publiparking.Core.Data.SqlServer.POCOExtended.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class tab_abilitazione_menu : ISoftDeleted, IGestioneStato
    {
        public const int MENU_PRIMO_LIVELLO = 1;
        public const int MENU_SECONDO_LIVELLO = 2;
        public const int MENU_TERZO_LIVELLO = 3;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_risorsa_stato = p_idRisorsa;
        }


    }
}
