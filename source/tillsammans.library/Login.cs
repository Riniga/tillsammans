using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

public class Login : ITableEntity
{
    [JsonProperty(PropertyName = "email")]
    public string Email;
    [JsonProperty(PropertyName = "token")]
    public Guid Token;
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string PartitionKey { get; set; }

    public Login(string email)
    {
        PartitionKey = Email;
        RowKey = Email;
    }


}