using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Services;

public interface IIdentityService
{
    long GetUserId(string emailAddress);
}

public class IdentityService : IIdentityService
{
    private readonly DataDbContext _dataDbContext;

    public IdentityService(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }

    public long GetUserId(string emailAddress)
    {
        var normalizedEmailAddress = emailAddress.ToUpper();

        // TODO: Add caching
        return _dataDbContext.Set<User>()
            .Where(_ => _.NormalizedEmail == normalizedEmailAddress)
            .Select(_ => _.Id)
            .SingleOrDefault();
    }
}
