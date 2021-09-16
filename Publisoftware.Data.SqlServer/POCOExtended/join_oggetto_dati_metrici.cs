using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_oggetto_dati_metrici : ISoftDeleted, IGestioneStato
    {
        public const decimal MIN_DIFF_SUP_CATASTO_SUP_DICHIARATA_ACCETTABILE = 1M; // 1mq

        // Se la superficie non tassabile è superiore si devono obbligatoriamente inserire le anotazioni
        public const decimal MIN_SUP_NON_TASSABILE_SENZA_ANNOTAZIONI = 1M; // 1mq

        // Minima discrepanza accettabile tra la somma dei mq nelle join relative ad un oggetto e la sup
        // tassabile del tab_oggetto_di_contribuzione
        public const decimal MIN_DISCREPANZA_DA_DATI_METRICI = 1M; // 1mq

        public const string ATT_VER = "ATT-VER";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = System.DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }
    }
}
