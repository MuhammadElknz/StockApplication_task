using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Common.Interfaces.SignalR
{
    public interface IStockHub
    {
        Task SubscribeToHub(string Message);
        Task NotifyAllStocksPrices(IEnumerable<Stock> stocks);
    }
}
