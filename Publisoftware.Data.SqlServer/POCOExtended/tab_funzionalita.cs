using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_funzionalita.Metadata))]
    public partial class tab_funzionalita : IValidator, ISoftDeleted, IGestioneStato
    {

        public tab_funzionalita(string p_codice, string p_descrizione, string p_icona, int p_ordine, int p_struttura, int p_risorsa)
        {
            tab_applicazioni = new List<tab_applicazioni>();
            codice = p_codice;
            descrizione = p_descrizione;
            icona = p_icona;
            ordine = p_ordine;
            data_stato = DateTime.Now;
            id_struttura_stato = p_struttura;
            id_risorsa_stato = p_risorsa;
            if (!IsValid)
            {
                throw new ArgumentException("Error creating object", "tab_funzionalita not valid");
            }
        }

        /// <summary>
        /// Property che restituisce il Fullcode associato alla funzionalitè
        /// </summary>
        public string FullCode
        {
            get
            {
                try // Perché try/catch???
                {
                    if (!(string.IsNullOrEmpty(this.tab_procedure.codice)) &&
                    !(string.IsNullOrEmpty(this.codice)))
                    {
                        return this.tab_procedure.codice + this.codice;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch (Exception ) { return string.Empty; }
            }
        }
        /// <summary>
        /// Property che restituisce la validità della funzionalità
        /// </summary>
        public bool IsValid
        {
            get { return checkValidity(); }
        }
        /// <summary>
        /// Verifica la validità della funzionalità
        /// </summary>
        /// <returns></returns>
        protected bool checkValidity()
        {
            bool _isValid = false;

            try // Perché try/catch???
            {
                _isValid = (!string.IsNullOrEmpty(codice) && this.codice.Length.Equals(5) && this.descrizione.Length > 0 && this.ordine > 0);

                return _isValid;
            }
            catch (Exception ) { return _isValid; }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }
        /// <summary>
        /// crea l'applicazione associata alla funzionalità corrente
        /// </summary>
        /// <param name="p_idPagine"></param>
        /// <param name="p_codice"></param>
        /// <param name="p_descrizione"></param>
        /// <param name="p_struttura"></param>
        /// <param name="p_risorsa"></param>
        /// <returns></returns>
        public tab_applicazioni createApplicazione(tab_pagine p_pagina, string p_codice, string p_descrizione,string p_icona, int? p_ordine, int p_struttura, int p_risorsa, string p_action, string p_parametri_url,
                                                   bool p_schedulable)
        {
            try // Perché try/catch???
            {
                if (p_ordine == null)
                {
                    p_ordine = (this.tab_applicazioni.Count == 0) ? 0 : this.tab_applicazioni.Max(c => c.ordine);
                    p_ordine = p_ordine + 1;
                }

                if (!p_pagina.schedulable)
                {
                    p_schedulable = false;
                }
                    


                var applicazione = new tab_applicazioni(p_pagina, p_codice, p_descrizione, p_icona, p_ordine.Value, p_struttura, p_risorsa, p_action, p_parametri_url, p_schedulable);
                this.tab_applicazioni.Add(applicazione);
                return applicazione;
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
            public int id_tab_funzionalita { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Selezionare una procedura")]
            public int id_tab_procedure { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Inserire soltanto numeri positivi")]
            public int ordine { get; set; }

            [Required]
            public string descrizione { get; set; }

            [Required]
            [RegularExpression("[A-Za-z]{1}[0-9]{4}", ErrorMessage = "Formato Codice non valido (Es: F9999)")]
            public string codice { get; set; }

            public string icona { get; set; }
        }
    }
}
