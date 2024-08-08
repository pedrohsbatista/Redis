using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Redis.Model.Config;
using Redis.Model.Entities;

namespace Redis.Infra.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cbo> Cbos { get; set; }

        private readonly AppSettings _appSettings;

        public ApplicationDbContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_appSettings.SQLiteConnection);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
