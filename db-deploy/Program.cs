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
            var mysqlconnectionString = Environment.GetEnvironmentVariable("MySQLConnectionString");
             
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
                case "MySQL":
                     var upgradermysql = DeployChanges.To
                        .MySqlDatabase(mysqlconnectionString)
                        .JournalToSqlTable("dbo", "$__schema_journal")
                        .WithScriptsFromFileSystem("./mysql")                                
                        .LogToConsole()
                        .Build();
                    var resultmysql = upgradermysql.PerformUpgrade();
                    if (!resultmysql.Successful)
                    {
                        Console.WriteLine(resultmysql.Error);
                        return -1;
                    }
                break;
            }

            Console.WriteLine("Success!");
            return 0;
        }
    }
}
