using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

    public class CosmosHelper
    {

        private static string endpointUrl = Environment.GetEnvironmentVariable("EndpointUrl");  //ConfigurationManager.AppSettings["EndpointUrl"];
        private static string primaryKey = Environment.GetEnvironmentVariable("PrimaryKey");  //ConfigurationManager.AppSettings["PrimaryKey"];
        private static string databaseId = Environment.GetEnvironmentVariable("DatabaseId");  //ConfigurationManager.AppSettings["DatabaseId"];
        private static string containerId = Environment.GetEnvironmentVariable("ContainerId");  //ConfigurationManager.AppSettings["ContainerId"];
        private static string partitionKeyPath = "/username";

        //public static async Task<string> GetTokenForUserFromCosmosDb(string username)
        //{
        //    var container = await GetContainer();
        //    ItemResponse<Login> response = null;
        //    try {
        //        response = await container.ReadItemAsync<Login>(username, new PartitionKey(username));
        //    } catch (Exception ex) { return null; }

        //    return response.Resource.Token;
        //}


        //public static async Task<string> GetTokenForUserFromCosmosDb_(string username)
        //{
        //    var queryDefinition = new QueryDefinition("SELECT * FROM Logins WHERE Logins.username='" + username + "'");
        //    var container = await GetContainer();
        //    var queryResultSetIterator = container.GetItemQueryIterator<Login>(queryDefinition);
        //    List<Login> logins = new List<Login>();

        //    while (queryResultSetIterator.HasMoreResults)
        //    {
        //        var currentResultSet = await queryResultSetIterator.ReadNextAsync();
        //        logins.AddRange(currentResultSet);
        //    }
        //    if (logins.Count>0) return logins.FirstOrDefault().Token;
        //    return null;
        //}



        //public static async Task<Login> TryLoginUserInCosmosDb(string username, string passwordhash)
        //{
        //    var user = (User)await CosmosUserHelper.GetUserFromCosmosDb(username);
        //    if (passwordhash != user.PasswordHash) return null;

        //    var token = await GetTokenForUserFromCosmosDb(username);
        //    if (token != null) await DeleteLoginFromCosmosDb(username);

            
        //    var login = new Login() { Username=username, Name= user.Name, Role=user.Role, Token= Convert.ToBase64String(Guid.NewGuid().ToByteArray()) };
        //    var container = await GetContainer();
        //    await container.CreateItemAsync<Login>(login, new PartitionKey(login.Username));

        //    return login;

        //}
        //internal static async Task DeleteLoginFromCosmosDb(string username)
        //{
        //    var container = await GetContainer();
        //    await container.DeleteItemAsync<Login>(username, new PartitionKey(username));
        //}

        //public static async Task Logout(string username, string token)
        //{
        //    var user = (User)await CosmosUserHelper.GetUserFromCosmosDb(username);
        //    var tokenInDb = await GetTokenForUserFromCosmosDb(username);
        //    if (token == tokenInDb)
        //    {
        //        var container = await GetContainer();
        //        await container.DeleteItemAsync<Login>(username, new PartitionKey(username));
        //    }
        //}

        public static async Task<Container> GetContainer()
        {
            var cosmosClient = new CosmosClient(endpointUrl, primaryKey);
            var database = cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId).Result.Database;
            var container = await database.CreateContainerIfNotExistsAsync(containerId, partitionKeyPath);
            return container;
        }
    }