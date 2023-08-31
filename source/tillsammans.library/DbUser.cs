using System.Collections.Generic;
using System.Linq;

public class DbUser
{
    private const string TableName = "Users";
    public void Create(User user)
    {
        var client = Helper.GetTableClient(TableName);
        client.AddEntity(user);
    }

    public User Read(string email)
    {
        var client = Helper.GetTableClient(TableName);
        return client.GetEntity<User>(email, email);
    }

    public List<User> ReadAll()
    {
        var client = Helper.GetTableClient(TableName);
        var users = client.Query<User>();
        return users.ToList<User>();
    }

    public void Update(User user)
    {
        var client = Helper.GetTableClient(TableName);
        client.UpdateEntity<User>(user,user.ETag);
    }

    public void Delete(string email)
    {
        var client = Helper.GetTableClient(TableName);
        client.DeleteEntity(email, email);
    }
}