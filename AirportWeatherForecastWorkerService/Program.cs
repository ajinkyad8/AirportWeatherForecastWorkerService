using AirportWeatherForecastWorkerService;
using AirportWeatherForecastWorkerService.Models;
using AirportWeatherForecastWorkerService.Services;
using AirportWeatherForecastWorkerService.Services.Interfaces;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddDbContextFactory<TAFContext>();
builder.Services.AddTransient<TAFDatabaseStorage>();
builder.Services.AddTransient<TAFFileStorage>();
builder.Services.AddTransient<TAFStorageProvider>();
builder.Services.AddTransient<ITAFProvider, TAFProvider>();
builder.Services.AddTransient<IAirportProvider, AirportProvider>();
builder.Services.Configure<TAFStorageOptions>(builder.Configuration.GetSection(TAFStorageOptions.TAFStorage));

var host = builder.Build();
host.Run();
