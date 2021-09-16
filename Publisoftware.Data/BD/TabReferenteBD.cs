using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabReferenteBD : EntityBD<tab_referente>
    {
        public TabReferenteBD()
        {

        }

        public static bool CheckDuplicatoCodiceFiscalePIVA(string p_codiceFiscalePIVA, int p_idReferente, dbEnte p_dbContext)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                return GetList(p_dbContext).Any(d => d.id_tab_referente != p_idReferente && 
                                                   ((d.cod_fiscale.Equals(p_codiceFiscalePIVA) && 
                                                     d.id_tipo_referente != anagrafica_tipo_contribuente.DITTA_INDIVIDUALE_ID) || 
                                                    (d.id_anag_contribuente.HasValue && 
                                                     d.tab_contribuente.cod_fiscale.Equals(p_codiceFiscalePIVA) && 
                                                     d.tab_contribuente.id_tipo_contribuente != anagrafica_tipo_contribuente.DITTA_INDIVIDUALE_ID)));
            }
            else if (p_codiceFiscalePIVA.Length == 11)
            {
                return GetList(p_dbContext).Any(d => d.id_tab_referente != p_idReferente && 
                                                    (d.p_iva.Equals(p_codiceFiscalePIVA) || 
                                                    (d.id_anag_contribuente.HasValue &&
                                                     d.tab_contribuente.p_iva.Equals(p_codiceFiscalePIVA))));
            }
            else
            {
                return false;
            }
        }

        public static bool CheckDuplicatoCodiceFiscalePersonaGiuridica(string p_codiceFiscalePersonaGiuridica, int p_idReferente, dbEnte p_dbContext)
        {
            if (!string.IsNullOrEmpty(p_codiceFiscalePersonaGiuridica) && p_codiceFiscalePersonaGiuridica.Length == 11)
            {
                return GetListInternal(p_dbContext).Any(d => d.id_tab_referente != p_idReferente && (d.cod_fiscale_pg.Equals(p_codiceFiscalePersonaGiuridica) || d.tab_contribuente.cod_fiscale_pg.Equals(p_codiceFiscalePersonaGiuridica)));
            }
            else
            {
                return false;
            }
        }

        //public static tab_referente GetByCodiceFiscalePIVAIdContribuente(string p_codiceFiscalePIVA, decimal p_idContribuente, dbEnte p_dbContext)
        //{
        //    tab_referente v_referente = null;

        //    if (p_codiceFiscalePIVA.Length == 16)
        //    {
        //        if (GetList(p_dbContext).Any(d => d.cod_fiscale.Equals(p_codiceFiscalePIVA)))
        //        {
        //            v_referente = GetByCodiceFiscalePIVA(p_codiceFiscalePIVA, p_dbContext);
        //        }
        //    }
        //    else if (p_codiceFiscalePIVA.Length == 11)
        //    {
        //        if (GetList(p_dbContext).Any(d => d.p_iva.Equals(p_codiceFiscalePIVA)))
        //        {
        //            v_referente = GetByCodiceFiscalePIVA(p_codiceFiscalePIVA, p_dbContext);
        //        }
        //    }

        //    if (v_referente != null)
        //    {
        //        List<join_referente_contribuente> v_joinReferenteContribuenteList = JoinReferenteContribuenteBD.GetList(p_dbContext).Where(d => d.id_tab_referente == v_referente.id_tab_referente).ToList();

        //        bool v_esito = false;

        //        foreach (join_referente_contribuente v_joinReferenteContribuente in v_joinReferenteContribuenteList)
        //        {
        //            if (v_joinReferenteContribuente.id_anag_contribuente == p_idContribuente)
        //            {
        //                v_esito = true;
        //            }
        //        }

        //        if (v_esito)
        //        {
        //            return v_referente;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public static tab_referente GetByCodiceFiscalePIVADiretto(string p_codiceFiscalePIVA, dbEnte p_dbContext)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                if (GetList(p_dbContext).Any(d => d.cod_fiscale.Equals(p_codiceFiscalePIVA)))
                {
                    return GetList(p_dbContext).Where(d => d.cod_fiscale.Equals(p_codiceFiscalePIVA)).SingleOrDefault();
                }
                else
                {
                    return null;
                }
            }
            else if (p_codiceFiscalePIVA.Length == 11)
            {
                if (GetList(p_dbContext).Any(d => d.p_iva.Equals(p_codiceFiscalePIVA)))
                {
                    return GetList(p_dbContext).Where(d => d.p_iva.Equals(p_codiceFiscalePIVA)).SingleOrDefault();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        // 15/11/2019: Come GetByCodiceFiscalePIVA ma con condizione su tipo di contribuente:
        //             - se cf "id_tipo_referente/id_tipo_contribuente==1"
        //             - se piva "id_tipo_referente/id_tipo_contribuente!=1"
        public static IQueryable<tab_referente> QueryByCodiceFiscalePIVAAndIdTipoPersonaQueryOrNull(string cfPiva, dbEnte ctx)
        {
            cfPiva = (cfPiva ?? "").Trim();
            IQueryable<tab_referente> query = null;
            if (cfPiva.Length == 16)
            { 
                query = GetList(ctx).Where(c =>
                        (c.id_anag_contribuente == null && c.id_tipo_referente==anagrafica_tipo_contribuente.PERS_FISICA_ID && c.cod_fiscale != null && c.cod_fiscale.ToUpper().Equals(cfPiva))
                        || (c.id_anag_contribuente != null && c.tab_contribuente.id_tipo_contribuente==anagrafica_tipo_contribuente.PERS_FISICA_ID
                                                           && (c.tab_contribuente.cod_fiscale != null && c.tab_contribuente.cod_fiscale.ToUpper().Equals(cfPiva)))
                        );
            }
            else if (cfPiva.Length == 11)
            {
                    query = GetList(ctx).Where(c =>
                            (
                                c.id_anag_contribuente == null && c.id_tipo_referente!=anagrafica_tipo_contribuente.PERS_FISICA_ID &&
                                    (
                                        (   c.p_iva != null       && c.p_iva.Equals(cfPiva)) || 
                                        (c.cod_fiscale_pg != null && c.cod_fiscale_pg.Equals(cfPiva))
                                    )
                            )
                            || 
                            (
                                c.id_anag_contribuente != null  && c.tab_contribuente.id_tipo_contribuente!=anagrafica_tipo_contribuente.PERS_FISICA_ID &&
                                (
                                    (   c.tab_contribuente.p_iva != null       && c.tab_contribuente.p_iva.Equals(cfPiva)) || 
                                    (c.tab_contribuente.cod_fiscale_pg != null && c.tab_contribuente.cod_fiscale_pg.Equals(cfPiva))
                                )
                                
                            )
                        );
            }
            return query;
        }

        // NOTA: attenzione, non controlla id_tipo_[referente/contribuente], meglio usare 
        //           QueryByCodiceFiscalePIVAAndIdTipoPersonaQueryOrNull(p_codiceFiscalePIVA, p_dbContext)?.SingleorDefult()
        //       o MEGLIO, che se hai CF_PIVA ripetute SingleOrDefault lancia eccezione:
        //           QueryByCodiceFiscalePIVAAndIdTipoPersonaQueryOrNull(p_codiceFiscalePIVA, p_dbContext)?.OrderBy(x=>x.id_tab_referente).FirstOrDefult()
        public static tab_referente GetByCodiceFiscalePIVA(string p_codiceFiscalePIVA, dbEnte p_dbContext)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                var query = GetList(p_dbContext).Where(c => (c.id_anag_contribuente == null && c.cod_fiscale != null && c.cod_fiscale.Equals(p_codiceFiscalePIVA))
                                                    || (c.id_anag_contribuente != null && (c.tab_contribuente.cod_fiscale != null && c.tab_contribuente.cod_fiscale.Equals(p_codiceFiscalePIVA))));
                return query.SingleOrDefault();
            }
            else if (p_codiceFiscalePIVA.Length == 11)
            {
                var query = GetList(p_dbContext).Where(c =>
                    (c.id_anag_contribuente == null && c.p_iva != null && c.p_iva.Equals(p_codiceFiscalePIVA))
                    || (c.id_anag_contribuente == null && c.cod_fiscale_pg != null && c.cod_fiscale_pg.Equals(p_codiceFiscalePIVA))
                    || (
                            c.id_anag_contribuente != null &&
                            (
                                (c.tab_contribuente.p_iva != null && c.tab_contribuente.p_iva.Equals(p_codiceFiscalePIVA))
                                // 15/11/2019: aggiunto cod_fiscale_pg
                                || (c.tab_contribuente.cod_fiscale_pg != null && c.tab_contribuente.cod_fiscale_pg.Equals(p_codiceFiscalePIVA))
                            )
                       )
                    );
                return query.SingleOrDefault();
            }
            else
            {
                return null;
            }
        }

        //public static tab_referente GetByCodiceFiscalePIVANonAnnullato(string p_codiceFiscalePIVA, dbEnte p_dbContext)
        //{
        //    if (p_codiceFiscalePIVA.Length == 16)
        //    {

        //        return GetList(p_dbContext).Where(c => !c.cod_stato.StartsWith(anagrafica_stato_contribuente.ANN) && ((c.id_anag_contribuente == null && c.cod_fiscale != null && c.cod_fiscale.Equals(p_codiceFiscalePIVA))
        //                                            || (c.id_anag_contribuente != null && (c.tab_contribuente.cod_fiscale != null && c.tab_contribuente.cod_fiscale.Equals(p_codiceFiscalePIVA)))))
        //                                   .SingleOrDefault();
        //    }
        //    else if (p_codiceFiscalePIVA.Length == 11)
        //    {
        //        return GetList(p_dbContext).Where(c => !c.cod_stato.StartsWith(anagrafica_stato_contribuente.ANN) && ((c.id_anag_contribuente == null && c.p_iva != null && c.p_iva.Equals(p_codiceFiscalePIVA))
        //                                            || (c.id_anag_contribuente != null && (c.tab_contribuente.p_iva != null && c.tab_contribuente.p_iva.Equals(p_codiceFiscalePIVA)))))
        //                                   .SingleOrDefault();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public static tab_referente_sto AggiornaStorico(int p_idReferente, 
                                                        dbEnte p_dbContext, 
                                                        bool creareSoloSeVariato = false, 
                                                        bool bSaveChanges = true)
        {
            tab_referente_sto v_referenteStoLast = TabReferenteStoBD.GetLastReferenteStoByIdReferente(p_idReferente, p_dbContext);
            tab_referente v_referente = GetById(p_idReferente, p_dbContext);

            return AggiornaStorico(v_referente, v_referenteStoLast, p_dbContext, creareSoloSeVariato, bSaveChanges);
        }

        public static tab_referente_sto AggiornaStorico(tab_referente v_referente, tab_referente_sto v_referenteStoLast, dbEnte p_dbContext, bool creareSoloSeVariato = false, bool bSaveChanges = true)
        {
            bool bCreaNuovoStorico;
            if (v_referenteStoLast == null) // Se non c'è già uno storico deve creare!
            {
                bCreaNuovoStorico = true;
            }
            else if (creareSoloSeVariato)
            {
                // Se dobbiamo creare solo se i dati sono variati, impostiamo temporaneamente a false
                // e verifichiamo se i dati sono variati, nel qual caso imposteremo a true:
                bCreaNuovoStorico = false;
            }
            else
            {
                bCreaNuovoStorico = true;
            }

            int countModified = 0;
            if (v_referenteStoLast != null && v_referenteStoLast.id_anag_contribuente == null && v_referente.id_anag_contribuente == null)
            {
                if (!v_referenteStoLast.data_inizio_stato.HasValue)
                {
                    v_referenteStoLast.data_inizio_stato = Convert.ToDateTime("01/1/1900");
                    ++countModified;
                }

                if (!v_referenteStoLast.data_inizio_validita_indirizzo.HasValue)
                {
                    v_referenteStoLast.data_inizio_validita_indirizzo = Convert.ToDateTime("01/1/1900");
                    ++countModified;
                }

                if (!v_referenteStoLast.data_inizio_validita_altri_dati.HasValue)
                {
                    v_referenteStoLast.data_inizio_validita_altri_dati = Convert.ToDateTime("01/1/1900");
                    ++countModified;
                }

                //Variazione stato
                if (v_referente.id_stato_referente != v_referenteStoLast.id_stato_referente ||
                    v_referente.id_tipo_referente != v_referenteStoLast.id_tipo_referente)
                {
                    bCreaNuovoStorico = true;

                    if (v_referente.data_inizio_stato.HasValue &&
                        v_referenteStoLast.data_inizio_stato.HasValue &&
                        v_referente.data_inizio_stato.Value.Date == v_referenteStoLast.data_inizio_stato.Value.Date)
                    {
                        v_referenteStoLast.data_fine_stato = v_referente.data_inizio_stato;
                    }
                    else
                    {
                        v_referenteStoLast.data_fine_stato = v_referente.data_inizio_stato.HasValue ? v_referente.data_inizio_stato.Value.AddDays(-1) : DateTime.Now.AddDays(-1);
                    }

                    ++countModified;
                }

                //Variazione indirizzo
                if (v_referente.id_toponimo != v_referenteStoLast.id_toponimo ||
                    v_referente.id_strada_db_poste != v_referenteStoLast.id_strada_db_poste ||
                    v_referente.cod_citta != v_referenteStoLast.cod_citta ||
                   (v_referente.frazione ?? "") != (v_referenteStoLast.frazione ?? "") ||
                   (v_referente.indirizzo ?? "") != (v_referenteStoLast.indirizzo ?? "") ||
                   (v_referente.cap ?? "") != (v_referenteStoLast.cap ?? "") ||
                   (v_referente.stato ?? "") != (v_referenteStoLast.stato ?? "") ||
                   (v_referente.condominio ?? "") != (v_referenteStoLast.condominio ?? "") ||
                    v_referente.nr_civico != v_referenteStoLast.nr_civico ||
                   (v_referente.sigla_civico ?? "") != (v_referenteStoLast.sigla_civico ?? "") ||
                   (v_referente.colore ?? "") != (v_referenteStoLast.colore ?? "") ||
                    v_referente.km != v_referenteStoLast.km ||
                    v_referente.id_toponimo_normalizzato != v_referenteStoLast.id_toponimo_normalizzato)
                {
                    bCreaNuovoStorico = true;

                    if (v_referente.data_inizio_validita_indirizzo.HasValue &&
                        v_referenteStoLast.data_inizio_validita_indirizzo.HasValue &&
                        v_referente.data_inizio_validita_indirizzo.Value.Date == v_referenteStoLast.data_inizio_validita_indirizzo.Value.Date)
                    {
                        v_referenteStoLast.data_fine_validita_indirizzo = v_referente.data_inizio_validita_indirizzo;
                    }
                    else
                    {
                        v_referenteStoLast.data_fine_validita_indirizzo = v_referente.data_inizio_validita_indirizzo.HasValue ? v_referente.data_inizio_validita_indirizzo.Value.AddDays(-1) : DateTime.Now.AddDays(-1);
                    }

                    ++countModified;
                }

                //Variazione altri dati
                if ((v_referente.nome ?? "") != (v_referenteStoLast.nome ?? "") ||
                    (v_referente.cognome ?? "") != (v_referenteStoLast.cognome ?? "") ||
                    (v_referente.cod_fiscale ?? "") != (v_referenteStoLast.cod_fiscale ?? "") ||
                    (v_referente.rag_sociale ?? "") != (v_referenteStoLast.rag_sociale ?? "") ||
                    (v_referente.denominazione_commerciale ?? "") != (v_referenteStoLast.denominazione_commerciale ?? "") ||
                    (v_referente.p_iva ?? "") != (v_referenteStoLast.p_iva ?? "") ||
                    (v_referente.cod_fiscale_pg ?? "") != (v_referenteStoLast.cod_fiscale_pg ?? "") ||
                    (v_referente.fax ?? "") != (v_referenteStoLast.fax ?? "") ||
                    (v_referente.cell ?? "") != (v_referenteStoLast.cell ?? "") ||
                    (v_referente.tel ?? "") != (v_referenteStoLast.tel ?? "") ||
                    (v_referente.pec ?? "") != (v_referenteStoLast.pec ?? "") ||
                    (v_referente.e_mail ?? "") != (v_referenteStoLast.e_mail ?? "") ||
                    (v_referente.stato_nas ?? "") != (v_referenteStoLast.stato_nas ?? "") ||
                    v_referente.cod_comune_nas != v_referenteStoLast.cod_comune_nas ||
                    v_referente.id_sesso != v_referenteStoLast.id_sesso ||
                    v_referente.data_nas != v_referenteStoLast.data_nas ||
                    v_referente.data_morte != v_referenteStoLast.data_morte)
                {
                    bCreaNuovoStorico = true;

                    if (v_referente.data_inizio_validita_altri_dati.HasValue &&
                        v_referenteStoLast.data_inizio_validita_altri_dati.HasValue &&
                        v_referente.data_inizio_validita_altri_dati.Value == v_referenteStoLast.data_inizio_validita_altri_dati.Value)
                    {
                        v_referenteStoLast.data_fine_validita_altri_dati = v_referente.data_inizio_validita_altri_dati;
                    }
                    else
                    {
                        v_referenteStoLast.data_fine_validita_altri_dati = v_referente.data_inizio_validita_altri_dati.HasValue ? v_referente.data_inizio_validita_altri_dati.Value.AddDays(-1) : DateTime.Now.AddDays(-1);
                    }

                    ++countModified;
                }

                if (countModified > 0)
                {
                    p_dbContext.Entry(v_referenteStoLast).State = System.Data.Entity.EntityState.Modified;
                }
            }

            if (bCreaNuovoStorico)
            {
                tab_referente_sto v_referenteSto = new tab_referente_sto();
                v_referenteSto.setProperties(v_referente);
                v_referenteSto.tab_referente = v_referente;
                p_dbContext.tab_referente_sto.Add(v_referenteSto);

                if (bSaveChanges)
                {
                    p_dbContext.SaveChanges();
                }
                return v_referenteSto;
            }

            if (bSaveChanges)
            {
                p_dbContext.SaveChanges();
            }
            return v_referenteStoLast;
        }

        public static tab_referente GetReferenteByIdReferente(int p_idReferente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_tab_referente == p_idReferente).FirstOrDefault();
        }

        public static IQueryable<tab_referente> GetListRefAttByCf(int p_idEnteG, int p_idEnte, string p_codFiscale, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.cod_fiscale == p_codFiscale && c.cod_stato.StartsWith(anagrafica_stato_contribuente.ATT));
        }

        public static IQueryable<tab_referente> GetListMancataNotifica(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.flag_ricerca_indirizzo_mancata_notifica == "1");
        }
    }
}
