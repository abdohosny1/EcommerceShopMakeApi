using EcommerceShop.Core.Repository;
using EcommerceShop.EF;
using EcommerceShop.EF.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add dbcontext
var connection = builder.Configuration.GetConnectionString("DefultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(
    op => op.UseSqlServer(connection,
           b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

//add service
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
