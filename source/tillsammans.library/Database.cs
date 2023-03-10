using System;
public abstract class Database
{

    public abstract User GetUser(string username);
    public abstract User AddLoggedInUser(User user);
    public abstract bool RemoveLoggedInUser(User user);
    internal abstract bool VerifyUserToken(User user);
    internal static Database GetDatabase<T>()
    {
        return new JsonDatabase();
    }

    
    
}

