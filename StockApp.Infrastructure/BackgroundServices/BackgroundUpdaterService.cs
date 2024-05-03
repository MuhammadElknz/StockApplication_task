using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockApp.Application.Common.Constants;
using StockApp.Application.Common.Interfaces.SignalR;
using StockApp.Application.Features.StockService;
using StockApp.Infrastructure.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Infrastructure.BackgroundServices
{
    public class BackgroundUpdaterService : BackgroundService
    {

        public IServiceProvider Services { get; }
        public BackgroundUpdaterService(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;


        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {


             await UpdateStockPriceInBackground();

             await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private async Task UpdateStockPriceInBackground()
        {

            using (var scope = Services.CreateScope())
            {
                var stockService = scope.ServiceProvider.GetRequiredService<IStockService>();
                var context = scope.ServiceProvider.GetRequiredService<IHubContext<StockHub, IStockHub>>();

                var stocks = await stockService.UpdateAllStocksPrice();
                await context.Clients.Groups(GroupsConstants.StockGroup).NotifyAllStocksPrices(stocks);


            }
        }
    }
}
