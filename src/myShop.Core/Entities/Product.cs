namespace myShop.Core.Entites;

public class Product(string Name, string Description, decimal Price, string ProductImageUrl, int BrandId) : BaseEntity, IAggregateRoot
{    
    [Required]
    public string? Name { get; private set; } = Name;

    public string? Description { get; private set; } = Description;
    public decimal Price { get; private set; } = Price;
    public string? ProductImageUrl { get; private set; } = ProductImageUrl;
    public int BrandId { get; private set; } = BrandId;
    public Brand? Brand { get; private set; }
    public int AvailableStock { get; private set; }
    public int MaxStockThreshold { get; private set; }
    public int OverstockUnits { get; private set; }

    public void UpdateProductDetails(string name, string description, decimal price, string? productImageUrl)
    {
        Name = name;
        Description = description;
        Price = price;

        if (!string.IsNullOrEmpty(productImageUrl))
        {
            ProductImageUrl = productImageUrl;
        }
    }

    public void UpdateProductBrandId(int brandId)
    {
        BrandId = brandId;
    }

    public void SetMaxStockThreshold(int maxStockThreshold)
    {
        if (maxStockThreshold < 0)
        {
            throw new ProductDomainException($"MaxStock Threshold must be greater than zero");
        }
        MaxStockThreshold = maxStockThreshold;
    }

    public void AddStock(int quantity)
    {
        int newStock = AvailableStock + quantity;
        if (newStock > MaxStockThreshold)
        {
            AvailableStock = MaxStockThreshold;
            OverstockUnits = newStock - MaxStockThreshold;
        }
        else
        {
            AvailableStock = newStock;
        }
    }

    public void RemoveStock(int quantity)
    {
        if (AvailableStock == 0)
        {
            throw new ProductDomainException($"Empty stock, product {Name} is sold out");
        }
        if (quantity <= 0)
        {
            throw new ProductDomainException($"Quantity must be greater than zero");
        }
        AvailableStock -= quantity;
    }
}