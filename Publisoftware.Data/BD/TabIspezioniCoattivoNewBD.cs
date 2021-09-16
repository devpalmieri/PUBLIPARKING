using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabIspezioniCoattivoNewBD : EntityBD<tab_ispezioni_coattivo_new>
    {
        public TabIspezioniCoattivoNewBD()
        {

        }

        public static IQueryable<tab_ispezioni_coattivo_new> GetListAttiDaEmettere(dbEnte p_dbContext, int p_idEnte, int p_idRisorsa)
        {
            IQueryable<tab_ispezioni_coattivo_new> res = GetList(p_dbContext)
                .Where(
                        ic => ic.id_ente == p_idEnte
                            &&
                            ic.flag_fine_ispezione_totale.CompareTo("1") == 0
                            &&
                            ic.flag_esito_ispezione_totale.CompareTo("1") == 0
                            &&
                            ic.flag_emissione_avviso_coattivo.CompareTo("1") != 0
                            &&
                            ic.cod_stato.CompareTo(CodStato.VAL_VAL) == 0
                            &&
                            ic.flag_supervisione_finale.CompareTo("1") != 0
                            &&
                            ((ic.id_risorsa_supervisione.HasValue && ic.id_risorsa_supervisione == p_idRisorsa) || !ic.id_risorsa_supervisione.HasValue)
                    );
            return res;
        }

        public static IQueryable<tab_ispezioni_coattivo_new> getIspezioniFor(dbEnte p_dbContext, int p_idEnte, TIPO_INTERROGAZIONE scelta)
        {
            if (scelta == TIPO_INTERROGAZIONE.TUTTE)
            {
                return GetListAttiTutte(p_dbContext, p_idEnte);
            }
            else if (scelta == TIPO_INTERROGAZIONE.IN_ISPEZIONE)
            {
                return GetListAttiInIspezione(p_dbContext, p_idEnte);
            }
            else if (scelta == TIPO_INTERROGAZIONE.ISPEZIONE_POSITIVA)
            {
                return GetListAttiInSupervisione(p_dbContext, p_idEnte);
            }
            else if (scelta == TIPO_INTERROGAZIONE.ISPEZIONE_NEGATIVA)
            {
                return GetListIspezioneNegativa(p_dbContext, p_idEnte);
            }
            else if (scelta == TIPO_INTERROGAZIONE.AVV_DA_EMETTERE)
            {
                return GetListAttiConAvvDaEmettere(p_dbContext, p_idEnte);
            }
            else
            {
                return null;
            }
        }

        public static IQueryable<tab_ispezioni_coattivo_new> GetListAttiTutte(dbEnte p_dbContext, int p_idEnte)
        {
            IQueryable<tab_ispezioni_coattivo_new> res = GetList(p_dbContext)
                .Where(ic =>
                    ic.id_ente == p_idEnte
                    &&
                    ic.cod_stato.Equals(CodStato.VAL_VAL)
                );
            return res;
        }

        public static IQueryable<tab_ispezioni_coattivo_new> GetListAttiInIspezione(dbEnte p_dbContext, int p_idEnte)
        {
            IQueryable<tab_ispezioni_coattivo_new> res = GetList(p_dbContext)
                .Where(ic =>
                    ic.id_ente == p_idEnte
                    &&
                    ic.cod_stato.Equals(CodStato.VAL_VAL)
                    &&
                    (ic.flag_fine_ispezione_totale.Equals("0") || ic.flag_fine_ispezione_totale == null)
                    &&
                    (!ic.flag_supervisione_finale.Equals("1") || ic.flag_supervisione_finale == null)
                );
            return res;
        }

        public static IQueryable<tab_ispezioni_coattivo_new> GetListAttiInSupervisione(dbEnte p_dbContext, int p_idEnte)
        {
            IQueryable<tab_ispezioni_coattivo_new> res = GetList(p_dbContext)
                .Where(ic =>
                    ic.id_ente == p_idEnte
                    &&
                    ic.cod_stato.Equals(CodStato.VAL_VAL)
                    &&
                    ic.flag_fine_ispezione_totale.Equals("1")
                    &&
                    ic.flag_esito_ispezione_totale.Equals("1")
                    &&
                    (!ic.flag_supervisione_finale.Equals("1") || ic.flag_supervisione_finale == null)
                );
            return res;
        }

        public static IQueryable<tab_ispezioni_coattivo_new> GetListIspezioneNegativa(dbEnte p_dbContext, int p_idEnte)
        {
            IQueryable<tab_ispezioni_coattivo_new> res = GetList(p_dbContext)
                .Where(ic =>
                    ic.id_ente == p_idEnte
                    &&
                    ic.cod_stato.Equals(CodStato.VAL_VAL)
                    &&
                    ic.flag_fine_ispezione_totale.Equals("1")
                    &&
                    ic.flag_esito_ispezione_totale.Equals("2")
                    &&
                    (!ic.flag_supervisione_finale.Equals("1") || ic.flag_supervisione_finale == null)
                );
            return res;
        }

        public static IQueryable<tab_ispezioni_coattivo_new> GetListAttiConAvvDaEmettere(dbEnte p_dbContext, int p_idEnte)
        {
            IQueryable<tab_ispezioni_coattivo_new> res = GetList(p_dbContext)
                .Where(ic =>
                    ic.id_ente == p_idEnte
                    &&
                    ic.cod_stato.Equals(CodStato.VAL_VAL)
                    &&
                    ic.flag_fine_ispezione_totale.Equals("1")
                    &&
                    ic.flag_supervisione_finale.Equals("1")
                    &&
                    (!ic.flag_emissione_avviso_coattivo.Equals("1") || ic.flag_emissione_avviso_coattivo == null)
                );
            return res;
        }

        public static void ChiudiIspezione(tab_ispezioni_coattivo_new p_ispezione)
        {
            p_ispezione.cod_stato = tab_ispezioni_coattivo_new.VAL_OLD;

            foreach (join_ispezioni_ingiunzioni j in p_ispezione.join_ispezioni_ingiunzioni.Where(j => j.cod_stato == join_ispezioni_ingiunzioni.VAL_VAL))
            {
                j.cod_stato = join_ispezioni_ingiunzioni.VAL_OLD;

                if (j.tab_ingiunzioni_ispezione.cod_stato == tab_ingiunzioni_ispezione.VAL_VAL
                    &&
                    !j.tab_ingiunzioni_ispezione.join_ispezioni_ingiunzioni.Any(a => a.cod_stato == join_ispezioni_ingiunzioni.VAL_VAL))
                {
                    j.tab_ingiunzioni_ispezione.cod_stato = tab_ingiunzioni_ispezione.VAL_OLD;
                }
            }

            foreach (join_tab_ispezioni_coattivo_tipo_ispezione t in p_ispezione.join_tab_ispezioni_coattivo_tipo_ispezione.Where(j => j.cod_stato == join_tab_ispezioni_coattivo_tipo_ispezione.VAL_VAL))
            {
                t.cod_stato = join_tab_ispezioni_coattivo_tipo_ispezione.VAL_OLD;
                if(t.flag_fine_ispezione.Equals("1") && t.flag_esito_ispezione.Equals("2"))
                {
                    t.flag_fine_ispezione = "2";
                }

            }
        }

        public static void AnnullaIspezione(tab_ispezioni_coattivo_new p_ispezione)
        {
            p_ispezione.cod_stato = tab_ispezioni_coattivo_new.ANN_ANN;

            foreach (join_ispezioni_ingiunzioni j in p_ispezione.join_ispezioni_ingiunzioni.Where(j => j.cod_stato == join_ispezioni_ingiunzioni.VAL_VAL))
            {
                j.cod_stato = join_ispezioni_ingiunzioni.ANN_ANN;

                if (j.tab_ingiunzioni_ispezione.cod_stato == tab_ingiunzioni_ispezione.VAL_VAL
                    &&
                    !j.tab_ingiunzioni_ispezione.join_ispezioni_ingiunzioni.Any(a => a.cod_stato == join_ispezioni_ingiunzioni.VAL_VAL))
                {
                    j.tab_ingiunzioni_ispezione.cod_stato = tab_ingiunzioni_ispezione.ANN_ANN;
                }
            }
        }

        public static tab_ispezioni_coattivo_new GetLastOldByIdContribuente(decimal v_idContribuente, dbEnte p_specificDB)
        {
            return GetList(p_specificDB).WhereByCodStato(tab_ispezioni_coattivo_new.VAL_OLD).WhereByIdContribuente(v_idContribuente).OrderByDataRilevazioneMorosita().ToList().LastOrDefault();
        }

        public static IQueryable<tab_ispezioni_coattivo_new> GetListAperte(dbEnte p_dbContext, int p_idEnte)
        {
            return GetList(p_dbContext).WhereByIdEnte(p_idEnte)
                                .WhereByFlagFineIspezioneTotaleNullOr("0")
                                .WhereByFlagSupervisioneFinaleNullOr("0")
                                .WhereByCodStato(CodStato.VAL_VAL);
        }
    }
}
