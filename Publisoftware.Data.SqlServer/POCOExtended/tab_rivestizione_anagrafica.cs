using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_rivestizione_anagrafica.Metadata))]
    public partial class tab_rivestizione_anagrafica : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public const string TIPO_BD_INFOCAMERE = "INFOCAMERE";
        public const string TIPO_BD_ANAGRAFE_COMUNALE = "ANACOM"; // Anagrafe comunale
        public const string TIPO_BD_PUNTOFISCO = "PUNTOFISCO";
        public const string TIPO_BD_RICHIESTA_VESTIZIONE_REGIONE_LOMBARDIA = "SV REGIONE LOMBARDIA"; // Custom
        public const string TIPO_BD_ACI = "ACI";
        public const string TIPO_BD_ACI_FERMI = "ACI_FERMI";

        public const string TIPO_ESTRAZIONE_PERSONE_FISICHE_RESIDENTI = "PFRES";
        public const string TIPO_ESTRAZIONE_PERSONE_FISICHE_NON_RESIDENTI = "PFNORES";
        public const string TIPO_ESTRAZIONE_PERSONE_FISICHE = "PF";
        public const string TIPO_ESTRAZIONE_PERSONE_GIURIDICHE = "PG";
        public const string TIPO_ESTRAZIONE_SIA_PERSONE_FISICHE_CHE_GIURIDICHE = "PFG";

        public const string CODICE_SERVIZIO_CO1_142 = "CO1.142";
        public const string CODICE_SERVIZIO_CO1_151 = "CO1.151";
        // Di FM1.14 non si devono generare richieste, si hanno però le risposte a "CODICE_SERVIZIO_RICHIESTE_LOMBARDIA" in questo formato
        // public const string CODICE_SERVIZIO_FM1_14 = "FM1.14";
        public const string CODICE_SERVIZIO_RICHIESTE_LOMBARDIA_V1 = "RL CSV V1.0";

        public const string CODICE_SERVIZIO_CSV_ANAGRAFE_RESIDENTI_FIRENZE = "CSV ANAG FI";
        public const string CODICE_SERVIZIO_RICHIESTA_LAC = "LAC"; // Tipicamente si chiedono via email
        
        public const string TIPO_RIVESTIZIONE_MASSIVA = "MS";

        public const string COD_STATO_VAL_VAL = "VAL-VAL";
        public const string COD_STATO_ATT_ATT = "ATT-ATT";

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

        }

        #region Obsoleti (forse usati in codice commentato)
        [Obsolete("Usare CODICE_SERVIZIO_CO1_142", true)]
        public const string CODICE_SERVIZIO_C01_142 = "C01.142";
        [Obsolete("Usare CODICE_SERVIZIO_CO1_151", true)]
        public const string CODICE_SERVIZIO_C01_151 = "C01.151";
        [Obsolete("Usare CODICE_SERVIZIO_CSV_ANAGRAFE_RESIDENTI_FIRENZE", true)]
        public const string CODICE_SERVIZIO_CSV_ANAGRAFE_RESIDENTI_FIRENZE_OLD = "CSV ANAG";
        #endregion
    }
}
