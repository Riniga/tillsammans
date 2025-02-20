using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DbCompetition
{
    public static string ContainerName = "Competitions";
    [JsonProperty(PropertyName = "id")]
    public string Id;
    [JsonProperty(PropertyName = "responsibleemail")]
    public string ResponsibleEmail;
    [JsonProperty(PropertyName = "name")]
    public string Name;
    [JsonProperty(PropertyName = "startdate")]
    public DateTime StartDate;
    [JsonProperty(PropertyName = "enddate")]
    public DateTime EndDate;

    

    public DbCompetition() { }
    public DbCompetition(string responsible, string name, DateTime startDate, DateTime endDate) 
    {
        ResponsibleEmail = responsible;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }
    public DbCompetition(string name)
    {
        var database = DatabaseBase.GetDefaultDatabase();
        var competitionfromdb = database.ReadCompetition(name).Result;
        ResponsibleEmail = competitionfromdb.ResponsibleEmail;
        Name = competitionfromdb.Name;
        StartDate = competitionfromdb.StartDate;
        EndDate = competitionfromdb.EndDate;
    }
    public bool Create()
    {
        var database = DatabaseBase.GetDefaultDatabase();
        return database.CreateCompetition(this).Result;

    }
    public bool Update()
    {
        var database = DatabaseBase.GetDefaultDatabase();
        return database.UpdateCompetition(this).Result;
    }

    public bool Delete()
    {
        var database = DatabaseBase.GetDefaultDatabase();
        return database.DeleteCompetition(Name).Result;

    }
}