using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ravenapi.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ravenapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class getBurnAddressNewController : ControllerBase
    {
        // GET api/values
        //https://localhost:44368/api/getBurnAddressNew/:?burnAddressRef=1004&Tag=boat
        [HttpGet("{burnAddressRef,Tag}")]
        public ActionResult<String> Get(String burnAddressRef, String Tag)
        {
            String errorMessage = "";
            String jsonBurnAddress = "";
            bool error = false;

            var user = User.Identity.Name;
            bool exist = DataService.CheckBurnAddress(Int32.Parse(user), burnAddressRef);
            if (exist == true)
            {
                error = true;
                errorMessage = "Error, Address Reference already exist.";
            }


            bool isParentCust = DataService.CheckIsParentCust(Int32.Parse(user));
            if(isParentCust==false)
            {
                error = true;
                errorMessage = "Error, customer partners can not register Burn Addresses.";
            }

            if (error == false)
            {
                jsonBurnAddress = JsonConvert.SerializeObject(DataService.GetBurnAddressNew(Int32.Parse(user), burnAddressRef, Tag));
                return jsonBurnAddress;
            }
            else
            {
                return errorMessage;
            }
        }
    }
}