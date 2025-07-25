
using Microsoft.EntityFrameworkCore;
using MythsApi.Api.Endpoints;
using MythsApi.Api.Migrate;
using MythsApi.Application.Interfaces;
using MythsApi.Infrastructure.Data;
using MythsApi.Infrastructure.Mapping;
using MythsApi.Infrastructure.Repositories;
using MythsApi.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
bool useInSQLDb = builder.Configuration.GetValue<bool>("UseInSQLDb");

// EF Core - Add DbContext with SQL Server
if (useInSQLDb)
{
    builder.Services.AddDbContext<MythsDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
    builder.Services.AddScoped<IMythRepository, MythRepository>();
}
else
{
    builder.Services.AddDbContext<MythsDbContext>(options =>
        options.UseInMemoryDatabase("MythsInMemoryDb"));
    builder.Services.AddScoped<IMythRepository, InMemoryMythRepository>();
}


//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MythMapperProfile>();
    cfg.LicenseKey = builder.Configuration["Automapper:LicenseKey"];
});

// Register Services for Dependency Injection

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
app.Services.InitializeAndSeedDBIfNotFound(useInSQLDb);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //// Serve the generated Swagger JSON and Swagger UI
    app.UseSwagger();  
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Myths API V1");
        options.RoutePrefix = "swagger"; // Access UI at /swagger
    });
}

app.UseHttpsRedirection();
app.MapMythEndpoints();

//app.UseAuthorization();
//app.MapControllers();
app.Run();
