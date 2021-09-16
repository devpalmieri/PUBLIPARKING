using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_denunce_contratti:ISoftDeleted, IGestioneStato
    {
        public const string DOCUMENTI_DENUNCE_SUBFOLDER = "denunce";
        public static readonly string DOCUMENTI_DENUNCE_SUBFOLDER_FINAL_SLASH = DOCUMENTI_DENUNCE_SUBFOLDER + "/";
        
        public const string DOCUMENTI_IMU_SUBFOLDER = "imu";
        public static readonly string DOCUMENTI_IMU_SUBFOLDER_FINAL_SLASH = DOCUMENTI_IMU_SUBFOLDER + "/";

        public const string DOCUMENTI_GESTIONE_CANONE_PATRIMONIALE_OCCUPAZIONE_SUOLO_PUBBLICO_SUBFOLDER = "gcposp";
        public static readonly string DOCUMENTI_GESTIONE_CANONE_PATRIMONIALE_OCCUPAZIONE_SUOLO_PUBBLICO_SUBFOLDER_FINAL_SLASH = DOCUMENTI_GESTIONE_CANONE_PATRIMONIALE_OCCUPAZIONE_SUOLO_PUBBLICO_SUBFOLDER + "/";

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
