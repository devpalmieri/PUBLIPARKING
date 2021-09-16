using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models
{
    public class CambiaEnteWebViewModel
    {
        public int selEnteId { get; set; }

        public List<anagrafica_ente> listEnte { get; set; }

        public bool isCallCenter { get; set; }

        public string Descrizione_Ente { get; set; }
        public string Url_Ente { get; set; }
        public int selected_id_tipo_ente { get; set; }
        public List<tab_tipo_ente> listTipoEnte { get; set; }
    }
}
