$password = New-Guid
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
$database = "liquiddemocracy"
$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password"
cd ./API/
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:liquiddemocracy" "$connectionString"
Set-Clipboard -Value  $connectionString
cd ../Entities/
dotnet ef migrations add InitialMigration -s ../API/
dotnet ef database update -s ../API/
cd ..