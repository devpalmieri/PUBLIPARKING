using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Configuration
{
    public class ParkActionFilter : IAsyncActionFilter
    {
        private StartMode _options;
        public ParkActionFilter(IConfiguration configuration)
        {

            _options = new StartMode();
            configuration.Bind(_options);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ((Microsoft.AspNetCore.Mvc.Controller)context.Controller).ViewBag.StartMode = _options;
            await next();
        }
    }
}
