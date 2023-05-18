using System;
using System.Collections.Generic;

public sealed class Users
{
    public List<User> AllUsers;

    private static readonly Lazy<Users> lazy = new Lazy<Users>(() => new Users());
    public static Users Instance { get { return lazy.Value; } }
    private Users() { AllUsers = new List<User>(); }
    
}