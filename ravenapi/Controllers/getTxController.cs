using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ravenapi.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ravenapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class getTxController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getTx/f68f8b50-251d-11ea-833c-42010a800041
        [HttpGet("{tx}")]
        public ActionResult<String> Get(string tx)
        {
            var user = User.Identity.Name;
            string jsonTx = JsonConvert.SerializeObject(DataService.GetTx(Int32.Parse(user), tx));
            return jsonTx;
        }
    }
}