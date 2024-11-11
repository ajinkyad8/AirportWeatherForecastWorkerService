using System.Text.Json.Serialization;

namespace AirportWeatherForecastWorkerService.Models
{
    public class TAF
    {
        public int AirportCode { get; set; }
        public DateTime IssueTime { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string RawTAF { get; set; }

        public TAF()
        {
        }

        public TAF(TAFDTO data, int airportCode)
        {
            this.AirportCode = airportCode;
            this.IssueTime = data.IssueTime;
            this.ValidFrom = DateTime.UnixEpoch.AddSeconds(data.validTimeFrom);
            this.ValidTo = DateTime.UnixEpoch.AddSeconds(data.validTimeTo);
            this.RawTAF = data.RawTAF;
        }
    }
}
