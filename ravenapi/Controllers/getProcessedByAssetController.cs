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
    public class getProcessedByAssetController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getProcessedByAsset/GORAVEN
        
        [HttpGet("{asset}")]
        public ActionResult<String> Get(string asset)
        {
            var user = User.Identity.Name;
            string processedByAssetJson = JsonConvert.SerializeObject(DataService.GetProcessedByAsset(Int32.Parse(user),asset));
            return processedByAssetJson;
        }
    }
}