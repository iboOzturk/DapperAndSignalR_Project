using DapperAndSignalR_Project.API.Hubs;
using DapperAndSignalR_Project.API.Models;
using DapperAndSignalR_Project.API.Repositories;
using DapperAndSignalR_Project.API.Repositories.LoginRepositories;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:44308/").AllowAnyHeader().
        AllowAnyMethod().AllowCredentials().SetIsOriginAllowed((host) => true);
    });
});

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
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();
app.MapHub<ProductHub>("/ProductHub");
app.Run();
