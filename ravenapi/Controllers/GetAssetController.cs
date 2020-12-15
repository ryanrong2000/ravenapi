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
    public class getAssetController : ControllerBase
    {
      
        // GET api/values
        //https://localhost:44368/api/getAsset/GORAVEN
        [HttpGet("{asset}")]
        public ActionResult<String> Get(string asset)
        {
            try
            {
                var user = User.Identity.Name;
                string jsonAsset = JsonConvert.SerializeObject(DataService.GetAsset(Int32.Parse(user), asset));
                return jsonAsset;
                
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}