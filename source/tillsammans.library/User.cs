using System;
public class User
{
    public string FullName;
    public string Personnumber;
    public string Adress;
    public string PostalCode;
    public string City;
    public string PrimaryPhone;
    public string SecondaryPhone;
    public string Email;
    public string Club;
    public string Zone;
    public string Password;
    public Guid Token;

    public static User LoginUser(User user)
    {
        var database = Database.GetDefaultDatabase();
        User userFromDb =  database.GetUser(user.Email).Result;
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