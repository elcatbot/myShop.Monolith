using myShop.Infrastructure.Data.Queries;

namespace myShop.Api.Extensions;

public static class DIServices
{
    public static void AddCustomDI(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDataBases(builder.Configuration);

        builder.Services.AddScoped<IProductFacadeServices, ProductFacadeServices>();

    }

    public static void AddDataBases(this IServiceCollection services, IConfiguration configuration)
    {
        switch (configuration["DataStore"])
        {
            case "SqlServer":
                services.AddDbContext<ShopContext>(o =>
                {
                    o.UseSqlServer(configuration["ConnectionString"], options =>
                        options.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)
                    );
                    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });
                break;
            default:
                services.AddDbContext<ShopContext>(o => o.UseInMemoryDatabase("ProductDB"));
                break;
        }
        
        services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddTransient<IProductQueries, ProductQueries>();
    }
}

