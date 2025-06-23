namespace myShop.Infrastructure.Data;

public class BaseRepository<T>(ShopContext context) : IRepository<T> where T : BaseEntity, IAggregateRoot
{
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
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }
}