# CREATE THE DATABASE SAMPLE OF CI/CD WITH github ACTIONS

-Create Azure Database and Server (Use Portal or CLI)
-Add User (SHOWN BELOW)
CREATE USER [github_action_user] WITH PASSWORD = 'S0meVery_Very+Str0ngPazzworD!';
ALTER ROLE db_owner ADD MEMBER [github_action_user];

-Add Roles
ALTER ROLE db_ddladmin ADD MEMBER [github_action_user];
ALTER ROLE db_datareader ADD MEMBER [github_action_user];
ALTER ROLE db_datawriter ADD MEMBER [github_action_user];

-CREATE THE SQL CONNECTION STRING SECRET in github
secrets.AZURE_SQL_CONNECTION_STRING

-Change the .env files to match the connection string to the new database
-Push Code
-Run Workflow

To test locally run ---  dotnet test
