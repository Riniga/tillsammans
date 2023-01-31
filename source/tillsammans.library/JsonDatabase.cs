using System;
public class JsonDatabase : Database
{
    private static string guid = "7b610e0a-b9f4-49bb-b896-52b79a10e157";
    public override User GetUser(string username)
    {
        var user=  new User();
        user.Username=username;
        //TODO: Popolate from database
        user.Password = "24Vpqws5zGoUWkR6O95HB2BlYhW0zbMRaXMxVhIWx/Q=";
        user.FullName ="Ingmar Stenmark";
        return user;
    }
    public override User AddLoggedInUser(User user)
    {
        user.Password = string.Empty;
        user.Token= new Guid(guid); // Guid.NewGuid();
        //TODO: Store user in database
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
        //TODO: Get user token from loggedin database table
        if(user.Token.ToString()==guid) return true;
        return false;
    }
}