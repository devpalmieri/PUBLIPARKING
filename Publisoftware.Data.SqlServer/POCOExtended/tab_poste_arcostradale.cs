using Publisoftware.Utility.CAP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_poste_arcostradale
    {
        public int EsponenteDalAssegnato
        {
            get
            {
                return EsponenteDal != null ? CAPHelper.AssigneValueToSiglaCivico(EsponenteDal) : 0;
            }
        }

        public int EsponenteAlAssegnato
        {
            get
            {
                return EsponenteAl != null ? CAPHelper.AssigneValueToSiglaCivico(EsponenteAl) : 300000;
            }
        }
    }
}
