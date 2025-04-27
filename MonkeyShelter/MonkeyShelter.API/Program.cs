using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MonkeyShelter.Core.Interfaces;
using MonkeyShelter.Infrastructure;
using MonkeyShelter.Infrastructure.Repositories;
using MonkeyShelter.Services.BackgroundJobs;
using MonkeyShelter.Services.Mapping;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;

namespace MonkeyShelter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            // Add Swagger configuration
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MonkeyShelter API", Version = "v1" });
            });

            //Add dbcontext
            builder.Services.AddDbContext<MonkeyShelterDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register IRepositories and their implementations
            builder.Services.AddScoped<IMonkeyRepository, MonkeyRepository>();
            builder.Services.AddScoped<ISpeciesRepository, SpeciesRepository>();
            builder.Services.AddScoped<IShelterRepository, ShelterRepository>();
            builder.Services.AddScoped<IVetCheckRepository, VetCheckRepository>();

            builder.Services.AddHostedService<VetCheckReminderService>();

            // Add Redis cache
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379"; // Redis server set
                options.InstanceName = "MonkeyShelter_"; // instance name
            });
            //Redis can be started in Docker container:
            //docker run --name redis -p 6379:6379 -d redis


            //Adding Automapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MonkeyShelter API v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
