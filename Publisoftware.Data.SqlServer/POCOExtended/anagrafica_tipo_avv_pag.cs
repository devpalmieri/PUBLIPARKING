using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_avv_pag.Metadata))]
    public partial class anagrafica_tipo_avv_pag : ISoftDeleted
    {

        // Fermo Amministrativo
        //     se FERMO_AMM_OLD = 11 && tab_avv_pag.cod_stato == 'VAL-PFM' è preavviso
        //     se FERMO_AMM_OLD = 11 && tab_avv_pag.cod_stato == 'VAL-FIS' è un fermo

        public const int ServizioAvvisiOrdinari = 0;
        public const int ServizioAvvisiOmessaInfedeleDen = 30;
        public const int ServizioIngiunzioniFiscali = 3;
        public const int ServizioSollecitiPreIngiunzioni = 2;
        public const int ServizioOmessoPagamento = 4;
        public const int ServizioSollecitiPostIngiunzione = 5;
        public const int ServizioIntimazioni = 6;
        public const int ServizioOrdiniAlterzo = 8;
        public const int ServizioPignoramentiImmobiliari = 10;
        public const int ServizioPignoramentiMobiliari = 11;
        public const int ServizioCitazioni = 9;
        public const int ServizioAttiCautelari = 7;
        public const int ServizioRateizzazioni = 26;
        public const int ServizioDichiarazioniStragiudiziali = 29;
        public const int ServizioComunicazioni = 1029;

        public const int FERMO_AMM_OLD = 11;

        public const int FERMO_AMMINISTRATIVO = 250;

        /***************************************************/
        //SOLO PER LOMBARDIA        
        // Natura entrata Tributaria
        public const int SOLLECITO_POST_INGIUNZIONE_T = 304;
        // Natura entrata Generica
        public const int SOLLECITO_POST_INGIUNZIONE_G = 305;
        // Natura entrata Extra Tributaria
        public const int SOLLECITO_POST_INGIUNZIONE_E = 306;
        /***************************************************/

        // Natura entrata Generica
        public const int PRIMO_SOLLECITO_POST_INGIUNZIONE_G = 307;
        // Natura entrata Tributaria
        public const int PRIMO_SOLLECITO_POST_INGIUNZIONE_T = 308;
        // Natura entrata Extra Tributaria
        public const int PRIMO_SOLLECITO_POST_INGIUNZIONE_E = 309;

        public const int SOLLECITO_POST_INGIUNZIONE_G_CON_ONERI = 315;

        // Natura entrata Generica
        public const int INTIMAZIONE_INGIUNZIONE = 301;

        public const int PRE_FERMO_AMM = 210; // Preavviso di Fermo Amministrativo

        public const int IPOTECA = 214;
        public const int PRE_IPOTECA = 213;

        // Avviso di Pignoramento dei Crediti del Debitore verso Terzi
        public const int PIGN_TERZI = 67;       // -               

        // Avviso di Pignoramento dei Crediti del Debitore verso Terzi - Pensioni
        public const int PIGN_TERZI_PEN = 73;
        public const int PIGN_IMMOB = 201;
        public const int PIGN_MOB = 202;
        // Avviso di Pignoramento con Citazione del Terzo
        public const int PIGN_TERZI_CIT = 216; // -
        // Avviso di Pignoramento con ordine al terzo
        public const int PIGN_TERZI_ORD = 212; // -

        public const int SOLLECITO_DI_PAG = 104;
        public const int SOLLECITO_DI_PAG_CDS = 1420;
        public const int INTIM_DI_PAG = 60;
        public const int INTIM_DI_PAG_ACQ = 72;

        public const string FLAG_NOTIFICA_NO = "0";
        public const string FLAG_NOTIFICA_SI = "1";

        public const string FLAG_TIPO_COMPOSTO = "1";


        public const string FLAG_NATURA_E = "E";
        public const string FLAG_NATURA_T = "T";
        public const string FLAG_NATURA_G = "G";

        public const int DOMANDA_AMMISSIONE_AL_PASSIVO = 205;
        public const int RICHIESTA_STRAGIUDIZIALE = 251;
        public const int ING_FISC = 15;//207;
        public const int ING_FISC_SANZIONE_CDS = 300;//207;
        public const int ING_FISC_SANZIONE_689 = 402;
        public const int ING_FISC_CDS = 131;
        public const int VERBALE_CDS = 409;
        public const int AVVISO_NON_DISPONIBILE = 27;

        public const int RATEIZZAZIONE_COATTIVO_ACCERTAMENTI_ATTI_COATTIVI = 203;
        public const int RATEIZZAZIONE_SINGOLA = 311;
        //public const int RATEIZZAZIONE_ACCERTAMENTI = 3469;
        public const int RATEIZZAZIONE_SINGOLA_OLTRETERMINI = 2467;
        public const int DEFINIZIONE_AGEVOLATA = 1419;
        public const int DEFINIZIONE_AGEVOLATA2 = 3466;
        public const int DEFINIZIONE_AGEVOLATA3 = 3464;

        public const int AVVISO_ORDINARIO_ICI = 14;
        public const int AVVISO_ORDINARIO_IMU = 120;
        public const int AVVISO_ACCERTAMENTO_ESECUTIVO_IMU = 509;

        //---------------------
        public const int AVVISO_ORDINARIO_TARI = 106;
        public const int AVVISO_ORDINARIO_TARI_NOT = 124;
        public const int AVVISO_SUPPLETIVO_TARI = 115;
        public const int AVVISO_SUPPLETIVO_TARI_NOT = 116;

        public const int AVVISO_ORD_TARI_POP = 119;
        public const int AVVISO_SUPPLETIVO_TARI_POP = 127;
        //---------------------


        public const int AVVISO_ACC_OMESSA_TARSU = 12;
        public const int AVVISO_ACC_INFEDELE_TARSU = 13;
        public const int AVVISO_ACC_OMESSA_TARI = 128;
        public const int AVVISO_ACC_INFEDELE_TARI = 130;

        public const int AVVISO_ACC_OMESSA_ESEC_TARI = 511;
        public const int AVVISO_ACC_INFEDELE_ESEC_TARI = 512;


        public const int AVVISO_AOP_TARI = 126;
        public const int AVVISO_AOP_POP_TARI = 121;
        public const int AVVISO_AOP_TARI_ESECUTIVO = 501;

        public const int AVVISO_LMP_VOTIVE = 411;

        public const int AVVISO_ORD_ICP = 62;
        public const int AVVISO_ORD_ICP_NOT = 63;

        public const int AVVISO_ORD_TOSAP = 65;
        public const int AVVISO_ORD_COSAP = 55;
        public const int AVVISO_ORD_COSAP_RUTA = 56;
        public const int AVVISO_ORD_TARIG = 64;
        //---------------------

        public const int FATTURA_SPESE_CONTRATTUALI = 26;

        public const int ING_FISC_LOMBARDIA1 = 206;
        public const int ING_FISC_LOMBARDIA2 = 233;
        public const int ING_FISC_LOMBARDIA3 = 243;
        public const int ING_FISC_ONERI_CONCESSORI = 407;

        public const int AVVISO_POP = 119;
        public const int AVVISO_POP_SUPPLETIVO = 127;

        public const int AVVISO_COSTITUZIONE_MORA = 28;

        public const int PREAVVISO_FATTURAZIONE = 47;

        public const int AFFISSIONE_MANIFESTI = 3477;

        public const int AREE_MERCATO_SPUNTISTI = 3478;
        public const int ID_ENTRATA_AREE_MERCATO_SPUNTISTI = 1224;
        public const int ID_ENTRATA_AFFISSIONI = 1222;

        public const int TIPO_AVV_PAG_CDS = 93;
        public const int ID_ENTRATA_CDS = 6;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        // -------------------------------------------------------
        // Significato campi id_servizio && id_servizio_true

        /// <summary>
        /// id_servizio: anagrafica_servizi.id_tipo_servizio (FK) &lt;==&gt; anagrafica_tipo_servizi.id_tipo_servizio (PK)
        /// </summary>
        public int AnagraficaServizi_IdTipoServizio
        {
            get { return id_servizio; }
        }

        /// <summary>
        /// id_servizio: anagrafica_servizi.id_tipo_servizio (FK) &lt;==&gt; anagrafica_tipo_servizi.id_tipo_servizio (PK)
        /// </summary>
        public int AnagraficaTipoServizi_IdTipoServizio
        {
            get { return id_servizio; }
        }

        /// <summary>
        /// id_serviziotrue: anagrafica_servizi.id_servizio (PK)
        /// </summary>
        public int? AnagraficaServizi_IdServizio
        {
            get { return id_serviziotrue; }
        }

        // -------------------------------------------------------

        public bool IsAttoSuccessivoIngiunzione
        {
            get
            {
                return id_servizio == anagrafica_tipo_servizi.INTIM ||
                       id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA ||
                       id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI ||
                       id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                       id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                       id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                       id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI;
            }
        }

        [DisplayName("Tipo avviso")]
        public string TipoAvviso
        {
            get
            {
                string v_testo = cod_tipo_avv_pag + " / " + descr_tipo_avv_pag;

                return v_testo;
            }
        }

        [DisplayName("Tipo avviso")]
        public string TipoAvvisoEsteso
        {
            get
            {
                string v_testo = cod_tipo_avv_pag + " / " + descr_tipo_avv_pag;

                if (id_servizio == anagrafica_tipo_servizi.ING_FISC)
                {
                    switch (flag_natura_avv_collegati)
                    {
                        case "T":
                            v_testo = v_testo + " (Entrate Tributarie)";
                            break;
                        case "G":
                            v_testo = v_testo + " (Tutte le Entrate)";
                            break;
                        case "E":
                            v_testo = v_testo + " (Entrate Extra Tributarie)";
                            break;
                        default:
                            break;
                    }
                }

                return v_testo;
            }
        }

        public string DescrizioneEntrataCollegata
        {
            get
            {
                return anagrafica_entrate1 != null ? anagrafica_entrate1.descrizione_entrata : string.Empty;
            }

        }

        public string TipoLista
        {
            get
            {
                //return "Lista di trasmissione " + descr_tipo_avv_pag;
                return descr_tipo_avv_pag;
            }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

            [Required]
            [DisplayName("Id")]
            public int id_tipo_avvpag { get; set; }

            [Required]
            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [Required]
            [DisplayName("Tipo servizio")]
            public int id_servizio { get; set; }

            [DisplayName("Tipo servizio")]
            public int id_serviziotrue { get; set; }

            [DisplayName("Codice tipo avviso")]
            [Required]
            public string cod_tipo_avv_pag { get; set; } //SS

            [DisplayName("Descrizione Tipo Avv Pag")]
            [Required]
            public string descr_tipo_avv_pag { get; set; }

            [DisplayName("Descrizione abbreviata")]
            public string descr_abbreviata { get; set; }

            [DisplayName("Notifica")]
            [Required]
            public string flag_notifica { get; set; } //SS

            [DisplayName("Tipo composto")]
            [Required]
            public string flag_tipo_composto { get; set; } //SS

            [Required]
            public string flag_doppio_aggio { get; set; } // Non più utilizzato!

            [Required]
            [DisplayName("Natura avviso collegato")]
            public string flag_natura_avv_collegati { get; set; }

            [DisplayName("Entrata avviso collegato")]
            public Nullable<int> id_entrata_avvpag_collegati { get; set; }

            // ---------------------------------------------------------------------
            [DisplayName("Stato avviso collegato")]
            public Nullable<int> id_stato_avvpag_collegati { get; set; }

            [DisplayName("Codice stato avviso collegato")]
            public string cod_stato_avvpag_collegati { get; set; }
            // ---------------------------------------------------------------------

            [DisplayName("Tipo servizio atti successivi")]
            public Nullable<int> id_tipo_servizio_avvpag_successivi { get; set; }
        }
    }
}
