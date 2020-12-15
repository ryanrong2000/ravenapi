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
    public class getProcessListController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getProcessList
        [HttpGet]
        public ActionResult<String> Get()
        {
            var user = User.Identity.Name;
            string jsonProcessList = JsonConvert.SerializeObject(DataService.GetProcessList(Int32.Parse(user)));
            return jsonProcessList;
        }
    }
}