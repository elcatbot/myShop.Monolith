namespace myShop.Infrastructure.Data.Queries;

public class ProductQueries(ShopContext context) : IProductQueries
{
    public async Task<Product> GetProductByIdAsync(int id) => (await context.Products.FindAsync(id))!;

    public async Task<IEnumerable<Product>> GetProductsAsync(int pageSize = 10, int pageIndex = 0)
    {
        IQueryable<Product> query = GetPaginatedQuery(pageSize , pageIndex);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name, int pageSize = 10, int pageIndex = 0)
    {
        IQueryable<Product> query = GetPaginatedQuery(pageSize , pageIndex);
        query.Where(p => p.Name!.Contains(name));

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByBrandAsync(int brandId, int pageSize = 10, int pageIndex = 0)
    {
        IQueryable<Product> query = GetPaginatedQuery(pageSize , pageIndex);
        query.Where(p => p.BrandId == brandId);

        return await query.ToListAsync();
    }

    private IQueryable<Product> GetPaginatedQuery(int pageSize = 10, int pageIndex = 0)
    {
        IQueryable<Product> query =
            context.Products
                .OrderBy(p => p.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);

        return query;
    }
}
