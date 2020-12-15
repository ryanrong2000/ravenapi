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
    public class getProcessController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getProcess/a37061f0-24d6-11ea-833c-42010a800041
        [HttpGet("{processId}")]
        public ActionResult<String> Get(string processId)
        {
            var user = User.Identity.Name;
            string jsonProcess = JsonConvert.SerializeObject(DataService.GetProcess(Int32.Parse(user), processId));
            return jsonProcess;
        }
    }
}