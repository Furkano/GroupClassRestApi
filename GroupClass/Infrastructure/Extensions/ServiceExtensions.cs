using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using GroupClass.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.EntityFrameworkCore.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Text;

namespace GroupClass.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        //public static void ConfigureLoggerService(this IServiceCollection services)
        //{
        //    services.AddSingleton<ILoggerManager, LoggerManager>();
        //}

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            services.AddDbContextPool<AppDbContext>(
                    dbContextOption => dbContextOption
                        .UseMySql(connectionString,
                            mySqlOptions => mySqlOptions
                                .ServerVersion(new Version(10, 4, 10), ServerType.MySql)
                                .CharSetBehavior(CharSetBehavior.NeverAppend)
                        )
                        .UseLoggerFactory(
                            LoggerFactory.Create(
                                    logging => logging
                                    .AddConsole()
                                    .AddFilter(level => level >= LogLevel.Information)
                                )
                        )
                        .EnableSensitiveDataLogging(true)
                        .EnableDetailedErrors()
                );
            //var connectionString = config["ConnectionStrings:DefaultConnection"];
            //services.AddDbContext<AppDbContext>(o => o.UseMySql(connectionString, option =>
            //{
            //    option.EnableRetryOnFailure(
            //        maxRetryCount: 5,
            //        maxRetryDelay: TimeSpan.FromSeconds(30),
            //        errorNumbersToAdd: null);
            //}));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
        public static void JwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]))
                    
                };
                //services.AddCors();
            });
        }
        public static void PolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });
        }
    }

}
