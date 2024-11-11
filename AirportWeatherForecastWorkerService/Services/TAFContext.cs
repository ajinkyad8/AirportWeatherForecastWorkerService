using AirportWeatherForecastWorkerService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AirportWeatherForecastWorkerService.Services
{
    public class TAFContext : DbContext
    {
        public DbSet<TAF> TAFs { get; set; }
        private readonly string _databasePath;

        public TAFContext(IOptions<TAFStorageOptions> options)
        {
            _databasePath = Path.Join(Directory.GetCurrentDirectory(), $"{options.Value.DatabaseName}.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_databasePath}");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TAF>().HasIndex(x => new { x.AirportCode, x.IssueTime }).IsUnique();
            builder.Entity<TAF>().HasKey(x => new {x.AirportCode, x.IssueTime});
            base.OnModelCreating(builder);
        }
    }
}
