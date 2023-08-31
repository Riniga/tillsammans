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
using Microsoft.Extensions.Primitives;
using System.Text;

namespace tillsammans.api
{
    public static class AuthenticatorApi
    {
        static AuthenticatorApi()
        {
            FunctionsAssemblyResolver.RedirectAssembly();
        }

        [FunctionName("Login")]
        public static async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            //TODO: Could this be simplified are we reaching over the river for water?
            string userJson= await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            var user = userObject.ToObject<User>();
            user.Password = Helper.HashPassword(user.Password); //TODO: Should and Could the password be hashed before sending it to service?
            
            Logger.Instance.Log("Try to login user: " +user.Email);
            Logger.Instance.Log("With password: " + user.Password);

            var dbLogin = new DbLogin(user);

            Logger.Instance.Log("Return user on login:" + JsonConvert.SerializeObject(dbLogin.Login) ); 
            return new OkObjectResult(dbLogin.Login);
        }
        [FunctionName("Logout")]
        public static async Task<IActionResult> Logout([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string loginJson = await new StreamReader(req.Body).ReadToEndAsync();
            var loginObject = JObject.Parse(loginJson);
            var login= loginObject.ToObject<Login>();
            var dbLogin = new DbLogin(login);
            dbLogin.Delete();
            return new OkObjectResult(true);
        }

        //[FunctionName("ResetPassword")]
        //public static async Task<IActionResult> ResetPassword([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        //{
        //    var resetUser = new DbResetPassword(req.Query["email"]);
        //    var result = resetUser.Reset();
        //    return new OkObjectResult(result);
        //}

        //[FunctionName("SetPassword")]
        //public static async Task<IActionResult> SetPassword([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        //{

        //    var token = req.Query["token"];
        //    var email = req.Query["email"];
        //    var password = req.Query["password"];

            
        //    var resetUser = new DbResetPassword(email);
        //    var result = resetUser.Reset();
        //    return new OkObjectResult(result);
        //}

    }
}
