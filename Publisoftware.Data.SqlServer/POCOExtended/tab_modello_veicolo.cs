using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_modello_veicolo.Metadata))]
    public partial class tab_modello_veicolo : ISoftDeleted, IGestioneStato
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_struttura, int p_risorsa)
        {
            data_inserimento = DateTime.Now;
            id_struttura_stato = p_struttura;
            id_risorsa_stato = p_risorsa;
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_modello { get; set; }

            [DisplayName("Marca")]
            public int id_marca { get; set; }

            [DisplayName("Tipo")]
            public int id_tipo_veicolo { get; set; }

            [Required]
            [DisplayName("Modello")]
            public string descr_modello { get; set; }

            [Required]
            [DisplayName("Serie")]
            public string descr_serie { get; set; }
        }
    }
}
