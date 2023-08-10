using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

public class DbUser
{
    [JsonProperty(PropertyName = "id")]
    public string Id => Email;
    [JsonProperty(PropertyName = "email")]
    public string Email;
    [JsonProperty(PropertyName = "fullname")]
    public string FullName;
    [JsonProperty(PropertyName = "personnumber")]
    public string Personnumber;
    [JsonProperty(PropertyName = "adress")]
    public string Adress;
    [JsonProperty(PropertyName = "postalcode")]
    public string PostalCode;
    [JsonProperty(PropertyName = "city")]
    public string City;
    [JsonProperty(PropertyName = "primaryphone")]
    public string PrimaryPhone;
    [JsonProperty(PropertyName = "secondaryphone")]
    public string SecondaryPhone;
    [JsonProperty(PropertyName = "license")]
    public string License;
    [JsonProperty(PropertyName = "club")]
    public string Club;
    [JsonProperty(PropertyName = "zone")]
    public string Zone;
    [JsonProperty(PropertyName = "password")]
    public string Password;

    public DbUser() { }
    public DbUser(string email, string fullName, string personnumber, string adress, string postalCode, string city, string primaryPhone, string secondaryPhone,string license, string club, string zone, string password) 
    {
        Email = email;
        FullName = fullName;
        Personnumber = personnumber;
        Adress = adress;
        PostalCode = postalCode;
        City = city;
        PrimaryPhone = primaryPhone;
        SecondaryPhone = secondaryPhone;
        License = license;
        Club = club;
        Zone = zone;
        Password = password;
    }
    public DbUser(string email)
    {
        var database = DatabaseBase.GetDefaultDatabase();
        var userfromdb = database.ReadUser(email).Result;
        Email = userfromdb.Email;
        FullName = userfromdb.FullName;
        Personnumber = userfromdb.Personnumber;
        Adress = userfromdb.Adress;
        PostalCode = userfromdb.PostalCode;
        City = userfromdb.City;
        PrimaryPhone = userfromdb.PrimaryPhone;
        SecondaryPhone = userfromdb.SecondaryPhone;
        License = userfromdb.License;
        Club = userfromdb.Club;
        Zone = userfromdb.Zone;
        Password = userfromdb.Password;
    }
    public bool Create()
    {
        var database = DatabaseBase.GetDefaultDatabase();
        return database.CreateUser(this).Result;
    }
    public bool Update()
    {
        var database = DatabaseBase.GetDefaultDatabase();
        return database.UpdateUser(this).Result;
    }

    public bool Delete()
    {
        var database = DatabaseBase.GetDefaultDatabase();
        return database.DeleteUser(Email).Result;

    }
}