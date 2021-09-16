using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class VerbaliTemplateBD : EntityBD<VerbaliTemplate>
    {
        public VerbaliTemplateBD()
        {

        }

        public static VerbaliTemplate GetVerbaleTemplateByNome(string p_nome, DbParkCtx v_context)
        {
            return GetList(v_context).Where(v => v.nome.Equals(p_nome))
                                     .OrderByDescending(v => v.idVerbaleTemplate)
                                     .FirstOrDefault();
        }
    }
}
