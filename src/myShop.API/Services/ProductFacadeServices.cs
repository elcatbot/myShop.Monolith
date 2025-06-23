namespace myShop.Api.Services;

public class ProductFacadeServices(IRepository<Product> repository, IProductQueries queries) : IProductFacadeServices
{
    public IRepository<Product> Repository { get; } = repository;
    public IProductQueries Queries { get; } = queries;
}