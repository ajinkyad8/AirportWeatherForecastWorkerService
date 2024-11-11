using AirportWeatherForecastWorkerService.Models;
using AirportWeatherForecastWorkerService.Services.Interfaces;
using System.Buffers.Text;
using System.Buffers;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AirportWeatherForecastWorkerService.Services
{
    public class TAFProvider : ITAFProvider
    {
        public async Task<List<TAF>> GetTAFs(List<Airport> airports)
        {
            var client = new HttpClient();
            var result = new List<TAF>();
            foreach (var airport in airports)
            {
                using var response = await client.GetAsync($"https://aviationweather.gov/api/data/taf?ids={airport.ICAO_CODE}&format=json");
                response.EnsureSuccessStatusCode();
                var tafDataJson = await response.Content.ReadAsStringAsync();
                var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                jsonSerializerOptions.Converters.Add(new DateTimeConverter());
                var tafData = JsonSerializer.Deserialize<List<TAFDTO>>(tafDataJson, jsonSerializerOptions)!;
                result.Add(new TAF(tafData[0], airport.Code));
            }
            return result;
        }
    }

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            return DateTime.ParseExact(reader.GetString() ?? string.Empty, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            Span<byte> utf8Date = new byte[29];

            bool result = Utf8Formatter.TryFormat(value, utf8Date, out _, new StandardFormat('R'));
            Debug.Assert(result);

            writer.WriteStringValue(utf8Date);
        }
    }
}
