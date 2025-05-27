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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using MonkeyShelter.Services.Reports;
using MonkeyShelter.Services;

namespace MonkeyShelter.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Add Swagger configuration
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MonkeyShelter API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter 'Bearer' [space] and token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement 
                    {
                        {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                        }
                    });
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
            builder.Services.AddScoped<IReportService, ReportService>();

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

            //JWT Bearer config
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MonkeyShelterDbContext>()
                .AddDefaultTokenProviders();

            //jwt secret key from appsetting
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "MonkeyShelterAPI",
                    ValidAudience = "MonkeyShelterClient",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MonkeyShelter API v1"));
            }

            //Global exception Middleware.
            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseAuthentication(); //middleware

            //Roler manager creates role "Manager" in DB
            //User manager creates user ("admin","Admin123") in DB

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                if (!await roleManager.RoleExistsAsync("Manager"))
                    await roleManager.CreateAsync(new IdentityRole("Manager"));

                if (await userManager.FindByNameAsync("admin") == null)
                {
                    var admin = new IdentityUser { UserName = "admin" };
                    await userManager.CreateAsync(admin, "Admin123!");
                    await userManager.AddToRoleAsync(admin, "Manager");
                }
            }

            //You can create custom, super..locally in your db

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
