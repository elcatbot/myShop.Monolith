namespace myShop.Core.Interfaces;

public interface IRepository<T>  where T : IAggregateRoot
{
    Task AddAsync(T entity);
    Task UpdateAsync(T tentity);
    Task DeleteAsync(T entity);
}