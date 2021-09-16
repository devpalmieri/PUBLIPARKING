using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.Helper
{
    public class FonteHelper
    {
        //Fonte è un char(3)
        public const string FONTE_WEB = "WEB";
        public const string FONTE_BO = "CBO";
        public const string FONTE_FO = "CFO";

        public const string FONTE_ENT = "ENT";

        public const string FONTE_ANAGRAFICA_COMUNALE = "ANA";

        public const string FONTE_PUNTO_FISCO = "FIS";
        public const string FONTE_PUNTO_FISCO_DA_INA_SAIA = "SIA"; // anche SIATEL
        public const string FONTE_PUNTO_FISCO_DA_CCIAA = "CCO";
        
        public const string FONTE_ERRATA_PARENTESI_TONDA = "FPT"; // [F]onte [P]arente [T]onda
        public const string FONTE_ERRATA_NA = "FNA"; // [F]onte [N]on [A]pplicabile

        public const string FONTE_F24 = "F24"; // Versamenti F24

        public static string GetFonte(bool isGenerica, ModalitaOperativaEnum modOp, string flag_IE)
        {
            return isGenerica ? FONTE_WEB : (flag_IE == anagrafica_risorse.RISORSA_INTERNA ? (modOp == ModalitaOperativaEnum.BackOffice ? FONTE_BO : FONTE_FO) : FONTE_ENT);
        }

        public static bool SetEditableViewBag(string v_fonte, string p_risorsaIE)
        {
            //Il dottore e Pietro hanno voluto che si controllasse che la fonte fosse diverso da "ENT" piuttosto che uguale a BO e FO
            if ((/*v_fonte == FONTE_BO || v_fonte == FONTE_FO*/ v_fonte != FONTE_ENT /*|| string.IsNullOrEmpty(v_fonte)*/) && p_risorsaIE == anagrafica_risorse.RISORSA_ESTERNA)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
