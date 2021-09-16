using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_note
    {
        public string data_efficacia_String
        {
            get
            {
                if (data_efficacia.HasValue)
                {
                    return data_efficacia.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
