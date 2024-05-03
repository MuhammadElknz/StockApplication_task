using Microsoft.Extensions.DependencyInjection;
using StockApp.Application.Features.AccountService;
using StockApp.Application.Features.OrderService;
using StockApp.Application.Features.StockService;
using StockApp.Application.Features.TokenService;
using StockApp.Application.Mappings;

namespace StockApp.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
      


            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}