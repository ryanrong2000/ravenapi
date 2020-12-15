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
    public class getProcessedByAssetRefController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getProcessedByAssetRef/GORAVEN

        [HttpGet("{assetRef}")]
        public ActionResult<String> Get(string assetRef)
        {
            var user = User.Identity.Name;
            string processedByAssetRefJson = JsonConvert.SerializeObject(DataService.GetProcessedByAssetRef(Int32.Parse(user), assetRef));
            return processedByAssetRefJson;
        }
    }
}