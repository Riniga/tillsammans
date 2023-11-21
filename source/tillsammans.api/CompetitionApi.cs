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
using System.Security.Principal;
using Microsoft.Extensions.Primitives;

namespace tillsammans.api
{
    public static class CompetitionApi
    {
        static CompetitionApi()
        {
            FunctionsAssemblyResolver.RedirectAssembly();
        }

        [FunctionName("CreateCompetition")]
        public static async Task<IActionResult> CreateUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string competitionJson = await new StreamReader(req.Body).ReadToEndAsync();
            var competitionObject = JObject.Parse(competitionJson);
            var competition = competitionObject.ToObject<DbCompetition>();
            var result = competition.Create();

            return new OkObjectResult(result);
        }

        [FunctionName("CreateUsers")]
        public static async Task<IActionResult> CreateUsers([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var bodyJson = JObject.Parse(body);
            var usersArray = (JArray)bodyJson["users"];
            var users = usersArray.ToObject<List<DbUser>>();

            bool result = true;
            foreach (var user in users)
            {
                user.Password = DbLogin.HashPassword(user.Password); //TODO: Should and Could the password be hashed before sending it to service?
                result = user.Create();
            }
            
            return new OkObjectResult(result);
        }


        [FunctionName("ReadUser")]
        public static async Task<IActionResult> ReadUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            // TODO: Verify token 
            var user = new DbUser(req.Query["email"]);
            user.Password = String.Empty;
            return new OkObjectResult(user);
        }
        [FunctionName("ReadAllUser")]
        public static async Task<IActionResult> ReadAllUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            return new OkObjectResult(Users.Instance.AllUsers );
        }


        [FunctionName("UpdateUser")]
        public static async Task<IActionResult> UpdateUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var bodyJson = JObject.Parse(body);
            
            var request = bodyJson.ToObject<AuthoraizedRequest>();

            if(!IsAuthoraized(request.Token, "manager")) return new UnauthorizedResult();
            var result = request.User.Update();

            return new OkObjectResult(result);
        }




        [FunctionName("UpdateUserOld")]
        public static async Task<IActionResult> UpdateUserOld([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string userJson = await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            var request = userObject.ToObject<AuthoraizedRequest>();
            
            if(!IsAuthoraized(request.Token, "manager")) return new UnauthorizedResult();
            // TODO: Handle password???

            var result = request.User.Update();

            return new OkObjectResult(result);
        }

        private static bool IsAuthoraized(StringValues token, string role)
        {
            DbUser loggedInUser = DbLogin.GetUserFromToken(token).Result;
            if (loggedInUser == null) return false; 
            if (loggedInUser.Roles.Contains(role)) return true;
            return false;
        }
    }
}
