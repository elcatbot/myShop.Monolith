namespace myShop.Api.DTOs;

public class ProductDto()
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ProductImageUrl { get; set; }
    public int BrandId { get; set; }

    public Product ToEntity() =>
        new Product(
            Name = Name!,
            Description = Description!,
            Price = Price,
            ProductImageUrl = ProductImageUrl!,
            BrandId = BrandId
        );

    public ProductDto ToDto(Product entity) =>
        new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            BrandId = entity.BrandId,
            ProductImageUrl = entity.ProductImageUrl
        };

    public IEnumerable<ProductDto> ToListDto(IEnumerable<Product> entityList)
    {
        List<ProductDto> listDtos = new();
        foreach (var entity in entityList)
        {
            listDtos.Add(
                new ProductDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    Price = entity.Price,
                    BrandId = entity.BrandId,
                    ProductImageUrl = entity.ProductImageUrl
                }
            );
        }
        return listDtos;
    }
    
}

