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

namespace tillsammans.api
{
    public static class Authenticator
    {
        static Authenticator()
        {
            //FunctionsAssemblyResolver.RedirectAssembly();
        }
        [FunctionName("Login")]
        public static async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            //TODO: Could this be simplified are we reaching over the river for water?
            string userJson= await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            User user  = User.LoginUser(userObject.ToObject<User>());
            user.Password = HashPassword(user.Password); //TODO: Should and Could the password be hashed before sending it to service?
            return new OkObjectResult(User.LoginUser(user));
        }
        [FunctionName("Logout")]
        public static async Task<IActionResult> Logout([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string userJson= await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            User user  = userObject.ToObject<User>();
            
            return new OkObjectResult(User.LogoutUser(user));
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
