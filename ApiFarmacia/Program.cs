//CONTENEDOR DE SERVICIOS O DEPENDENCIAS

using System.Reflection;
using ApiFarmacia.Extencions;
using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using Persistence;


var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()); // para mapear objetos de una clase a otra automáticamente pa los Dtos.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureRateLimit();

/*dotnet ef database update --project ./Persistencia/ --startup-project ./API/
 */

// builder.Services.ConfigureApiVersioning();

builder.Services.AddDbContext<FarmaciaContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConexMysql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseIpRateLimiting();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
