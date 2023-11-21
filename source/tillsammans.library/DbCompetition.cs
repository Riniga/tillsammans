using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DbCompetition
{
    [JsonProperty(PropertyName = "id")]
    public string Id;
    [JsonProperty(PropertyName = "responsibleemail")]
    public string ResponsibleEmail;
    [JsonProperty(PropertyName = "name")]
    public string Name;
    [JsonProperty(PropertyName = "startdate")]
    public string StartDate;
    [JsonProperty(PropertyName = "enddate")]
    public string EndDate;
    [JsonProperty(PropertyName = "postalcode")]
    public string PostalCode;
    [JsonProperty(PropertyName = "location")]
    public string Location;
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
    [JsonProperty(PropertyName = "roles")]
    public List<string> Roles;
    [JsonProperty(PropertyName = "password")]
    public string Password;

    public DbCompetition() { }
    public DbCompetition(string email, string fullName, string personnumber, string adress, string postalCode, string city, string primaryPhone, string secondaryPhone,string license, string club, string zone, string password) 
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
        Roles = new List<string>();
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
        Roles = userfromdb.Roles;
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

    public static implicit operator DbUser(FeedResponse<DbLogin> v)
    {
        throw new NotImplementedException();
    }
}