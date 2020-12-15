using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ravenapi.Entities;
using ravenapi.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ravenapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class getBurnAddressByRefController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getBurnAddressByRef/1000
        [HttpGet("{burnAddressRef}")]
        public ActionResult<String> Get(string burnAddressRef)
        {
            var user = User.Identity.Name;
            string jsonBurnAddress = JsonConvert.SerializeObject(DataService.GetBurnAddressByRef(Int32.Parse(user), burnAddressRef));
            return jsonBurnAddress;
        }
    }
}