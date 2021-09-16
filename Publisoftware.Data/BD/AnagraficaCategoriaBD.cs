using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaCategoriaBD : EntityBD<anagrafica_categoria>
    {
        public AnagraficaCategoriaBD()
        {

        }

        public static anagrafica_categoria getAnagraficaByArticoloCommaSubCodice(string p_Articolo_Comma_SubCodice, int p_idEnte, int p_idEntrata, dbEnte p_context)
        {
            return GetList(p_context).Where(a => a.sigla_cat_contr == p_Articolo_Comma_SubCodice)
                                 .Where(a => a.id_ente == p_idEnte)
                                 .Where(a => a.id_entrata == p_idEntrata).FirstOrDefault();
        }
    }
}
