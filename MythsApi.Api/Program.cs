using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MythsApi.Application.Interfaces;
using MythsApi.Infrastructure.Data;
using MythsApi.Infrastructure.Mapping;
using MythsApi.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// EF Core - Add DbContext with SQL Server
builder.Services.AddDbContext<MythsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var loggerFactory = builder.Services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
////  AutoMapper - Register Mapping Profiles
//var mapperConfig = new MapperConfiguration(cfg =>
//{
//    //cfg.AddProfile<MythMapperProfile>();  
//    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());

//}, loggerFactory);
////builder.Services.AddAutoMapper(typeof(MythMapperProfile));

//IMapper mapper = mapperConfig.CreateMapper();
//builder.Services.AddSingleton(mapper); // register with DI
builder.Services.AddAutoMapper(cfg =>
{
    //cfg.AddProfile<MythMapperProfile>();  
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});
 

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Register Services for Dependency Injection
builder.Services.AddScoped<IMythService, MythService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  //  app.MapOpenApi();
    //// Serve the generated Swagger JSON and Swagger UI
    app.UseSwagger(); // Serves /swagger/v1/swagger.json
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = "swagger"; // Access UI at /swagger
    });
}
 
var summaries = new[]
{
    "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

 

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
app.Run();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}