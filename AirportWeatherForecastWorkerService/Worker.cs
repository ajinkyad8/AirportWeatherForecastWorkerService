using AirportWeatherForecastWorkerService.Services;
using AirportWeatherForecastWorkerService.Services.Interfaces;

namespace AirportWeatherForecastWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ITAFProvider _tAFProvider;
        private readonly TAFStorageProvider _tAFStorageProvider;
        private readonly IAirportProvider _airportProvider;
        private readonly int _frequency;

        public Worker(ITAFProvider tAFProvider, TAFStorageProvider tAFStorageProvider,
             IAirportProvider airportProvider,
             IConfiguration configuration)
        {
            _tAFProvider = tAFProvider;
            _tAFStorageProvider = tAFStorageProvider;
            _airportProvider = airportProvider;
            _frequency = configuration.GetValue<int>("Frequency");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var airports = await _airportProvider.GetAirports();
                var tafData = await _tAFProvider.GetTAFs(airports);
                var storageProvider = _tAFStorageProvider.GetTAFStorage();
                await storageProvider.StoreTAFs(tafData);
                await Task.Delay(_frequency, stoppingToken);
            }
        }
    }
}
