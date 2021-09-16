using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models.Menu
{
    #region Menu Class
    [Serializable]
    public class MenuNodeFirstLevel
    {
        public int IdMenuFirst { get; set; }

        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public int Ordine { get; set; }
        public bool IsLink { get; set; }
        public bool IsVisible { get; set; }
        public string controller { get; set; }
        public string Action { get; set; }
        public string Tooltip { get; set; }
        public string TipoMenu { get; set; }
        public ICollection<MenuNodeSecondLevel> ListMenuSecondLevel { get; set; }
    }
    [Serializable]
    public class MenuNodeSecondLevel
    {
        public int IdMenuSecond { get; set; }

        public int? IdMenuFirst { get; set; }
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public int Ordine { get; set; }
        public bool IsLink { get; set; }
        public bool IsVisible { get; set; }
        public string controller { get; set; }
        public string Action { get; set; }
        public string Tooltip { get; set; }
        public string TipoMenu { get; set; }
        public ICollection<MenuNodeThirdLevel> ListMenuThirdLevel { get; set; }
    }
    [Serializable]
    public class MenuNodeThirdLevel
    {
        public int IdMenuThird { get; set; }
        public int? IdMenuSecond { get; set; }
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public int Ordine { get; set; }
        public bool IsLink { get; set; }
        public bool IsVisible { get; set; }
        public string controller { get; set; }
        public string Action { get; set; }
        public string Tooltip { get; set; }
        public string TipoMenu { get; set; }
    }
    #endregion Menu Class
}
