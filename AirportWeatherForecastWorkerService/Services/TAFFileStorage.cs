using AirportWeatherForecastWorkerService.Models;
using AirportWeatherForecastWorkerService.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text.Json;

namespace AirportWeatherForecastWorkerService.Services
{
    public class TAFFileStorage : ITAFStorage
    {
        private TAFStorageOptions _tafStorageOptions;

        public TAFFileStorage(IOptions<TAFStorageOptions> options)
        {
            _tafStorageOptions = options.Value;
        }
        public async Task<bool> StoreTAFs(List<TAF> tAFs)
        {
            foreach (var tAF in tAFs)
            {
                var path = GetFilePath(tAF);
                await File.WriteAllTextAsync(path, JsonSerializer.Serialize(tAF));
            }
            return true;
        }

        private string GetFilePath(TAF tAF)
        {
            var basePath = Directory.GetCurrentDirectory();
            var folderPath = Path.Join(basePath, _tafStorageOptions.FolderName);
            var epochTime = (new DateTimeOffset(tAF.IssueTime)).ToUnixTimeSeconds();
            return Path.Join(folderPath, $"{tAF.AirportCode}_{epochTime}.json");
        }
    }
}
