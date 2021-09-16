using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_rssfeed_fisco.Metadata))]
    public partial class tab_rssfeed_fisco : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }
       

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID Feed")]
            public int IDFeed { get; set; }
            
            [DisplayName("Titolo")]
            public string Title { get; set; }
            [DisplayName("Url")]
            public string Url { get; set; }
        }
    }
}
