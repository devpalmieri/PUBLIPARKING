using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.dto
{
    public class OperazioneDTO
    {
        /// <summary>
        ///         ''' Elenco dei codici di occupazione validi
        ///         ''' </summary>
        ///         ''' HACK: Se la lista degli stati viene aggiornata, aggiornare anche statiValidi
        private const string statiValidi = "L,O,R,G,S,A,V,P,E";
        /// <summary>
        ///         ''' Stato stallo sconosciuto
        ///         ''' </summary>
        public const string statoSconosciuto = "";
        /// <summary>
        ///         ''' Lo stallo è libero
        ///         ''' </summary>
        public const string statoLibero = "L";
        /// <summary>
        ///         ''' Stallo occupato con indicazione dal parcometro
        ///         ''' </summary>
        public const string statoRegolareConNumero = "O";
        /// <summary>
        ///         ''' Stallo occupato con titolo esposto
        ///         ''' </summary>
        public const string statoRegolareConBiglietto = "R";
        /// <summary>
        ///         ''' Occupazione gratuita dello stallo
        ///         ''' </summary>
        public const string statoGratuito = "G";
        /// <summary>
        ///         ''' Occupazione abusiva dello stallo
        ///         ''' </summary>
        public const string statoPagamentoScaduto = "S";
        /// <summary>
        ///         ''' Occupazione abusiva dello stallo
        ///         ''' </summary>
        public const string statoAbusivo = "A";
        /// <summary>
        ///         ''' Occupazione stallo verbalizzata
        ///         ''' </summary>
        public const string statoVerbalizzato = "V";
        /// <summary>
        ///         ''' Emissione di preavviso di verbale
        ///         ''' </summary>
        public const string statoPreavviso = "P";
        /// <summary>
        ///         ''' Riconferma dello stato preavvisato
        ///         ''' </summary>
        public const string statoGiaPreavvisato = "E";

        /// <summary>
        ///         ''' Indica se lo stato è valido
        ///         ''' </summary>
        ///         ''' <param name="stato">Codice stato da verificare</param>
        public static bool isValidStato(string stato)
        {
            string[] listaStati = statiValidi.Split(',');
            bool valido = false;

            foreach (var curVal in listaStati)
            {
                if (curVal == stato)
                {
                    valido = true;
                    break;
                }
            }

            return valido;
        }

        /// <summary>
        ///         ''' Identificatore univoco dell'operazione (chiave primaria).
        ///         ''' </summary>
        public Int64 id { get; set; }

        /// <summary>
        ///         ''' Codice dell'operatore che ha eseguito l'operazione
        ///         ''' </summary>
        public Int32 idOperatore { get; set; }

        /// <summary>
        ///         ''' Identificatore dello stalli su cui è avvenuta l'operazione
        ///         ''' </summary>
        public Int32 idStallo { get; set; }

        /// <summary>
        ///         ''' Data di esecuzione dell'operazione
        ///         ''' </summary>
        public DateTime data { get; set; }

        /// <summary>
        ///         ''' Stato dello stallo
        ///         ''' </summary>
        public string stato { get; set; } = "";

        /// <summary>
        ///         ''' Coordinata X di trasmissione del terminale
        ///         ''' </summary>
        public double X { get; set; }

        /// <summary>
        ///         ''' Coordinata Y di trasmissione del terminale
        ///         ''' </summary>
        public double Y { get; set; }

        /// <summary>
        ///         ''' Nomi dei file delle foto di verifica
        ///         ''' </summary>
        public string fileFoto { get; set; } = "";

        /// <summary>
        ///         ''' Codice del titolo esporto
        ///         ''' </summary>
        public string codiceTitolo { get; set; } = "";

        /// <summary>
        ///         ''' Identificatore del preavviso di verbale emesso
        ///         ''' </summary>
        public Int32 idVerbale { get; set; }

        /// <summary>
        ///         ''' Identificatore della penale emessa
        ///         ''' </summary>
        public Int32 idPenale { get; set; }

        /// <summary>
        ///         ''' Targa dell'auto sullo stallo
        ///         ''' </summary>
        public string targa { get; set; } = "";

        /// <summary>
        ///         ''' Data di scadenza operazione.
        ///         ''' </summary>
        public DateTime scadenza { get; set; }
    }
}
