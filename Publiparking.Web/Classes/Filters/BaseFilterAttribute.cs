using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Publiparking.Web.Classes.Filters
{
    public class BaseFilterAttribute : FilterAttribute, System.Web.Mvc.IActionFilter
    {
        public void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {

        }
    }
}