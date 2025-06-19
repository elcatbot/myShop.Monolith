namespace myShop.Infrastructure.Data;

public class BaseRepository<T>(ShopContext context) : IRepository<T> where T : BaseEntity, IAggregateRoot
{
    public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();
    
    public async Task<T> GetByIdAsync(int id) => (await context.Set<T>().FindAsync(id))!;
    
    public async Task AddAsync(T entity)
    {
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        context.Update<T>(entity);
        await context.SaveChangesAsync();
    }
}