using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace Publisoftware.Data
{
    public partial class join_tab_supervisione_profili : ISoftDeleted, IGestioneStato
    {
        /// <summary>
        /// Annullato
        /// </summary>
        public const string ANN_UFF = "ANN-UFF";
        public const string ANN_ANN = "ANN-ANN";

        /// <summary>
        /// Sospeso
        /// </summary>
        public const string SSP_IST = "SSP-IST";

        public const string ANN = "ANN-";
        public const string ATT = "ATT-";
        public const string VAL = "VAL-";
        public const string SSP = "SSP-";

        public const int ATT_ATT_ID = 1;
        public const string ATT_ATT = "ATT-ATT";

        public const string SSP_DIS = "SSP-DIS";
        public const string ANN_DIS = "ANN-DIS";

        /// <summary>
        /// FERMI
        /// </summary> 
        // ---------------------------------------------------------------------------------------------
        [Obsolete("Non distingue il tipo di operazione su cui si è verificato l'errore", true)]
        public const string RIP_ERR = "RIP-ERR"; // problema  nei nostri dati in LeggiPratica
        // ---------------------------------------------------------------------------------------------

        public const string ISC_ERR= "ISC-ERR"; // problema  nei nostri dati in LeggiPratica iscrizione
        public const string REV_ERR= "REV-ERR"; // problema  nei nostri dati in LeggiPratica revoca
        public const string CVA_ERR= "CVA-ERR"; // problema  nei nostri dati in LeggiPratica cancellazione VA
        public const string CIN_ERR= "CIN-ERR"; // problema  nei nostri dati in LeggiPratica cancellazione IN

        public const string IFF_ERR = "IFF-ERR"; // problema  nei nostri dati CreaPratica iscrizione
        public const string RFF_ERR = "RFF-ERR"; // problema  nei nostri dati CreaPratica revoca
        public const string VFF_ERR = "VFF-ERR"; // problema  nei nostri dati CreaPratica cancellazione VA
        public const string NFF_ERR = "NFF-ERR"; // problema  nei nostri dati CreaPratica cancellazione IN

        public const string VAL_DIS = "VAL-DIS"; // Pratica in attesa di iscrizione
        public const string VAL_IF1 = "VAL-IF1";
        public const string VAL_IF2 = "VAL-IF2";
        public const string ANN_IF3 = "ANN-IF3"; // Iscrizione fermo annullato per errori "validazione" (dopo 3 tentativi)
        // Ambiguo in vecchie iscrizioni:
        public const string ANN_IFF = "ANN-IFF"; // Iscrizione fermo annullato con supervisione per targa radiata et similia (ex terzo tentativo, ma era ambiguo)

        // Questi lo usa anche il programma "vecchio" delle revoche in attesa di implementazione web services ACI
        public const string REV_DIS = "REV-DIS"; // Pratica creata in attesa di revoca
        public const string ANN_REV = "ANN-REV"; // Annullato con supervisione (ex terzo tentativo)
        
        // Qui stati che serviranno quando l'ACI ci darà la possibilità:
        public const string REV_DI1 = "REV-DI1"; // Revoca fermo errore 1
        public const string REV_DI2 = "REV-DI2";
        // public const string REV_DI3 = "REV-DI3"; // Revoca fermo annullato per errori "validazione" (dopo x tentativi)
        public const string ANN_RNV = "ANN-RNV"; // Revoca fermo annullato per mancanza vincoli

        [Obsolete("Non distingue il tipo di cancellazione", false)]
        public const string CAN_DIS = "CAN-DIS"; // Pratica in attesa di cancellazione
        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string CAN_CA1 = "CAN-CA1"; // Cancellazione Fermo errore 1
        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string CAN_CA2 = "CAN-CA2";
        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string ANN_CAN = "ANN-CAN"; // Cancellazione fermo annullato per errori "validazione" (dopo x tentativi)

        public const string CVA_DIS = "CVA-DIS"; // Pratica in attesa di cancellazione per [V]endita [A]nteriore
        public const string CVA_CA1 = "CVA-CA1"; // Cancellazione Fermo VA errore 1
        public const string CVA_CA2 = "CVA-CA2";
        public const string ANN_CA3 = "ANN-CA3";// Cancellazione fermo VA annullato per errori "validazione" (dopo x tentativi)
        // Ambiguo in vecchie iscrizione cancellazione
        public const string ANN_CVA = "ANN-CVA"; // Annullato con supervisione (ex terzo tentativo)
        
        public const string ANN_VNV = "ANN-VNV"; // Cancellazione fermo VA annullato per mancanza veicoli

        public const string CIN_DIS = "CIN-DIS"; // Pratica in attesa di cancellazione per [IN]debita iscrizione
        public const string CIN_CA1 = "CIN-CA1"; // Cancellazione Fermo IN errore 1
        public const string CIN_CA2 = "CIN-CA2";
        public const string ANN_CI3 = "ANN-CI3"; // Cancellazione fermo IN annullato per errori "validazione" (dopo x tentativi)
        // Ambiguo in vecchie iscrizione cancellazione
        public const string ANN_CIN = "ANN-CIN"; // // Cancellazione fermo IN annullato con supervisione (ex terzo tentativo)
        public const string ANN_CNV = "ANN-CNV"; // Cancellazione fermo IN annullato per mancanza veicoli


        public const string VAL_CRE = "VAL-CRE"; // Pratica iscrizione creata
        public const string VAL_CR1 = "VAL-CR1"; // Iscrizione creata errore 1
        public const string VAL_CR2 = "VAL-CR2"; // Iscrizione creata errore 2
        public const string ANN_VCR = "ANN-VCR"; // Iscrizione creata errore 3

        public const string REV_CRE = "REV-CRE"; // Pratica revoca creata
        public const string REV_CR1 = "REV-CR1"; // Revoca creata errore 1
        public const string REV_CR2 = "REV-CR2"; // Revoca creata errore 2
        public const string ANN_RCR = "ANN-RCR"; // Revoca creata errore
        

        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string CAN_CRE = "CAN-CRE"; // Pratica cancellazione creata
        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string CAN_CR1 = "CAN-CR1"; // Pratica cancellazione creata err 1
        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string CAN_CR2 = "CAN-CR2"; // Pratica cancellazione creata err 2
        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string ANN_CRC = "ANN-CRC"; // Pratica cancellazione creata err
        
        
        public const string CVA_CRE = "CVA-CRE"; // Pratica cancellazione VA creata
        public const string CVA_CR1 = "CVA-CR1"; // Pratica cancellazione VA creata err 1
        public const string CVA_CR2 = "CVA-CR2"; // Pratica cancellazione VA creata err 2
        public const string ANN_CRA = "ANN-CRA"; // Pratica cancellazione VA creata err 3

        public const string CIN_CRE = "CIN-CRE"; // Pratica cancellazione IN creata
        public const string CIN_CR1 = "CIN-CR1"; // Pratica cancellazione IN creata err 1
        public const string CIN_CR2 = "CIN-CR2"; // Pratica cancellazione IN creata err 2
        public const string ANN_CRI = "ANN-CRI"; // Pratica cancellazione IN creata err 3
        
        public const string VAL_PRE = "VAL-PRE"; // Pratica iscrizione presentata
        public const string VAL_PR1 = "VAL-PR1"; // err 1
        public const string VAL_PR2 = "VAL-PR2"; // err 2
        public const string ANN_VPR = "ANN-VPR"; // err 3
        [Obsolete("Eliminare una volta che non ci sono più stati di questo tipo nel DB")] 
        public const string VAL_RECUPERA = "VAL-VPR"; //<- vanno recuperati a causa errore definizione ANN_VPR 
        
        public const string REV_PRE = "REV-PRE"; // Pratica revoca presentata
        public const string REV_PR1 = "REV-PR1"; // err 1
        public const string REV_PR2 = "REV-PR2"; // 
        // public const string REV_PR3 = "REV-PR3"; // 
        public const string ANN_RPR = "ANN-RPR"; //
        
        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string CAN_PRE = "CAN-PRE"; // Pratica cancellazione presentata
        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string CAN_PR1 = "CAN-PR1"; // err 1 
        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string CAN_PR2 = "CAN-PR2"; // 
        // [Obsolete("Non distingue il tipo di cancellazione", true)]
        // public const string ANN_CPR = "ANN-CPR"; // 

        public const string CVA_PRE = "CVA-PRE"; // Pratica cancellazione presentata
        public const string CVA_PR1 = "CVA-PR1"; // err 1 
        public const string CVA_PR2 = "CVA-PR2"; // err 2
        public const string ANN_PRA = "ANN-PRA"; // err 3

        public const string CIN_PRE = "CIN-PRE"; // Pratica cancellazione presentata
        public const string CIN_PR1 = "CIN-PR1"; // err 1 
        public const string CIN_PR2 = "CIN-PR2"; // err 2
        public const string ANN_PRI = "ANN-PRI"; // err 3
        
        public const string VAL_ISC = "VAL-ISC"; // Pratica fermo iscritta
        // ---
        [Obsolete("Non distingue il tipo di cancellazione", true)]
        public const string CAN_ISC = "CAN-ISC"; // Pratica fermo cancellata
        // ---
        public const string CVA_ISC = "CVA-ISC"; // Pratica fermo cancellata per VA
        public const string CVM_ISC = "CVM-ISC"; // Pratica fermo cancellata per VA "a mano" (senza usare i web services ACI)
        public const string CIN_ISC = "CIN-ISC"; // Pratica fermo cancellata per IN
        public const string CIM_ISC = "CIM-ISC"; // Pratica fermo cancellata per IN "a mano" (senza usare i web services ACI)
        public const string REV_ISC = "REV-ISC"; // Pratica fermo revocata
        // iscritte 

        public const string VAL_VAL = "VAL-VAL"; // valido (iscrizione ipoteca o qualsiasi iscrizione che non ha bisogno di passi successivi)

        // ----
        // Da vedere chi li usa

        [Obsolete] //<- se levi aggiungi commento con il significato di questo stato!
        public const string VAL_NAC = "VAL-NAC";
        [Obsolete] //<- se levi aggiungi commento con il significato di questo stato!
        public const string ANN_PEC = "ANN-PEC";


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

        public string data_protocollo_registro_iscrizione_bene_String
        {
            get
            {
                if (data_protocollo_registro_iscrizione_bene.HasValue)
                {
                    return data_protocollo_registro_iscrizione_bene.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
