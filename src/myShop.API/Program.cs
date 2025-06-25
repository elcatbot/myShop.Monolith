var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddCustomDI();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v3/swagger.json", "v3"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
