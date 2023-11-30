using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;
using OnlineStore.Logic.Concerns.ProductConcern;
using OnlineStore.Logic.Concerns.ProductConcern.GetAll;
using OnlineStore.Logic.Concerns.ProductConcern.GetById;
using OnlineStore.Logic.Concerns.ProductConcern.Post;
using OnlineStore.Logic.Concerns.ProductConcern.Put;
using OnlineStore.Logic.UnitTests.SpecimenBuilders;
using System.Runtime.CompilerServices;

using GetAll = OnlineStore.Logic.Concerns.ProductConcern.GetAll;
using GetById = OnlineStore.Logic.Concerns.ProductConcern.GetById;
using Post = OnlineStore.Logic.Concerns.ProductConcern.Post;
using Put = OnlineStore.Logic.Concerns.ProductConcern.Put;

namespace OnlineStore.Logic.UnitTests.Concerns;

public class ProductConcernTests
{
    private readonly Fixture _fixture;
    private readonly IList<Product> _initialProducts;

    public ProductConcernTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new UniqueIdSpecimenBuilder());
        _initialProducts = GenerateMockProducts(5);
    }

    [Fact]
    public async Task GetAllHandler_ReturnsCorrectResults()
    {
        // Arrange
        var mockDbContext = await GenerateMockDataDbContextAsync();
        var mockMapper = GenerateMockMapper();
        var handler = new GetAll.Handler(mockDbContext, mockMapper);

        var command = new ProductGetAllCommand();

        // Act
        var dtos = await handler.Handle(command, CancellationToken.None);

        // Assert
        dtos.Count().Should().Be(_initialProducts.Count);
        dtos.Should().BeEquivalentTo(_initialProducts, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task GetByIdHandler_ReturnsCorrectResult()
    {
        // Arrange
        var mockDbContext = await GenerateMockDataDbContextAsync();
        var mockMapper = GenerateMockMapper();
        var handler = new GetById.Handler(mockDbContext, mockMapper);

        var entityToGet = _initialProducts[Random.Shared.Next(0, _initialProducts.Count - 1)];
        var command = new ProductGetByIdCommand(entityToGet.Id);

        // Act
        var dto = await handler.Handle(command, CancellationToken.None);

        // Assert
        dto.Should().BeEquivalentTo(entityToGet, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task PostHandler_CreatesCorrectly()
    {
        // Arrange
        var mockDbContext = await GenerateMockDataDbContextAsync();
        var mockMapper = GenerateMockMapper();
        var handler = new Post.Handler(mockDbContext, mockMapper);

        var beforeRecordCount = await mockDbContext.Set<Product>().CountAsync();
        var dtoToCreate = _fixture.Create<ProductPostDto>();
        var command = new ProductPostCommand(dtoToCreate);

        // Act
        var createdId = await handler.Handle(command, CancellationToken.None);

        // Assert
        var afterRecordCount = await mockDbContext.Set<Product>().CountAsync();
        afterRecordCount.Should().Be(beforeRecordCount + 1);
        var createdEntity = await mockDbContext.Set<Product>().FindAsync(createdId);
        createdEntity.Should().BeEquivalentTo(dtoToCreate, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task PutHandler_UpdatesCorrectly()
    {
        // Arrange
        var mockDbContext = await GenerateMockDataDbContextAsync();
        var mockMapper = GenerateMockMapper();
        var handler = new Put.Handler(mockDbContext, mockMapper);

        var beforeRecordCount = await mockDbContext.Set<Product>().CountAsync();

        var entityToUpdate = _initialProducts[Random.Shared.Next(0, _initialProducts.Count - 1)];
        var dtoToUpdate = _fixture.Create<ProductPutDto>();
        var command = new ProductPutCommand(entityToUpdate.Id, dtoToUpdate);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var afterRecordCount = await mockDbContext.Set<Product>().CountAsync();
        afterRecordCount.Should().Be(beforeRecordCount);
        var updatedEntity = await mockDbContext.Set<Product>().FindAsync(entityToUpdate.Id);
        updatedEntity.Should().BeEquivalentTo(dtoToUpdate, opt => opt.ExcludingMissingMembers());
    }

    // TODO: Move this so that it is reusable
    private async Task<DataDbContext> GenerateMockDataDbContextAsync([CallerMemberName] string callerMemberName = "")
    {
        var options = new DbContextOptionsBuilder<DataDbContext>()
            .UseInMemoryDatabase(callerMemberName)
            .Options;

        var dataDbContext = new DataDbContext(options);
        dataDbContext.Set<Product>().AddRange(_initialProducts);
        await dataDbContext.SaveChangesAsync();

        return dataDbContext;
    }

    // TODO: Move this so that it is reusable
    private static IMapper GenerateMockMapper()
    {
        var mapperConfiguration = new MapperConfiguration(configurationExpression => configurationExpression.AddProfile<ProductProfile>());

        return mapperConfiguration.CreateMapper();
    }

    // TODO: Move this so that it is reusable
    private List<Product> GenerateMockProducts(int productCount)
    {
        return _fixture.Build<Product>()
            .Without(_ => _.ProductAttachments)
            .Without(_ => _.ShoppingCartItems)
            .CreateMany(productCount)
            .ToList();
    }
}
