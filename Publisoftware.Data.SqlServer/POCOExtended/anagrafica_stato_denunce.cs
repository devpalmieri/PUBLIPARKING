using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_stato_denunce : ISoftDeleted
    {
        public const string ANN = "ANN-";
        public const string ATT = "ATT-";


        // Annullato
        public const int ANNULATO_ID = 31;
        public const string ANNULATO = "ANN-ANN";

        // 
        // public const int _ID= 45	
        // ANN-RRT	Annullata Richiesta Rettificata
        // 
        // public const int _ID= 51	
        // ATT-ARC	Archiviata
        // 

        //	Acquisita
        public const int ATT_ATT_ID = 38;
        public const string ATT_ATT = "ATT-ATT";

        // Cessato
        public const int ATT_CES_ID = 15;
        public const string ATT_CES = "ATT-CES";

        // 
        // public const int _ID= 44	
        // ATT-COC	Concessione Consegnata Contribuente
        // 
        // public const int _ID= 39	
        // ATT-DAC	Domanda Acquisita
        // 
        // public const int _ID= 42	
        // ATT-DAN	Domanda Non Accolta dall'Ente
        // 
        // public const int _ID= 41	
        // ATT-DAS	Domanda Accolta dall'Ente
        // 
        // public const int _ID= 50	
        // ATT-DCM	Acquisita Documentazione per Comunicazione

        // 	Definitivo
        public const int ATT_DEF_ID = 5;
        public const string ATT_DEF = "ATT-DEF";

        // 
        // public const int _ID= 43	
        // ATT-DIC	Inviata Comunicazione Ritiro Concessione
        // 
        // public const int _ID= 40	
        // ATT-DTE	Domanda Trasmessa all'Ente
        // 
        // public const int _ID= 47	
        // ATT-MVS	Penale Sosta Non Pagata
        // 
        // public const int _ID= 46	
        // ATT-PAG	Penale Sosta Pagata
        // 
        // public const int _ID= 49	
        // ATT-PRD	Comunicazione Prodotta
        // 
        // public const int _ID= 24	
        // ATT-PRE	Preliminare
        // 
        // public const int _ID= 37	
        // ATT-PRO	Protocollata
        // 
        // public const int _ID= 48	
        // ATT-VDI	Acquisita Variazione Dati Comunicazione IMU
        // 
        // public const int _ID= 34	
        // SAL-PRE	Sospeso per Sopralluogo Allacciamento
        // 
        // public const int _ID= 33	
        // SOS-PRE	Sospeso in Fase di Stipula

        // Sospeso temporaneo
        public const int SSP_ATT_ID = 52;
        public const string SSP_ATT = "SSP-ATT";

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
