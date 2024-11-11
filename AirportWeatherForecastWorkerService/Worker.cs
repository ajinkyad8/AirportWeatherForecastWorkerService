using AirportWeatherForecastWorkerService.Services;
using AirportWeatherForecastWorkerService.Services.Interfaces;

namespace AirportWeatherForecastWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ITAFProvider _tAFProvider;
        private readonly TAFStorageProvider _tAFStorageProvider;
        private readonly IAirportProvider _airportProvider;
        private readonly ILogger<Worker> _logger;
        private readonly int _frequency;

        public Worker(ITAFProvider tAFProvider, TAFStorageProvider tAFStorageProvider,
             IAirportProvider airportProvider,
             IConfiguration configuration,
             ILogger<Worker> logger)
        {
            _tAFProvider = tAFProvider;
            _tAFStorageProvider = tAFStorageProvider;
            _airportProvider = airportProvider;
            _logger = logger;
            _frequency = configuration.GetValue<int>("Frequency");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var airports = await _airportProvider.GetAirports();
                    var tafData = await _tAFProvider.GetTAFs(airports);
                    var storageProvider = _tAFStorageProvider.GetTAFStorage();
                    await storageProvider.StoreTAFs(tafData);
                    await Task.Delay(_frequency, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error while trying to save TAF data. Message: {ex.Message}. Stacktrace: {ex.StackTrace}.");
                }
            }
        }
    }
}
