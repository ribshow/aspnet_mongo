using aspnet_mongo.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using aspnet_mongo.Data;
using NuGet.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<aspnet_mongoContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("aspnet_mongoContext") ?? throw new InvalidOperationException("Connection string 'aspnet_mongoContext' not found.")));

ContextMongoDb.ConnectionString = builder.Configuration.GetSection("MongoConnection:ConnectionString").Value;
ContextMongoDb.DatabaseName = builder.Configuration.GetSection("MongoConnection:Database").Value;

ContextMongoDb.IsSSL = Convert.ToBoolean(builder.Configuration.GetSection("MongoConnection:IsSSL").Value);

// Configurar as opções de conexão
builder.Services.Configure<Settings>(builder.Configuration.GetSection("ConnectionStrings"));

// Registrar o contexto MongoDB
builder.Services.AddSingleton<ContextMongoDb>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
