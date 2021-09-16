using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models.SpidModels
{
    public class SpidUser
    {
        public string SpidCode { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string FiscalNumber { get; set; }

        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string IvaCode { get; set; }
        public string Mobile { get; set; }
    }
}
