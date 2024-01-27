using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;

namespace tillsammans.api
{
    public static class AuthenticatorApi
    {
        static AuthenticatorApi()
        {
            FunctionsAssemblyResolver.RedirectAssembly();
        }

        [FunctionName("TestAuthenticationApi")]
        public static async Task<IActionResult> Test([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            Dictionary<string, string> AllTests = new Dictionary<string, string>();
            AllTests.Add("Create user object", "failed");
            AllTests.Add("Create user in database", "failed");
            AllTests.Add("Login user", "failed");
            AllTests.Add("Verified correct token", "failed");
            AllTests.Add("Verified wrong token", "failed");
            AllTests.Add("Logged out user", "failed");
            AllTests.Add("Delete user", "failed");
            try
            { 
                var user = new DbUser("test@test.nu", "Test Testsson", "0202020202-0202", "adress", "12345", "city", "123", "456","license", "club", "zone", "QuLWdRplKNXLjEz3IQyoJ8aGrY/OlPTOMWw2YidkzIk=");
                AllTests["Create user object"] = "passed";
                
                user.Create();
                AllTests["Create user in database"] = "passed";
                
                var login = await DbLogin.LoginUser(user);
                AllTests["Login user"] = "passed";

                var result = await DbLogin.GetUserFromToken(login.Token.ToString()) ;
                if (result.Email==login.Email) AllTests["Verified correct token"] = "passed";
                
                var correctToken = login.Token;
                login.Token = Guid.NewGuid();
                result = await DbLogin.GetUserFromToken(login.Token.ToString()) ;
                if (result==null) AllTests["Verified wrong token"] = "passed";

                login.Token = correctToken;
                if (login.Logout()) AllTests["Logged out user"] = "passed";

                user.Delete();
                AllTests["Delete user"] = "passed";
            }
            catch (Exception) { }


            return new OkObjectResult(AllTests);
        }

        [FunctionName("Login")]
        public static async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            //TODO: Could this be simplified are we reaching over the river for water?
            string userJson= await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            var user = userObject.ToObject<DbUser>();
            user.Password = DbLogin.HashPassword(user.Password); //TODO: Should and Could the password be hashed before sending it to service?
            

            Logger.Instance.Log("Try to login user: " +user.Email);
            Logger.Instance.Log("With password: " + user.Password);
            
            var login = await DbLogin.LoginUser(user);

            Logger.Instance.Log("Return user on login:" + JsonConvert.SerializeObject(login) ); 
            return new OkObjectResult(login);
        }
        [FunctionName("Logout")]
        public static async Task<IActionResult> Logout([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string loginJson = await new StreamReader(req.Body).ReadToEndAsync();
            var loginObject = JObject.Parse(loginJson);
            DbLogin login= loginObject.ToObject<DbLogin>();
            return new OkObjectResult(login.Logout());
        }
        [FunctionName("HashPassword")]
        public static async Task<IActionResult> HashPassword([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            var password = req.Query["password"];
            string hash = DbLogin.HashPassword(password);
            return new OkObjectResult(hash);
        }
    }
}
