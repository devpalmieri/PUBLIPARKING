using System;


namespace Publisoftware.Data
{
    public partial class tab_f24_a1 : ISoftDeleted, IGestioneStato
    {
        public const string CAR_CAR = "CAR-CAR";
        public const string CAR_MOV = "CAR-MOV";
        public const string ANN_ANN = "ANN-ANN";

        public bool IsSoftDeletable
        {
            get { return true; }
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
    }
   
}
    