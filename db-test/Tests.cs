using NUnit.Framework;
using DotNetEnv;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AzureSQLDevelopers.Database
{
    public class Tests
    {
        private string connectionString;

        [OneTimeSetUp]
        public void Setup()
        {
            DotNetEnv.Env.Load(Environment.CurrentDirectory + "/" + Env.DEFAULT_ENVFILENAME);   
            connectionString = Environment.GetEnvironmentVariable("ConnectionString");           
            
        }

        [Test]
        public void CheckEmptyJSON()
        {                    
            using(var conn = new SqlConnection(connectionString))
            {
                var result = conn.ExecuteScalar<string>("web.get_trainingsessionsync", new { @json = "{}" }, commandType: CommandType.StoredProcedure);
                var jsonResult = JObject.Parse(result);
                Assert.AreEqual("Full", (string)jsonResult.SelectToken("Metadata.Sync.Type"));
            }            
        }           
    }
}

