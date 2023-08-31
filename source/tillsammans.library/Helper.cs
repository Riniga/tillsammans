using Azure.Data.Tables;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

public class Helper
{
    public static TableClient GetTableClient(string name)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        var tableService = new TableServiceClient(connectionString);
        var client = tableService.GetTableClient(name);
        client.CreateIfNotExists();
        return client;
    }
    public static string HashPassword(string password)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: Encoding.ASCII.GetBytes("AzureWebsite"),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return hashed;
    }
}
