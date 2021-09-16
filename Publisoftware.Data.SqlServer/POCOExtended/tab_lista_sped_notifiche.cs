using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_lista_sped_notifiche : ISoftDeleted, IGestioneStato
    {
        /// <summary>
        /// Stato iniziale lista sped not
        /// </summary>
        public const string SPE_PRE = "SPE-PRE"; //Appena Consolidata
        public const string PRE_ASS = "PRE-ASS"; //Assegnazione Messi Da Terminare terminata
        public const string DEF_DEF = "DEF-DEF"; //Lista con stampa Approvata
        public const string DEF_STA = "DEF-STA"; //Lista totalmente stampata
        public const string SPE_DEF = "SPE-DEF"; //Lista Spedita

        public bool IsSoftDeletable
        {
            get { return true; }
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

        public static string buildNumeroLista(tab_liste p_listaEmissione, int p_anno, int p_progLista)
        {
            return String.Format("{0}{1}{2}{3}", p_listaEmissione.anagrafica_ente.cod_ente, p_listaEmissione.tab_tipo_lista.cod_lista, p_anno.ToString(), p_progLista.ToString("D7"));
        }
        public static string buildNumeroLista(string p_cod_ente,string p_cod_lista, int p_anno, int p_progLista)
        {
            return String.Format("{0}{1}{2}{3}", p_cod_ente, p_cod_lista, p_anno.ToString(), p_progLista.ToString("D7"));
        }
        

        public string data_lista_String
        {
            get
            {
                if(data_lista.HasValue)
                {
                    return data_lista.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
                
            }
            set
            {
                data_lista = DateTime.Parse(value);
            }
        }
    }
}
