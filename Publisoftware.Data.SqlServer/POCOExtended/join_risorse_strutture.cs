using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_risorse_strutture.Metadata))]
    public partial class join_risorse_strutture : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";

        public ModalitaOperativaEnum mod_operativa
        {
            get
            {
                if (flag_modalita_operativa == null)
                    return ModalitaOperativaEnum.ALL;
                else
                    return (ModalitaOperativaEnum)((char)flag_modalita_operativa[0]);
            }
            set
            {
                flag_modalita_operativa = ((char)value).ToString();
            }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string potereFirma
        {
            get
            {
                if (flag_responsabilita == "0" || flag_responsabilita == null)
                {
                    return string.Empty;
                }
                else
                {
                    return "Si";
                }
            }
        }

        private string _responsabileStruttura;
        public string responsabileStruttura
        {
            get
            {
                return _responsabileStruttura;
            }
            set
            {
                _responsabileStruttura = value;
            }
        }

        /// <summary>
        /// Gestisce l'aggiornamento dei campi utente dello stato
        /// </summary>
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

            [DisplayName("ID")]
            public int id_join_risorse_strutture { get; set; }


            [DisplayName("Mod. Operativa")]
            public string flag_modalita_operativa { get; set; }

            [DisplayName("Mod. Operativa")]
            public ModalitaOperativaEnum mod_operativa { get; set; }

            [DisplayName("Poteri di firma")]
            public string flag_responsabilita { get; set; }
        }
    }
}
