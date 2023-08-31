using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace tillsammans.api
{
    public static class UserApi
    {
        static UserApi()
        {
            FunctionsAssemblyResolver.RedirectAssembly();
        }

        [FunctionName("CreateUser")]
        public static async Task<IActionResult> CreateUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            // TODO: Verify token 
            string userJson = await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            var user = userObject.ToObject<User>();
            user.Password = Helper.HashPassword(user.Password); //TODO: Should and Could the password be hashed before sending it to service?

            var dbUser = new DbUser();
            dbUser.Create(user);

            return new OkObjectResult(true);
        }

        [FunctionName("CreateUsers")]
        public static async Task<IActionResult> CreateUsers([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var bodyJson = JObject.Parse(body);

            var usersArray = (JArray)bodyJson["users"];
            var users = usersArray.ToObject<List<User>>();

            bool result = true;
            var dbUser = new DbUser();
            foreach (var user in users)
            {
                user.Password = Helper.HashPassword(user.Password); //TODO: Should and Could the password be hashed before sending it to service?
                dbUser.Create(user);
            }
            
            return new OkObjectResult(result);
        }


        [FunctionName("ReadUser")]
        public static async Task<IActionResult> ReadUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            // TODO: Verify token 

            var dbUser = new DbUser();
            var user = dbUser.Read(req.Query["email"]);
            user.Password = String.Empty;
            return new OkObjectResult(user);
        }
        [FunctionName("ReadAllUser")]
        public static async Task<IActionResult> ReadAllUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            var dbUser = new DbUser();
            var users = dbUser.ReadAll();
            return new OkObjectResult(users);
        }

        [FunctionName("UpdateUser")]
        public static async Task<IActionResult> UpdateUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string userJson = await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            var user = userObject.ToObject<User>();

            // TODO: Handle password???
            // TODO: Verify token 
            var dbUser = new DbUser();

            dbUser.Update(user);
            return new OkObjectResult(true);
        }

        
    }
}
