using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.Interface
{
    public static class IContribuenteReferenteCampiComuniExts
    {
        /// <summary>
        /// Trova id_tipo_contribuente, id_tipo_referente, tab_contribuente.id_tipo_contribuente
        /// </summary>
        /// <param name="item">"this"</param>
        /// <returns>1 se persona fisica</returns>
        public static int FindIdTipoContribuente(this IContribuenteReferenteCampiComuni item)
        {
            tab_referente tRef = item as tab_referente;
            tab_contribuente tContrib = item as tab_contribuente;

            if (tRef != null && tRef.tab_contribuente != null)
            {
                return tRef.tab_contribuente.id_tipo_contribuente;
            }
            // Se è un contribuente ritorna idTipo, se è un referente e idTipo!=null lo ritorna...
            else if (item.idTipo != null)
            {
                return item.idTipo.Value;
            }
            // ...altrimenti cerca il tipo nella eventuale tab_contribuenti collegata al referente...
            else if (tRef != null)
            {

                // OK: il tipo è null ma è un referente
                return tRef.tab_contribuente.id_tipo_contribuente;
            }

            // ...se anche idTipo in tab_referente.tab_contribuente c'è qualcosa che non va e lancia eccezione
            StringBuilder errorMessage = new StringBuilder();
            string tipoTabellaContribuente = tContrib != null ? nameof(tab_contribuente) : (tRef != null ? nameof(tab_referente) : "tabella_ignota");
            throw new Exception($"Impossibile stabilire il tipo di contribuente per {tipoTabellaContribuente} con id = {item.PkIdAsDecimal.ToString("0")}");
        }

    }
}
