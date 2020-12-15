using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ravenapi.Entities;
using ravenapi.Services;
using Newtonsoft.Json;

namespace ravenapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class getProcessedByBurnAddressRefController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getProcessedByBurnAddressRef/1001

        [HttpGet("{burnAddressRef}")]
        public ActionResult<String> Get(string burnAddressRef)
        {
            var user = User.Identity.Name;
            string processedByBurnAddressRefJson = JsonConvert.SerializeObject(DataService.GetProcessedByBurnAddressRef(Int32.Parse(user), burnAddressRef));
            return processedByBurnAddressRefJson;
        }
    }
}