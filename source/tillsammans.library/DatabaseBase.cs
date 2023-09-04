using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class DatabaseBase
{

    public abstract Task<bool> CreateUser(DbUser user);
    public abstract Task<DbUser> ReadUser(string username);
    public abstract Task<List<DbUser>> ReadAllUsers();
    public abstract Task<bool> UpdateUser(DbUser user);
    public abstract Task<bool> DeleteUser(string username);
    
    public abstract Task<DbLogin> LoginUser(DbUser user);
    public abstract Task<bool> LogoutUser(DbLogin login);
    public abstract Task<DbUser> GetUserFromToken(string token);

    internal static DatabaseBase GetDatabase<T>()
    {
        return (DatabaseBase)Activator.CreateInstance(typeof(T));
    }
    internal static DatabaseBase GetDefaultDatabase()
    {
        return GetDatabase<CosmosDatabase>();
    }
}

