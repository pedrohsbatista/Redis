using Redis.Infra.Repositories;
using Redis.Infra.Caches;
using Redis.Model.Config;
using Redis.Model.Interfaces;
using Redis.Model.Services;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = builder.Configuration.GetSection("AppSettings");

builder.Services.Configure<AppSettings>(appSettings);

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<CboService>();

builder.Services.AddSingleton(typeof(ICache<>), typeof(RedisCache<>));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = appSettings["RedisConnection"];
    options.InstanceName = "redis";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
