using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_persone
    {
        public string Proprietario
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.contribuenteDisplay;
                }
                else
                {
                    if (codice_fiscale != null)
                    {
                        return cognome + " " + nome + " " + codice_fiscale;
                    }
                    else if (p_iva != null)
                    {
                        return denominazione + " " + p_iva;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }
    }
}
