using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;
using OnlineStore.Logic.UnitTests.Factories;
using OnlineStore.Logic.UnitTests.SpecimenBuilders;
using System.Runtime.CompilerServices;

namespace OnlineStore.Logic.UnitTests.Abstractions;

public abstract class BaseTests<TTests, TMappingProfile>
    where TTests : BaseTests<TTests, TMappingProfile>
    where TMappingProfile : Profile, new()
{
    protected readonly Fixture _fixture;
    protected readonly IMapper _mockMapper;

    protected User _initialUser;

    public BaseTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new UniqueIdSpecimenBuilder());

        _mockMapper = MockMapperFactory.Build<TMappingProfile>();
    }

    protected abstract Task InitializeDatabaseAsync(DataDbContext dataDbContext);

    protected async Task<DataDbContext> GenerateMockDataDbContextAsync([CallerMemberName] string callerMemberName = "")
    {
        var options = new DbContextOptionsBuilder<DataDbContext>()
            .UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={typeof(TTests).Name}-{callerMemberName};Trusted_Connection=True;MultipleActiveResultSets=True")
            .Options;

        var dataDbContext = new DataDbContext(options);

        await dataDbContext.Database.EnsureDeletedAsync();
        await dataDbContext.Database.MigrateAsync();

        _initialUser = new User { UserName = "unit@tests.com", Email = "unit@tests.com" };
        await dataDbContext.Set<User>().AddAsync(_initialUser);
        await dataDbContext.SaveChangesAsync();

        await this.InitializeDatabaseAsync(dataDbContext);

        return dataDbContext;
    }
}
