using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_pagine : IValidator, ISoftDeleted
    {

        public tab_pagine(string p_tipo,string p_descrizione, string p_controller, string p_nome, string p_actions)
        {
             tipo = p_tipo;
             descrizione = p_descrizione;
             controller = p_controller;             
             actions = p_actions;
             if (!IsValid)
             {
                 throw new ArgumentException("Error creating object", "tab_pagine not valid");
             }
        }
        

        /// <summary>
        /// Property che restituisce la validità della pagina
        /// </summary>
        public bool IsValid
        {
            get { return checkValidity(); }
        }
        /// <summary>
        /// Verifica la validità della pagina
        /// </summary>
        /// <returns></returns>
        protected bool checkValidity()
        {
            bool _isValid = false;
            try // Perché try/catch???
            {
                if (this.id_tab_pagine > 0)
                {
                    _isValid = this.descrizione.Length > 0;
                }
            }
            catch (Exception ){ }
            
            return _isValid;

        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
