using AirportWeatherForecastWorkerService.Enums;
using AirportWeatherForecastWorkerService.Models;
using AirportWeatherForecastWorkerService.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace AirportWeatherForecastWorkerService.Services
{
    public class TAFStorageProvider
    {
        private readonly TAFDatabaseStorage _tAFDatabaseStorer;
        private readonly TAFFileStorage _tAFFileStorer;
        private readonly TAFStorageOptions _tafStorageOptions;

        public TAFStorageProvider(TAFDatabaseStorage tAFDatabaseStorer, TAFFileStorage tAFFileStorer, IOptions<TAFStorageOptions> tAFStorageOptions)
        {
            _tAFDatabaseStorer = tAFDatabaseStorer;
            _tAFFileStorer = tAFFileStorer;
            _tafStorageOptions = tAFStorageOptions.Value;
        }

        public ITAFStorage GetTAFStorage()
        {
            switch (_tafStorageOptions.store)
            {
                case TAFStores.JsonFIle:
                    return _tAFFileStorer;
                case TAFStores.Database:
                    return _tAFDatabaseStorer;
                default:
                    throw new Exception($"Storage not defined for {_tafStorageOptions.store}.");
            }
        }
    }
}
