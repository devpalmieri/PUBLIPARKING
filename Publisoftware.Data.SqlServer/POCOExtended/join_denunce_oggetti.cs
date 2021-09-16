using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_denunce_oggetti : ISoftDeleted, IGestioneStato
    {
        [ObsoleteAttribute("usare anagrafica_stato_denunce e CORRISPONDENTE id_stato")]
        public const string ATT_DEF = anagrafica_stato_denunce.ATT_DEF;

        [ObsoleteAttribute("usare anagrafica_stato_denunce e CORRISPONDENTE id_stato")]
        public const string ATT_ATT = anagrafica_stato_denunce.ATT_ATT;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }
    }
}
