using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Publiparking.Web.NinoServer.Controllers
{
    public class DefaultController : Controller
    {
        public class TelefonoPostTelModel
        {
            public string NewTel { get; set; }
        }
        static string UnTelefono { get; set; } = "0823 319319";
        // GET: Default

        public JsonResult Index()
        {
            // Response.TrySkipIisCustomErrors();
            //Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var pippo = new { a = 1, b = "aaa" };
            return Json(pippo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult provarisposta(int i, string s)
        {
            // Response.TrySkipIisCustomErrors();
            //Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var pippo = new { a = i, b = s };
            return Json(pippo, JsonRequestBehavior.AllowGet);
        }
        // GET: tel_telefono/?pippo=1
        public ActionResult Tel(int pippo = 1)
        {
            return Json(new { tel = UnTelefono }, JsonRequestBehavior.AllowGet);
        }

        // POST: Telefono/Tel
        // (ContentType: application/json)
        // Body:
        //{
        //	NewTel: "1234567890"
        //}
        [HttpPost]
        [ActionName("Tel")]
        public ActionResult TelPost(TelefonoPostTelModel data)
        {
            if (data == null)
            {
                return HttpNotFound();
            }
            if (String.IsNullOrWhiteSpace(data.NewTel))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { newtel = "Inserire un valore" });
            }
            UnTelefono = data.NewTel;
            return Json(data);
        }

    }
}