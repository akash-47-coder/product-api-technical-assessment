using Moq;
using Xunit;
using ProductApi.Application.Interfaces;
using ProductApi.Application.Services;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Tests;

public class ProductServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ProductExists_ReturnsProduct()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            ProductName = "Laptop",
            CreatedBy = "admin",
            CreatedOn = DateTime.UtcNow
        };

        var repository = new Mock<IProductRepository>();

        repository
            .Setup(x => x.GetByIdAsync(1))
            .Returns(Task.FromResult<Product?>(product));

        var service = new ProductService(repository.Object);

        // Act
        var result = await service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Laptop", result.ProductName);
        Assert.Equal("admin", result.CreatedBy);

        repository.Verify(
            x => x.GetByIdAsync(1));
    }

    [Fact]
    public async Task GetByIdAsync_ProductDoesNotExist_ReturnsNull()
    {
        // Arrange
        var repository = new Mock<IProductRepository>();

        repository
            .Setup(x => x.GetByIdAsync(1))
            .Returns(Task.FromResult<Product?>(null));

        var service = new ProductService(repository.Object);

        // Act
        var result = await service.GetByIdAsync(1);

        // Assert
        Assert.Null(result);

        repository.Verify(
            x => x.GetByIdAsync(1));
    }
}