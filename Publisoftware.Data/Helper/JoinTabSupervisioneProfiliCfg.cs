using System;
using System.Collections.Generic;

namespace Publisoftware.Data.BD.Helper
{
    public class JoinTabSupervisioneProfiliCfg
    {
        public const int TP_ISCRIZIONE_FERMO = 0;
        public const int TP_REVOCA_FERMO = 1;
        public const int TP_CANCELLAZIONE_FERMO_VA = 2;
        public const int TP_CANCELLAZIONE_FERMO_IN = 3;
        public const int TP_ANNULLA_FORMALITA = 4;
        
        /// <summary>
        /// Tipo di operazione F.A. ACI
        /// </summary>
        public enum TipoPratica_t
        {
            /// <summary>
            /// Iscrizione di un F.A.
            /// </summary>
            iscrizione_fermo = TP_ISCRIZIONE_FERMO,
            /// <summary>
            /// Revoca F.A.
            /// </summary>
            revoca_fermo = TP_REVOCA_FERMO, // NON IMPLEMENTATO dall' ACI
            /// <summary>
            /// Annullamento F.A. per [V]endita [A]nteriore
            /// </summary>
            cancellazione_fermo_va = TP_CANCELLAZIONE_FERMO_VA,
            /// <summary>
            /// Cancellazione F.A. per [IN]debita iscrizione
            /// </summary>
            cancellazione_fermo_in = TP_CANCELLAZIONE_FERMO_IN,
            /// <summary>
            /// Annulla una formalità - solo per i VAL-PRE e solo in giornata stessa
            /// </summary>
            [Obsolete("Non implementato - si può fare solo per i VAL-PRE e solo in giornata stessa", false)]
            annulla_formalita = TP_ANNULLA_FORMALITA // NON IMPLEMENTATO!
        }

        public const int OP_CREAPRATICA = 0;
        public const int OP_PRESENTAFORMALITA = 1;
        public const int OP_LEGGIPRATICA = 2;
        
        // Operazioni da effettuare
        public enum Operazione_t
        {
            // autoInBaseAOrario,

            creaPratica = OP_CREAPRATICA,
            presentaFormalita = OP_PRESENTAFORMALITA,
            leggiPratica = OP_LEGGIPRATICA
            
#if NON_CANCELLARE
            //, leggiRicevutaFormalita
            // annullaFormalita
#endif
        }

        //public static readonly IList<string> ErrorStateList;
        public static readonly IDictionary<string, string> ErrorFromTo;
        
        public static readonly IDictionary<int, IDictionary<string, string>> DescrizioneStatiPerOperazioneCreaPratica;
        public static readonly IDictionary<int, IDictionary<string, string>> DescrizioneStatiPerOperazionePresentaFormalita;
        public static readonly IDictionary<int, IDictionary<string, string>> DescrizioneStatiPerOperazioneLeggiPratica;


        public static readonly IList<string> StatiCreaPraticaIscrizione;
        public static readonly IList<string> StatiCreaPraticaRevoca;
        public static readonly IList<string> StatiCreaPraticaCancellazioneVA;
        public static readonly IList<string> StatiCreaPraticaCancellazioneIN;

        public static readonly IList<string> StatiPresentaFormalitaIscrizione;
        public static readonly IList<string> StatiPresentaFormalitaRevoca;
        public static readonly IList<string> StatiPresentaFormalitaCancellazioneVA;
        public static readonly IList<string> StatiPresentaFormalitaCancellazioneIN;

        public static readonly IList<string> StatiLeggiPraticaIscrizione;
        public static readonly IList<string> StatiLeggiPraticaRevoca;
        public static readonly IList<string> StatiLeggiPraticaCancellazioneVA;
        public static readonly IList<string> StatiLeggiPraticaCancellazioneIN;

        static JoinTabSupervisioneProfiliCfg()
        {
            StatiCreaPraticaIscrizione = new List<string>
            {
                join_tab_supervisione_profili.VAL_DIS,
                join_tab_supervisione_profili.VAL_IF1,
                join_tab_supervisione_profili.VAL_IF2
            };
            StatiCreaPraticaRevoca = new List<string>
            {
                join_tab_supervisione_profili.REV_DIS,
                join_tab_supervisione_profili.REV_DI1,
                join_tab_supervisione_profili.REV_DI2
            };
            // StatiCreaPraticaCancellazione = new List<string> {
            //     join_tab_supervisione_profili.CAN_DIS,
            //     join_tab_supervisione_profili.CAN_CA1,
            //     join_tab_supervisione_profili.CAN_CA2
            // };
            StatiCreaPraticaCancellazioneVA = new List<string>
            {
                join_tab_supervisione_profili.CVA_DIS,
                join_tab_supervisione_profili.CVA_CA1,
                join_tab_supervisione_profili.CVA_CA2
            };
            StatiCreaPraticaCancellazioneIN = new List<string>
            {
                join_tab_supervisione_profili.CIN_DIS,
                join_tab_supervisione_profili.CIN_CA1,
                join_tab_supervisione_profili.CIN_CA2
            };

            StatiPresentaFormalitaIscrizione = new List<string>
            {
                join_tab_supervisione_profili.VAL_CRE,
                join_tab_supervisione_profili.VAL_CR1,
                join_tab_supervisione_profili.VAL_CR2
            };
            StatiPresentaFormalitaRevoca = new List<string>
            {
                join_tab_supervisione_profili.REV_CRE,
                join_tab_supervisione_profili.REV_CR1,
                join_tab_supervisione_profili.REV_CR2
            };
            // StatiPresentaFormalitaCancellazione = new List<string> {
            //     join_tab_supervisione_profili.CAN_CRE,
            //     join_tab_supervisione_profili.CAN_CR1,
            //     join_tab_supervisione_profili.CAN_CR2
            // };
            StatiPresentaFormalitaCancellazioneVA = new List<string>
            {
                join_tab_supervisione_profili.CVA_CRE,
                join_tab_supervisione_profili.CVA_CR1,
                join_tab_supervisione_profili.CVA_CR2
            };
            StatiPresentaFormalitaCancellazioneIN = new List<string>
            {
                join_tab_supervisione_profili.CIN_CRE,
                join_tab_supervisione_profili.CIN_CR1,
                join_tab_supervisione_profili.CIN_CR2
            };

            StatiLeggiPraticaIscrizione = new List<string>
            {
                join_tab_supervisione_profili.VAL_PRE,
                join_tab_supervisione_profili.VAL_PR1,
                join_tab_supervisione_profili.VAL_PR2,
                join_tab_supervisione_profili.VAL_RECUPERA
            };
            StatiLeggiPraticaRevoca = new List<string>
            {
                join_tab_supervisione_profili.REV_PRE,
                join_tab_supervisione_profili.REV_PR1,
                join_tab_supervisione_profili.REV_PR2
            };
            // StatiLeggiPraticaCancellazione = new List<string> {
            //     join_tab_supervisione_profili.CAN_PRE,
            //     join_tab_supervisione_profili.CAN_PR1,
            //     join_tab_supervisione_profili.CAN_PR2
            // };
            StatiLeggiPraticaCancellazioneVA = new List<string>
            {
                join_tab_supervisione_profili.CVA_PRE,
                join_tab_supervisione_profili.CVA_PR1,
                join_tab_supervisione_profili.CVA_PR2
            };
            StatiLeggiPraticaCancellazioneIN = new List<string>
            {
                join_tab_supervisione_profili.CIN_PRE,
                join_tab_supervisione_profili.CIN_PR1,
                join_tab_supervisione_profili.CIN_PR2
            };

            // ----------------------------------------------------------------------------------------------------
            // Definizione passaggio di stato in caso di errori
            
            ErrorFromTo = new Dictionary<string, string>
            {
                // ------------------------------------------------------------------------------------
                // Crea pratica

                // Crea Pratica Iscrizione
                {join_tab_supervisione_profili.VAL_DIS, join_tab_supervisione_profili.VAL_IF1},
                {join_tab_supervisione_profili.VAL_IF1, join_tab_supervisione_profili.VAL_IF2},
                {join_tab_supervisione_profili.VAL_IF2, join_tab_supervisione_profili.ANN_IF3}, // ex ANN_IFF

                // Crea Pratica Revoca
                {join_tab_supervisione_profili.REV_DIS, join_tab_supervisione_profili.REV_DI1},
                {join_tab_supervisione_profili.REV_DI1, join_tab_supervisione_profili.REV_DI2},
                {join_tab_supervisione_profili.REV_DI2, join_tab_supervisione_profili.ANN_REV},
                
                // Crea Pratica Cancellazione OBSOLETI (no distinzione VA da IN)
                // {join_tab_supervisione_profili.CAN_DIS, join_tab_supervisione_profili.CAN_CA1},
                // {join_tab_supervisione_profili.CAN_CA1, join_tab_supervisione_profili.CAN_CA2},
                // {join_tab_supervisione_profili.CAN_CA2, join_tab_supervisione_profili.ANN_CAN},

                // Crea Pratica Cancellazione VA
                {join_tab_supervisione_profili.CVA_DIS, join_tab_supervisione_profili.CVA_CA1},
                {join_tab_supervisione_profili.CVA_CA1, join_tab_supervisione_profili.CVA_CA2},
                {join_tab_supervisione_profili.CVA_CA2, join_tab_supervisione_profili.ANN_CA3}, //ex ambiguo ANN_CVA

                // Crea Pratica Cancellazione IN
                {join_tab_supervisione_profili.CIN_DIS, join_tab_supervisione_profili.CIN_CA1},
                {join_tab_supervisione_profili.CIN_CA1, join_tab_supervisione_profili.CIN_CA2},
                {join_tab_supervisione_profili.CIN_CA2, join_tab_supervisione_profili.ANN_CI3}, // ex ambiguo ANN_CIN

                // ------------------------------------------------------------------------------------
                // Presenta formalità

                // Presenta formalità iscrizione
                {join_tab_supervisione_profili.VAL_CRE, join_tab_supervisione_profili.VAL_CR1},
                {join_tab_supervisione_profili.VAL_CR1, join_tab_supervisione_profili.VAL_CR2},
                {join_tab_supervisione_profili.VAL_CR2, join_tab_supervisione_profili.ANN_VCR},

                // Presenta formalità revoca
                {join_tab_supervisione_profili.REV_CRE, join_tab_supervisione_profili.REV_CR1},
                {join_tab_supervisione_profili.REV_CR1, join_tab_supervisione_profili.REV_CR2},
                {join_tab_supervisione_profili.REV_CR2, join_tab_supervisione_profili.ANN_RCR},

                // ---
                // Presenta formalità cancellazione - OBSOLETO: non distingueva tra VA e IN
                // {join_tab_supervisione_profili.CAN_CRE, join_tab_supervisione_profili.CAN_CR1},
                // {join_tab_supervisione_profili.CAN_CR1, join_tab_supervisione_profili.CAN_CR2},
                // {join_tab_supervisione_profili.CAN_CR2, join_tab_supervisione_profili.ANN_CRC},
                // ---

                // Presenta formalità cancellazione VA
                {join_tab_supervisione_profili.CVA_CRE, join_tab_supervisione_profili.CVA_CR1},
                {join_tab_supervisione_profili.CVA_CR1, join_tab_supervisione_profili.CVA_CR2},
                {join_tab_supervisione_profili.CVA_CR2, join_tab_supervisione_profili.ANN_CRA},

                // Presenta formalità cancellazione IN
                {join_tab_supervisione_profili.CIN_CRE, join_tab_supervisione_profili.CIN_CR1},
                {join_tab_supervisione_profili.CIN_CR1, join_tab_supervisione_profili.CIN_CR2},
                {join_tab_supervisione_profili.CIN_CR2, join_tab_supervisione_profili.ANN_CRI},

                // ------------------------------------------------------------------------------------
                // Leggi pratica

                // leggi pratica iscrizione
                {join_tab_supervisione_profili.VAL_PRE, join_tab_supervisione_profili.VAL_PR1},
                {join_tab_supervisione_profili.VAL_PR1, join_tab_supervisione_profili.VAL_PR2},
                {join_tab_supervisione_profili.VAL_RECUPERA, join_tab_supervisione_profili.ANN_VPR},
                {join_tab_supervisione_profili.VAL_PR2, join_tab_supervisione_profili.ANN_VPR}, // ex ANN_VPR

                // leggi pratica revoca
                {join_tab_supervisione_profili.REV_PRE, join_tab_supervisione_profili.REV_PR1},
                {join_tab_supervisione_profili.REV_PR1, join_tab_supervisione_profili.REV_PR2},
                {join_tab_supervisione_profili.REV_PR2, join_tab_supervisione_profili.ANN_RPR},

                // leggi pratica cancellazione - no non distingueva VA e IN
                // {join_tab_supervisione_profili.CAN_PRE, join_tab_supervisione_profili.CAN_PR1},
                // {join_tab_supervisione_profili.CAN_PR1, join_tab_supervisione_profili.CAN_PR2},
                // {join_tab_supervisione_profili.CAN_PR2, join_tab_supervisione_profili.ANN_CPR}

                // leggi pratica cancellazione VA
                {join_tab_supervisione_profili.CVA_PRE, join_tab_supervisione_profili.CVA_PR1},
                {join_tab_supervisione_profili.CVA_PR1, join_tab_supervisione_profili.CVA_PR2},
                {join_tab_supervisione_profili.CVA_PR2, join_tab_supervisione_profili.ANN_PRA},

                // leggi pratica cancellazione IN
                {join_tab_supervisione_profili.CIN_PRE, join_tab_supervisione_profili.CIN_PR1},
                {join_tab_supervisione_profili.CIN_PR1, join_tab_supervisione_profili.CIN_PR2},
                {join_tab_supervisione_profili.CIN_PR2, join_tab_supervisione_profili.ANN_PRI}
            };

            // ----------------------------------------------------------------------------------------------------
            // Definizione descrizione stati
            // N.B.
            //         TP_REVOCA_FERMO li definisci quando l'ACI permetterà di effettuare revoche massive
            //         TP_ANNULLA_FORMALITA non è implementabile in modo massivo (o perlomeno non solo in base agli stati)
            
            // Associa TipoPratica_t con gli stati e le descrizioni degli stati (per uso web)
            DescrizioneStatiPerOperazioneCreaPratica = new Dictionary<int, IDictionary<string, string>>();
            DescrizioneStatiPerOperazionePresentaFormalita = new Dictionary<int, IDictionary<string, string>>();
            DescrizioneStatiPerOperazioneLeggiPratica = new Dictionary<int, IDictionary<string, string>>();


            // ------- CreaPratica

            DescrizioneStatiPerOperazioneCreaPratica[TP_ISCRIZIONE_FERMO] = new Dictionary<string, string>
            {
                // Crea pratica - iscrizione
                {join_tab_supervisione_profili.VAL_DIS, "fermi da iscrivere"},
                {join_tab_supervisione_profili.VAL_IF1, "fermi da iscrivere - errore primo tentativo"},
                {join_tab_supervisione_profili.VAL_IF2, "fermi da iscrivere - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_IF3, "fermi da iscrivere - errore terzo e ultimo tentativo"},
                
                {join_tab_supervisione_profili.ANN_IFF, "iscrizione fermi - iscrizione e supervisione annullate"},
                
                {join_tab_supervisione_profili.ANN_ANN, "iscrizione fermi - annullato con supervisione perché risultano annullati gli avvisi corrispondenti"},
                
                // {join_tab_supervisione_profili.VAL_CRE, "fermi da iscrivere in attesa di presentazione all'ACI"},
                
                {join_tab_supervisione_profili.IFF_ERR, "iscrizione fermi - errore risposta ACI"}
            };
            DescrizioneStatiPerOperazioneCreaPratica[TP_REVOCA_FERMO] = new Dictionary<string, string>
            {
                // Crea pratica - revoke
                {join_tab_supervisione_profili.REV_DIS, "revoca fermi"},
                {join_tab_supervisione_profili.REV_DI1, "revoca fermi - errore primo tentativo"},
                {join_tab_supervisione_profili.REV_DI2, "revoca fermi - errore secondo tentativo"},
                
                // {join_tab_supervisione_profili.REV_DI3, "fermi da revocare - errore terzo e ultimo tentativo"},
                {join_tab_supervisione_profili.ANN_REV, "fermi da revocare - errore terzo e ultimo tentativo"},
                
                // probabilmente è  già stato revocato a mano:
                {join_tab_supervisione_profili.ANN_RNV, "revoca fermi - annullati perché vincoli assenti"},

                // {join_tab_supervisione_profili.REV_CRE, "fermi da revocare in attesa di presentazione all'ACI"},
                
                {join_tab_supervisione_profili.RFF_ERR, "revoca fermi - errore risposta ACI"},
            };
            DescrizioneStatiPerOperazioneCreaPratica[TP_CANCELLAZIONE_FERMO_VA] = new Dictionary<string, string>
            {
                // Crea pratica - cancellazione v.a.
                {join_tab_supervisione_profili.CVA_DIS, "cancellazione per v.a. - fermo cancellato"},
                {join_tab_supervisione_profili.CVA_CA1, "fermi da cancellare per v.a. - errore primo tentativo"},
                {join_tab_supervisione_profili.CVA_CA2, "fermi da cancellare per v.a. - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_CA3, "fermi da cancellare per v.a. - errore terzo e ultimo tentativo"},
                
                {join_tab_supervisione_profili.ANN_CVA, "cancellazione per v.a. - cancellazione e supervisione annullate"},
                
                {join_tab_supervisione_profili.ANN_VNV, "cancellazione per v.a. - annullato con supervisione perché vincoli assenti "},
                
                // {join_tab_supervisione_profili.CVA_CRE, "fermi da cancellare per v.a. in attesa di presentazione all'ACI"},
                
                {join_tab_supervisione_profili.VFF_ERR, "cancellazione per v.a. - errore risposta ACI"},
            };
            DescrizioneStatiPerOperazioneCreaPratica[TP_CANCELLAZIONE_FERMO_IN] = new Dictionary<string, string>
            {
                // Crea pratica - cancellazione i.n.
                {join_tab_supervisione_profili.CIN_DIS, "fermi da cancellare per in.isc."},
                {join_tab_supervisione_profili.CIN_CA1, "fermi da cancellare per in.isc. - errore primo tentativo"},
                {join_tab_supervisione_profili.CIN_CA2, "fermi da cancellare per in.isc. - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_CI3, "fermi da cancellare per in.isc. - errore terzo e ultimo tentativo"},
                
                {join_tab_supervisione_profili.ANN_CIN, "fermi da cancellare per in.isc. - cancellazione e supervisione annullate"},
                
                {join_tab_supervisione_profili.ANN_CNV, "fermi da cancellare per in.isc. - annullato con supervisione perché vincoli assenti "},
                
                // {join_tab_supervisione_profili.CIN_CRE, "fermi da cancellare per in.isc. in attesa di presentazione all'ACI"},
                
                {join_tab_supervisione_profili.NFF_ERR, "fermi da cancellare per in.isc. - errore risposta ACI"},
            };

            // ------- PresentaFormalita
            
            DescrizioneStatiPerOperazionePresentaFormalita[TP_ISCRIZIONE_FERMO] = new Dictionary<string, string> 
            {
                {join_tab_supervisione_profili.VAL_CRE, "fermi da iscrivere in attesa di presentazione all'ACI"},
                {join_tab_supervisione_profili.VAL_CR1, "fermi da iscrivere in attesa di presentazione all'ACI - errore primo tentativo"},
                {join_tab_supervisione_profili.VAL_CR2, "fermi da iscrivere in attesa di presentazione all'ACI - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_VCR, "fermi da iscrivere in attesa di presentazione all'ACI - errore terzo ed ultimo tentativo"},
                // // {join_tab_supervisione_profili.ANN_VCR, "fermi da iscrivere in attesa di presentazione all'ACI - errore terzo tentativo"},
                // {join_tab_supervisione_profili.VAL_PRE, "fermi da iscrivere - in attesa di lettura risposta ACI"},
            };
            DescrizioneStatiPerOperazionePresentaFormalita[TP_REVOCA_FERMO] = new Dictionary<string, string>
            {
                {join_tab_supervisione_profili.REV_CRE, "fermi da revocare in attesa di presentazione all'ACI"},
                {join_tab_supervisione_profili.REV_CR1, "fermi da revocare in attesa di presentazione all'ACI - errore primo tentativo"},
                {join_tab_supervisione_profili.REV_CR2, "fermi da revocare in attesa di presentazione all'ACI - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_RCR, "fermi da revocare in attesa di presentazione all'ACI - errore terzo ed ultimo tentativo"},

                // {join_tab_supervisione_profili.REV_PRE, "fermi da iscrivere - in attesa di lettura risposta ACI"},
            };
            DescrizioneStatiPerOperazionePresentaFormalita[TP_CANCELLAZIONE_FERMO_VA] = new Dictionary<string, string> 
            {
                {join_tab_supervisione_profili.CVA_CRE, "fermi da cancellare per v.a. in attesa di presentazione all'ACI"},
                {join_tab_supervisione_profili.CVA_CR1, "fermi da cancellare per v.a. in attesa di presentazione all'ACI - errore primo tentativo"},
                {join_tab_supervisione_profili.CVA_CR2, "fermi da cancellare per v.a. in attesa di presentazione all'ACI - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_CRA, "fermi da cancellare per v.a. in attesa di presentazione all'ACI - errore terzo ed ultimo tentativo"},
                
                // {join_tab_supervisione_profili.CVA_PRE, "fermi da cancellare per v.a. - in attesa di lettura risposta ACI"},
            };
            DescrizioneStatiPerOperazionePresentaFormalita[TP_CANCELLAZIONE_FERMO_IN] = new Dictionary<string, string> 
            {
                {join_tab_supervisione_profili.CIN_CRE, "fermi da cancellare per in.isc. in attesa di presentazione all'ACI"},
                {join_tab_supervisione_profili.CIN_CR1, "fermi da cancellare per in.isc. in attesa di presentazione all'ACI - errore primo tentativo"},
                {join_tab_supervisione_profili.CIN_CR2, "fermi da cancellare per in.isc. in attesa di presentazione all'ACI - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_CRI, "fermi da cancellare per in.isc. in attesa di presentazione all'ACI - errore terzo ed ultimo tentativo"},

                // {join_tab_supervisione_profili.CIN_PRE, "fermi da cancellare per in.isc. - in attesa di lettura risposta ACI"},
            };

            // ------- LeggiPratica

            DescrizioneStatiPerOperazioneLeggiPratica[TP_ISCRIZIONE_FERMO] = new Dictionary<string, string> 
            {
                {join_tab_supervisione_profili.VAL_PRE, "fermi da iscrivere in attesa di lettura risposta ACI"},
                {join_tab_supervisione_profili.VAL_PR1, "fermi da iscrivere in attesa di lettura risposta ACI - errore primo tentativo"},
                {join_tab_supervisione_profili.VAL_PR2, "fermi da iscrivere in attesa di lettura risposta ACI - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_VPR, "fermi da iscrivere in attesa di lettura risposta ACI - errore terzo ed ultimo tentativo"},
                {join_tab_supervisione_profili.ISC_ERR, "fermi da iscrivere in attesa di lettura risposta ACI - errore risposta ACI"},
                {join_tab_supervisione_profili.VAL_ISC, "fermi iscritti"}
            };
            DescrizioneStatiPerOperazioneLeggiPratica[TP_REVOCA_FERMO] = new Dictionary<string, string>
            {
                {join_tab_supervisione_profili.REV_PRE, "fermi da revocare in attesa di lettura risposta ACI"},
                {join_tab_supervisione_profili.REV_PR1, "fermi da revocare in attesa di lettura risposta ACI - errore primo tentativo"},
                {join_tab_supervisione_profili.REV_PR2, "fermi da revocare in attesa di lettura risposta ACI - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_RPR, "fermi da revocare in attesa di lettura risposta ACI - errore terzo ed ultimo tentativo"},
                {join_tab_supervisione_profili.REV_ERR, "fermi da revocare in attesa di lettura risposta ACI - errore risposta ACI"},
                {join_tab_supervisione_profili.REV_ISC, "fermi revocati"}
            };
            DescrizioneStatiPerOperazioneLeggiPratica[TP_CANCELLAZIONE_FERMO_VA] = new Dictionary<string, string> 
            {
                {join_tab_supervisione_profili.CVA_PRE, "fermi da cancellare per v.a. in attesa di lettura risposta ACI"},
                {join_tab_supervisione_profili.CVA_PR1, "fermi da cancellare per v.a. in attesa di lettura risposta ACI - errore primo tentativo"},
                {join_tab_supervisione_profili.CVA_PR2, "fermi da cancellare per v.a. in attesa di lettura risposta ACI - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_PRA, "fermi da cancellare per v.a. in attesa di lettura risposta ACI - errore terzo ed ultimo tentativo"},
                {join_tab_supervisione_profili.CVA_ERR, "fermi da cancellare per v.a. in attesa di lettura risposta ACI - errore risposta ACI"},
                {join_tab_supervisione_profili.CVA_ISC, "fermi cancellati per v.a. (con supervisione)"},
                {join_tab_supervisione_profili.CVM_ISC, "fermi cancellati per v.a. in modo non automatico (a mezzo PEC o altro)"},
            };
            DescrizioneStatiPerOperazioneLeggiPratica[TP_CANCELLAZIONE_FERMO_IN] = new Dictionary<string, string> 
            {
                {join_tab_supervisione_profili.CIN_PRE, "fermi da cancellare per in.isc. in attesa di lettura risposta ACI"},
                {join_tab_supervisione_profili.CIN_PR1, "fermi da cancellare per in.isc. in attesa di lettura risposta ACI - errore primo tentativo"},
                {join_tab_supervisione_profili.CIN_PR2, "fermi da cancellare per in.isc. in attesa di lettura risposta ACI - errore secondo tentativo"},
                {join_tab_supervisione_profili.ANN_PRI, "fermi da cancellare per in.isc. in attesa di lettura risposta ACI - errore terzo ed ultimo tentativo"},
                // {join_tab_supervisione_profili.ANN_PRI, "fermi da cancellare per in.isc. in attesa di lettura risposta ACI - errore terzo tentativo"},
                {join_tab_supervisione_profili.CIN_ERR, "fermi da cancellare per in.isc. in attesa di lettura risposta ACI - errore risposta ACI"},
                {join_tab_supervisione_profili.CIN_ISC, "fermi cancellati per in.isc. (con supervisione)"},
            };
        } // static ctor

        // Il ref è ovviamenet inutile, ma specifica la semantica
        private static void AppendFromTo(IDictionary<string, string> from, ref IDictionary<string, string> to)
        {
            foreach (var kvp in from)
            {
                if (!to.ContainsKey(kvp.Key))
                {
                    to.Add(kvp);
                }
            }
        }

        public static IDictionary<string, string> GetStatiDescrizioniDic(int tipoPratica, int ? tipoOperazione)
        {
            IDictionary<string, string> statiDescrizioniDic = new Dictionary<string, string>();
            if (tipoPratica == TP_ISCRIZIONE_FERMO)
            {
                if (tipoOperazione == OP_CREAPRATICA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneCreaPratica[TP_ISCRIZIONE_FERMO], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == JoinTabSupervisioneProfiliCfg.OP_PRESENTAFORMALITA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazionePresentaFormalita[TP_ISCRIZIONE_FERMO], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == JoinTabSupervisioneProfiliCfg.OP_LEGGIPRATICA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneLeggiPratica[TP_ISCRIZIONE_FERMO], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == null)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneCreaPratica[TP_ISCRIZIONE_FERMO], ref statiDescrizioniDic);
                    AppendFromTo(DescrizioneStatiPerOperazionePresentaFormalita[TP_ISCRIZIONE_FERMO], ref statiDescrizioniDic);
                    AppendFromTo(DescrizioneStatiPerOperazioneLeggiPratica[TP_ISCRIZIONE_FERMO], ref statiDescrizioniDic);
                }
            }
            else if (tipoPratica == JoinTabSupervisioneProfiliCfg.TP_CANCELLAZIONE_FERMO_VA)
            {
                if (tipoOperazione == OP_CREAPRATICA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneCreaPratica[TP_CANCELLAZIONE_FERMO_VA], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == JoinTabSupervisioneProfiliCfg.OP_PRESENTAFORMALITA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazionePresentaFormalita[TP_CANCELLAZIONE_FERMO_VA], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == JoinTabSupervisioneProfiliCfg.OP_LEGGIPRATICA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneLeggiPratica[TP_CANCELLAZIONE_FERMO_VA], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == null)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneCreaPratica[TP_CANCELLAZIONE_FERMO_VA], ref statiDescrizioniDic);
                    AppendFromTo(DescrizioneStatiPerOperazionePresentaFormalita[TP_CANCELLAZIONE_FERMO_VA], ref statiDescrizioniDic);
                    AppendFromTo(DescrizioneStatiPerOperazioneLeggiPratica[TP_CANCELLAZIONE_FERMO_VA], ref statiDescrizioniDic);
                }
            }
            else if (tipoPratica == JoinTabSupervisioneProfiliCfg.TP_CANCELLAZIONE_FERMO_IN)
            {
                if (tipoOperazione == OP_CREAPRATICA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneCreaPratica[TP_CANCELLAZIONE_FERMO_IN], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == JoinTabSupervisioneProfiliCfg.OP_PRESENTAFORMALITA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazionePresentaFormalita[TP_CANCELLAZIONE_FERMO_IN], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == JoinTabSupervisioneProfiliCfg.OP_LEGGIPRATICA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneLeggiPratica[TP_CANCELLAZIONE_FERMO_IN], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == null)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneCreaPratica[TP_CANCELLAZIONE_FERMO_IN], ref statiDescrizioniDic);
                    AppendFromTo(DescrizioneStatiPerOperazionePresentaFormalita[TP_CANCELLAZIONE_FERMO_IN], ref statiDescrizioniDic);
                    AppendFromTo(DescrizioneStatiPerOperazioneLeggiPratica[TP_CANCELLAZIONE_FERMO_IN], ref statiDescrizioniDic);
                }
            }
            else if (tipoPratica == JoinTabSupervisioneProfiliCfg.TP_REVOCA_FERMO)
            {
                if (tipoOperazione == OP_CREAPRATICA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneCreaPratica[TP_REVOCA_FERMO], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == JoinTabSupervisioneProfiliCfg.OP_PRESENTAFORMALITA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazionePresentaFormalita[TP_REVOCA_FERMO], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == JoinTabSupervisioneProfiliCfg.OP_LEGGIPRATICA)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneLeggiPratica[TP_REVOCA_FERMO], ref statiDescrizioniDic);
                }
                else if (tipoOperazione == null)
                {
                    AppendFromTo(DescrizioneStatiPerOperazioneCreaPratica[TP_REVOCA_FERMO], ref statiDescrizioniDic);
                    AppendFromTo(DescrizioneStatiPerOperazionePresentaFormalita[TP_REVOCA_FERMO], ref statiDescrizioniDic);
                    AppendFromTo(DescrizioneStatiPerOperazioneLeggiPratica[TP_REVOCA_FERMO], ref statiDescrizioniDic);
                }
            }

            return statiDescrizioniDic;
        }

        // public static IList<string> GetCodiceStatoEsitoOperazioneSiaSuccessoCheFallita(TipoPratica_t tipoPratica, Operazione_t operazione)
        // {
        //     IList<string> lOps = new List<string>();
        //     lOps.Add(GetCodiceStatoEsitoOperazioneFallita(tipoPratica, operazione));
        //     lOps.Add(GetCodiceStatoEsitoOperazioneSuccesso(tipoPratica));
        //     return lOps;
        // }

        // // Codici di errore in caso di errore nei dati del Database (per es. in CreaPratica non si trova la targa),
        // // La descrizione dell'errore sarà in tab_esito_aci
        // public static string GetCodiceStatoEsitoOperazioneNonEffettuabile(TipoPratica_t tipoPratica, Operazione_t operazione)
        // {
        //     string stato = "";
        //     switch (operazione)
        //     {
        //         case Operazione_t.creaPratica:
        //             return join_tab_supervisione_profili.IFF_ERR;
        //             break;
        //         case Operazione_t.presentaFormalita:
        //             stato = "bho";
        //             break;
        //         case Operazione_t.leggiPratica:
        //             stato = "bho";
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException();
        //     }
        // /*
        //             switch (tipoPratica)
        //             {
        //                 case TipoPratica_t.iscrizione_fermo:
        //                     {
        //                         switch (operazione)
        //                         {
        //                             case Operazione_t.creaPratica:
        //                                 stato = join_tab_supervisione_profili.IFF_ERR;
        //                                 break;
        //                             case Operazione_t.presentaFormalita:
        //                                 stato = "bho";
        //                                 break;
        //                             case Operazione_t.leggiPratica:
        //                                 stato = "bho";
        //                                 break;
        //                             default:
        //                                 throw new ArgumentOutOfRangeException();
        //                         }
        //                     }
        //                     break;
        //                 case TipoPratica_t.revoca_fermo:
        //                     {
        //                         switch (operazione)
        //                         {
        //                             case Operazione_t.creaPratica:
        //                                 stato = "bho";
        //                                 break;
        //                             case Operazione_t.presentaFormalita:
        //                                 stato = "bho";
        //                                 break;
        //                             case Operazione_t.leggiPratica:
        //                                 stato = "bho";
        //                                 break;
        //                             default:
        //                                 throw new ArgumentOutOfRangeException();
        //                         }
        //                     }
        //                     break;
        //                 case TipoPratica_t.cancellazione_fermo_va:
        //                     {
        //                         switch (operazione)
        //                         {
        //                             case Operazione_t.creaPratica:
        //                                 stato = "bho";
        //                                 break;
        //                             case Operazione_t.presentaFormalita:
        //                                 stato = "bho";
        //                                 break;
        //                             case Operazione_t.leggiPratica:
        //                                 stato = "bho";
        //                                 break;
        //                             default:
        //                                 throw new ArgumentOutOfRangeException();
        //                         }
        //                     }
        //                     break;
        //                 case TipoPratica_t.cancellazione_fermo_in:
        //                     {
        //                         switch (operazione)
        //                         {
        //                             case Operazione_t.creaPratica:
        //                                 stato = "bho";
        //                                 break;
        //                             case Operazione_t.presentaFormalita:
        //                                 stato = "bho";
        //                                 break;
        //                             case Operazione_t.leggiPratica:
        //                                 stato = "bho";
        //                                 break;
        //                             default:
        //                                 throw new ArgumentOutOfRangeException();
        //                         }
        //                     }
        //                     break;
        //
        //                 case TipoPratica_t.annulla_formalita:
        //                 default:
        //                     throw new ArgumentOutOfRangeException();
        //             }
        // */
        //     return stato;
        // }

#if NON_USATO
        public static string GetCodiceStatoEsitoOperazioneFallita3(TipoPratica_t tipoPratica, Operazione_t operazione)
        {
            string stato = "";
            switch (tipoPratica)
            {
                case TipoPratica_t.iscrizione_fermo:
                    {
                        switch (operazione)
                        {
                            case Operazione_t.creaPratica:
                                stato = join_tab_supervisione_profili.ANN_IFF;
                                break;
                            case Operazione_t.presentaFormalita:
                                stato = join_tab_supervisione_profili.ANN_VCR;
                                break;
                            case Operazione_t.leggiPratica:
                                stato = join_tab_supervisione_profili.ANN_VPR;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;
                case TipoPratica_t.revoca_fermo:
                    {
                        switch (operazione)
                        {
                            case Operazione_t.creaPratica:
                                stato = join_tab_supervisione_profili.ANN_REV;
                                break;
                            case Operazione_t.presentaFormalita:
                                stato = join_tab_supervisione_profili.ANN_RCR;
                                break;
                            case Operazione_t.leggiPratica:
                                stato = join_tab_supervisione_profili.ANN_RPR;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;
                case TipoPratica_t.cancellazione_fermo_va:
                    {
                        switch (operazione)
                        {
                            case Operazione_t.creaPratica:
                                stato = join_tab_supervisione_profili.ANN_CVA;
                                break;
                            case Operazione_t.presentaFormalita:
                                stato = join_tab_supervisione_profili.ANN_CRA;
                                break;
                            case Operazione_t.leggiPratica:
                                stato = join_tab_supervisione_profili.ANN_PRA;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;
                case TipoPratica_t.cancellazione_fermo_in:
                    {
                        switch (operazione)
                        {
                            case Operazione_t.creaPratica:
                                stato = join_tab_supervisione_profili.ANN_CIN;
                                break;
                            case Operazione_t.presentaFormalita:
                                stato = join_tab_supervisione_profili.ANN_CRI;
                                break;
                            case Operazione_t.leggiPratica:
                                stato = join_tab_supervisione_profili.ANN_PRI;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    break;

                case TipoPratica_t.annulla_formalita:
                    throw new ArgumentOutOfRangeException();

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return stato;
        }
#endif
        
        // Successo SOLO per webservices ACI, non ritorna eventuali
        // stati successo "a amano" (tipo "CVM-ISC")
        public static string GetCodiceStatoEsitoOperazioneSuccesso(TipoPratica_t tipoPratica)
        {
            switch (tipoPratica)
            {
                case TipoPratica_t.iscrizione_fermo:
                    // Dati vincolo iscrizione
                    {
                        // (spostati all'inizio di creaPratica!")
                        //  var vincoloPs = leggiVincoloResult.VincoloResult;
                        //
                        //  joinTabSupervisioneProfili.vincolo_isc_rp_formalita_trascrizione =
                        //          vincoloPs.RpFormalitaTrascrizione;
                        //  joinTabSupervisioneProfili.vincolo_isc_data_formalita_trascrizione =
                        //          vincoloPs.DataFormalitaTrascrizione;
                        //  joinTabSupervisioneProfili.vincolo_isc_provincia_rp_formalita_trascrizione =
                        //          vincoloPs.ProvinciaRPFormalitaTrascrizione;
                        //  joinTabSupervisioneProfili.vincolo_isc_data_vincolo = vincoloPs.DataVincolo;
                        //  joinTabSupervisioneProfili.vincolo_ufficio_pra_competente =
                        //          vincoloPs.UfficioPRACompetente;

                        return join_tab_supervisione_profili.VAL_ISC;
                    }
                case TipoPratica_t.revoca_fermo:
                    return join_tab_supervisione_profili.REV_ISC;
                case TipoPratica_t.cancellazione_fermo_va:
                    return join_tab_supervisione_profili.CVA_ISC;
                case TipoPratica_t.cancellazione_fermo_in:
                    return join_tab_supervisione_profili.CIN_ISC;

                case TipoPratica_t.annulla_formalita:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    } // class
}