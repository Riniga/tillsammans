using System;
public class User
{
    public string Username;
    public string Password;
    public string FullName;
    public Guid Token;

    public static User LoginUser(User user)
    {
        var database = Database.GetDatabase<JsonDatabase>();
        User userFromDb = database.GetUser(user.Username);
        if(userFromDb.Password==user.Password)
        {
            return(database.AddLoggedInUser(userFromDb));
        }
        return user;
    }
    public static bool LogoutUser(User user)
    {
        var database = Database.GetDatabase<JsonDatabase>();
        return database.RemoveLoggedInUser(user);
    }
}