using AutoMapper;

namespace OnlineStore.Logic.UnitTests.Factories;

public static class MockMapperFactory
{
    public static IMapper Build<TProfile>()
        where TProfile : Profile, new()
    {
        var mapperConfiguration = new MapperConfiguration(configurationExpression => configurationExpression.AddProfile<TProfile>());

        return mapperConfiguration.CreateMapper();
    }
}
