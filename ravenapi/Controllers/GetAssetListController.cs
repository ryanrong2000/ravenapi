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
    public class GetAssetListController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getAssetList
        [HttpGet]
        public ActionResult<String> Get()
        {
            try
            {
                var user = User.Identity.Name;
                string jsonAssetList = JsonConvert.SerializeObject(DataService.GetAssetList(Int32.Parse(user)));
                return jsonAssetList;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}