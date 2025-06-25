using myShop.Core.Exceptions;

namespace myShop.Tests.Core;

public class ProductEntityTests
{
    private readonly string _name = "Fake product 1";
    private readonly string _description = "Fake Description 1";
    private readonly int _price = 10;
    private readonly string _imageUrl = "fakeImageUrl 1";
    private readonly int _brandId = 10;

    [Fact]
    public void UpdateProductDetails_ReturnsSuccess()
    {
        // Ararnge
        var nameUpdated = "My product test";
        var descriptionUpdated = "My product Description 1";
        var priceUpdated = 20;
        var imageUrlUpdated = "My product ImageUrl 1";
        var product = new Product(_name, _description, _price, _imageUrl, _brandId);

        // Act
        product.UpdateProductDetails(nameUpdated, descriptionUpdated, priceUpdated, imageUrlUpdated);

        // Assert
        Assert.Equal(nameUpdated, product.Name);
        Assert.Equal(descriptionUpdated, product.Description);
        Assert.Equal(priceUpdated, product.Price);
        Assert.Equal(imageUrlUpdated, product.ProductImageUrl);
    }

    [Fact]
    public void UpdateProductBrandId_ReturnsSuccess()
    {
        // Ararnge
        var brandIdUpdated = 3;
        var product = new Product(_name, _description, _price, _imageUrl, _brandId);

        // Act
        product.UpdateProductBrandId(brandIdUpdated);

        // Assert
        Assert.Equal(brandIdUpdated, product.BrandId);
    }

    [Fact]
    public void SetMaxStockThreshold_PositiveNumber_ReturnsSuccess()
    {
        // Ararnge
        var maxStockThreshold = 35;
        var product = new Product(_name, _description, _price, _imageUrl, _brandId);

        // Act
        product.SetMaxStockThreshold(maxStockThreshold);

        // Assert
        Assert.Equal(maxStockThreshold, product.MaxStockThreshold);
    }

    [Fact]
    public void SetMaxStockThreshold_NegativeNumber_ReturnsException()
    {
        // Ararnge
        var maxStockThreshold = -10;

        // Act
        var product = new Product(_name, _description, _price, _imageUrl, _brandId);

        // Assert
        Assert.Throws<ProductDomainException>(() => product.SetMaxStockThreshold(maxStockThreshold));
    }


    [Theory]
    [InlineData(2, 10)]
    [InlineData(10, 20)]
    [InlineData(57, 100)]
    public void AddStock_WhenIsEmptyAndLessThanMaxStockThreshold_ReturnsSuccess(int quantity, int maxStockThreshold)
    {
        // Ararnge
        var product = new Product(_name, _description, _price, _imageUrl, _brandId);
        product.SetMaxStockThreshold(maxStockThreshold);

        // Act
        product.AddStock(quantity);

        // Assert
        Assert.Equal(quantity, product.AvailableStock);
        Assert.Equal(0, product.OverstockUnits);
    }

    [Theory]
    [InlineData(150, 110)]
    [InlineData(20, 10)]
    [InlineData(90, 45)]
    public void AddStock_WhenAvaialbleStockGreaterThanMaxStockThreshold_ReturnsSuccess(int quantity, int maxStockThreshold)
    {
        // Ararnge
        var product = new Product(_name, _description, _price, _imageUrl, _brandId);
        var overstockUnits = quantity - maxStockThreshold;
        product.SetMaxStockThreshold(maxStockThreshold);

        // Act
        product.AddStock(quantity);

        // Assert
        Assert.Equal(maxStockThreshold, product.AvailableStock);
        Assert.Equal(overstockUnits, product.OverstockUnits);
    }

    [Theory]
    [InlineData(150)]
    [InlineData(10)]
    [InlineData(57)]
    public void RemoveStock_WhenAvaialbleStockIsZero_ReturnsSuccess(int quantity)
    {
        // Ararnge
        var product = new Product(_name, _description, _price, _imageUrl, _brandId);

        // Act
        product.AddStock(0);

        // Assert
        Assert.Throws<ProductDomainException>(() => product.RemoveStock(quantity));
    }
    
    [Theory]
    [InlineData(30, 120)]
    [InlineData(10, 95)]
    [InlineData(57, 280)]
    public void RemoveStock_ReturnsSuccess(int quantity, int availableStock)
    {
        // Ararnge
        var product = new Product(_name, _description, _price, _imageUrl, _brandId);
        product.SetMaxStockThreshold(availableStock);
        product.AddStock(availableStock);

        // Act
        product.RemoveStock(quantity);

        // Assert
        Assert.Equal(availableStock - quantity, product.AvailableStock);
    }
}
