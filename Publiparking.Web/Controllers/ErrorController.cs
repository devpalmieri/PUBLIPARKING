using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publiparking.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Controllers
{
    public class ErrorController : LoggedBaseController
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly HttpContext _context;

        public ErrorController(ILogger<ErrorController> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _context = accessor.HttpContext;
        }
        [Microsoft.AspNetCore.Mvc.Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case StatusCodes.Status404NotFound:
                    {
                        ViewBag.ErrorMessage = "Errore. Pagina non trovata.";
                        _logger.LogWarning($" 404 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case StatusCodes.Status204NoContent:
                    {
                        ViewBag.ErrorMessage = "Spiacente contenuto non disponibile";
                        _logger.LogWarning($" 204 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case StatusCodes.Status400BadRequest:
                    {
                        ViewBag.ErrorMessage = "Errore. Richiesta non validata.";
                        _logger.LogWarning($" 400 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case StatusCodes.Status403Forbidden:
                    {
                        ViewBag.ErrorMessage = "Errore. Accesso non autorizzato.";
                        _logger.LogWarning($" 403 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case StatusCodes.Status429TooManyRequests:
                    {
                        ViewBag.ErrorMessage = "Errore. Inviate un numero eccessivo di richieste.";
                        _logger.LogWarning($" 429 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case StatusCodes.Status401Unauthorized:
                    {
                        ViewBag.ErrorMessage = "Errore. Non autorizzato.";
                        _logger.LogWarning($" 401 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case StatusCodes.Status500InternalServerError:
                    {
                        ViewBag.ErrorMessage = "Errore interno del server.";
                        _logger.LogWarning($" 500 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case StatusCodes.Status405MethodNotAllowed:
                    {
                        ViewBag.ErrorMessage = "Non è possibile accedere alla risorsa.";
                        _logger.LogWarning($" 405 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case StatusCodes.Status419AuthenticationTimeout:
                    {
                        ViewBag.ErrorMessage = "Errore. Timeout Authentication.";
                        _logger.LogWarning($" 419 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case StatusCodes.Status501NotImplemented:
                    {
                        ViewBag.ErrorMessage = "Metodo della richiesta non implementato.";
                        _logger.LogWarning($" 501 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
                case StatusCodes.Status502BadGateway:
                    {
                        ViewBag.ErrorMessage = "Risposta non valida del server.";
                        _logger.LogWarning($" 502 Error Occurred. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                        ViewBag.OriginalPath = statusCodeResult.OriginalPath;
                        ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
                    }
            }

            var JWToken = _context.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(JWToken))
                return View("NotFound");
            else
                return View("NotFoundUnLogged");
        }
        [Microsoft.AspNetCore.Mvc.Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"Percorso {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;


            var JWToken = _context.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(JWToken))
                return View("Error");
            else
                return View("ErrorUnlogged");
        }
    }

}
