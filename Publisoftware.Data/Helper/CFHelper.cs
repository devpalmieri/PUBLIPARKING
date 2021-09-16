using Publisoftware.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.Helper
{
    // History
    // 26/02/2018: ho spostato le funzioni che non dipendevano dal Database in "Publisoftware.Utilities"
    //             perché utilizzate in progetto senza Entity Framework

    public static class CFHelper
    {
        private static string trovaCodiceComune(int comune, dbEnte dbContext)
        {
            if (dbContext.ser_comuni.Any(d => d.cod_comune == comune))
            {
                return dbContext.ser_comuni.Where(d => d.cod_comune == comune)
                                           .FirstOrDefault()
                                           .cod_catasto;
            }
            else
            {
                return "Errore";
            }
        }

        private static string trovaCodiceStatoEstero(string nazione, dbEnte dbContext)
        {
            if (dbContext.ser_stati_esteri_new.Any(d => d.denominazione_stato.ToUpper() == nazione.ToUpper()))
            {
                return dbContext.ser_stati_esteri_new.Where(d => d.denominazione_stato.ToUpper() == nazione.ToUpper() && 
                                                                 d.data_inizio_validita != d.data_fine_validita)
                                                     .FirstOrDefault()
                                                     .codice_stato;
            }
            else
            {
                return "Errore";
            }
        }

        public static string transformCodiceFiscale(string p_codiceFiscale)
        {
            return CodiceFiscaleUtilities.transformCodiceFiscale(p_codiceFiscale);
        }

        public static string CalcolaCF(string nome, string cognome, string giorno, string mese, string anno, string sesso, string cod_comune, string nazione, dbEnte dbContext)
        {
            string belfioreComune_o_CodiceStatoEstero = null;

            if (nazione != null && nazione != string.Empty)
            {
                if (nazione.ToUpper() == "ITALIA")
                {
                    if (cod_comune != null && cod_comune != string.Empty)
                    {
                        belfioreComune_o_CodiceStatoEstero = trovaCodiceComune(Convert.ToInt32(cod_comune), dbContext);
                    }
                }
                else
                {
                    belfioreComune_o_CodiceStatoEstero = trovaCodiceStatoEstero(nazione, dbContext);
                }
            }

            if (belfioreComune_o_CodiceStatoEstero == null)
            {
                return "Errore";
            }

            return CodiceFiscaleUtilities.CalcolaCF(nome, cognome, giorno, mese, anno, sesso, belfioreComune_o_CodiceStatoEstero);
        }

        public static string getCodiceFiscale(DateTime? data_nas, string nome, string cognome, int? cod_comune_nas, string stato_nas, int? id_sesso, dbEnte dbContext)
        {
            string v_codiceFiscale = string.Empty;
            string comune = cod_comune_nas?.ToString() ?? String.Empty;
            string sesso = id_sesso?.ToString() ?? String.Empty;

            if (data_nas.HasValue && nome != string.Empty && nome != null && cognome != string.Empty && cognome != null)
            {
                string v_giorno = data_nas.Value.Day.ToString();
                string v_mese = data_nas.Value.Month.ToString();
                string v_anno = data_nas.Value.Year.ToString();

                v_codiceFiscale = CalcolaCF(nome, cognome, v_giorno, v_mese, v_anno, sesso, comune, stato_nas, dbContext);
            }

            return v_codiceFiscale.ToUpper();
        }
    }
}
