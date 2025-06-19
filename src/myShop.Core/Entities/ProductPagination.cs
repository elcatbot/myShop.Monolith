namespace myShop.Core.Entites;

public class ProductPagination(int pageIndex, int pageSize, long count, IEnumerable<Product> products)
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long Count { get; } = count;
    public IEnumerable<Product> Products { get; set; } = products;
}