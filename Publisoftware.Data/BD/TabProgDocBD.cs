using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabProgDocBD : EntityBD<tab_prog_doc>
    {
        public TabProgDocBD()
        {

        }

        public static int ReturnProgressivoIncrementatoByTipoDocEntrateAnno(int p_idTipoDocEntrate, int p_anno, string p_modalita, dbEnte p_dbContext)
        {
            tab_prog_doc v_tabProgDoc = GetList(p_dbContext).WhereByIdTipoDocEntrateSenzAltro(p_idTipoDocEntrate)
                                                            .WhereByAnno(p_anno)
                                                            .FirstOrDefault();

            if (v_tabProgDoc == null)
            {
                v_tabProgDoc = new tab_prog_doc();

                v_tabProgDoc.anno = p_anno;
                v_tabProgDoc.id_tipo_doc_entrate = p_idTipoDocEntrate;
                v_tabProgDoc.id_entrata = TabTipoDocEntrateBD.GetById(p_idTipoDocEntrate, p_dbContext).id_entrata;
                v_tabProgDoc.prog_tipo_doc_entrata = 0;
                v_tabProgDoc.prog_sped_not_tipo_doc = 0;

                p_dbContext.tab_prog_doc.Add(v_tabProgDoc);
            }

            if (p_modalita == "0")
            {
                v_tabProgDoc.prog_tipo_doc_entrata = v_tabProgDoc.prog_tipo_doc_entrata + 1;
            }
            else
            {
                v_tabProgDoc.prog_sped_not_tipo_doc = v_tabProgDoc.prog_sped_not_tipo_doc.HasValue ? v_tabProgDoc.prog_sped_not_tipo_doc + 1 : 1;
            }

            p_dbContext.SaveChanges();

            if (p_modalita == "0")
            {
                return v_tabProgDoc.prog_tipo_doc_entrata;
            }
            else
            {
                return v_tabProgDoc.prog_sped_not_tipo_doc.Value;
            }
        }

        public static tab_prog_doc GetTabProgDocIncrementatoByTipoDocEntrateAnno(int p_idTipoDocEntrate, int p_anno, dbEnte p_dbContext)
        {
            tab_prog_doc v_tabProgDoc = GetList(p_dbContext).WhereByIdTipoDocEntrateSenzAltro(p_idTipoDocEntrate)
                                                            .WhereByAnno(p_anno)
                                                            .FirstOrDefault();

            if (v_tabProgDoc == null)
            {
                v_tabProgDoc = new tab_prog_doc();

                v_tabProgDoc.anno = p_anno;
                v_tabProgDoc.id_tipo_doc_entrate = p_idTipoDocEntrate;
                v_tabProgDoc.id_entrata = TabTipoDocEntrateBD.GetById(p_idTipoDocEntrate, p_dbContext).id_entrata;
                v_tabProgDoc.prog_tipo_doc_entrata = 0;

                p_dbContext.tab_prog_doc.Add(v_tabProgDoc);
            }

            v_tabProgDoc.prog_tipo_doc_entrata = v_tabProgDoc.prog_tipo_doc_entrata + 1;

            p_dbContext.SaveChanges();

            return v_tabProgDoc;
        }

    }
}
