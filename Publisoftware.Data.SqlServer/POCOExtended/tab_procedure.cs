using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_procedure.Metadata))]
    public partial class tab_procedure : IValidator, ISoftDeleted, IGestioneStato
    {

        public tab_procedure(string p_codice, string p_descrizione, string p_icona, int p_ordine, int p_struttura, int p_risorsa)
        {
            tab_funzionalita = new List<tab_funzionalita>(); 
            codice = p_codice;
            descrizione = p_descrizione;
            icona = p_icona;
            ordine = p_ordine;
            data_stato = DateTime.Now;
            id_struttura_stato = p_struttura;
            id_risorsa_stato = p_risorsa;
            if (!IsValid)
            {
                throw new ArgumentException("Error creating object", "tab_procedure not valid");
            }
        }
        /// <summary>
        /// Property che restituisce la validità della procedura
        /// </summary>
        public bool IsValid
        {
            get {return checkValidity(); }
        }
        /// <summary>
        /// Verifica la validità della procedura
        /// </summary>
        /// <returns></returns>
        protected bool checkValidity()
        {
            bool _isValid = false;
            try // Perché try/catch???
            {
                if (!string.IsNullOrEmpty(codice) && this.codice.Length.Equals(5))
                {
                    _isValid = (this.descrizione.Length > 0 && this.ordine > 0);

                }
                return _isValid;
            }
            catch (Exception ) { return _isValid; }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }
        /// <summary>
        /// crea la funzionalità associata alla procedura corrente
        /// </summary>
        /// <param name="p_codice"></param>
        /// <param name="p_descrizione"></param>
        /// <param name="p_struttura"></param>
        /// <param name="p_risorsa"></param>
        /// <returns></returns>
        public tab_funzionalita createFunzionalita(string p_codice, string p_descrizione,string p_icona, int p_struttura, int p_risorsa)
        {
            try // Perché try/catch???
            {
                int p_ordine = (this.tab_funzionalita.Count == 0) ? 0 : this.tab_funzionalita.Max(c=>c.ordine);              
                var funzionalita = new tab_funzionalita(p_codice, p_descrizione, p_icona, p_ordine + 1, p_struttura, p_risorsa);
                this.tab_funzionalita.Add(funzionalita);
                return funzionalita;
            }
            catch (Exception ) { return null; }            
        }
        /// <summary>
        /// Gestisce l'aggiornamento dei campi utente dello stato
        /// </summary>
        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_tab_procedure { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Inserire soltanto numeri positivi")]
            public int ordine { get; set; }

            [Required]
            public string descrizione { get; set; }

            [Required]
            [RegularExpression("[A-Za-z]{1}[0-9]{4}", ErrorMessage = "Formato Codice non valido (Es: P9999)")]
            public string codice { get; set; }

            public string icona { get; set; }
        }
    }
}
