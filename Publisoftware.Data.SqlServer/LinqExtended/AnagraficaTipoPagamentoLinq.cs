using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoPagamentoLinq
    {
        public static IQueryable<anagrafica_tipo_pagamento> WhereByPrincipali(this IQueryable<anagrafica_tipo_pagamento> p_query)
        {
            return p_query.Where(d => d.id_tipo_pagamento == anagrafica_tipo_pagamento.BOLPOSTA ||
                                      d.id_tipo_pagamento == anagrafica_tipo_pagamento.ACCRTELEM ||
                                      d.id_tipo_pagamento == anagrafica_tipo_pagamento.BONBANCA ||
                                      d.id_tipo_pagamento == anagrafica_tipo_pagamento.MODF24 ||
                                      d.id_tipo_pagamento == anagrafica_tipo_pagamento.POS ||
                                      d.id_tipo_pagamento == anagrafica_tipo_pagamento.POSTAGIRO ||
                                      d.id_tipo_pagamento == anagrafica_tipo_pagamento.BONTELEM ||
                                      d.id_tipo_pagamento == anagrafica_tipo_pagamento.BOLTELEM);
        }

        public static IQueryable<anagrafica_tipo_pagamento> OrderByDefault(this IQueryable<anagrafica_tipo_pagamento> p_query)
        {
            return p_query.OrderBy(e => e.desc_tipo_pagamento);
        }
    }
}
