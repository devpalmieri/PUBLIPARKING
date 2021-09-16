using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinRisorseSerComuniBD : EntityBD<join_risorse_ser_comuni>
    {
        public JoinRisorseSerComuniBD()
        {

        }

        public static IQueryable<join_risorse_ser_comuni> GetByTipoDocEntrateRegioneProvinciaRangeValidita(int? cod_provincia,
                                                                                                           int? cod_regione,
                                                                                                           DateTime dataQuery,
                                                                                                           int id_tipo_doc_entrate,
                                                                                                           int id_ruolo_mansione,
                                                                                                           int id_ente,
                                                                                                           dbEnte p_dbContext)
        {
            if (id_ruolo_mansione == anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID)
            {
                if (GetList(p_dbContext).Where(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                    (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                    (x.id_tipo_doc_entrate == id_tipo_doc_entrate || x.id_tipo_doc_entrate == null) &&
                                                     x.data_inizio_validita <= dataQuery.Date &&
                                                     dataQuery.Date <= x.data_fine_validita &&
                                                     x.anagrafica_risorse.id_ente_appartenenza == id_ente &&
                                                     x.anagrafica_risorse.id_ruolo_mansione == anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID).Count() > 0)
                {
                    return GetList(p_dbContext).Where(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                     (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                     (x.id_tipo_doc_entrate == id_tipo_doc_entrate || x.id_tipo_doc_entrate == null) &&
                                                      x.data_inizio_validita <= dataQuery.Date &&
                                                      dataQuery.Date <= x.data_fine_validita &&
                                                      x.anagrafica_risorse.id_ente_appartenenza == id_ente &&
                                                      x.anagrafica_risorse.id_ruolo_mansione == anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID);
                }
                else
                {
                    return GetList(p_dbContext).Where(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                           (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                           (x.id_tipo_doc_entrate == id_tipo_doc_entrate || x.id_tipo_doc_entrate == null) &&
                                                            x.data_inizio_validita <= dataQuery.Date &&
                                                            dataQuery.Date <= x.data_fine_validita &&
                                                            x.anagrafica_risorse.id_ente_appartenenza == anagrafica_ente.ID_ENTE_PUBLISERVIZI &&
                                                            x.anagrafica_risorse.id_ruolo_mansione == anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID);
                }
            }
            else
            {
                if (GetList(p_dbContext).Where(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                    (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                    (x.id_tipo_doc_entrate == id_tipo_doc_entrate || x.id_tipo_doc_entrate == null) &&
                                                     x.data_inizio_validita <= dataQuery.Date &&
                                                     dataQuery.Date <= x.data_fine_validita &&
                                                     x.anagrafica_risorse.id_ente_appartenenza == id_ente &&
                                                    (x.anagrafica_risorse.id_ruolo_mansione == null || x.anagrafica_risorse.id_ruolo_mansione != anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID)).Count() > 0)
                {
                    return GetList(p_dbContext).Where(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                           (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                           (x.id_tipo_doc_entrate == id_tipo_doc_entrate || x.id_tipo_doc_entrate == null) &&
                                                            x.data_inizio_validita <= dataQuery.Date &&
                                                            dataQuery.Date <= x.data_fine_validita &&
                                                            x.anagrafica_risorse.id_ente_appartenenza == id_ente &&
                                                           (x.anagrafica_risorse.id_ruolo_mansione == null || x.anagrafica_risorse.id_ruolo_mansione != anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID));
                }
                else
                {
                    return GetList(p_dbContext).Where(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                           (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                           (x.id_tipo_doc_entrate == id_tipo_doc_entrate || x.id_tipo_doc_entrate == null) &&
                                                            x.data_inizio_validita <= dataQuery.Date &&
                                                            dataQuery.Date <= x.data_fine_validita &&
                                                            x.anagrafica_risorse.id_ente_appartenenza == anagrafica_ente.ID_ENTE_PUBLISERVIZI &&
                                                           (x.anagrafica_risorse.id_ruolo_mansione == null || x.anagrafica_risorse.id_ruolo_mansione != anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID));
                }
            }
        }

        public static IQueryable<join_risorse_ser_comuni> GetByRegioneProvinciaRangeValidita(int? cod_provincia,
                                                                                             int? cod_regione,
                                                                                             DateTime dataQuery,
                                                                                             int id_ente,
                                                                                             dbEnte p_dbContext)
        {
            if (GetList(p_dbContext).Where(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                 x.data_inizio_validita <= dataQuery.Date &&
                                                 dataQuery.Date <= x.data_fine_validita &&
                                                 x.anagrafica_risorse.id_ente_appartenenza == id_ente &&
                                                 x.anagrafica_risorse.id_ruolo_mansione == anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID).Count() > 0)
            {
                return GetList(p_dbContext).Where(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                       (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                        x.data_inizio_validita <= dataQuery.Date &&
                                                        dataQuery.Date <= x.data_fine_validita &&
                                                        x.anagrafica_risorse.id_ente_appartenenza == id_ente &&
                                                        x.anagrafica_risorse.id_ruolo_mansione == anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID);
            }
            else
            {
                return GetList(p_dbContext).Where(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                       (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                        x.data_inizio_validita <= dataQuery.Date &&
                                                        dataQuery.Date <= x.data_fine_validita &&
                                                        x.anagrafica_risorse.id_ente_appartenenza == anagrafica_ente.ID_ENTE_PUBLISERVIZI &&
                                                        x.anagrafica_risorse.id_ruolo_mansione == anagrafica_ruolo_mansione.COD_RUOLO_MANSIONE_UFFICIALE_RISCOSSIONE_ID);
            }
        }
    }
}
