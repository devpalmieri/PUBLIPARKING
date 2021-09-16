using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto
{
    public class TransLogDTO
    {


        /// <summary>
        ///         ''' Identificatore univoco del titolo (chiave primaria).
        ///         ''' </summary>
        public Int64 idTicket { get; set; }

        /// <summary>
        ///         ''' Identificatore univoco del Parcometro (chiave primaria).
        ///         ''' </summary>
        public Int32 idPDM { get; set; }

        /// <summary>
        ///         ''' Tipologia di pagamento (1 = Contanti, 2 = Tessera con Apertura/Chiusura)
        ///         ''' </summary>
        public Int32 idPayType { get; set; }


        public Int32 recordType { get; set; }

        /// <summary>
        ///         ''' Data di pagamento del titolo.
        ///         ''' </summary>
        public DateTime payDateTime { get; set; }

        /// <summary>
        ///         ''' Data di scadenza del titolo.
        ///         ''' </summary>
        public DateTime expDateTime { get; set; }

        /// <summary>
        ///         ''' Importo pagato.
        ///         ''' </summary>
        public Int32 amount { get; set; }

        /// <summary>
        ///         ''' Codice del titolo.
        ///         ''' </summary>
        public Int32 ticketNumber { get; set; }

        /// <summary>
        ///         ''' Targa Pagata.
        ///         ''' </summary>
        public string licenseNumber { get; set; }

        /// <summary>
        ///         ''' Stallo Pagato.
        ///         ''' </summary>
        public Int32 parkingSpaceNumber { get; set; }

        /// <summary>
        ///         ''' Tipologia di servizio
        ///         ''' </summary>
        public Int32 idServiceType { get; set; }

        /// <summary>
        ///         ''' Note.
        ///         ''' </summary>
        public string note { get; set; }

        /// <summary>
        ///         ''' ' tlSpecialId=progressivo operazione PYNG
        ///         ''' </summary>
        public Int32 specialId { get; set; }
        /// <summary>
        ///         ''' tlParkingSpaceNo= codice zona tariffaria PYNG.
        ///         ''' </summary>
        public string parkingSpaceNo { get; set; }
        /// <summary>
        ///         ''' tlServiceCarSender=identificativo sosta PYNG
        ///         ''' </summary>
        public string serviceCarSender { get; set; }

        /// <summary>
        ///         ''' tlParkingSpaceNo= codice zona tariffaria PYNG.
        ///         ''' </summary>
        public string tlPDM_TicketNo_union { get; set; }


    }
}
