using Publiparking.Core.Data.SqlServer.POCOExtended.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class tab_utenti : ISoftDeleted, IGestioneStato
    {
        public const string ACQ_ACQ = "ACQ-ACQ";
        public const string ATT_ATT = "ATT-ATT";
        public const string ANN_ANN = "ANN-ANN";

        public static string TIPOUTENTE_FISICO = "0";
        public static string TIPOUTENTE_GIURIDICO = "1";

        public static string USERNAME_SPID = "username_spid_";
        public static string PASSWORD_SPID = "password_spid_";

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

        public string GetUsernameSpid()
        {
            return USERNAME_SPID + DateTime.Now.Year.ToString();
        }

        public string GetPasswordSpid()
        {
            return PASSWORD_SPID + DateTime.Now.Year.ToString();
        }

        public string nominativoDisplay
        {
            get
            {
                return cognome + " " + nome;
            }
        }

        public string utenteDisplay
        {
            get
            {
                return nome + " " + cognome + " - " + codice_fiscale;
            }
        }
    }
}
