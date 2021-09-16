using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Publiparking.Web.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RpcExController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult( new { PropA= "value1", PropB = 10 });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new JsonResult(new { PropA = "value1", PropB = id });
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return new NotFoundObjectResult(new { PropA = "N.A.", PropB = 404 });
        }

        [HttpPost("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return new JsonResult(new { PropA = "PrpA non valida", PropB = "Specifica un numero"})
            {
                StatusCode = 400
            };
        }
    }
}
