using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_tipo_contribuente : ISoftDeleted
    {
        public const string PERS_FISICA = "PF";
        public const int PERS_FISICA_ID = 1;

        public const string PERS_GIURIDICA = "PG";
        public const int PERS_GIURIDICA_ID = 2;

        public const string DITTA_INDIVIDUALE = "DI";
        public const int DITTA_INDIVIDUALE_ID = 4;

        public const string CONDOMINIO = "CO";
        public const int CONDOMINIO_ID = 5;

        public const string SOCIETA = "SC";

        public const int SRL_SOCIETA_RESP_LIMITATA = 1012;
        public const int SPA_SOCIETA_PER_AZIONI = 1013;
        public const int SAS_SOCIETA_ACCOMANDITA_SEMPLICE = 1014;
        public const int SAA_SOCIETA_ACCOMANDITA_PER_AZIONI = 1015;
        public const int SNC_SOCIETA_NOME_COLLETTIVO_ID = 1016;
        public const int COOP_COOPERATIVA_SOCIALE_ID = 1017;
        public const int COOP_SOCIETA_COOPERATIVA_ID = 1018;
        public const int SS_SOCIETA_SEMPLICE = 1019;
        public const int CONTRIBUENTE_TERZO = 1020;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string DescrizioneTipoSigla
        {
            get
            {
                //return descr_sigla + " - " + desc_tipo_contribuente;
                return desc_tipo_contribuente;
            }
        }
    }
}
