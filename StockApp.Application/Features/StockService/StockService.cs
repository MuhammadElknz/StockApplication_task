using StockApp.Application.Common.Interfaces;
using StockApp.Application.Common.Models;
using StockApp.Application.DTOS;
using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StockApp.Application.Features.StockService
{
    public class StockService : IStockService
    {

        private readonly IUnitOfWork _unitOfWork;

        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedList<Stock>> GetAll(StockListDTO param)
        {
            var query = _unitOfWork.StockRepo.GetAllRelatedEntities();


            var stocks = await PagedList<Stock>.CreateAsync(query, param.PageNumber, param.PageSize);

            return stocks;

        }

        public async Task<Stock> GetStockById(int Id)
        {
            return await _unitOfWork.StockRepo.GetByIdAsync(Id);
        }

        public async Task<IEnumerable<Stock>> UpdateAllStocksPrice()
        {
            var stocks = await _unitOfWork.StockRepo.GetAllAsync();

            foreach (var stock in stocks)
            {
                 stock.SetRandomPrice();
                _unitOfWork.StockRepo.Update(stock);
            }

            await _unitOfWork.SaveAsync();


            return stocks;
        }
    }
}
