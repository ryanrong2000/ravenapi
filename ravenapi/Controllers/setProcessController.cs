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
    public class setProcessController : ControllerBase
    {
        // put api/values
        //https://localhost:44368/api/setProcess/:?address=xxxx&asset=GORAVEN
        [HttpPut("{address,asset}")]
        public ActionResult<String> Put(string address, string asset)
        {
            String errorMessage = "";
            bool error = false;

            var user = User.Identity.Name;
            bool pairExist = DataService.CheckProcessExist(address, asset);
            if (pairExist == true)
            {
                error = true;
                errorMessage = "Error, address asset pair process already exist.";
            }
            bool addressExist = DataService.CheckProcessAddress(Int32.Parse(user), address);
            if(addressExist==false)
            {
                error = true;
                errorMessage = "Error, address is not associated to account.";
            }
            bool assetExist = DataService.CheckProcessAsset(Int32.Parse(user), asset);
            if(assetExist==false)
            {
                error = true;
                errorMessage = "Error, asset is not associated to account.";
            }
            if (error == false)
            {
                string processId = DataService.SetProcess(Int32.Parse(user), address, asset);
                return processId;
            }
            else
            {
                return errorMessage;
            }

        }
    }
}