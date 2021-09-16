using Publisoftware.Data.LinqExtended;
using Publisoftware.Utility.CAP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabToponimiBD : EntityBD<tab_toponimi>
    {
        public TabToponimiBD()
        {

        }

        public static string GetFrazione(string IdToponimo, dbEnte p_dbContext)
        {
            int v_idToponimoAsInt = Convert.ToInt32(IdToponimo);
            tab_toponimi v_toponimo = TabToponimiBD.GetById(v_idToponimoAsInt, p_dbContext);
            return v_toponimo?.frazione_toponimo != null ? v_toponimo.frazione_toponimo : string.Empty;
        }

        public static int? GetIdByDescrizione(int p_codComune, string p_descrizione, dbEnte p_dbContext)
        {
            int? v_idToponimo = TabToponimiBD.GetList(p_dbContext).WhereByCodComune(p_codComune)
                                    .Where(t => t.descrizione_toponimo.Equals(p_descrizione) && t.flag_val_toponimo == "1")
                                    .OrderByDescending(o => o.data_inizio_val_toponimo)
                                    .AsNoTracking()
                                    .FirstOrDefault()?.id_toponimo;
            return v_idToponimo;
        }

        public static string GetCAP(string CodComune, string IdToponimo, string IdStrada, string NrCivico, string SiglaCivico, string Colore, string Km, dbEnte p_dbContext)
        {
            string v_result = string.Empty;

            if (!String.IsNullOrWhiteSpace(CodComune))
            {
                ser_comuni v_serComune = SerComuniBD.GetByCodComune(CodComune, p_dbContext);
                if (v_serComune == null)
                {
                    throw new ApplicationException($"Nessun {nameof(ser_comuni)} trovato con codice comune {CodComune}");
                }
                tab_poste_comune v_comune = TabPosteComuneBD.GetList(p_dbContext)
                                                                      .WhereByCodComune(v_serComune.cod_istat)
                                                                      .FirstOrDefault();

                if (v_comune == null)
                {
                    throw new ApplicationException($"Nessun {nameof(tab_poste_comune)} trovato con codice comune {CodComune} e codice istat {v_serComune.cod_istat}");
                }
                if (v_comune.MultiCAP == "S")
                {
                    tab_poste_strada v_strada = null;

                    if (IdToponimo != string.Empty)
                    {
                        int v_idToponimoAsInt = Convert.ToInt32(IdToponimo);
                        tab_toponimi v_toponimo = TabToponimiBD.GetById(v_idToponimoAsInt, p_dbContext);
                        if (v_toponimo == null)
                        {
                            throw new ApplicationException($"Nessun {nameof(tab_toponimi)} trovato con id {v_idToponimoAsInt}");
                        }
                        v_strada = v_toponimo.tab_poste_strada;
                    }
                    else if (IdStrada != string.Empty)
                    {
                        int v_idStradaAsInt = Convert.ToInt32(IdStrada);
                        v_strada = TabPosteStradaBD.GetById(v_idStradaAsInt, p_dbContext);
                    }

                    if (v_strada != null)
                    {
                        if (v_strada.MultiCap == "S")
                        {
                            if (Km != string.Empty)
                            {
                                int m = Convert.ToInt32(Convert.ToDecimal(Km.Replace(",", ".")));
                                tab_poste_arcostradale v_arcostradale = TabPosteArcostradaleBD.GetList(p_dbContext)
                                                                                                    .WhereByIdStrada(v_strada.id)
                                                                                                    .WhereByRangeKm(m)
                                                                                                    .FirstOrDefault();
                                if (v_arcostradale != null)
                                {
                                    v_result = v_arcostradale.CAP;
                                }
                            }

                            if (NrCivico != string.Empty)
                            {
                                int civico = Convert.ToInt32(NrCivico);
                                tab_poste_arcostradale v_arcostradale = null;
                                List<tab_poste_arcostradale> v_arcoStradaleList = null;

                                if (civico % 2 != 0)
                                {
                                    if (Colore == string.Empty)
                                    {
                                        v_arcoStradaleList = TabPosteArcostradaleBD.GetList(p_dbContext)
                                                                                        .WhereByIdStrada(v_strada.id)
                                                                                        .WhereSenzaColore()
                                                                                        .WhereByRangeNrCivicoDispari(civico)
                                                                                        .ToList();
                                    }
                                    else
                                    {
                                        v_arcoStradaleList = TabPosteArcostradaleBD.GetList(p_dbContext)
                                                                                        .WhereByIdStrada(v_strada.id)
                                                                                        .WhereByColore(Colore)
                                                                                        .WhereByRangeNrCivicoDispari(civico)
                                                                                        .ToList();
                                    }
                                }
                                else
                                {
                                    if (Colore == string.Empty)
                                    {
                                        v_arcoStradaleList = TabPosteArcostradaleBD.GetList(p_dbContext)
                                                                                        .WhereByIdStrada(v_strada.id)
                                                                                        .WhereSenzaColore()
                                                                                        .WhereByRangeNrCivicoPari(civico)
                                                                                        .ToList();
                                    }
                                    else
                                    {
                                        v_arcoStradaleList = TabPosteArcostradaleBD.GetList(p_dbContext)
                                                                                        .WhereByIdStrada(v_strada.id)
                                                                                        .WhereByColore(Colore)
                                                                                        .WhereByRangeNrCivicoPari(civico)
                                                                                        .ToList();
                                    }
                                }

                                if (v_arcoStradaleList != null && v_arcoStradaleList.Count != 0 && SiglaCivico == string.Empty)
                                {
                                    v_arcostradale = v_arcoStradaleList.FirstOrDefault();
                                }
                                else if (v_arcoStradaleList != null && v_arcoStradaleList.Count != 0 && SiglaCivico != string.Empty)
                                {
                                    int p_valoreSigla = CAPHelper.AssigneValueToSiglaCivico(SiglaCivico);
                                    v_arcostradale = v_arcoStradaleList.Where(d => p_valoreSigla >= d.EsponenteDalAssegnato && p_valoreSigla <= d.EsponenteAlAssegnato && p_valoreSigla != -1).FirstOrDefault();
                                }

                                if (v_arcostradale != null)
                                {
                                    v_result = v_arcostradale.CAP;
                                }
                            }
                        }
                        else
                        {
                            v_result = v_strada.CAP;
                        }
                    }
                }
                else
                {
                    v_result = v_comune.CAP;
                }
            }

            return v_result;
        }

        public static tab_toponimi GetByIdEnte(int p_idEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.id_toponimo_ente == p_idEnte).SingleOrDefault();
        }
    }
}
