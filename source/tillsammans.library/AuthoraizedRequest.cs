using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AuthoraizedRequest
{
    [JsonProperty(PropertyName = "token")]
    public string Token;
    [JsonProperty(PropertyName = "user")]
    public DbUser User;
}