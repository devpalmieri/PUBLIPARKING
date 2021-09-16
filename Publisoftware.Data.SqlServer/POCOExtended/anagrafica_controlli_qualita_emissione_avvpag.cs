using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_controlli_qualita_emissione_avvpag.Metadata))]
    public partial class anagrafica_controlli_qualita_emissione_avvpag : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";

        public const string CODE_DATNOT = "DATNOT";
        public const string CODE_MAXNOT = "MAXNOT";
        public const string CODE_IMPMIN = "IMPMIN";
        public const string CODE_DECSAN = "DECSAN";
        public const string CODE_CONDEC = "CONDEC";
        public const string CODE_CONSCO = "CONSCO";
        public const string CODE_REFSCO = "REFSCO";
        public const string CODE_TERSCO = "TERSCO";
        public const string CODE_CONEMI = "CONEMI";
        public const string CODE_REFEMI = "REFEMI";
        public const string CODE_AVVPRE = "AVVPRE";
        public const string CODE_CONFIS = "CONFIS";

        [DisplayName("Modalità esecuzione")]
        public ModalitaEsecuzioneEnum modalita_esecuzione_Ext
        {
            get
            {
                if (modalita_esecuzione == "A")
                    return ModalitaEsecuzioneEnum.A;
                else
                    return ModalitaEsecuzioneEnum.O;
            }
            set
            {

                modalita_esecuzione = value.ToString();
            }
        }

        [DisplayName("Tipo record controllo")]
        public TiporecordControlloEnum tipo_record_controllo_Ext
        {
            get
            {
                if (tipo_record_controllo == "ANA")
                    return TiporecordControlloEnum.ANA;
                else
                    return TiporecordControlloEnum.AVV;
            }
            set
            {

                tipo_record_controllo = value.ToString();
            }
        }

        [DisplayName("Tipo Avviso")]
        public MacrotipoAvvpagEnum macrotipo_avvpag
        {
            get
            {
                if (flag_avvpag_composto == null)
                    return MacrotipoAvvpagEnum.COMPOSTO;
                else if (flag_avvpag_composto == true)
                    return MacrotipoAvvpagEnum.COMPOSTO;
                else
                    return MacrotipoAvvpagEnum.SEMPLICE;
            }
            set
            {

               flag_avvpag_composto = Convert.ToBoolean((int)value);
            }
        }

     
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

            [Required]
            [DisplayName("ID")]
            public int id_anagrafica_controllo { get; set; }

            [Required(ErrorMessage = "Selezionare un ente")]
            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [Required(ErrorMessage = "Selezionare una entrata")]
            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [Required(ErrorMessage = "Selezionare un tipo lista")]
            [DisplayName("Tipo Lista")]
            public int id_tipo_lista { get; set; }

            [Required(ErrorMessage = "Selezionare tipo servizio")]
            [DisplayName("Tipo Servizio")]
            public int id_tipo_servizio { get; set; }

            //[Required(ErrorMessage = "Selezionare un ordine di visualizzazione")]
            [Required]
            [DisplayName("ordine di visualizzazione")]
            [Range(1, Int32.MaxValue, ErrorMessage = "Formato non valido")]
            public int ordine_visualizzazione { get; set; }


            [Required]
            [DisplayName("Descrizione")]
            public string descrizione_controllo { get; set; }

            [Required]            
            [RegularExpression("[a-zA-Z0-9]{6,6}", ErrorMessage = "Formato Codice non valido (Es: A12345))")]
            [DisplayName("Codice controllo")]
            public string codice_controllo { get; set; }

            [Required(ErrorMessage = "Composto = TRUE or Semplice = FALSE")]
            [DisplayName("Avviso composto")]
            public bool flag_avvpag_composto { get; set; }
            
        }
    }

    public enum TipiControlloEnum
    {
        [Display(Name = "TIPO1")]
        TIPO1 = 1,
        [Display(Name = "TIPO2")]
        TIPO2 = 2,
        [Display(Name = "TIPO3")]
        TIPO3 = 3
    }

    public enum MacrotipoAvvpagEnum
    {
        [Display(Name = "Composto")]
        COMPOSTO = 1,
        [Display(Name = "Semplice")]
        SEMPLICE = 0
    }

    public enum ModalitaEsecuzioneEnum
    {
        [Display(Name = "Automatica")]
        A = 0,
        [Display(Name = "Operatore")]
        O = 1
    }

    public enum TiporecordControlloEnum
    {
        [Display(Name = "Controlli anagrafici")]
        ANA = 0,
        [Display(Name = "Controlli su avvisi")]
        AVV = 1
    }
}
