using AirportWeatherForecastWorkerService.Models;
using AirportWeatherForecastWorkerService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirportWeatherForecastWorkerService.Services
{
    public class TAFDatabaseStorage : ITAFStorage
    {
        private readonly IDbContextFactory<TAFContext> _dbContextFactory;

        public TAFDatabaseStorage(IDbContextFactory<TAFContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<bool> StoreTAFs(List<TAF> tAFs)
        {
            using var context = _dbContextFactory.CreateDbContext();
            foreach (var tAF in tAFs)
            {
                await context.TAFs.Where(x => x.AirportCode == tAF.AirportCode && x.IssueTime == tAF.IssueTime).ExecuteDeleteAsync();
                context.Add(tAF);
                await context.SaveChangesAsync();
            }
            return true;
        }
    }
}
