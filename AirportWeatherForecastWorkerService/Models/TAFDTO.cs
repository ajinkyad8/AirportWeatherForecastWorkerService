namespace AirportWeatherForecastWorkerService.Models
{
    public class TAFDTO
    {
        public required string icaoId { get; set; }
        public DateTime IssueTime { get; set; }
        public int validTimeFrom { get; set; }
        public int validTimeTo { get; set; }
        public required string RawTAF { get; set; }
    }
}
