using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_applicazioni.Metadata))]
    public partial class tab_applicazioni : IValidator, ISoftDeleted, IGestioneStato
    {
        public tab_applicazioni(tab_pagine p_pagina, string p_codice, string p_descrizione, string p_icona, int p_ordine, int p_struttura, int p_risorsa, string p_actionName, string p_parametri_url, bool p_schedulable)
        {
            id_tab_pagine = p_pagina.id_tab_pagine;
            codice = p_codice;
            descrizione = p_descrizione;
            icona = p_icona;
            ordine = p_ordine;
            data_stato = DateTime.Now;
            id_struttura_stato = p_struttura;
            id_risorsa_stato = p_risorsa;
            actionName = p_actionName;
            parametri_url = p_parametri_url;
          
            if (!IsValid)
            {
                throw new ArgumentException("Error creating object", "tab_applicazioni not valid");
            }
        }
        /// <summary>
        /// Property che restituisce la validità dell'applicazione
        /// </summary>
        public bool IsValid
        {
            get { return checkValidity(); }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }
        /// <summary>
        /// Verifica la validità dell'applicazione
        /// </summary>
        /// <returns></returns>
        protected bool checkValidity()
        {
            bool _isValid = false;
            try
            {
                if (this.id_tab_pagine > 0)
                {
                    _isValid = (!string.IsNullOrEmpty(codice) && this.codice.Length.Equals(5) && this.descrizione.Length > 0 && this.ordine > 0);

                }
                return _isValid;
            }
            catch (Exception){return _isValid;}

        }

        /// <summary>
        /// Property che restituisce il Fullcode associato all'applicazione
        /// </summary>
        public string FullCode
        {
            get
            {
                // perché try/cath???
                try
                {
                    if (!(string.IsNullOrEmpty(this.tab_funzionalita.tab_procedure.codice)) &&
                    !(string.IsNullOrEmpty(this.tab_funzionalita.codice)) &&
                    !(string.IsNullOrEmpty(this.codice)))
                    {
                        return this.tab_funzionalita.tab_procedure.codice +
                                this.tab_funzionalita.codice +
                                this.codice;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch (Exception) { return string.Empty;}
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetUrlParameters()
        {
            return GetUrlParameters(this.parametri_url);
        }
        public static Dictionary<string, string> GetUrlParameters(string parametri_url)
        {
            Dictionary<string, string> urlParams = new Dictionary<string, string>();

            if (!String.IsNullOrWhiteSpace(parametri_url))
            {
                List<string> lstParams = parametri_url.Split('&').ToList();
                lstParams.ForEach((currParam) =>
                {
                    List<string> chiave_val = currParam.Split('=').ToList();
                    if (chiave_val.Count == 2)
                    {
                        urlParams.Add(chiave_val.ElementAt(0), chiave_val.ElementAt(1));
                    }
                    else if(chiave_val.Count > 2)
                    {
                        string v_valore = chiave_val.ElementAt(1);
                        for (int i = 2; i < chiave_val.Count(); i++)
                        {
                            v_valore = v_valore + "=" + chiave_val.ElementAt(i);
                        }

                        urlParams.Add(chiave_val.ElementAt(0), v_valore);
                    }
                });
            }

            return urlParams;
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
            public int id_tab_applicazioni { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Inserire soltanto numeri positivi")]
            public int ordine { get; set; }

            [Required]
            public string descrizione { get; set; }

            [Required]
            [RegularExpression("[A-Za-z]{1}[0-9]{4}", ErrorMessage = "Formato Codice non valido (Es: A9999)")]
            public string codice { get; set; }

            public string icona { get; set; }

            [Required]
            [StringLength(100)]
            [DisplayName("testo Menu")]
            public string label_menu { get; set; }

            [Required]
            [DisplayName("pagina associata")]
            public int id_tab_pagine { get; set; }

            [Required]
            [DisplayName("action")]
            public string actionName { get; set; }

            [DisplayName("Parametri URL")]
            public int parametri_url { get; set; }

            [Required]
            [DisplayName("tipo applicazione")]
            public string tipo_applicazione { get; set; }

            [Required]
            [DisplayName("Selezione Contribuente Obbligatoria")]
            public bool contribuente_required { get; set; }

            [Required]
            [DisplayName("Visualizzazione solo con terzo debitore")]
            public bool terzo_required { get; set; }

            [Required]
            [DisplayName("flag visualizzazione")]
            public bool flag_visualizazione { get; set; }

            [Required]
            [DisplayName("livello autorizzazione interno")]
            public int livello_autorizzazione_interno { get; set; }

            [DisplayName("livello autorizzazione esterno")]
            public int livello_autorizzazione_esterno { get; set; }

            [DisplayName("flag sistema")]
            public bool flag_sistema { get; set; }

            [DisplayName("Entrata")]
            public int id_entrata { get; set; }
        }
    }
}
