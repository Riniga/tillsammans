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
            
            var user = new DbUser("test@test.nu", "Test Testsson", "0202020202-0202", "adress", "12345", "city", "123", "456","license", "club", "zone", "QuLWdRplKNXLjEz3IQyoJ8aGrY/OlPTOMWw2YidkzIk=");
            user.Create();

            // Login in user
            var login = await DbLogin.LoginUser(user);
            AllTests.Add("Retrieved token: ", login.Token.ToString());

            // Verify token
            var result = login.Verify();
            AllTests.Add("Verified correct token: ", result.ToString());
            var correctToken = login.Token;
            login.Token = Guid.NewGuid();
            result = login.Verify();
            AllTests.Add("Verified wrong token: ", result.ToString());

            login.Token = correctToken;
            result = login.Logout();
            AllTests.Add("Logged out user: ", result.ToString());
            
            user.Delete();

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
    }
}
