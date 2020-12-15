using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ravenapi.Entities;
using ravenapi.Helpers;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;


namespace ravenapi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        //IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {
        /*
        private readonly IConfiguration configuration;
        public UserService(IConfiguration config)
        {
            this.configuration = config;
        }
        */

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        /*
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" },
            new User { Id = 2, FirstName = "John", LastName = "Doe", Username = "john", Password = "john" }
        };
        */
        
        

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            User user = DataService.UserLogin(username, password);

            // return null if user not found
            if (user.Id == 0)
              return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return user;

        }

        /*
        public User Authenticate(string username, string password)
        {
            var constr = configuration.GetConnectionString("MySqlConnection");
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from `goraven`.`cust` where `UserName` = " + username + " and `Password` = " + password + " limit 1;";
                //MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    User user = new User();
                    while (reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader.GetString("CustId"));
                        user.Username = reader.GetString("UserName");

                        // authentication successful so generate jwt token
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, user.Id.ToString())
                            }),
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        user.Token = tokenHandler.WriteToken(token);

                        
                    }
                    // remove password before returning
                    user.Password = null;
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            //if (user == null)
              //  return null;

            //return user;
        }
        */

        /*
        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.Password = null;
                return x;
            });
        }
        */

        
    }
}
