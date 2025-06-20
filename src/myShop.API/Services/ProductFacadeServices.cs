using myShop.Api.Controllers;
using myShop.Core.Entites;
using myShop.Core.Interfaces;

namespace myShop.Api.Services;

public class ProductFacadeServices(
    IRepository<Product> repository,
    IProductQueries queries,
    ILogger<ProductController> logger
)
{
    public IRepository<Product> Repository { get; } = repository;
    public IProductQueries Queries { get; } = queries;
    public ILogger<ProductController> Logger { get; } = logger;
}