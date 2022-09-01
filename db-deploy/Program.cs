using System;
using System.Reflection;
using DbUp;
using DotNetEnv;
using DbUp.MySql;
using DbUp.Engine.Output;

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
                
                case "Postgres": // POSTGRES Implementation
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
                case "SQL_Server": // SQL SERVER Implementation
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
                case "MySQL": // MYSQL Implementation
                    //Special case for MYSQL with DBUP. You have  to manually create the Journal
                     var connectionManager = new MySqlConnectionManager(mysqlconnectionString);
                     var logger = new ConsoleUpgradeLog();
                     MySqlTableJournal myJournal = new MySqlTableJournal(() => connectionManager, 
                        () => logger, "production", "$__schema_journal");

                     var upgradermysql = DeployChanges.To
                        .MySqlDatabase(connectionManager)
                        .JournalTo(myJournal)
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
