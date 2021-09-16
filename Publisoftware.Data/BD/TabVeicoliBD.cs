using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabVeicoliBD : EntityBD<tab_veicoli>
    {
        public TabVeicoliBD()
        {

        }
        /// <summary>
        /// Filtro per la targa con stato attivo
        /// </summary>
        /// <param name="p_targa"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_veicoli GetAttivoByTarga(string p_targa, dbEnte p_dbContext)
        {
                return GetList(p_dbContext).Where(r => r.targa != null && r.targa.Equals(p_targa) && r.cod_stato == tab_veicoli.ATT_ATT).SingleOrDefault();
        }
        /// <summary>
        /// Filtro per la targa e cf con stato in input
        /// </summary>
        /// <param name="p_targa"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_veicoli GetAttivoByTarga(string p_cfPivaProprietario, string p_targa, string p_codStato, dbEnte p_dbContext)
        {
                return GetList(p_dbContext).Where(r => r.targa != null && r.targa.Equals(p_targa) && r.cod_stato.StartsWith(p_codStato) && r.cf_piva_proprietario==p_cfPivaProprietario).OrderByDescending(o => o.data_interrogazione).ThenByDescending(o => o.data_ultima_formalita) .FirstOrDefault();
        }

        /// <summary>
        /// aggiorna lo stato per tutti i veicoli di un soggetto Attivi e data aggiornamento precedente alla data indicata
        /// </summary>
        /// <param name="p_cfPivaSoggettoIsp"></param>
        /// <param name="p_codStato"></param>
        /// <param name="p_dateFile"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static void UpdateStatoForAttivi(String p_cfPivaSoggettoIsp, String p_codStato, int p_idStato, DateTime? p_dateFile, dbEnte p_dbContext)
        {
            TabVeicoliBD.GetList(p_dbContext).Where(v => v.cod_stato == tab_veicoli.ATT_ATT && v.data_interrogazione < p_dateFile && v.cf_piva_proprietario == p_cfPivaSoggettoIsp)
                                             .ToList().ForEach(u => { u.cod_stato = p_codStato; u.id_stato = p_idStato; });
        }

        /// <summary>
        /// aggiorna lo stato per tutti i veicoli di un soggetto Attivi e data aggiornamento precedente alla data indicata
        /// </summary>
        /// <param name="p_codStato"></param>
        /// <param name="p_dateFile"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static void UpdateStatoForAttivi(String p_codStato, DateTime? p_dateFile, dbEnte p_dbContext)
        {
            TabVeicoliBD.GetList(p_dbContext).Where(v => v.cod_stato == tab_veicoli.ATT_ATT && v.data_interrogazione < p_dateFile)
                                                                .ToList().ForEach(u => u.cod_stato = p_codStato);
        }

        /// <summary>
        /// Cessa tutti i veicoli per targa nello stato attivo
        /// </summary>
        /// <param name="p_targa"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static void ChiudiByTarga(String p_targa, dbEnte p_dbContext)
        {
            TabVeicoliBD.GetList(p_dbContext).Where(v => v.cod_stato == tab_veicoli.ATT_ATT && v.targa.Equals(p_targa))
                                                         .ToList().ForEach(u => { u.cod_stato = tab_veicoli.ATT_CES; u.id_stato = tab_veicoli.ATT_CES_ID; });
        }

        public static tab_veicoli GetByCfPivaMaxDataInt(string p_cfPivaProprietario, dbEnte p_dbContext)
        {
                return GetList(p_dbContext).Where(r => r.cf_piva_proprietario== p_cfPivaProprietario && r.cod_stato == tab_veicoli.ATT_ATT).OrderByDescending(r => r.data_interrogazione).FirstOrDefault();
        }

        public static tab_veicoli creaVeicolo(string p_targa, bool? p_targaEstera, string p_marca, string p_modello, Int32 p_idTipoVeicolo, string p_fonte, Int32 p_idStrutturaStato, Int32 p_idRisorsa, dbEnte p_context)
        {

            tab_veicoli v_veicolo = new tab_veicoli();

            v_veicolo.data_interrogazione = DateTime.Now;
            v_veicolo.targa = p_targa;

            if (p_targaEstera.HasValue & p_targaEstera.Value)
                v_veicolo.ultima_formalita = "TARGA ESTERA";

            v_veicolo.marca = p_marca;
            v_veicolo.modello = p_modello;
            v_veicolo.tipo_veicolo = p_idTipoVeicolo;
            v_veicolo.fonte = p_fonte;
            v_veicolo.id_stato = 1;
            v_veicolo.cod_stato = "CAR-CAR";
            v_veicolo.data_stato = DateTime.Now;
            v_veicolo.id_struttura_stato = p_idStrutturaStato;
            v_veicolo.id_risorsa_stato = p_idRisorsa;


            p_context.tab_veicoli.Add(v_veicolo);

            return v_veicolo;
        }
    }
}
