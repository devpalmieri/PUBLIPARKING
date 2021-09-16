using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabTerzoBD : EntityBD<tab_terzo>
    {
        public TabTerzoBD()
        {

        }

        public static IQueryable<tab_terzo> GetTerziByRicercaTerzo(int p_codiceTerzo, int p_tipoTerzo, string p_cognome, string p_nome, string p_codiceFiscale, string p_ragioneSociale, string p_partitaIva, dbEnte p_dbContext)
        {
            IQueryable<tab_terzo> v_terziList = GetListInternal(p_dbContext);

            if (p_codiceTerzo > 0)
            {
                v_terziList = v_terziList.Where(d => d.id_terzo == p_codiceTerzo);
            }
            else
            {
                if (p_tipoTerzo == tab_terzo.TIPO_FISICO)
                {
                    if (p_cognome != string.Empty)
                    {
                        v_terziList = v_terziList.Where(d => d.cognome.Contains(p_cognome));
                    }

                    if (p_nome != string.Empty)
                    {
                        v_terziList = v_terziList.Where(d => d.nome.Contains(p_nome));
                    }

                    if (p_codiceFiscale != string.Empty)
                    {
                        v_terziList = v_terziList.Where(d => d.cod_fiscale.Contains(p_codiceFiscale) &&
                                                             d.id_tipo_terzo != anagrafica_tipo_contribuente.DITTA_INDIVIDUALE_ID);
                    }
                }
                else
                {
                    if (p_ragioneSociale != string.Empty)
                    {
                        v_terziList = v_terziList.Where(d => d.rag_sociale.Contains(p_ragioneSociale));
                    }

                    if (p_partitaIva != string.Empty)
                    {
                        v_terziList = v_terziList.Where(d => d.p_iva.Contains(p_partitaIva));
                    }
                }
            }

            return v_terziList;
        }

        public static IQueryable<tab_terzo> GetByCodiceFiscalePivaAndIdTipoPersonaQueryOrNull(string cfPiva, dbEnte ctx)
        {
            cfPiva = (cfPiva ?? "").Trim();
            cfPiva = cfPiva.ToUpper();

            switch (cfPiva.Length)
            {
                case 16:
                    return GetListInternal(ctx)
                        .Where(x =>
                            x.id_tipo_terzo == anagrafica_tipo_contribuente.PERS_FISICA_ID
                            && x.cod_fiscale.ToUpper().Equals(cfPiva));
                case 11:
                    return GetListInternal(ctx)
                        .Where(x =>
                            x.id_tipo_terzo != anagrafica_tipo_contribuente.PERS_FISICA_ID
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

        public static tab_terzo GetByCodiceFiscalePIVA(string p_codiceFiscalePIVA, dbEnte p_dbContext)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                return GetList(p_dbContext).Where(c => (c.cod_fiscale != null && (c.cod_fiscale.Equals(p_codiceFiscalePIVA) || c.cod_fiscale_pg.Equals(p_codiceFiscalePIVA)))).FirstOrDefault();
            }
            else if (p_codiceFiscalePIVA.Length == 11)
            {
                return GetList(p_dbContext).Where(c => (c.p_iva != null && (c.p_iva.Equals(p_codiceFiscalePIVA) || c.cod_fiscale.Equals(p_codiceFiscalePIVA) || c.cod_fiscale_pg.Equals(p_codiceFiscalePIVA)))).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static bool CheckDuplicatoCodiceFiscalePIVA(string p_codiceFiscalePIVA, int p_idTerzo, dbEnte p_dbContext)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                return GetList(p_dbContext).Any(d => d.id_terzo != p_idTerzo &&
                                                     d.cod_fiscale.Equals(p_codiceFiscalePIVA) &&
                                                     d.id_tipo_terzo != anagrafica_tipo_contribuente.DITTA_INDIVIDUALE_ID);
            }
            else if (p_codiceFiscalePIVA.Length == 11)
            {
                return GetList(p_dbContext).Any(d => d.id_terzo != p_idTerzo &&
                                                     d.p_iva.Equals(p_codiceFiscalePIVA));
            }
            else
            {
                return false;
            }
        }

        public static void AggiornaStorico(int idTerzo, dbEnte dbContext)
        {
            tab_terzo_sto terzoStoLast = TabTerzoStoBD.GetLastTerzoStoByIdTerzo(idTerzo, dbContext);
            tab_terzo terzo = GetById(idTerzo, dbContext);
            AggiornaStorico(terzo, terzoStoLast, dbContext);
        }

        public static void AggiornaStorico(
            tab_terzo v_terzo,
            tab_terzo_sto v_terzoStoLast, // null per tab_terzo nuovi
            dbEnte p_dbContext,
            bool bSaveChanges = true)
        {
            tab_terzo_sto v_terzoSto = new tab_terzo_sto();

            if (v_terzoStoLast != null)
            {
                if (!v_terzoStoLast.data_inizio_stato.HasValue)
                {
                    v_terzoStoLast.data_inizio_stato = Convert.ToDateTime("01/1/1900");
                }

                if (!v_terzoStoLast.data_inizio_validita_indirizzo.HasValue)
                {
                    v_terzoStoLast.data_inizio_validita_indirizzo = Convert.ToDateTime("01/1/1900");
                }

                if (!v_terzoStoLast.data_inizio_validita_altri_dati.HasValue)
                {
                    v_terzoStoLast.data_inizio_validita_altri_dati = Convert.ToDateTime("01/1/1900");
                }

                //Variazione stato
                if (v_terzo.id_stato_terzo != v_terzoStoLast.id_stato_terzo ||
                    v_terzo.id_tipo_terzo != v_terzoStoLast.id_tipo_terzo)
                {
                    if (v_terzo.data_inizio_stato.HasValue &&
                        v_terzoStoLast.data_inizio_stato.HasValue &&
                        v_terzo.data_inizio_stato.Value.Date == v_terzoStoLast.data_inizio_stato.Value.Date)
                    {
                        v_terzoStoLast.data_fine_stato = v_terzo.data_inizio_stato;
                    }
                    else
                    {
                        v_terzoStoLast.data_fine_stato = v_terzo.data_inizio_stato.HasValue ? v_terzo.data_inizio_stato.Value.AddDays(-1) : DateTime.Now.AddDays(-1);
                    }
                }

                //Variazione indirizzo
                if (v_terzo.id_toponimo != v_terzoStoLast.id_toponimo ||
                    v_terzo.id_strada_db_poste != v_terzoStoLast.id_strada_db_poste ||
                    v_terzo.cod_citta != v_terzoStoLast.cod_citta ||
                   (v_terzo.frazione ?? "") != (v_terzoStoLast.frazione ?? "") ||
                   (v_terzo.indirizzo ?? "") != (v_terzoStoLast.indirizzo ?? "") ||
                   (v_terzo.cap ?? "") != (v_terzoStoLast.cap ?? "") ||
                   (v_terzo.stato ?? "") != (v_terzoStoLast.stato ?? "") ||
                   (v_terzo.condominio ?? "") != (v_terzoStoLast.condominio ?? "") ||
                    v_terzo.nr_civico != v_terzoStoLast.nr_civico ||
                   (v_terzo.sigla_civico ?? "") != (v_terzoStoLast.sigla_civico ?? "") ||
                   (v_terzo.colore ?? "") != (v_terzoStoLast.colore ?? "") ||
                    v_terzo.km != v_terzoStoLast.km ||
                    v_terzo.id_toponimo_normalizzato != v_terzoStoLast.id_toponimo_normalizzato)
                {
                    if (v_terzo.data_inizio_validita_indirizzo.HasValue &&
                        v_terzoStoLast.data_inizio_validita_indirizzo.HasValue &&
                        v_terzo.data_inizio_validita_indirizzo.Value.Date == v_terzoStoLast.data_inizio_validita_indirizzo.Value.Date)
                    {
                        v_terzoStoLast.data_fine_validita_indirizzo = v_terzo.data_inizio_validita_indirizzo;
                    }
                    else
                    {
                        v_terzoStoLast.data_fine_validita_indirizzo = v_terzo.data_inizio_validita_indirizzo.HasValue ? v_terzo.data_inizio_validita_indirizzo.Value.AddDays(-1) : DateTime.Now.AddDays(-1);
                    }
                }

                //Variazione altri dati
                if ((v_terzo.nome ?? "") != (v_terzoStoLast.nome ?? "") ||
                    (v_terzo.cognome ?? "") != (v_terzoStoLast.cognome ?? "") ||
                    (v_terzo.cod_fiscale ?? "") != (v_terzoStoLast.cod_fiscale ?? "") ||
                    (v_terzo.rag_sociale ?? "") != (v_terzoStoLast.rag_sociale ?? "") ||
                    (v_terzo.denominazione_commerciale ?? "") != (v_terzoStoLast.denominazione_commerciale ?? "") ||
                    (v_terzo.p_iva ?? "") != (v_terzoStoLast.p_iva ?? "") ||
                    (v_terzo.fax ?? "") != (v_terzoStoLast.fax ?? "") ||
                    (v_terzo.cell ?? "") != (v_terzoStoLast.cell ?? "") ||
                    (v_terzo.tel ?? "") != (v_terzoStoLast.tel ?? "") ||
                    (v_terzo.pec ?? "") != (v_terzoStoLast.pec ?? "") ||
                    (v_terzo.pec_stragiudiziale ?? "") != (v_terzoStoLast.pec_stragiudiziale ?? "") ||
                    (v_terzo.e_mail ?? "") != (v_terzoStoLast.e_mail ?? "") ||
                    (v_terzo.stato_nas ?? "") != (v_terzoStoLast.stato_nas ?? "") ||
                    v_terzo.cod_comune_nas != v_terzoStoLast.cod_comune_nas ||
                    v_terzo.id_sesso != v_terzoStoLast.id_sesso ||
                    v_terzo.data_nas != v_terzoStoLast.data_nas ||
                    v_terzo.data_morte != v_terzoStoLast.data_morte)
                {
                    if (v_terzo.data_inizio_validita_altri_dati.HasValue &&
                        v_terzoStoLast.data_inizio_validita_altri_dati.HasValue &&
                        v_terzo.data_inizio_validita_altri_dati.Value.Date == v_terzoStoLast.data_inizio_validita_altri_dati.Value.Date)
                    {
                        v_terzoStoLast.data_fine_validita_altri_dati = v_terzo.data_inizio_validita_altri_dati;
                    }
                    else
                    {
                        v_terzoStoLast.data_fine_validita_altri_dati = v_terzo.data_inizio_validita_altri_dati.HasValue ? v_terzo.data_inizio_validita_altri_dati.Value.AddDays(-1) : DateTime.Now.AddDays(-1);
                    }
                }
            }

            v_terzoSto.setProperties(v_terzo, true);
            p_dbContext.tab_terzo_sto.Add(v_terzoSto);

            if (bSaveChanges)
            {
                p_dbContext.SaveChanges();
            }
        }

        public static IQueryable<tab_terzo> GetListMancataNotifica(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.flag_ricerca_indirizzo_mancata_notifica == "1");
        }
    }
}
