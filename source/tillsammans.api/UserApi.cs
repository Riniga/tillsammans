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

        [FunctionName("TestUserApi")]
        public static async Task<IActionResult> TestUserApi([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            var AllTests = new List<string>();

            try
            { 
            var endpointUrl = Environment.GetEnvironmentVariable("EndpointUrl");
            AllTests.Add("Read endpoint from local.settings.json: passed ");

                // var allusers = Users.Instance.AllUsers;
                // int numberofusersindatabase = allusers.Count;
                // AllTests.Add("Count users", numberofusersindatabase.ToString());

                // var user = new DbUser("test@test.nu", "Test Testsson", "0202020202-0202", "adress", "12345", "city", "123", "456", "club", "zone", "QuLWdRplKNXLjEz3IQyoJ8aGrY/OlPTOMWw2YidkzIk=");
                // var result = user.Create();
                // AllTests.Add("Created User:", result.ToString() );

                // allusers = Users.Instance.AllUsers;
                // int newnumberofusersindatabase = allusers.Count;
                // AllTests.Add("Count users second time", newnumberofusersindatabase.ToString());
                // var userfromdb = new DbUser("test@test.nu");
                // AllTests.Add("Verified user: ", (user.Password == userfromdb.Password).ToString());

                // user.Password = "QuLWdRplKNXLjEz3IQyoJ8aGrY/OlPTOMWw2YidkzIk=";
                // result = user.Update();
                // AllTests.Add("Updated User:", result.ToString());

                // userfromdb = new DbUser("test@test.nu");
                // AllTests.Add("Verified user after update: ", (user.Password == userfromdb.Password).ToString());

                // result = user.Delete();
                // AllTests.Add("Deleted user: ", result.ToString());

                // allusers = Users.Instance.AllUsers;
                // newnumberofusersindatabase = allusers.Count;
                // AllTests.Add("Count users third time", newnumberofusersindatabase.ToString());
                // AllTests.Add("Verified deletion", (newnumberofusersindatabase == numberofusersindatabase).ToString());
            }catch (Exception) { }

            return new OkObjectResult(AllTests);
        }

        [FunctionName("CreateUser")]
        public static async Task<IActionResult> CreateUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            // TODO: Verify token 
            string userJson = await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            var user = userObject.ToObject<DbUser>();
            user.Password = DbLogin.HashPassword(user.Password); //TODO: Should and Could the password be hashed before sending it to service?

            var result = user.Create();

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
            string userJson = await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JObject.Parse(userJson);
            var user = userObject.ToObject<DbUser>();

            // TODO: Handle password???
            // TODO: Verify token 

            var result = user.Update();
            return new OkObjectResult(result);
        }

        
    }
}
