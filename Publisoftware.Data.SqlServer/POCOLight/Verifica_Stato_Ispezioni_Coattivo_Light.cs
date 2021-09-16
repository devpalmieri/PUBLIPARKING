using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class Dettaglio_Verifica_Stato_Ispezioni_Coattivo_Light
    {
        public string Riga { get; set; }
        public string Sigla { get; set; }
        public int Id_Tipo_Ispezione { get; set; }
        public string descrizione { get; set; }
        public string Esito_Ispezione { get; set; }
        public int Numero { get; set; }
    }

    public class Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light
    {
        public Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light()
        {}
        public Dettaglio_Verifica_Stato_Ispezioni_Coattivo_ACI_Light(int _anno, int _mese,int _totale)
        {
            this.Anno = _anno;
            if (_anno == 0)
                AnnoString = "TOTALE:";
            this.Mese = _mese;
            this.TotaleParziale = _totale;
        }
        public int Anno { get; set; }
        public int Mese { get; set; }
        public int TotaleParziale { get; set; }
        public string AnnoString { get; set; }
        public string MeseString
        {
            get
            {
                if (Mese > 0 && Mese < 13)
                {
                    return GetMonthName(Mese);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public static string GetMonthName(int nMonth)
        {
            var month = (MonthNameEnum)nMonth;
            return month.ToString();
        }
    }
}
