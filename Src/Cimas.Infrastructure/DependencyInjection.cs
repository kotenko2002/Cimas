using Cimas.Application;
using Cimas.Application.Interfaces;
using Cimas.Domain.Users;
using Cimas.Infrastructure.Auth;
using Cimas.Infrastructure.Common;
using Cimas.Infrastructure.Identity;
using Cimas.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cimas.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDatabaseServices(configuration)
                .AddAuthScheme(configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services
                .Configure<JwtConfig>(configuration.GetSection(JwtConfig.Section))
                .AddScoped<IJwtTokensService, JwtTokensService>();

            return services;
        }

        private static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<CimasDbContext>()
                .AddUserManager<CustomUserManager>();
            services.AddScoped<ICustomUserManager, CustomUserManager>();
            services.AddDbContext<CimasDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        private static IServiceCollection AddAuthScheme(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = configuration["Jwt:ValidAudience"],
                    ValidIssuer = configuration["Jwt:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]))
                };
            });

            return services;
        }
    }
}
