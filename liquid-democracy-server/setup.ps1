$password = New-Guid
$port = "5433"
$database = "liquiddemocracy"
docker ps -aq | xargs docker stop
cd ./API/
dotnet user-secrets init
$connectionString = "Host=localhost;Port=$port;Username=postgres;Password=$password;Database=$database"
dotnet user-secrets set "ConnectionStrings:liquiddemocracy" "$connectionString"
Set-Clipboard -Value $connectionString
docker run -e POSTGRES_PASSWORD=$password -p 5433:5432 -d postgres
cd ../Repository/
rm -r ./Migrations/
dotnet ef migrations add InitialMigration -s ../API/
dotnet ef database update -s ../API/
cd ..

