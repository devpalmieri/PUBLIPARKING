using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Publiparking.WebApi.ParkServerList.Controllers
{
    public class ServerListController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string filepath = HttpContext.Current.Server.MapPath("~/ServerList.json");

            string JsonText = System.IO.File.ReadAllText(filepath);

            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonText, Encoding.UTF8, "application/json");
            return response;

        }
    }
}
