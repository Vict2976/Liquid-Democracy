$password = New-Guid
docker ps -aq | xargs docker stop
$string = uname -v
if ($string -clike "*/RELEASE_X86_64*") { 
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
} else {
    docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/azure-sql-edge
}
$database = "liquiddemocracy"
$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password"
cd ./API/
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:liquiddemocracy" "$connectionString"
Set-Clipboard -Value  $connectionString
cd ../Entities/
rm -r ./Migrations/
dotnet ef migrations add InitialMigration -s ../API/
dotnet ef database update -s ../API/
cd ..