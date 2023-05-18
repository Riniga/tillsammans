using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace tillsammans.api
{
    public static class Authenticator
    {
        static Authenticator()
        {
            FunctionsAssemblyResolver.RedirectAssembly();
        }

        [FunctionName("Testing")]
        public static async Task<IActionResult> Testing([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string userJson= await new StreamReader(req.Body).ReadToEndAsync();
           Console.WriteLine("Authenticator initialized...");
            var database = new JsonDatabase();
            database.Test();

            return new OkObjectResult(userJson);


        }
        [FunctionName("Login")]
        public static async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            //TODO: Could this be simplified are we reaching over the river for water?
            string userJson= await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            var user = userObject.ToObject<User>();
            user.Password = HashPassword(user.Password); //TODO: Should and Could the password be hashed before sending it to service?
            
            Logger.Instance.Log("Try to login user: " +user.Username);
            user = User.LoginUser(user);

            Logger.Instance.Log("Return user on login:" + JsonConvert.SerializeObject(user) ); 
            return new OkObjectResult(user);
        }
        [FunctionName("Logout")]
        public static async Task<IActionResult> Logout([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string userJson= await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            User user  = userObject.ToObject<User>();
            
            return new OkObjectResult(User.LogoutUser(user));
        }

        [FunctionName("CreateUser")]
        public static async Task<IActionResult> CreateUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string userJson= await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            var user = userObject.ToObject<User>();
            user.Password = HashPassword(user.Password); //TODO: Should and Could the password be hashed before sending it to service?
            return new OkObjectResult(user);
        }

        private static string HashPassword(string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: Encoding.ASCII.GetBytes("AzureWebsite"),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
