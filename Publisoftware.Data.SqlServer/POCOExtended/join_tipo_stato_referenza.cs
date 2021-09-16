using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_tipo_stato_referenza : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string desc_tipo_relazione
        {
            get
            {
                return anagrafica_tipo_relazione != null ? anagrafica_tipo_relazione.desc_tipo_relazione : string.Empty;
            }
        }

        public string coobbligato
        {
            get
            {
                return flag_coobbligato == tab_referente.COOBBLIGATO ? "Coobbligato in solido" : (flag_coobbligato == tab_referente.COOBBLIGATO_PARZIALE ? "Coobbligato parziale" : (flag_coobbligato == tab_referente.GARANTE ? "Garante" : "Nessuna coobbligazione"));
            }
        }
    }
}
