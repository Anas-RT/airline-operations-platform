using AirlineOperations.Api.Interfaces.IRepositories;
using AirlineOperations.Api.Interfaces.IServices;
using AirlineOperations.Api.Repositories;
using AirlineOperations.Api.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Database connection string missing.");

builder.Services.AddSingleton(
    NpgsqlDataSource.Create(connectionString)
);
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddScoped<INetworkOverviewRepository,NetworkOverviewRepository>();
builder.Services.AddScoped<INetworkOverviewService,NetworkOverviewService>();
builder.Services.AddScoped<IAirlinePerformanceRepository, AirlinePerformanceRepository>();
builder.Services.AddScoped<IAirlinePerformanceService, AirlinePerformanceService>();
builder.Services.AddScoped<IDelaySeverityRepository, DelaySeverityRepository>();
builder.Services.AddScoped<IDelaySeverityService, DelaySeverityService>();
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("Frontend");
app.UseAuthorization();


app.MapControllers();

app.Run();
