//using Publisoftware.Web.MVCPortal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Publiparking.Web.Classes.Filters
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        //private bool IsAjax(ExceptionContext filterContext)
        //{
        //    return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        //}

        //public override void OnException(ExceptionContext filterContext)
        //{
        //    if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
        //    {
        //        return;
        //    }

        //    //if want to get different of the request
        //    var currentController = (string)filterContext.RouteData.Values["controller"];
        //    var currentActionName = (string)filterContext.RouteData.Values["action"];

        //    BaseController baseController = (filterContext.Controller is BaseController) ? (BaseController)filterContext.Controller : null;

        //    // if the request is AJAX return JSON else view.
        //    if (IsAjax(filterContext))
        //    {
        //        //Because its a exception raised after ajax invocation
        //        //Lets return Json

        //        //Se sto in debug genero messaggio errore più significativo da mostrare nel popup
        //        string clientDisplayMsg = "Internal Server Error.";
        //        if (baseController != null && baseController.appRunMode == "DEBUG")
        //        {
        //            clientDisplayMsg = String.Format("Controller:{0};Action:{1};Exception:{2}", currentController, currentActionName, filterContext.Exception.Message);
        //        }

        //        filterContext.Result = new JsonResult()
        //        {
        //            Data = clientDisplayMsg,
        //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //        };

        //        filterContext.ExceptionHandled = true;
        //        filterContext.HttpContext.Response.Clear();
        //        filterContext.HttpContext.Response.StatusCode = 500;
        //    }
        //    else
        //    {
        //        //Normal Exception
        //        //So let it handle by its default ways.
        //        base.OnException(filterContext);
        //        var v = filterContext.Result as ViewResult;
        //        v.ViewBag.isDebug = baseController.appRunMode == "DEBUG";

        //    }
        //}
    }
}