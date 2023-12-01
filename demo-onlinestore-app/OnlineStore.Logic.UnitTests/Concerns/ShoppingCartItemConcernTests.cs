using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetAll;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetById;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Post;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Put;
using OnlineStore.Logic.UnitTests.Abstractions;
using OnlineStore.Logic.UnitTests.Helpers;

using GetAll = OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetAll;
using GetById = OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetById;
using Post = OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Post;
using Put = OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Put;

namespace OnlineStore.Logic.UnitTests.Concerns;

public class ShoppingCartItemConcernTests : BaseTests<ShoppingCartItemConcernTests, ShoppingCartItemProfile>
{
    private Product _initialProduct;
    private IList<ShoppingCartItem> _initialShoppingCartItems;

    [Fact]
    public async Task GetAllHandler_ReturnsCorrectResults()
    {
        // Arrange
        var mockDbContext = await GenerateMockDataDbContextAsync();
        var handler = new GetAll.Handler(mockDbContext, _mockMapper);

        var command = new ShoppingCartItemGetAllCommand(_initialUser.Id);

        // Act
        var dtos = await (await handler.Handle(command, CancellationToken.None)).ToListAsync();

        // Assert
        dtos.Count.Should().Be(_initialShoppingCartItems.Count);
        dtos.Should().BeEquivalentTo(_initialShoppingCartItems, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task GetByIdHandler_ReturnsCorrectResult()
    {
        // Arrange
        var mockDbContext = await GenerateMockDataDbContextAsync();
        var handler = new GetById.Handler(mockDbContext, _mockMapper);

        var entityToGet = _initialShoppingCartItems[Random.Shared.Next(0, _initialShoppingCartItems.Count - 1)];
        var command = new ShoppingCartItemGetByIdCommand(entityToGet.Id, _initialUser.Id);

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

        var dtoToCreate = _fixture.Create<ShoppingCartItemPostDto>();
        dtoToCreate.ProductId = _initialProduct.Id;
        var command = new ShoppingCartItemPostCommand(_initialUser.Id, dtoToCreate);

        // Act
        var createdId = await handler.Handle(command, CancellationToken.None);

        // Assert
        var afterRecordCount = await mockDbContext.Set<ShoppingCartItem>().CountAsync();
        afterRecordCount.Should().Be(_initialShoppingCartItems.Count + 1);
        var createdEntity = await mockDbContext.Set<ShoppingCartItem>().FindAsync(createdId);
        createdEntity.Should().BeEquivalentTo(dtoToCreate, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task PutHandler_UpdatesCorrectly()
    {
        // Arrange
        var mockDbContext = await GenerateMockDataDbContextAsync();
        var handler = new Put.Handler(mockDbContext, _mockMapper);

        var entityToUpdate = _initialShoppingCartItems[Random.Shared.Next(0, _initialShoppingCartItems.Count - 1)];
        var dtoToUpdate = _fixture.Create<ShoppingCartItemPutDto>();
        dtoToUpdate.ProductId = _initialProduct.Id;
        var command = new ShoppingCartItemPutCommand(entityToUpdate.Id, _initialUser.Id, dtoToUpdate);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var afterRecordCount = await mockDbContext.Set<ShoppingCartItem>().CountAsync();
        afterRecordCount.Should().Be(_initialShoppingCartItems.Count);
        var updatedEntity = await mockDbContext.Set<ShoppingCartItem>().FindAsync(entityToUpdate.Id);
        updatedEntity.Should().BeEquivalentTo(dtoToUpdate, opt => opt.ExcludingMissingMembers());
    }

    protected override async Task<DataDbContext> InitializeDatabaseAsync(DataDbContext dataDbContext)
    {
        _initialProduct = _fixture.GenerateProductEntities(1)[0];
        await dataDbContext.Set<Product>().AddAsync(_initialProduct);
        await dataDbContext.SaveChangesAsync();

        _initialShoppingCartItems = _fixture.GenerateShoppingCartItemEntities(5, _initialUser.Id, _initialProduct.Id);
        await dataDbContext.Set<ShoppingCartItem>().AddRangeAsync(_initialShoppingCartItems);
        await dataDbContext.SaveChangesAsync();

        return dataDbContext;
    }
}
