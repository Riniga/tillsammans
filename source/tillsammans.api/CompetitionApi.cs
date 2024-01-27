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
        public static async Task<IActionResult> CreateCompetition([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string competitionJson = await new StreamReader(req.Body).ReadToEndAsync();
            var competitionObject = JObject.Parse(competitionJson);
            var competition = competitionObject.ToObject<DbCompetition>();
            var result = competition.Create();

            return new OkObjectResult(result);
        }

        [FunctionName("CreateCompetitions")]
        public static async Task<IActionResult> CreateCompetitions([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var bodyJson = JObject.Parse(body);
            var competitionsArray = (JArray)bodyJson["competitions"];
            var competitions = competitionsArray.ToObject<List<DbCompetition>>();

            bool result = true;
            foreach (var competition in competitions)
            {
                result = competition.Create();
            }
            
            return new OkObjectResult(result);
        }


        [FunctionName("ReadCompetition")]
        public static async Task<IActionResult> ReadCompetition([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            // TODO: Verify token 
            var competition = new DbCompetition(req.Query["name"]);
            return new OkObjectResult(competition);
        }
        [FunctionName("ReadAllCompetitions")]
        public static async Task<IActionResult> ReadAllCompetitions([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            return new OkObjectResult(Competitions.Instance.AllCompetitions);
        }


        [FunctionName("UpdateCompetition")]
        public static async Task<IActionResult> UpdateCompetition([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var bodyJson = JObject.Parse(body);
            var request = bodyJson.ToObject<AuthoraizedRequest>();

            if(!IsAuthoraized(request.Token, "manager")) return new UnauthorizedResult();
            
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
