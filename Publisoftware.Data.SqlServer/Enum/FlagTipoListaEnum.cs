using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public enum FlagTipoListaEnum
    {
        [Display(Name = "Lista di Emissione")]
        LISTA_DI_EMISSIONE = 'C',
        [Display(Name = "Lista trasmessa dall'Ente")]
        LISTA_TRASMESSA = 'I'
    }

    //PROVA
    public static class FlagTipoListaEnumMethods
    {

        public static String GetDBString(this FlagTipoListaEnum enFlag)
        {
            return ((char)enFlag).ToString();
        }
    }
}
