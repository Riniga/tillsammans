using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Json;
public class MemoryDatabase : Database
{
    public MemoryDatabase()
    {
        //LoadFromJson();
        var user1 = new User() { Email = "Ingmar", Password = "24Vpqws5zGoUWkR6O95HB2BlYhW0zbMRaXMxVhIWx/Q=", FullName = "Ingmar Stenmark", Token = Guid.NewGuid() };
        var user2 = new User() { Email = "test", Password = "QuLWdRplKNXLjEz3IQyoJ8aGrY/OlPTOMWw2YidkzIk=", FullName = "Test Testsson", Token = Guid.NewGuid() };
        Users.Instance.AllUsers.Add(user1);
        Users.Instance.AllUsers.Add(user2);
    }

    private static void LoadFromJson()
    {
        string fileName = "users.json";
        string readText = File.ReadAllText(fileName);
        Console.WriteLine(readText);

        var users = JsonSerializer.Deserialize<List<User>>(readText);

        Console.WriteLine(Users.Instance.AllUsers.Count);

        foreach (var user in users)
        {
            Console.WriteLine("Email: " + user.Email);
        }
    }

    public async override Task<User> GetUser(string email)
    {
        await CosmosHelper.GetContainer();
        foreach(User user in Users.Instance.AllUsers)
        if (user.Email == email) return user;
        return null;
    }
    public override User AddLoggedInUser(User user)
    {
        user.Password = string.Empty;
        user.Token= Guid.NewGuid(); 
        
        Logger.Instance.Log("The users token is set to  " + user.Token);
        return user;
    }
    public override bool RemoveLoggedInUser(User user)
    {
        if (VerifyUserToken(user)) 
        {
            //TODO: Remove user from logged in database table...
            return true;
        }
        return false;
    }
    internal override bool VerifyUserToken(User user)
    {
        var userinDb = (User)Users.Instance.AllUsers.Where(listuser => listuser.Email==user.Email);
        if(user.Token==userinDb.Token) return true;
        return false;
    }

    public List<User> GetAllUsers()
    {
        return Users.Instance.AllUsers;
    }
}