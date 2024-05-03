using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockApp.Application.Common.Interfaces;
using StockApp.WebApi.Services;
using System.Text;

namespace StockApp.WebApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services,IConfiguration config)
        {

            services.AddScoped<ICurrentUserService,CurrentUser>();

            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200"));
            });


            return services;
        }
    }
}
