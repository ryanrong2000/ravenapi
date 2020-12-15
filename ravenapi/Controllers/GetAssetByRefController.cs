using System;
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
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ravenapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class getAssetByRefController : ControllerBase
    {
        private readonly IActionContextAccessor _accessor;
        public getAssetByRefController(IActionContextAccessor accessor)
        {
            _accessor = accessor;
        }

        // GET api/values
        //https://localhost:44368/api/getAsset/GORAVEN
        [HttpGet("{assetRef}")]
        public ActionResult<String> Get(string assetRef)
        {
            var user = User.Identity.Name;
            var remoteIpAddress = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();

            try
            {
                string jsonAsset = JsonConvert.SerializeObject(DataService.GetAssetByRef(Int32.Parse(user), assetRef));
                DataService.InsertLog(Int32.Parse(user), remoteIpAddress.ToString(), "getAssetByRef/" + assetRef, 0, null);
                return jsonAsset;
            }
            catch (Exception ex)
            {
                DataService.InsertLog(Int32.Parse(user), remoteIpAddress.ToString(), "getAssetByRef/" + assetRef, 1, ex.ToString());
                return ex.ToString();
            }

        }
    }
}