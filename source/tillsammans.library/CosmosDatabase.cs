using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public class CosmosDatabase : DatabaseBase
{
    public CosmosDatabase() {}

    public async override Task<bool> CreateUser(DbUser user)
    {
        var container = await GetContainer(ContainerType.Users);
        Logger.Instance.Log("Retrieved Cosmos Db Container: " + container.Id);
        try
        {
            await container.CreateItemAsync<DbUser>(user, new PartitionKey(user.Email));
        }
        catch (Exception ex) { return false; }
        return true;
    }

    public async override Task<DbUser> ReadUser(string email)
    {
        var container = await GetContainer(ContainerType.Users);
        ItemResponse<DbUser> response = null;
        try {
            response = await container.ReadItemAsync<DbUser>(email, new PartitionKey(email));
        } catch (Exception ex) { return null; }
        return response.Resource;
    }
    public async override Task<List<DbUser>> ReadAllUsers()
    {
        var container = await GetContainer(ContainerType.Users);
        var queryDefinition = new QueryDefinition("SELECT * FROM Users");
        var queryResultSetIterator = container.GetItemQueryIterator<DbUser>(queryDefinition);

        List<DbUser> users = new List<DbUser>();
        while (queryResultSetIterator.HasMoreResults)
        {
            var currentResultSet = await queryResultSetIterator.ReadNextAsync();
            users.AddRange(currentResultSet);
        }
        return users;
    }
    public async override Task<bool> UpdateUser(DbUser user)
    {
        var container = await GetContainer(ContainerType.Users);
        try
        {
            await container.ReplaceItemAsync<DbUser>(user, user.Email, new PartitionKey(user.Email));
        }
        catch (Exception ex) { return false; }
        return true;
    }
    public async override Task<bool> DeleteUser(string email)
    {
        var container = await GetContainer(ContainerType.Users);
        try
        {
            await container.DeleteItemAsync<DbUser>(email, new PartitionKey(email));
        }
        catch (Exception ex) { return false; }
        return true;
    }


    public async override Task<DbLogin> LoginUser(DbUser user)
    {
        user.Password = string.Empty;
        DbLogin login = new DbLogin() {Email= user.Email, Token= Guid.NewGuid() };
        
        var container = await GetContainer(ContainerType.Logins);
        try
        {
            await container.DeleteItemAsync<DbLogin>(login.Email, new PartitionKey(login.Email));
        }
        catch (Exception ex) {  }

        try
        {
            await container.CreateItemAsync<DbLogin>(login, new PartitionKey(user.Email));
        }
        catch (Exception ex) { return new DbLogin(); }

        
        return login;
    }
    public async override Task<bool> LogoutUser(DbLogin login)
    {
        if (await VerifyLogin(login)) 
        {
            try
            {
                var container = await GetContainer(ContainerType.Logins);
                await container.DeleteItemAsync<DbLogin>(login.Email, new PartitionKey(login.Email));
            }
            catch (Exception ex) { return false; }
            return true;
        }
        return false;
    }
    internal async override Task<bool> VerifyLogin(DbLogin login)
    {

        var container = await GetContainer(ContainerType.Logins);
        ItemResponse<DbLogin> response = null;
        try
        {
            response = await container.ReadItemAsync<DbLogin>(login.Email, new PartitionKey(login.Email));
        }
        catch (Exception ex) { return false; }

        if(login.Token == response.Resource.Token) return true;
        return false;
    }

    private async Task<Container> GetContainer(ContainerType containerType)
    {
        var cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("EndpointUrl"), Environment.GetEnvironmentVariable("PrimaryKey"));
        Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(Environment.GetEnvironmentVariable("DatabaseId"));
        var container = (containerType == ContainerType.Users) ?
                            await database.CreateContainerIfNotExistsAsync("Users", "/email") :
                            await database.CreateContainerIfNotExistsAsync("Logins", "/email");
        return container;
    }
}