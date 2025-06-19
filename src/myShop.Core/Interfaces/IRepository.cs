namespace myShop.Core.Interfaces;

public interface IRepository<T>  where T : IAggregateRoot
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T tentity);
    Task DeleteAsync(T entity);
}