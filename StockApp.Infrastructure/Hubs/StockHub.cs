using StockApp.Application.Common.Interfaces.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StockApp.Application.Common.Interfaces;
using StockApp.Application.Features.StockService;
using Microsoft.AspNetCore.Authorization;
using StockApp.Application.Common.Constants;

namespace StockApp.Infrastructure.Hubs
{
    [Authorize]   
    public sealed class StockHub : Hub<IStockHub>
    { 
        private readonly IStockService _stockService;

        public StockHub(IStockService stockService)
        {
            _stockService = stockService;
        }

        public override async Task OnConnectedAsync()
        {



            // await Clients.All.SubscribeToHub("Test");
        }

        public async Task JoinStocksGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,GroupsConstants.StockGroup);
        }

        public async Task GetAllStocksPrices()
        {
            var stocks=  await _stockService.UpdateAllStocksPrice();

            await Clients.Groups(GroupsConstants.StockGroup).NotifyAllStocksPrices(stocks);

        }

    }
}
