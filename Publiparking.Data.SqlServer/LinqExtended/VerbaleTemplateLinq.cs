using Publiparking.Data.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publiparking.Data.LinqExtended
{
    public static class VerbaleTemplateLinq
    {
        public static VerbaleTemplateDTO toVerbaleTemplateDTO(this VerbaliTemplate iniziale)
        {
            VerbaleTemplateDTO v_template = new VerbaleTemplateDTO();
            v_template.id = iniziale.idVerbaleTemplate;
            v_template.nome = iniziale.nome;
            v_template.testo = iniziale.testo;
            return v_template;
        }
    }
}
