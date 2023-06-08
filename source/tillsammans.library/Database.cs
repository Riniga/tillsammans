using System;
using System.Threading.Tasks;

public abstract class Database
{

    public abstract Task<User> GetUser(string username);
    public abstract User AddLoggedInUser(User user);
    public abstract bool RemoveLoggedInUser(User user);
    internal abstract bool VerifyUserToken(User user);
    internal static Database GetDatabase<T>()
    {
        return (Database)Activator.CreateInstance(typeof(T));
    }
    internal static Database GetDefaultDatabase()
    {
        return GetDatabase<MemoryDatabase>();
    }

    
    
}

