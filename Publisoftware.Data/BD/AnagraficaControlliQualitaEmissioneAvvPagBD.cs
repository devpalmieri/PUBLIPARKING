using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaControlliQualitaEmissioneAvvPagBD : EntityBD<anagrafica_controlli_qualita_emissione_avvpag>
    {
        public AnagraficaControlliQualitaEmissioneAvvPagBD()
        {

        }

        public static IQueryable<anagrafica_controlli_qualita_emissione_avvpag> getControlliFor(int id_tipo_lista, int id_ente , int id_entrata, int id_tipo_servizio, dbEnte dbContext)
        {
            return dbContext.anagrafica_controlli_qualita_emissione_avvpag
                .Where(acq => acq.id_ente == anagrafica_ente.ID_ENTE_GENERICO || acq.id_ente == id_ente)
                .Where(acq => acq.id_tipo_lista == id_tipo_lista)
                .Where(acq => acq.id_entrata == anagrafica_entrate.NESSUNA_ENTRATA || acq.id_entrata == id_entrata)
                .Where(acq => !acq.id_tipo_servizio.HasValue || acq.id_tipo_servizio == id_tipo_servizio);
        }
    }
}
