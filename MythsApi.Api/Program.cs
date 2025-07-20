 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MythsApi.Api.Migrate;
using MythsApi.Application.Interfaces;
using MythsApi.Infrastructure.Data;
using MythsApi.Infrastructure.Mapping;
using MythsApi.Infrastructure.Repositories;
using MythsApi.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// EF Core - Add DbContext with SQL Server
builder.Services.AddDbContext<MythsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
 


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

builder.Services.AddAutoMapper(cfg =>
{
  cfg.AddProfile<MythMapperProfile>();  
    cfg.LicenseKey = builder.Configuration["Automapper:LicenseKey"];
});

// Register Services for Dependency Injection
builder.Services.AddScoped<IMythRepository, MythRepository>();
builder.Services.AddScoped<IMythService, MythService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
var app = builder.Build();

// Initialize the database (migrate and seed)
app.Services.InitializeAndSeedDBIfNotFound();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() )
{ 
    //// Serve the generated Swagger JSON and Swagger UI
    app.UseSwagger(); // Serves /swagger/v1/swagger.json
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = "swagger"; // Access UI at /swagger
    });
}
  
app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
app.Run();
 