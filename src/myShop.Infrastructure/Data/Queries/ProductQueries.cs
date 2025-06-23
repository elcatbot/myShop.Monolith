using System.Linq.Expressions;

namespace myShop.Infrastructure.Data.Queries;

public class ProductQueries(ShopContext context) : IProductQueries
{
    IQueryable<Product>? _query;

    public async Task<Product> GetProductByIdAsync(int id) => (await context.Products.FirstOrDefaultAsync(p => p.Id.Equals(id)))!;

    public async Task<IEnumerable<Product>> GetProductsAsync(int pageSize = 10, int pageIndex = 0)
    {
        _query = context.Products;
        _query = GetPaginated_query( _query, pageSize, pageIndex);
        return await _query.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name, int pageSize = 10, int pageIndex = 0)
    {
        _query = context.Products.Where(p => p.Name!.ToLower()!.Contains(name.ToLower()));
        _query = GetPaginated_query( _query, pageSize, pageIndex);
        return await _query.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandIdAsync(int brandId, int pageSize = 10, int pageIndex = 0)
    {
        _query = context.Products.Where(p => p.BrandId == brandId);
        _query = GetPaginated_query(_query, pageSize, pageIndex);
        return await _query.ToListAsync();
    }

    private IQueryable<Product> GetPaginated_query(IQueryable<Product> _query, int pageSize = 10, int pageIndex = 0) =>
        _query
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize);
}
