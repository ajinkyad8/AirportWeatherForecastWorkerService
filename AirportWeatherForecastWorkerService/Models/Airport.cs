namespace AirportWeatherForecastWorkerService.Models
{
    public class Airport
    {
        public int Code { get; set; }
        public required string ICAO_CODE { get; set; }
        public required string IATA_CODE { get; set; }
    }
}
