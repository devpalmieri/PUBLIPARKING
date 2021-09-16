using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabOggettiBD : EntityBD<tab_oggetti>
    {
        public TabOggettiBD()
        {

        }

        public static tab_oggetti GetByTarga(string p_targa, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(o => o.descrizione_oggetto.Equals(p_targa) && o.id_tipo_oggetto == tab_tipo_oggetto.TASSA_AUTO).SingleOrDefault();
        }
        public static tab_oggetti creaOggetto(string p_numStallo, DateTime p_emissioneVerbale, Int32 p_idStrutturaStato, Int32 p_idOperatore, Int32 p_idVeicolo, Int32 p_idToponimo, double p_X, double p_Y, Int32 p_idTipoOggetto, Int32 p_idEnte, Int32 p_idEntrata, dbEnte p_context)
        {            
            string v_x = p_X.ToString();
            string v_y = p_Y.ToString();
            tab_oggetti v_oggetto = new tab_oggetti();

            v_oggetto.id_ente = p_idEnte;
            v_oggetto.id_ente_gestito = p_idEnte;
            v_oggetto.id_entrata = p_idEntrata;
            v_oggetto.id_tipo_oggetto = p_idTipoOggetto;
            v_oggetto.id_toponimo = p_idToponimo;
            v_oggetto.id_edificio = p_idVeicolo;

            tab_toponimi v_toponimo = TabToponimiBD.GetById(p_idToponimo, p_context);


            v_oggetto.indirizzo = v_toponimo.descrizione_toponimo;
            v_oggetto.frazione = v_toponimo.frazione_toponimo;

            ser_comuni v_comune = SerComuniBD.GetByCodComune(v_toponimo.cod_comune_toponimo, p_context);

            v_oggetto.citta = v_comune.des_comune;

            ser_province v_provincia = SerProvinceBD.GetByCodProvincia(v_comune.cod_provincia, p_context);

            v_oggetto.prov = v_provincia.sig_provincia;

            v_oggetto.cod_citta = v_toponimo.cod_comune_toponimo;
            v_oggetto.cap = v_toponimo.cap_toponimo;


            v_oggetto.descrizione_oggetto = p_numStallo;
            v_oggetto.longitudine_oggetto = v_x;
            v_oggetto.latitudine_oggetto = v_y;
            v_oggetto.data_attivazione_oggetto = p_emissioneVerbale;
            v_oggetto.id_stato_oggetto = 1;
            v_oggetto.cod_stato_oggetto = "ATT-ATT";
            v_oggetto.data_stato = p_emissioneVerbale;
            v_oggetto.id_struttura_stato = p_idStrutturaStato;
            v_oggetto.id_risorsa_stato = p_idOperatore;

            p_context.tab_oggetti.Add(v_oggetto);

            v_oggetto.cod_oggetto = p_idEnte.ToString() + v_oggetto.id_oggetto.ToString();


            return v_oggetto;
        }

    }
}
