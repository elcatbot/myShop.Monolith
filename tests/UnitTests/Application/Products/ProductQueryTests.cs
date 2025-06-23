namespace myShop.Tests.Application;

public class QueryTests
{
    [Fact]
    public async Task GetProductList_ReturnsSuccess()
    {
        // Arange
        var data = ProductFakeList.GetFakeProductList();
        var total = data.Count();  // 5 Products

        var pageSize = total;
        var pageIndex = 0;
        var expectedItemsInPage = total;

        var mockContext = new Mock<ShopContext>();
        mockContext.Setup(x => x.Products).ReturnsDbSet(data);

        // Act
        var query = new ProductQueries(mockContext.Object);
        var sut = await query.GetProductsAsync(pageSize, pageIndex);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(total, sut.Count());
        Assert.IsType<List<Product>>(sut);
    }

    [Fact]
    public async Task GetProductList__ReturnsEmptyList()
    {
        // Arange
        var mockContext = new Mock<ShopContext>();
        mockContext.Setup(x => x.Products).ReturnsDbSet(new List<Product>());

        // Act
        var query = new ProductQueries(mockContext.Object);
        var sut = await query.GetProductsAsync();

        // Assert
        Assert.Empty(sut);
        Assert.IsType<List<Product>>(sut);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task GetProductById_ReturnsSuccess(int id)
    {
        // Arange
        var data = new List<Product>() { new Product("Fake aProduct 1", "Fake Description 1", 11, "fakeImageUrl 1", 1) };
        data[0].Id = id;

        var mockContext = new Mock<ShopContext>();
        mockContext.Setup(x => x.Products).ReturnsDbSet(data);

        // Act
        var query = new ProductQueries(mockContext.Object);
        var sut = await query.GetProductByIdAsync(id);

        // Assert
        Assert.NotNull(sut);
        Assert.IsType<Product>(sut);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetProductByBrandId_ReturnsSuccess(int brandId)
    {
        // Arange
        var data = ProductFakeList.GetFakeProductList();

        var pageSize = 10;
        var pageIndex = 0;
        var expectedItemsInPage = 2;

        var mockContext = new Mock<ShopContext>();
        mockContext.Setup(x => x.Products).ReturnsDbSet(data);

        // Act
        var query = new ProductQueries(mockContext.Object);
        var sut = await query.GetProductsByBrandIdAsync(brandId, pageSize, pageIndex);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(expectedItemsInPage, sut.Count());
        Assert.IsType<List<Product>>(sut);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(300)]
    public async Task GetProductByBrandId_ReturnsEmptyList(int id)
    {
        // Arange
        var data = ProductFakeList.GetFakeProductList();

        var pageSize = 10;
        var pageIndex = 0;

        var mockContext = new Mock<ShopContext>();
        mockContext.Setup(x => x.Products).ReturnsDbSet(data);

        // Act
        var query = new ProductQueries(mockContext.Object);
        var sut = await query.GetProductsByBrandIdAsync(id, pageSize, pageIndex);

        // Assert
        Assert.Empty(sut);
        Assert.IsType<List<Product>>(sut);
    }

    [Theory]
    [InlineData("Fake")]
    public async Task GetProductsListByName_ReturnsSuccess(string name)
    {
        // Arange
        var data = ProductFakeList.GetFakeProductList();
        var total = data.Count();  // 5 Products

        var pageSize = 10;
        var pageIndex = 0;
        var expectedItemsInPage = 3;

        var mockContext = new Mock<ShopContext>();
        mockContext.Setup(x => x.Products).ReturnsDbSet(data);

        // Act
        var query = new ProductQueries(mockContext.Object);
        var sut = await query.GetProductsByNameAsync(name, pageSize, pageIndex);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(expectedItemsInPage, sut.Count());
        Assert.IsType<List<Product>>(sut);
    }
}
