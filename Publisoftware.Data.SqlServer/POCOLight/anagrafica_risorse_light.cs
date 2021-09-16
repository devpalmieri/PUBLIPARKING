using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class anagrafica_risorse_light : BaseEntity<anagrafica_risorse_light>
    {
        public int id_risorsa { get; set; }
        public string cognome { get; set; }
        public string nome { get; set; }
        public string matricola { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string Strutture { get; set; }
        public int idJoinRisorsaStruttura { get; set; }
    }
}