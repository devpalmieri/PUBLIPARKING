using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models
{
    public class CloseModal
    {
        public bool SetClose { get; set; }
        public bool SetManageData { get; set; }
        public string Destination { get; set; }
        public string Target { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string OnSuccess { get; set; }
    }
}
