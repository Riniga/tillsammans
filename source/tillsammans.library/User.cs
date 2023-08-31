using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Newtonsoft.Json;
using System;

public class User : ITableEntity
{
    public User(string email, string fullName, string personnumber, string adress, string postalCode, string city, string primaryPhone, string secondaryPhone, string license, string club, string zone, string password)
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
        PartitionKey = Email;
        RowKey = Email;
    }

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

    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string PartitionKey { get; set; }
}