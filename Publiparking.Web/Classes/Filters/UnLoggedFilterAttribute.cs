using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Publiparking.Web.Classes.Filters
{
    public class UnLoggedFilterAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.logo_ente_img = String.Empty;

            //if (filterContext.Controller is UnloggedBaseController)
            //{
            //    UnloggedBaseController unloggedController = (UnloggedBaseController)filterContext.Controller;
            //    filterContext.Controller.ViewBag.logo_ente_img = unloggedController.buildImgUrl("logo_ente.png");
            //}

        }
    }
}