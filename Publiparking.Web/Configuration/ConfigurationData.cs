using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Configuration
{
    public static class ConfigurationData
    {
        public static IConfiguration Configuration;
        public static bool IsDevelopment;
        public static string Environment;
    }
}
