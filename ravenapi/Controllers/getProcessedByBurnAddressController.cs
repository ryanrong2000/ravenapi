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
    public class getProcessedByBurnAddressController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getProcessedByBurnAddress/RXeLadh8WSARhotqtBNo5MJ4XXXXW1C3oz

        [HttpGet("{burnAddress}")]
        public ActionResult<String> Get(string burnAddress)
        {
            var user = User.Identity.Name;
            string processedByBurnAddressJson = JsonConvert.SerializeObject(DataService.GetProcessedByBurnAddress(Int32.Parse(user), burnAddress));
            return processedByBurnAddressJson;
        }
    }
}