# CREATE THE SQL DATABASE SAMPLE OF CI/CD WITH github ACTIONS

-Create Azure SQL Database and Server (Use Portal or CLI)
-Add User (SHOWN BELOW)
CREATE USER [github_action_user] WITH PASSWORD = 'S0meVery_Very+Str0ngPazzworD!';
ALTER ROLE db_owner ADD MEMBER [github_action_user];

-Add Roles
ALTER ROLE db_ddladmin ADD MEMBER [github_action_user];
ALTER ROLE db_datareader ADD MEMBER [github_action_user];
ALTER ROLE db_datawriter ADD MEMBER [github_action_user];

-CREATE THE SQL CONNECTION STRING SECRET in github
secrets.AZURE_SQL_CONNECTION_STRING

-Change the .env files to match you connection string and databasetype
OPTIONS: for DatabaseType
SQL_Server
Postgres
MySql

-Push Code
-Run Workflow

# CREATE THE POSTGRES DATABASE SAMPLE OF CI/CD WITH github ACTIONS

-Create Azure Postgres Database and Server (Use Portal or CLI)

-CREATE THE POSTGRES CONNECTION STRING SECRET in github
secrets.AZURE_SQL_CONNECTION_STRING

-Change the .env files to match you connection string and databasetype
OPTIONS: for DatabaseType
SQL_Server
Postgres
MySql

-Push Code
-Run Workflow

# CREATE THE MYSQL DATABASE SAMPLE OF CI/CD WITH github ACTIONS

-Create Azure MySQL Database and Server (Use Portal or CLI)

-CREATE THE MYSQL CONNECTION STRING SECRET in github
secrets.AZURE_SQL_CONNECTION_STRING

-Change the .env files to match you connection string and databasetype
OPTIONS: for DatabaseType
SQL_Server
Postgres
MySql

-Push Code
-Run Workflow

To test locally run change directory to the project for the tests and run ---  dotnet test



