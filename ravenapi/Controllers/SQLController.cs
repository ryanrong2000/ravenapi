using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace ravenapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SQLController : ControllerBase
    {
        
        string myConnectionString = "Server=;Database=;Uid=;Pwd=";
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {
                return getCust();
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
            
        }

        public string getCust()
        {
            string custName = "";
            MySqlConnection conn = new MySqlConnection(myConnectionString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from goraven.cust;";
                //MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    custName = reader.GetString("CustName");
                }
            }
            catch(Exception ex)
            {
                custName = ex.ToString();
            }
            return custName;

        }
    }


}