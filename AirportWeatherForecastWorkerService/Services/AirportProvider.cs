using AirportWeatherForecastWorkerService.Models;
using AirportWeatherForecastWorkerService.Services.Interfaces;
using System.Text.Json;

namespace AirportWeatherForecastWorkerService.Services
{
    public class AirportProvider : IAirportProvider
    {
        public async Task<List<Airport>> GetAirports()
        {
            var filePath = Path.Join(Directory.GetCurrentDirectory(), "airports.json");
            using var stream = File.OpenRead(filePath);
            return (await JsonSerializer.DeserializeAsync<List<Airport>>(stream))!;
        }
    }
}
