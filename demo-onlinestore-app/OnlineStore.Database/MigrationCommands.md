-- Drop the database
dotnet ef database drop --startup-project OnlineStore.App/OnlineStore.App.csproj --project OnlineStore.Database/OnlineStore.Database.csproj --context DataDbContext --verbose -- "Server=(localdb)\\mssqllocaldb;Database=onlinestore-db;Trusted_Connection=True;MultipleActiveResultSets=True"

-- Create migration
dotnet ef migrations add InitialCreate --startup-project OnlineStore.App/OnlineStore.App.csproj --project OnlineStore.Database/OnlineStore.Database.csproj --context DataDbContext --verbose -- "Server=(localdb)\\mssqllocaldb;Database=onlinestore-db;Trusted_Connection=True;MultipleActiveResultSets=True"

-- Apply migration
dotnet ef database update --startup-project OnlineStore.App/OnlineStore.App.csproj --project OnlineStore.Database/OnlineStore.Database.csproj --context DataDbContext --verbose -- "Server=(localdb)\\mssqllocaldb;Database=onlinestore-db;Trusted_Connection=True;MultipleActiveResultSets=True"