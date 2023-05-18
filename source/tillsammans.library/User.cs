using System;
public class User
{
    public string Username;
    public string Password;
    public string FullName;
    public Guid Token;
    public static User LoginUser(User user)
    {
        var database = Database.GetDefaultDatabase();
        User userFromDb = database.GetUser(user.Username);
        if(userFromDb.Password==user.Password)
        {
            //Logger.Instance.Log("Correct password!");
            return(database.AddLoggedInUser(userFromDb));
        }
        else 
        {
            Logger.Instance.Log("Wrong password (db.pass <> user.pass): " + userFromDb.Password + " <> " + user.Password);
        }
        return user;
    }
    public static bool LogoutUser(User user)
    {
        var database = Database.GetDefaultDatabase();
        return database.RemoveLoggedInUser(user);
    }
}