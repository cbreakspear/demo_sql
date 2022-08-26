using System;
using System.Reflection;
using DbUp;
using DotNetEnv;

namespace AzureSQLDevelopers.Database.Deploy
{
    class Program
    {
        static int Main(string[] args)
        {  
            DotNetEnv.Env.Load(Environment.CurrentDirectory + "/" + Env.DEFAULT_ENVFILENAME);
            var databaseType = Environment.GetEnvironmentVariable("DatabaseType");
            var sqlconnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
            var postgresconnectionString = Environment.GetEnvironmentVariable("PostgresConnectionString");
             
            switch(databaseType)
            {
                
                case "Postgres":
                    var upgradersql = DeployChanges.To
                        .PostgresqlDatabase(postgresconnectionString)
                        .JournalToPostgresqlTable("public", "$__schema_journal")
                        .WithScriptsFromFileSystem("./postgres")                                
                        .LogToConsole()
                        .Build();
                    var resultsql = upgradersql.PerformUpgrade();
                    if (!resultsql.Successful)
                    {
                        Console.WriteLine(resultsql.Error);
                        return -1;
                    }
                    break;
                case "SQL_Server":
                    var upgraderpostgres = DeployChanges.To
                        .SqlDatabase(sqlconnectionString)
                        .JournalToSqlTable("dbo", "$__schema_journal")
                        .WithScriptsFromFileSystem("./sql")                                
                        .LogToConsole()
                        .Build();
                    var resultpostgres = upgraderpostgres.PerformUpgrade();
                    if (!resultpostgres.Successful)
                    {
                        Console.WriteLine(resultpostgres.Error);
                        return -1;
                    }
                    break;
                case "My_SQL":
                break;
            }

            Console.WriteLine("Success!");
            return 0;
        }
    }
}
