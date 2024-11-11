using AirportWeatherForecastWorkerService.Models;

namespace AirportWeatherForecastWorkerService.Services.Interfaces
{
    public interface IAirportProvider
    {
        Task<List<Airport>> GetAirports();
    }
}
