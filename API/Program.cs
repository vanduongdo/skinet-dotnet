using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Scoped = one instance per request, Transient = one instance per lifetime, effectively scoped to the method level and Singleton = one instance per application
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope(); // once this has finished executing, the scope will be disposed, closing the connection.
    // create services by using the using the statement when it's outside the scope
    // The framwork is going to tidy things up and dispose of the scope when the request is complete.
    var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();
