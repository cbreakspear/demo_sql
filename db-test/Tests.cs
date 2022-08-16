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
            DotNetEnv.Env.Load(Environment.CurrentDirectory + "/" + Env.DEFAULT_ENVFILENAME);   
            connectionString = Environment.GetEnvironmentVariable("ConnectionString");                  
            using(var conn = new SqlConnection(connectionString))
            {
                 using(var cmd = new SqlCommand("sp_SelectProducts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@product", SqlDbType.Int).Value = 1;
                    conn.Open();
                    var result = cmd.ExecuteScalar().ToString();
                    var jsonResult = result;
                    Assert.AreEqual("1", jsonResult);
                }
            
                
            }            
        }           
    }
}

