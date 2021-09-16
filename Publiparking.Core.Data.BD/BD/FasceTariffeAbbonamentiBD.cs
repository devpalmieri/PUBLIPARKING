using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class FasceTariffeAbbonamentiBD : EntityBD<FasceTariffeAbbonamenti>
    {

        public const string GIORNI = "G";
        public const string SETTIMANE = "S";
        public const string MESI = "M";
        public const string ANNI = "A";

        public FasceTariffeAbbonamentiBD()
        {

        }
    }
}
