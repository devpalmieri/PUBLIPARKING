using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tipo_oggetto.Metadata))]
    public partial class tab_tipo_oggetto : ISoftDeleted
    {
        [Obsolete("Usare TASSA_AUTO_ID")]
        
        public const int TASSA_AUTO = 100;
        public const int TASSA_AUTO_ID = 100;

        // Utenza TARSU si usa anche per TOSAP (vedi db_gragnano "select * from tab_oggetti where id_entrata = 8 order by id_oggetto desc")
        public const int UTENZE_TARSU_ID = 7;
        
        public const int UTENZE_COSAP_ID = 16;
        
        public const int UTENZA_ICI_IMU = 18;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public bool isUbicazione
        {
            get
            {
                if (flag_ubicazione_oggetto == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [DisplayName("Ente")]
        public string Ente
        {
            get
            {
                if (anagrafica_ente != null)
                {
                    return anagrafica_ente.descrizione_ente;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Ente gestito")]
        public string EnteGestito
        {
            get
            {
                if (anagrafica_ente_gestito != null)
                {
                    return anagrafica_ente_gestito.descrizione_ente;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Entrata")]
        public string Entrata
        {
            get
            {
                if (anagrafica_entrate != null)
                {
                    return anagrafica_entrate.descrizione_entrata;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tipo_oggetto { get; set; }

            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [DisplayName("Ente gestito")]
            public int id_ente_gestito { get; set; }

            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [Required]
            [DisplayName("Codice")]
            public string cod_tipo_oggetto { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string des_tipo_oggetto { get; set; }

            [Required]
            [RegularExpression("^[0-9]+$", ErrorMessage = "Formato non valido")]
            [DisplayName("Progressivo")]
            public int progressivo { get; set; }
        }
    }
}
