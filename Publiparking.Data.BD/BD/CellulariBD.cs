using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class CellulariBD : EntityBD<Cellulari>
    {
        public CellulariBD()
        {

        }

        public static Cellulari getCellulareByNumero(string p_numero, DbParkCtx p_context)
        {
            return GetList(p_context).Where(a => a.numero.Contains(p_numero)).Where(a=> !a.dataCessazione.HasValue).SingleOrDefault();

        }

        public static Cellulari getCellulareByIdAbbonamentoAndMaster(int p_idAbbonamento, DbParkCtx p_context)
        {
            return GetList(p_context).Where(a => a.master && a.idAbbonamento.Equals(p_idAbbonamento) && !a.dataCessazione.HasValue).SingleOrDefault();

        }
        public static bool disattivaCellulare(int p_idcellulare, DbParkCtx p_context)
        {
            bool retval = false;
            Cellulari v_cellulare = GetById(p_idcellulare, p_context);
            if (v_cellulare != null)
            {
                v_cellulare.dataCessazione = DateTime.Now;
                retval = true;
            }
            return retval;
        }

        public static Cellulari creaCellulare(int p_idAbbonamento, string p_numero,bool p_isMaster,  DbParkCtx p_context)
        {
            Cellulari v_cellulare = null;
            if (p_isMaster)
            {
                //si verifica che sull'abbonamento non ci sia un master
                Cellulari v_cellulareMaster = CellulariBD.getCellulareByIdAbbonamentoAndMaster(p_idAbbonamento, p_context);
                if (v_cellulareMaster != null)
                {
                    //disattiva cellulare
                    v_cellulareMaster.dataCessazione = DateTime.Now;
                    //inserisce il cellulare come slave
                    insertCellulare(p_idAbbonamento, v_cellulareMaster.numero, false, p_context);
                    Cellulari v_cellularSlave = CellulariBD.getCellulareByNumero(p_numero, p_context);
                    if (v_cellularSlave != null)
                    {
                        v_cellularSlave.dataCessazione = DateTime.Now;
                    }

                }               
            }
            
            v_cellulare =  insertCellulare(p_idAbbonamento, p_numero, p_isMaster, p_context);
            
            return v_cellulare;
        }

        private static Cellulari insertCellulare(int p_idAbbonamento, string p_numero, bool p_isMaster, DbParkCtx p_context)
        {
            Cellulari v_cellulare = new Cellulari();
            v_cellulare.idAbbonamento = p_idAbbonamento;
            v_cellulare.master = p_isMaster;
            v_cellulare.numero = p_numero;
            v_cellulare.dataCessazione = null;
            v_cellulare.dataCodiceVerifica = null;
            v_cellulare.dataAttivazione = DateTime.Now;
            v_cellulare.codiceVerifica = null;
            p_context.Cellulari.Add(v_cellulare);
            return v_cellulare;

        }
    }
}
