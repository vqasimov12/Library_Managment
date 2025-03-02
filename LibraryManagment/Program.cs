using Application;
using DAL.SqlServer;
using Microsoft.OpenApi.Models;
using RestaurantManagement.Middlewares;
using RestaurantManagment.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServerServices(conn);
builder.Services.AddApplicationServices();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();
// app.UseMiddleware<RateLimitMiddleware>(2, TimeSpan.FromMinutes(1));

app.MapControllers();

app.Run();
