using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.Infra.Repositories;
using Redis.Model.Config;
using Redis.Model.Interfaces;
using Redis.Model.Services;

namespace Redis.Test
{
    [SetUpFixture]
    public class TestSetup
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
  
            services.AddOptions();

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<CboService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            ServiceProvider = services.BuildServiceProvider();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            ServiceProvider.Dispose();
        }
    }
}
