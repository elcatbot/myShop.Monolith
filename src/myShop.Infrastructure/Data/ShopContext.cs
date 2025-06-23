namespace myShop.Infrastructure.Data;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions options) : base(options) { }
    public ShopContext() { }

    public virtual DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}