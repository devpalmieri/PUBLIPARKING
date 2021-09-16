using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_anagrafe : ISoftDeleted, IGestioneStato
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public const string STATO_ANAGRAFE_RESIDENTE = "RES";
        public const string STATO_ANAGRAFE_DECEDUTO = "DEC";
        public const string STATO_ANAGRAFE_CAN = "CAN";
        public const string STATO_ANAGRAFE_EMIGRATO = "EMI";

        public const string COD_STATO_ATTIVO = "ATT-ATT";
        public const string COD_STATO_CESSATO = "ATT-CES"; // Chiuso: esiste un record con dati nuovi

        /// <summary>
        /// Gestisce l'aggiornamento dei campi utente dello stato
        /// </summary>
        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        /// <summary>
        /// Controlla che 2 record siano uguali a meno dei campi ad uso interno, cioè non vengono considerati:<br />
        /// data_aggiornamento_anagrafe<br />
        /// data_stato<br />
        /// id_struttura_stato<br />
        /// id_risorsa_stato<br />
        /// data_inizio_validita<br />
        /// data_fine_validita<br />
        /// </summary>
        /// <param name="other">Altro record da confrontare</param>
        /// <returns>true se uguali</returns>
        public bool EqualsForUpdate(tab_anagrafe other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return // id_anagrafe == other.id_anagrafe &&
                id_ente == other.id_ente &&
                id_ente_gestito == other.id_ente_gestito &&
                cod_comune == other.cod_comune &&
                string.Equals(stato_anagrafe, other.stato_anagrafe, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(cognome, other.cognome, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(nome, other.nome, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(sesso, other.sesso, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(codice_fiscale, other.codice_fiscale, StringComparison.OrdinalIgnoreCase) &&
                data_nascita.Equals(other.data_nascita) &&
                anno_nascita == other.anno_nascita &&
                string.Equals(luogo_nascita, other.luogo_nascita, StringComparison.OrdinalIgnoreCase) &&
                id_strada == other.id_strada &&
                string.Equals(cod_strada, other.cod_strada, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(indirizzo, other.indirizzo, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(toponimo, other.toponimo, StringComparison.OrdinalIgnoreCase) &&
                numero == other.numero &&
                string.Equals(sigla, other.sigla, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(scala, other.scala, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(piano, other.piano, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(interno, other.interno, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(isolato, other.isolato, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(frazione, other.frazione, StringComparison.OrdinalIgnoreCase) &&
                numero_individuo == other.numero_individuo &&
                num_fam == other.num_fam &&
                string.Equals(parentela, other.parentela, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(sigla_parentela, other.sigla_parentela, StringComparison.OrdinalIgnoreCase) &&
                data_immigrazione.Equals(other.data_immigrazione) &&
                string.Equals(luogo_immigrazione, other.luogo_immigrazione, StringComparison.OrdinalIgnoreCase) &&
                data_emigrazione.Equals(other.data_emigrazione) &&
                string.Equals(luogo_emigrazione, other.luogo_emigrazione, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(indirizzo_emigrazione, other.indirizzo_emigrazione,
                    StringComparison.OrdinalIgnoreCase) &&
                string.Equals(civico_emigrazione, other.civico_emigrazione, StringComparison.OrdinalIgnoreCase) &&
                data_cambio_indirizzo.Equals(other.data_cambio_indirizzo) &&
                data_cancellazione.Equals(other.data_cancellazione) &&
                string.Equals(cittadinanza, other.cittadinanza, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(aire, other.aire, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(utenza, other.utenza, StringComparison.OrdinalIgnoreCase) &&
                tot_num_componenti == other.tot_num_componenti &&
                // data_aggiornamento_anagrafe.Equals(other.data_aggiornamento_anagrafe) &&
                id_stato == other.id_stato &&
                string.Equals(cod_stato, other.cod_stato, StringComparison.OrdinalIgnoreCase) &&
                // data_stato.Equals(other.data_stato) &&
                // id_struttura_stato == other.id_struttura_stato &&
                // id_risorsa_stato == other.id_risorsa_stato &&
                string.Equals(note, other.note, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(codiceconv, other.codiceconv, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(esponente, other.esponente, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(specie, other.specie, StringComparison.OrdinalIgnoreCase) &&
                id_rel_par_lac == other.id_rel_par_lac
                // data_inizio_validita.Equals(other.data_inizio_validita) &&
                // data_fine_validita.Equals(other.data_fine_validita) &&
                // -----------------------------------------------------------------------------------
                // Questi non vanno confrontati, solo le chiavi:                   
                //        Equals(anagrafica_risorse, other.anagrafica_risorse) &&
                //        Equals(anagrafica_strutture_aziendali, other.anagrafica_strutture_aziendali) &&
                //        Equals(anagrafica_ente, other.anagrafica_ente) &&
                //        Equals(anagrafica_ente_gestito, other.anagrafica_ente_gestito) &&
                //        Equals(tab_storico_anagrafe, other.tab_storico_anagrafe) &&
                //        Equals(ser_comuni, other.ser_comuni) &&
                //        Equals(anagrafica_rel_par_lac, other.anagrafica_rel_par_lac)
                // -----------------------------------------------------------------------------------
                ;
        }
    }
}