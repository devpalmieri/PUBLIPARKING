using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinFileBD : EntityBD<join_file>
    {
        public JoinFileBD()
        {
        }

        public static join_file getByIdOggettoNomeFile(Decimal p_idOggetto, string p_nomeFile, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(w => w.id_riferimento == p_idOggetto && w.nome_file == p_nomeFile).SingleOrDefault();
        }

        public static join_file creaJoinFile(int p_idOggetto, string p_nomeFile, DateTime p_dataFile, Int32 p_idOperatore, Int32 p_idStruttura, Int32 p_idEntrata, Int32 p_idTipoRecord, string p_codTipoRecord, dbEnte p_context)
        {

            join_file v_joinFile = new join_file();

            v_joinFile.id_entrata = p_idEntrata;
            v_joinFile.id_riferimento = p_idOggetto;
            v_joinFile.nome_file = p_nomeFile.Replace(".jpg.jpg.jpg", ".jpg").Replace(".jpg.jpg", ".jpg");
            v_joinFile.cod_stato = "ATT-ATT";
            v_joinFile.data_creazione_file = p_dataFile;
            v_joinFile.data_stato = p_dataFile;
            v_joinFile.id_struttura = p_idStruttura;
            v_joinFile.id_risorsa_stato = p_idOperatore;
            v_joinFile.id_tipo_record = p_idTipoRecord;
            v_joinFile.cod_tipo_record = p_codTipoRecord;
            p_context.join_file.Add(v_joinFile);

            return v_joinFile;
        }
    }
}
