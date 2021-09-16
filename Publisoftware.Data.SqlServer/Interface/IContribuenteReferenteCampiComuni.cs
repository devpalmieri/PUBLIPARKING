using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming
// ReSharper disable ConvertNullableToShortForm

namespace Publisoftware.Data.Interface
{
    public interface IContribuenteReferenteCampiComuni
    {
        /// <summary>
        /// ritorna id_tab_referente (int) o id_anag_contribuente (decimal) a seconda della tabella
        /// </summary>
        decimal PkIdAsDecimal { get; }

        string nome { get; set; }
        string cognome { get; set; }
        string cod_fiscale { get; set; }
        string rag_sociale { get; set; }
        string denominazione_commerciale { get; set; }
        string p_iva { get; set; }
        string cod_fiscale_pg { get; set; }
        string stato_nas { get; set; }
        string comune_nas { get; set; }
        Nullable<int> cod_comune_nas { get; set; }
        Nullable<System.DateTime> data_nas { get; set; }
        Nullable<System.DateTime> data_morte { get; set; }
        Nullable<int> id_sesso { get; set; }
        Nullable<int> id_toponimo { get; set; }
        string indirizzo { get; set; }
        string edificio { get; set; }
        Nullable<int> nr_civico { get; set; }
        string sigla_civico { get; set; }

        string colore { get; set; }
        Nullable<decimal> km { get; set; }
        string scala { get; set; }
        string piano { get; set; }
        string interno { get; set; }
        string condominio { get; set; }
        string frazione { get; set; }
        string stato { get; set; }
        string citta { get; set; }
        Nullable<int> cod_citta { get; set; }
        string cap { get; set; }
        string prov { get; set; }
        string e_mail { get; set; }
        string pec { get; set; }
        string tel { get; set; }
        string cell { get; set; }
        Nullable<int> id_utente { get; set; }

        Nullable<System.DateTime> data_inizio_validita_altri_dati { get; set; }
        Nullable<System.DateTime> data_fine_validita_altri_dati { get; set; }
        string fonte_altri_dati { get; set; }
        Nullable<System.DateTime> data_inizio_validita_indirizzo { get; set; }
        Nullable<System.DateTime> data_fine_validita_indirizzo { get; set; }
        string fonte_indirizzo { get; set; }
        Nullable<int> contatore_verifiche_indirizzo_valido { get; set; }
        string flag_irreperibilita_definitiva { get; set; }
        string flag_ricerca_indirizzo_emigrazione { get; set; }
        string cod_stato { get; set; }
        string fonte_stato { get; set; }


        Nullable<System.DateTime> DataInizioStatoRO { get; }
        Nullable<System.DateTime> DataFineStatoRO { get; }
        Nullable<System.DateTime> DataStatoRO { get; }


        string flag_ricerca_indirizzo_mancata_notifica { get; set; }
        string flag_verifica_cf_piva { get; set; }

        Nullable<int> id_strada_db_poste { get; set; }

        /// <summary>
        /// ritorna id_tipo_referente o id_tipo_contribuente a seconda della tabella.
        /// Se il referente è contribuente ritorna null e si deve controllare "this.tab_contribuente.id_tipo"
        /// </summary>
        /// <remarks>
        /// Se il referente è contribuente DEVE ritornare null 
        /// Per interrogare questa proprietà utilizzare IContribuenteReferenteCampiComuniExts.FindIdTipoContribuente
        /// </remarks>
        int? idTipo { get; }

        DateTime? data_ultima_verifica { get; set; }
        
        Nullable<int> id_toponimo_normalizzato { get; set; }
    }
}
