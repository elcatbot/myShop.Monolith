namespace myShop.Core.Interfaces;

public interface IProductFacadeServices
{
    IRepository<Product> Repository { get; }
    IProductQueries Queries { get; }
}