using Core.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;//Lấy DI container nội bộ của scope này để truy xuất các service bên trong.
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();//Thực hiện apply tất cả các migration vào database.
    await StoreContextSeed.SeedAsync(context);//SeedAsync để chèn dữ liệu mẫu vào database 
}
catch (System.Exception)
{
    Console.WriteLine();
    throw;
}

app.Run();
