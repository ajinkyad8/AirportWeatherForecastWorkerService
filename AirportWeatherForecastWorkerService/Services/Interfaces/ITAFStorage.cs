using AirportWeatherForecastWorkerService.Models;

namespace AirportWeatherForecastWorkerService.Services.Interfaces
{
    public interface ITAFStorage
    {
        Task<bool> StoreTAFs(List<TAF> tAFs);
    }
}
