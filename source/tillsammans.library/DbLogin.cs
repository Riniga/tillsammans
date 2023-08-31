using Microsoft.Azure.Cosmos;
using System;

public class DbLogin
{
    private const string TableName = "Logins";
    public Login Login;

    public DbLogin(Login login)
    {
        this.Login = login;
    }
    public DbLogin(User user)
    {
        Create(user.Email, user.Password);
    }

    private void Create(string email, string password)
    {
        this.Login = new Login(email);
        var userDb = new DbUser();
        User userFromDb = userDb.Read(email);
        if (userFromDb.Password == password)
        {
            Logger.Instance.Log("Correct password!");
            this.Login.Token = Guid.NewGuid();
            var client = Helper.GetTableClient(TableName);
            client.DeleteEntity(this.Login.Email, this.Login.Email);
            client.AddEntity(this.Login);
        }
        else Logger.Instance.Log("Wrong password (db.pass <> user.pass): " + userFromDb.Password + " <> " + password);
    }

    public void Read()
    {
        var client = Helper.GetTableClient(TableName);
        this.Login = client.GetEntity<Login>(Login.Email, Login.Email);
    }

    public void Delete()
    {
        var client = Helper.GetTableClient(TableName);
        client.DeleteEntity(Login.Email, Login.Email);
    }
}