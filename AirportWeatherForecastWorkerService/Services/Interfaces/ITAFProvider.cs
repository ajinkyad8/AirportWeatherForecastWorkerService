using AirportWeatherForecastWorkerService.Models;

namespace AirportWeatherForecastWorkerService.Services.Interfaces
{
    public interface ITAFProvider
    {
        Task<List<TAF>> GetTAFs(List<Airport> airports);
    }
}
