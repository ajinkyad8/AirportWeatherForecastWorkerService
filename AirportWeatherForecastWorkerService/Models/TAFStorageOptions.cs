using AirportWeatherForecastWorkerService.Enums;

namespace AirportWeatherForecastWorkerService.Models
{
    public class TAFStorageOptions
    {
        public const string TAFStorage = "TAFStorage";
        public TAFStores store { get; set; }
        public required string DatabaseName { get; set; }
        public required string FolderName { get; set; }
    }
}
