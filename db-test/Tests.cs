﻿using NUnit.Framework;
using DotNetEnv;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace AzureSQLDevelopers.Database
{
    public class Tests
    {
        private string connectionString;
        private string connectionType;

        [OneTimeSetUp]
        public void Setup()
        {
            DotNetEnv.Env.Load(Environment.CurrentDirectory + "/" + Env.DEFAULT_ENVFILENAME);
            connectionType = Environment.GetEnvironmentVariable("DatabaseType");
            if (connectionType == "Postgres")
            {
                connectionString = Environment.GetEnvironmentVariable("PostgresConnectionString");
            }
            else
            {
                connectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
            }

        }

        [Test]
        public void CheckEmptyJSON()
        {
            DotNetEnv.Env.Load(Environment.CurrentDirectory + "/" + Env.DEFAULT_ENVFILENAME);
            connectionType = Environment.GetEnvironmentVariable("DatabaseType");
            if (connectionType == "Postgres")
            {
                connectionString = Environment.GetEnvironmentVariable("PostgresConnectionString");
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    using (var cmd = new NpgsqlCommand("production.sp_SelectProducts", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@product", NpgsqlTypes.NpgsqlDbType.Text).Value = 1;
                        conn.Open();
                        var result = cmd.ExecuteScalar().ToString();
                        var jsonResult = result;
                        Assert.AreEqual("1", jsonResult);
                    }


                }
            }
            else
            {
                connectionString = Environment.GetEnvironmentVariable("SQLConnectionString");

                using (var conn = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand("production.sp_SelectProducts", conn))
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
}

