using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabContribuenteBD : EntityBD<tab_contribuente>
    {
        public TabContribuenteBD()
        {

        }

        public static new IQueryable<tab_contribuente> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            //Il dottore ha voluto togliere il blocco sulle 'acque reflue'
            //return GetListInternal(p_dbContext).Where(d => d.cod_fonte != "ANA_AR" && d.cod_fonte != "AR_" && (p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_anag_contribuente)));
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_anag_contribuente));
        }

        public static new tab_contribuente GetById(Decimal p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_anag_contribuente == p_id);
        }

        public static tab_contribuente GetByIdDetached(Decimal p_id, dbEnte p_dbContext)
        {
            tab_contribuente risp = null;

            try
            {
                risp = GetList(p_dbContext).SingleOrDefault(c => c.id_anag_contribuente == p_id);
                if (risp != null)
                {
                    ((IObjectContextAdapter)p_dbContext).ObjectContext.Detach(risp);
                }
            }
            catch (Exception) { }

            return risp;
        }

        public static IQueryable<tab_contribuente> QueryById(Decimal p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.id_anag_contribuente == p_id);
        }

        public static void SwapReferente(int p_idReferente, dbEnte p_dbContext)
        {
            tab_referente v_referente = TabReferenteBD.GetById(p_idReferente, p_dbContext);

            if (v_referente != null)
            {
                tab_contribuente v_contribuente;

                if (v_referente.cod_fiscale != null)
                {
                    v_contribuente = QueryByCodiceFiscalePIVAAndIdTipoPersonaQueryOrNull(v_referente.cod_fiscale, p_dbContext).FirstOrDefault();
                }
                else
                {
                    v_contribuente = QueryByCodiceFiscalePIVAAndIdTipoPersonaQueryOrNull(v_referente.p_iva, p_dbContext).FirstOrDefault();
                }

                v_referente.id_anag_contribuente = v_contribuente.id_anag_contribuente;
                AzzeraCampiReferente(v_referente);

                p_dbContext.SaveChanges();
            }
        }

        public static void AzzeraCampiReferente(tab_referente v_referente)
        {
            v_referente.id_stato_referente = null;
            v_referente.cod_stato_referente = null;
            v_referente.data_inizio_stato = null;
            v_referente.data_fine_stato = null;
            v_referente.fonte_stato = null;
            v_referente.nome = null;
            v_referente.cognome = null;
            v_referente.cod_fiscale = null;
            v_referente.rag_sociale = null;
            v_referente.denominazione_commerciale = null;
            v_referente.p_iva = null;
            v_referente.stato_nas = null;
            v_referente.comune_nas = null;
            v_referente.cod_comune_nas = null;
            v_referente.data_nas = null;
            v_referente.data_morte = null;
            v_referente.id_sesso = null;
            v_referente.id_tipo_referente = null;
            v_referente.id_toponimo = null;
            v_referente.id_strada_db_poste = null;
            v_referente.indirizzo = null;
            v_referente.edificio = null;
            v_referente.nr_civico = null;
            v_referente.sigla_civico = null;
            v_referente.colore = null;
            v_referente.km = null;
            v_referente.scala = null;
            v_referente.piano = null;
            v_referente.interno = null;
            v_referente.condominio = null;
            v_referente.frazione = null;
            v_referente.stato = null;
            v_referente.citta = null;
            v_referente.cod_citta = null;
            v_referente.cap = null;
            v_referente.prov = null;
            v_referente.e_mail = null;
            v_referente.pec = null;
            v_referente.fax = null;
            v_referente.tel = null;
            v_referente.cell = null;
            v_referente.note = null;
        }

        public static void AggiornaContribuenteDeceduto(Decimal p_idContribuente, dbEnte p_dbContext)
        {
            tab_contribuente v_contribuente = GetById(p_idContribuente, p_dbContext);

            v_contribuente.id_stato_contribuente = anagrafica_stato_contribuente.DEC_RIL_ID;
            v_contribuente.cod_stato_contribuente = anagrafica_stato_contribuente.DEC_RIL;

            p_dbContext.SaveChanges();
        }

        public static void AzzeraFlagErede(Decimal p_idContribuente, dbEnte p_dbContext)
        {
            tab_contribuente v_contribuente = GetById(p_idContribuente, p_dbContext);
            v_contribuente.flag_ricerca_eredi = "0";
            p_dbContext.SaveChanges();
        }

        public static void AzzeraFlagNuovoReferente(Decimal p_idContribuente, dbEnte p_dbContext)
        {
            tab_contribuente v_contribuente = GetById(p_idContribuente, p_dbContext);
            v_contribuente.flag_ricerca_nuovo_referente_pg = "0";
            p_dbContext.SaveChanges();
        }

        public static tab_contribuente_sto_new AggiornaStorico(Decimal p_idContribuente, dbEnte p_dbContext, bool creareSoloSeVariato = false, bool copiaSoloPropElementari = true, bool bSaveChanges = true)
        {
            tab_contribuente_sto_new v_contribuenteStoLast = TabContribuenteStoNewBD.GetList(p_dbContext)
                                                                                    .WhereByIdContribuente(p_idContribuente)
                                                                                    .OrderByStorico()
                                                                                    .FirstOrDefault();

            tab_contribuente v_contribuente = GetById(p_idContribuente, p_dbContext);
            return AggiornaStorico(v_contribuente, v_contribuenteStoLast, p_dbContext, creareSoloSeVariato, copiaSoloPropElementari, bSaveChanges);
        }

        public static tab_contribuente_sto_new AggiornaStorico(tab_contribuente v_contribuente,
                                                               dbEnte p_dbContext,
                                                               bool creareSoloSeVariato = false,
                                                               bool copiaSoloPropElementari = true,
                                                               bool bSaveChanges = true,
                                                               bool forceIgnoreInvalidList = false)
        {
            tab_contribuente_sto_new v_contribuenteStoLast = TabContribuenteStoNewBD.GetList(p_dbContext)
                                                                                    .WhereByIdContribuente(v_contribuente.id_anag_contribuente)
                                                                                    .OrderByStorico()
                                                                                    .FirstOrDefault();

            return AggiornaStorico(v_contribuente, v_contribuenteStoLast, p_dbContext, creareSoloSeVariato, copiaSoloPropElementari, bSaveChanges, forceIgnoreInvalidList);
        }

        public static tab_contribuente_sto_new AggiornaStorico(tab_contribuente v_contribuente,
                                                               tab_contribuente_sto_new v_contribuenteStoLast,
                                                               dbEnte p_dbContext,
                                                               bool creareSoloSeVariato = false,
                                                               bool copiaSoloPropElementari = true,
                                                               bool bSaveChanges = true,
                                                               bool forceIgnoreInvalidList = false)
        {
            DateTime dtNow = DateTime.Now;

            bool bCreaNuovoStorico = v_contribuenteStoLast == null; // Se non c'è già uno storico deve creare!

            if (v_contribuenteStoLast != null)
            {
                int countModified = 0;

                if (!v_contribuenteStoLast.data_inizio_validita_indirizzo.HasValue)
                {
                    v_contribuenteStoLast.data_inizio_validita_indirizzo = Convert.ToDateTime("01/1/1900");
                    ++countModified;
                }

                if (!v_contribuenteStoLast.data_inizio_validita_altri_dati.HasValue)
                {
                    v_contribuenteStoLast.data_inizio_validita_altri_dati = Convert.ToDateTime("01/1/1900");
                    ++countModified;
                }

                //if (!v_contribuenteStoLast.data_ultima_verifica.HasValue)
                //{
                //    v_contribuenteStoLast.data_ultima_verifica = Convert.ToDateTime("01/1/1900");
                //    ++countModified;
                //}

                //Variazione stato
                if (v_contribuente.id_stato_contribuente != v_contribuenteStoLast.id_stato_contribuente ||
                    v_contribuente.id_tipo_contribuente != v_contribuenteStoLast.id_tipo_contribuente)
                {
                    bCreaNuovoStorico = true;

                    if (v_contribuente.data_inizio_stato.Date == v_contribuenteStoLast.data_inizio_stato.Date)
                    {
                        v_contribuenteStoLast.data_fine_stato = v_contribuente.data_inizio_stato;
                    }
                    else
                    {
                        v_contribuenteStoLast.data_fine_stato = v_contribuente.data_inizio_stato.AddDays(-1);
                    }

                    ++countModified;
                }

                //Variazione indirizzo
                if (v_contribuente.id_toponimo != v_contribuenteStoLast.id_toponimo ||
                    v_contribuente.id_strada_db_poste != v_contribuenteStoLast.id_strada_db_poste ||
                    v_contribuente.cod_citta != v_contribuenteStoLast.cod_citta ||
                   (v_contribuente.frazione ?? "") != (v_contribuenteStoLast.frazione ?? "") ||
                   (v_contribuente.indirizzo ?? "") != (v_contribuenteStoLast.indirizzo ?? "") ||
                   (v_contribuente.cap ?? "") != (v_contribuenteStoLast.cap ?? "") ||
                   (v_contribuente.stato ?? "") != (v_contribuenteStoLast.stato ?? "") ||
                   (v_contribuente.condominio ?? "") != (v_contribuenteStoLast.condominio ?? "") ||
                    v_contribuente.nr_civico != v_contribuenteStoLast.nr_civico ||
                   (v_contribuente.sigla_civico ?? "") != (v_contribuenteStoLast.sigla_civico ?? "") ||
                   (v_contribuente.colore ?? "") != (v_contribuenteStoLast.colore ?? "") ||
                    v_contribuente.km != v_contribuenteStoLast.km ||
                    v_contribuente.id_toponimo_normalizzato != v_contribuenteStoLast.id_toponimo_normalizzato)
                {
                    bCreaNuovoStorico = true;

                    if (v_contribuente.data_inizio_validita_indirizzo.HasValue &&
                        v_contribuenteStoLast.data_inizio_validita_indirizzo.HasValue &&
                        v_contribuente.data_inizio_validita_indirizzo.Value.Date == v_contribuenteStoLast.data_inizio_validita_indirizzo.Value.Date)
                    {
                        v_contribuenteStoLast.data_fine_validita_indirizzo = v_contribuente.data_inizio_validita_indirizzo.Value;
                    }
                    else
                    {
                        v_contribuenteStoLast.data_fine_validita_indirizzo = v_contribuente.data_inizio_validita_indirizzo.HasValue ? v_contribuente.data_inizio_validita_indirizzo.Value.AddDays(-1) : DateTime.Now.AddDays(-1);
                    }

                    ++countModified;
                }

                //Variazione altri dati
                if ((v_contribuente.nome ?? "") != (v_contribuenteStoLast.nome ?? "") ||
                    (v_contribuente.cognome ?? "") != (v_contribuenteStoLast.cognome ?? "") ||
                    (v_contribuente.cod_fiscale ?? "") != (v_contribuenteStoLast.cod_fiscale ?? "") ||
                    (v_contribuente.cod_fiscale_pg ?? "") != (v_contribuenteStoLast.cod_fiscale_pg ?? "") ||
                    (v_contribuente.rag_sociale ?? "") != (v_contribuenteStoLast.rag_sociale ?? "") ||
                    (v_contribuente.denominazione_commerciale ?? "") != (v_contribuenteStoLast.denominazione_commerciale ?? "") ||
                    (v_contribuente.p_iva ?? "") != (v_contribuenteStoLast.p_iva ?? "") ||
                    (v_contribuente.fax ?? "") != (v_contribuenteStoLast.fax ?? "") ||
                    (v_contribuente.cell ?? "") != (v_contribuenteStoLast.cell ?? "") ||
                    (v_contribuente.tel ?? "") != (v_contribuenteStoLast.tel ?? "") ||
                    (v_contribuente.pec ?? "") != (v_contribuenteStoLast.pec ?? "") ||
                    (v_contribuente.e_mail ?? "") != (v_contribuenteStoLast.e_mail ?? "") ||
                    (v_contribuente.stato_nas ?? "") != (v_contribuenteStoLast.stato_nas ?? "") ||
                    v_contribuente.cod_comune_nas != v_contribuenteStoLast.cod_comune_nas ||
                    v_contribuente.id_sesso != v_contribuenteStoLast.id_sesso ||
                    v_contribuente.data_nas != v_contribuenteStoLast.data_nas ||
                    v_contribuente.data_morte != v_contribuenteStoLast.data_morte ||
                    v_contribuente.id_nucleo_familiare != v_contribuenteStoLast.id_nucleo_familiare ||
                    v_contribuente.id_anagrafe_comunale != v_contribuenteStoLast.id_anagrafe_comunale
                    )
                {
                    bCreaNuovoStorico = true;

                    if (v_contribuente.data_inizio_validita_altri_dati.HasValue &&
                        v_contribuenteStoLast.data_inizio_validita_altri_dati.HasValue &&
                        v_contribuente.data_inizio_validita_altri_dati.Value.Date == v_contribuenteStoLast.data_inizio_validita_altri_dati.Value.Date)
                    {
                        v_contribuenteStoLast.data_fine_validita_altri_dati = v_contribuente.data_inizio_validita_altri_dati.Value;
                    }
                    else
                    {
                        v_contribuenteStoLast.data_fine_validita_altri_dati = v_contribuente.data_inizio_validita_altri_dati.HasValue ? v_contribuente.data_inizio_validita_altri_dati.Value.AddDays(-1) : DateTime.Now.AddDays(-1);
                    }

                    ++countModified;
                }

                if (v_contribuente.flag_ricerca_indirizzo_emigrazione != v_contribuenteStoLast.flag_ricerca_indirizzo_emigrazione ||
                    v_contribuente.flag_ricerca_indirizzo_mancata_notifica != v_contribuenteStoLast.flag_ricerca_indirizzo_mancata_notifica ||
                    v_contribuente.flag_irreperibilita_definitiva != v_contribuenteStoLast.flag_irreperibilita_definitiva ||
                    v_contribuente.flag_ricerca_eredi != v_contribuenteStoLast.flag_ricerca_eredi ||
                    v_contribuente.flag_ricerca_nuovo_referente_pg != v_contribuenteStoLast.flag_ricerca_nuovo_referente_pg ||
                    v_contribuente.flag_verifica_cf_piva != v_contribuenteStoLast.flag_verifica_cf_piva ||
                    v_contribuente.flag_verifica_pec != v_contribuenteStoLast.flag_verifica_pec ||
                    v_contribuente.flag_verifica_stati != v_contribuenteStoLast.flag_verifica_stati)
                {
                    bCreaNuovoStorico = true;
                    ++countModified;
                }

                if (countModified > 0)
                {
                    p_dbContext.Entry(v_contribuenteStoLast).State = System.Data.Entity.EntityState.Modified;
                }
            }

            if (!creareSoloSeVariato)
            {
                bCreaNuovoStorico = true;
            }

            if (bCreaNuovoStorico)
            {
                tab_contribuente_sto_new v_contribuenteSto = new tab_contribuente_sto_new();
                v_contribuenteSto.setProperties(v_contribuente, copiaSoloPropElementari);
                v_contribuenteSto.tab_contribuente = v_contribuente;

                p_dbContext.tab_contribuente_sto_new.Add(v_contribuenteSto);

                if (bSaveChanges) { p_dbContext.SaveChanges(forceIgnoreInvalidList); }
                return v_contribuenteSto;
            }
            if (bSaveChanges) { p_dbContext.SaveChanges(forceIgnoreInvalidList); }
            return v_contribuenteStoLast;
        }

        public static bool CheckDuplicatoCodiceFiscalePIVA(string p_codiceFiscalePIVA, Decimal p_idContribuente, dbEnte p_dbContext)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                return GetListInternal(p_dbContext).Any(d => d.id_anag_contribuente != p_idContribuente &&
                                                             d.cod_fiscale.Equals(p_codiceFiscalePIVA) &&
                                                             d.id_tipo_contribuente != anagrafica_tipo_contribuente.DITTA_INDIVIDUALE_ID);
            }
            else if (p_codiceFiscalePIVA.Length == 11)
            {
                return GetListInternal(p_dbContext).Any(d => d.id_anag_contribuente != p_idContribuente &&
                                                             d.p_iva.Equals(p_codiceFiscalePIVA));
            }
            else
            {
                return false;
            }
        }

        public static bool CheckDuplicatoCodiceFiscalePersonaGiuridica(string p_codiceFiscalePersonaGiuridica, Decimal p_idContribuente, dbEnte p_dbContext)
        {
            if (!string.IsNullOrEmpty(p_codiceFiscalePersonaGiuridica) && p_codiceFiscalePersonaGiuridica.Length == 11)
            {
                return GetListInternal(p_dbContext).Any(d => d.id_anag_contribuente != p_idContribuente && d.cod_fiscale_pg.Equals(p_codiceFiscalePersonaGiuridica));
            }
            else
            {
                return false;
            }
        }

        public static IQueryable<tab_contribuente> GetContribuentiByRicercaContribuente(Decimal p_codiceContribuente, string p_tipoContribuenteSigla, string p_cognome, string p_nome, string p_codiceFiscale, string p_ragioneSociale, string p_denominazioneCommerciale, string p_partitaIva, string p_cod_fiscale_pg, dbEnte p_dbContext)
        {
            IQueryable<tab_contribuente> v_contribuentiList = GetListInternal(p_dbContext);

            if (p_codiceContribuente > 0)
            {
                v_contribuentiList = v_contribuentiList.Where(d => d.id_anag_contribuente == p_codiceContribuente);
            }
            else
            {
                if (p_tipoContribuenteSigla == anagrafica_tipo_contribuente.PERS_FISICA)
                {
                    if (p_cognome != string.Empty)
                    {
                        v_contribuentiList = v_contribuentiList.Where(d => d.cognome.Contains(p_cognome));
                    }

                    if (p_nome != string.Empty)
                    {
                        v_contribuentiList = v_contribuentiList.Where(d => d.nome.Contains(p_nome));
                    }

                    if (p_codiceFiscale != string.Empty)
                    {
                        v_contribuentiList = v_contribuentiList.Where(d => d.cod_fiscale.Contains(p_codiceFiscale) &&
                                                                           d.id_tipo_contribuente != anagrafica_tipo_contribuente.DITTA_INDIVIDUALE_ID);
                    }
                }
                else
                {
                    if (p_ragioneSociale != string.Empty)
                    {
                        v_contribuentiList = v_contribuentiList.Where(d => d.rag_sociale.Contains(p_ragioneSociale));
                    }

                    if (p_denominazioneCommerciale != string.Empty)
                    {
                        v_contribuentiList = v_contribuentiList.Where(d => d.denominazione_commerciale.Contains(p_denominazioneCommerciale));
                    }

                    if (p_partitaIva != string.Empty)
                    {
                        v_contribuentiList = v_contribuentiList.Where(d => d.p_iva.Contains(p_partitaIva));
                    }

                    if (p_cod_fiscale_pg != string.Empty)
                    {
                        v_contribuentiList = v_contribuentiList.Where(d => d.cod_fiscale_pg.Contains(p_cod_fiscale_pg));
                    }
                }
            }

            return v_contribuentiList.Include(x => x.anagrafica_tipo_contribuente);
        }

        public static bool CheckCoerenzaContribuente(Decimal p_idContribuente, string p_codiceFiscalePIVA, dbEnte p_dbContext)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                return GetListInternal(p_dbContext).Any(d => d.id_anag_contribuente == p_idContribuente && d.cod_fiscale.Equals(p_codiceFiscalePIVA));
            }
            else if (p_codiceFiscalePIVA.Length == 11)
            {
                return GetListInternal(p_dbContext).Any(d => d.id_anag_contribuente == p_idContribuente && d.p_iva.Equals(p_codiceFiscalePIVA));
            }
            else
            {
                return false;
            }
        }

        public static tab_contribuente GetByCodiceContribuente(Decimal p_codiceContribuente, dbEnte p_dbContext)
        {
            tab_contribuente v_contribuente = GetListInternal(p_dbContext).Where(d => d.id_anag_contribuente == p_codiceContribuente)
                                                                          .Include(x => x.anagrafica_tipo_contribuente)
                                                                          .FirstOrDefault();
            return v_contribuente;
        }

        // NOTA: questo ritorna FirstOrDefault e non SingleOrDefault e che uno stesso CF si può trovare in più contribuenti,
        //       uno con tipo 1 e un altro con anche la PIVA e tipo 4 ma non controlla il tipo
        public static tab_contribuente GetByCodiceFiscalePIVA(string p_codiceFiscalePIVA, dbEnte p_dbContext)
        {
            //Il dottore ha voluto togliere il blocco sulle 'acque reflue'
            if (p_codiceFiscalePIVA.Length == 16)
            {
                return GetListInternal(p_dbContext).Where(d => /*d.cod_fonte != "ANA_AR" && d.cod_fonte != "AR_" &&*/ (d.p_iva == null || d.p_iva == "") && (d.cod_fiscale.Equals(p_codiceFiscalePIVA))).OrderBy(o => o.id_anag_contribuente).FirstOrDefault();
            }
            else if (p_codiceFiscalePIVA.Length == 11)
            {
                return GetListInternal(p_dbContext).Where(d =>
                    /*d.cod_fonte != "ANA_AR" && d.cod_fonte != "AR_" &&*/
                    (d.p_iva.Equals(p_codiceFiscalePIVA)) || (d.cod_fiscale_pg != null && d.cod_fiscale_pg.Equals(p_codiceFiscalePIVA)))
                    .OrderBy(o => o.id_anag_contribuente).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        // NOTA: Questo ritorna SingleOrDefault al contrario di GetByCodiceFiscalePIVA, ma reinforza il tipo == 1 se CF
        //       tipo != 1 se PIVA
        public static IQueryable<tab_contribuente> QueryByCodiceFiscalePIVAAndIdTipoPersonaQueryOrNull(string cfPiva, dbEnte ctx)
        {
            cfPiva = (cfPiva ?? "").Trim();
            cfPiva = cfPiva.ToUpper();

            switch (cfPiva.Length)
            {
                case 16:
                    return GetListInternal(ctx)
                        .Where(x =>
                            x.id_tipo_contribuente == anagrafica_tipo_contribuente.PERS_FISICA_ID
                            && x.cod_fiscale.ToUpper().Equals(cfPiva));
                case 11:
                    return GetListInternal(ctx)
                        .Where(x =>
                            x.id_tipo_contribuente != anagrafica_tipo_contribuente.PERS_FISICA_ID
                            &&
                            (
                                (x.p_iva != null && x.p_iva.Equals(cfPiva)) ||
                                (x.cod_fiscale_pg != null && x.cod_fiscale_pg.ToUpper().Equals(cfPiva))
                            )
                        );
                default:
                    return null;
            }
        }

        public static tab_contribuente GetByCodiceFiscalePivaAndIdTipoPersona(string cfPiva, dbEnte ctx, bool bPrendiIlPrimo)
        {
            return bPrendiIlPrimo
                ? QueryByCodiceFiscalePIVAAndIdTipoPersonaQueryOrNull(cfPiva, ctx)?.OrderBy(x => x.id_anag_contribuente).FirstOrDefault()
                : QueryByCodiceFiscalePIVAAndIdTipoPersonaQueryOrNull(cfPiva, ctx)?.SingleOrDefault();
        }

        //public static tab_contribuente GetByCodiceFiscalePIVANonAnnullato(string p_codiceFiscalePIVA, dbEnte p_dbContext)
        //{
        //    if (p_codiceFiscalePIVA.Length == 16)
        //    {
        //        return GetListInternal(p_dbContext).Where(d => d.cod_fiscale.Equals(p_codiceFiscalePIVA) && !(d.cod_stato.StartsWith(anagrafica_stato_contribuente.ANN))).SingleOrDefault();
        //    }
        //    else if (p_codiceFiscalePIVA.Length == 11)
        //    {
        //        return GetListInternal(p_dbContext).Where(d => d.p_iva.Equals(p_codiceFiscalePIVA) && !(d.cod_stato.StartsWith(anagrafica_stato_contribuente.ANN))).SingleOrDefault();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public static IQueryable<tab_contribuente> GetListContribuentiAbilitati(string p_codiceFiscale, dbEnte p_dbContext)
        {
            IQueryable<tab_contribuente> risp;

            tab_referente v_referente = TabReferenteBD.QueryByCodiceFiscalePIVAAndIdTipoPersonaQueryOrNull(p_codiceFiscale, p_dbContext).FirstOrDefault();

            risp = GetListInternal(p_dbContext);

            if (v_referente != null)
            {
                var v_idContribuenti = v_referente.join_referente_contribuente.Select(a => a.id_anag_contribuente);
                risp = risp.Where(tc => v_idContribuenti.Contains(tc.id_anag_contribuente));
                risp = risp.Union(GetListInternal(p_dbContext).Where(c => c.cod_fiscale == p_codiceFiscale));

                return risp;
            }
            else
            {
                return null;
            }
        }

        public static IQueryable<tab_contribuente> getListByCodiceFiscale(string p_codiceFiscalePart, dbEnte p_dbContext)
        {
            return GetListInternal(p_dbContext).Where(l => l.cod_fiscale != null && l.cod_fiscale.Contains(p_codiceFiscalePart))
                                                   .OrderBy(o => o.cognome).ThenBy(o => o.nome)
                                                   .ThenBy(o => o.data_nas)
                                                   .ThenBy(o => o.rag_sociale).ThenBy(o => o.denominazione_commerciale);
        }

        public static IQueryable<tab_contribuente> GetListByPartitaIva(string p_partitaIvaPart, dbEnte p_dbContext)
        {
            return GetListInternal(p_dbContext).Where(l => l.p_iva != null && l.p_iva.Contains(p_partitaIvaPart))
                                                   .OrderBy(o => o.cognome).ThenBy(o => o.nome)
                                                   .ThenBy(o => o.data_nas)
                                                   .ThenBy(o => o.rag_sociale).ThenBy(o => o.denominazione_commerciale);
        }

        public static IQueryable<tab_contribuente> GetListByPivaRagSociale(dbEnte p_dbContext, string p_piva = "", string p_rag_sociale = "")
        {
            IQueryable<tab_contribuente> res = GetListInternal(p_dbContext);
            if (!String.IsNullOrWhiteSpace(p_piva))
                res = res.Where(c => c.p_iva.CompareTo(p_piva) == 0);
            if (!String.IsNullOrWhiteSpace(p_rag_sociale))
                res = res.Where(c => c.rag_sociale.ToUpper().CompareTo(p_rag_sociale.ToUpper()) == 0);

            return res;
        }

        public static IQueryable<tab_contribuente> GetListInsertableForProcConcorsuale(dbEnte p_dbContext, int p_id_ente, string p_piva = "", string p_rag_sociale = "")
        {
            IQueryable<tab_contribuente> res = GetListByPivaRagSociale(p_dbContext, p_piva, p_rag_sociale)/*.Where(c => !c.tab_procedure_concorsuali.Where(pc => pc.id_ente == p_id_ente).Any())*/;

            return res;
        }

        public static IQueryable<tab_contribuente> GetListMancataNotifica(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.flag_ricerca_indirizzo_mancata_notifica == "1");
        }

        public static tab_contribuente creaContribuente(string p_cognome, string p_tri_ana_utente, Int32 p_idOperatore, string p_fonte, Int32 p_idEnte, dbEnte p_context)
        {

            tab_contribuente v_contribuente = new tab_contribuente();

            v_contribuente.cognome = p_cognome;

            v_contribuente.id_ente = p_idEnte;
            v_contribuente.id_ente_gestito = p_idEnte;
            v_contribuente.id_entrata = anagrafica_entrate.NESSUNA_ENTRATA;
            v_contribuente.cod_fonte = p_fonte;
            v_contribuente.id_tipo_contribuente = anagrafica_tipo_contribuente.PERS_FISICA_ID;
            v_contribuente.cod_stato_contribuente = anagrafica_stato_contribuente.ATT_ATT;
            v_contribuente.id_stato_contribuente = anagrafica_stato_contribuente.ATT_ATT_ID;
            v_contribuente.cod_stato = anagrafica_stato_contribuente.ATT_ATT;
            v_contribuente.data_inizio_stato = DateTime.Now;
            v_contribuente.id_risorsa = p_idOperatore;

            //v_contribuente.data_aggiornamento = DateTime.Now;
            //v_contribuente.data_inizio_val = DateTime.Now;
            //v_contribuente.flag_verifica_dati = "0";
            v_contribuente.flag_verifica_cf_piva = "0";
            v_contribuente.data_inizio_stato = DateTime.Now;
            v_contribuente.stato = "IT";
            v_contribuente.tri_ana_utente = p_tri_ana_utente;
            p_context.tab_contribuente.Add(v_contribuente);

            return v_contribuente;
        }
    }
}
