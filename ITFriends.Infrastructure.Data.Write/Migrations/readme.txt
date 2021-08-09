IdentityServerMigration:

cd ..\project-prog-point\server\ITFriends\ITFriends.Identity.IdentityServer

dotnet ef migrations add InitialAppUserWriteDbContext -c AppUserWriteDbContext -o Migrations/WriteDb --project ../ITFriends.Infrastructure.Data.Write
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Migrations/IS4Db/PersistedGrantDb --project ../ITFriends.Infrastructure.Data.Write
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Migrations/IS4Db/ConfigurationDb --project ../ITFriends.Infrastructure.Data.Write

dotnet ef database update -c AppUserWriteDbContext
dotnet ef database update -c PersistedGrantDbContext
dotnet ef database update -c ConfigurationDbContext

WriteDbMigration:

cd ..\project-prog-point\server\ITFriends\ITFriends.Topic.Api

dotnet ef migrations add MigrationName -c AppDbContext -o Migrations/WriteDb --project ../ITFriends.Infrastructure.Data.Write

dotnet ef migrations add CascadeDeleteMessageWhenDeleteTopic -c AppDbContext -o Migrations/WriteDb --project ../ITFriends.Infrastructure.Data.Write

remove last migration: dotnet ef migrations remove -c AppDbContext --project ../ITFriends.Infrastructure.Data.Write