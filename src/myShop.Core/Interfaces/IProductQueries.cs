namespace myShop.Core.Interfaces;

public interface IProductQueries 
{
    Task<IEnumerable<Product>> GetProductsAsync(int pageSize = 10, int pageIndex = 0);
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name, int pageSize = 10, int pageIndex = 0);
    Task<IEnumerable<Product>> GetProductsByBrandIdAsync(int brandId, int pageSize = 10, int pageIndex = 0);
    Task<Product> GetProductByIdAsync(int id);
}