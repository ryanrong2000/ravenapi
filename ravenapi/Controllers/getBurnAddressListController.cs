using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ravenapi.Entities;
using ravenapi.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ravenapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class getBurnAddressListController : ControllerBase
    {
        
        // GET api/values
        //https://localhost:44368/api/getBurnAddressList
        [HttpGet]
        public ActionResult<String> Get()
        {
            var user = User.Identity.Name;
            string burnAddressListJson = JsonConvert.SerializeObject(DataService.GetBurnAddressList(Int32.Parse(user)));
            return burnAddressListJson;
        }
    }
}