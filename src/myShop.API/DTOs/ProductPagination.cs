namespace myShop.Api.DTOs;

public class ProductPagination(int pageIndex, int pageSize, long count, IEnumerable<ProductDto> products)
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long Count { get; } = count;
    public IEnumerable<ProductDto> Products { get; set; } = products;
}