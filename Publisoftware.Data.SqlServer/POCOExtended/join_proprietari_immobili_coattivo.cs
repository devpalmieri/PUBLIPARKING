using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_proprietari_immobili_coattivo.Metadata))]
    public partial class join_proprietari_immobili_coattivo : ISoftDeleted, IGestioneStato
    {
        public static string STATO_ANN_IST = "ANN-IST";
        public const string ATT_ATT = "ATT-ATT";

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

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }
    }
}
