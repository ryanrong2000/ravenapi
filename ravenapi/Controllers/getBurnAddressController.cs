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
    public class getBurnAddressController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getBurnAddress/RXb9MtXFLyJYaFxb2zuWbipNnXXXVNPhfA
        [HttpGet("{burnAddress}")]
        public ActionResult<String> Get(string burnAddress)
        {
            var user = User.Identity.Name;
            string jsonBurnAddress = JsonConvert.SerializeObject(DataService.GetBurnAddress(Int32.Parse(user), burnAddress));
            return jsonBurnAddress;
        }
    }
}