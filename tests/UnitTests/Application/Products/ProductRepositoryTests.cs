using System.Data.SqlTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace myShop.Tests.Application;

public class ProductRepositoryTests
{
    [Fact]
    public async Task AddProduct_ReturnsSuccess()
    {
        // Arange
        var product = new Product("Fake product 1", "Fake Description 1", 11, "fakeImageUrl 1", 1);
        var mockSet = new Mock<DbSet<Product>>();
        var mockContext = new Mock<ShopContext>();
        mockContext.Setup(p => p.Set<Product>()).Returns(mockSet.Object);

        // Act
        var repository = new BaseRepository<Product>(mockContext.Object);
        await repository.AddAsync(product);

        // Assert
        mockSet.Verify(p => p.Add(product), Times.Once());
        mockContext.Verify(m => m.SaveChangesAsync(CancellationToken.None).Result, Times.Once);
    }

    [Fact]
    public async Task AddProduct_WhenNameIsNull_ReturnsDbUpdateException()
    {
        // Arange
        var product = new Product(null!, "Fake Description 1", 11, "fakeImageUrl 1", 1);
        var mockSet = new Mock<DbSet<Product>>();
        var mockContext = new Mock<ShopContext>();
        mockContext.Setup(p => p.Set<Product>().Add(product)).Throws(new SqlNullValueException());

        // // Act
        var repository = new BaseRepository<Product>(mockContext.Object);
        var sut = repository.AddAsync(product);

        // Assert
        await Assert.ThrowsAsync<SqlNullValueException>(() => sut);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsSuccess()
    {
        // Arange
        var product = new Product("Fake product 1", "Fake Description 1", 11, "fakeImageUrl 1", 1);
        product.Id = 1;
        var mockSet = new Mock<DbSet<Product>>();
        var mockContext = new Mock<ShopContext>();
        mockContext.Setup(p => p.Set<Product>()).Returns(mockSet.Object);

        // // Act
        var repository = new BaseRepository<Product>(mockContext.Object);
        await repository.UpdateAsync(product);

        // Assert
        mockSet.Verify(p => p.Update(product), Times.Once());
        mockContext.Verify(m => m.SaveChangesAsync(CancellationToken.None).Result, Times.Once);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsSuccess()
    {
        // Arange
        var product = new Product("Fake product 1", "Fake Description 1", 11, "fakeImageUrl 1", 1);
        product.Id = 111;
        var data = ProductFakeList.GetFakeProductList();
        var mockSet = new Mock<DbSet<Product>>();
        var mockContext = new Mock<ShopContext>();
        mockContext.Setup(x => x.Set<Product>()).ReturnsDbSet(data, mockSet);

        // // Act
        var repository = new BaseRepository<Product>(mockContext.Object);
        await repository.DeleteAsync(product);

        // Assert
        mockSet.Verify(p => p.Remove(product), Times.Once());
        mockContext.Verify(m => m.SaveChangesAsync(CancellationToken.None).Result, Times.Once);
    }


}
