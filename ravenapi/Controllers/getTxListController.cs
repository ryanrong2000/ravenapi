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
    public class getTxListController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getTxList
        [HttpGet]
        public ActionResult<String> Get()
        {
            var user = User.Identity.Name;
            string jsonTxList = JsonConvert.SerializeObject(DataService.GetTxList(Int32.Parse(user)));
            return jsonTxList;
        }
    }
}