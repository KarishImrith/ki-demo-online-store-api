using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;
using OnlineStore.Logic.Concerns.ProductConcern;
using OnlineStore.Logic.Concerns.ProductConcern.GetAll;
using OnlineStore.Logic.Concerns.ProductConcern.GetById;
using OnlineStore.Logic.Concerns.ProductConcern.Post;
using OnlineStore.Logic.Concerns.ProductConcern.Put;
using OnlineStore.Logic.UnitTests.Abstractions;
using OnlineStore.Logic.UnitTests.Helpers;

using GetAll = OnlineStore.Logic.Concerns.ProductConcern.GetAll;
using GetById = OnlineStore.Logic.Concerns.ProductConcern.GetById;
using Post = OnlineStore.Logic.Concerns.ProductConcern.Post;
using Put = OnlineStore.Logic.Concerns.ProductConcern.Put;

namespace OnlineStore.Logic.UnitTests.Concerns;

public class ProductConcernTests : BaseTests<ProductConcernTests, ProductProfile>
{
    private IList<Product> _initialProducts;

    [Fact]
    public async Task GetAllHandler_ReturnsCorrectResults()
    {
        // Arrange
        var mockDbContext = await GenerateMockDataDbContextAsync();
        var handler = new GetAll.Handler(mockDbContext, _mockMapper);

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
        var handler = new GetById.Handler(mockDbContext, _mockMapper);

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
        var handler = new Post.Handler(mockDbContext, _mockMapper);

        var dtoToCreate = _fixture.Create<ProductPostDto>();
        var command = new ProductPostCommand(dtoToCreate);

        // Act
        var createdId = await handler.Handle(command, CancellationToken.None);

        // Assert
        var afterRecordCount = await mockDbContext.Set<Product>().CountAsync();
        afterRecordCount.Should().Be(_initialProducts.Count + 1);
        var createdEntity = await mockDbContext.Set<Product>().FindAsync(createdId);
        createdEntity.Should().BeEquivalentTo(dtoToCreate, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task PutHandler_UpdatesCorrectly()
    {
        // Arrange
        var mockDbContext = await GenerateMockDataDbContextAsync();
        var handler = new Put.Handler(mockDbContext, _mockMapper);

        var entityToUpdate = _initialProducts[Random.Shared.Next(0, _initialProducts.Count - 1)];
        var dtoToUpdate = _fixture.Create<ProductPutDto>();
        var command = new ProductPutCommand(entityToUpdate.Id, dtoToUpdate);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var afterRecordCount = await mockDbContext.Set<Product>().CountAsync();
        afterRecordCount.Should().Be(_initialProducts.Count);
        var updatedEntity = await mockDbContext.Set<Product>().FindAsync(entityToUpdate.Id);
        updatedEntity.Should().BeEquivalentTo(dtoToUpdate, opt => opt.ExcludingMissingMembers());
    }

    protected override async Task<DataDbContext> InitializeDatabaseAsync(DataDbContext dataDbContext)
    {
        _initialProducts = _fixture.GenerateProductEntities(5);
        await dataDbContext.Set<Product>().AddRangeAsync(_initialProducts);
        await dataDbContext.SaveChangesAsync();

        return dataDbContext;
    }
}
