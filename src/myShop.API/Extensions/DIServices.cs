namespace myShop.Api.Extensions;

public static class DIServices
{
    public static void AddCustomDI(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDataBases(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddOpenApiServices();
    }

    private static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductFacadeServices, ProductFacadeServices>();
        services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddTransient<IProductQueries, ProductQueries>();
    }

    private static void AddDataBases(this IServiceCollection services, IConfiguration configuration)
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
    }

    private static void AddOpenApiServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v3", new OpenApiInfo
            {
                Version = "v3",
                Title = "myShop API",
                Description = "An ASP.NET Core controller Api to manage Product, Basket, Ordering and Payment",
                TermsOfService = new Uri("http://localhost/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Viajero",
                },
                License = new OpenApiLicense
                {
                    Name = "License API Usage",
                }
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}

