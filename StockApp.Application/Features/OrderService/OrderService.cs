using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StockApp.Application.Common.Exceptions;
using StockApp.Application.Common.Interfaces;
using StockApp.Application.DTOS;
using StockApp.Application.Features.AccountService;
using StockApp.Application.Features.StockService;
using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Features.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAccountService _accountService;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IStockService stockService, IMapper mapper,ICurrentUserService currentUserService, IAccountService accountService, IUnitOfWork unitOfWork)
        {
            _stockService = stockService;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _accountService = accountService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrder(CreateOrderDTO createOrder)
        {
           
            var stock = await _stockService.GetStockById(createOrder.StockId);

            if (stock == null)
            {
                throw new NotFoundException("Stock is not found");
            }

            var appUser = await _accountService.GetUserById(createOrder.UserId);

            if (appUser == null)
            {
                throw new NotFoundException("User is not found");
            }

            var order = new Order(stock, createOrder.Quantity.Value, stock.Price, appUser);
            order.CreatedBy = appUser.UserName;

            await _unitOfWork.OrderRepo.AddAsync(order);
            await _unitOfWork.SaveAsync();


            return order;
        }

        public async Task<Order> UpdateOrder(updateOrderDTO updateOrder)
        {

            Order? order = await GetOrderById(updateOrder.Order_id);

            if (order == null)
            {
                throw new NotFoundException("order is not found");
            }

            var appUser = await _accountService.GetUserById(updateOrder.UserId);

            if (appUser == null)
            {
                throw new NotFoundException("User is not found");
            }
            var stock = await _stockService.GetStockById(updateOrder.StockId);
            var updated_order = new Order(stock, updateOrder.Quantity.Value, stock.Price, appUser);
            order.CreatedBy = appUser.UserName;
            order.Price=updated_order.Price;
            order.StockId=updated_order.StockId;
            order.Quantity=updated_order.Quantity;

            _unitOfWork.OrderRepo.Update(order);
            await _unitOfWork.SaveAsync();

            

            return await GetOrderById(order.Id);
        }

        public async Task<Order> DeleteOrder(DeleteOrderDTO deleteOrderDTO)
        {

            var order = await GetOrderById(deleteOrderDTO.Order_id);

            if (order == null)
            {
                throw new NotFoundException("order is not found");
            }

            var appUser = await _accountService.GetUserById(order.UserId);

            if (appUser == null)
            {
                throw new NotFoundException("User is not found");
            }


            _unitOfWork.OrderRepo.Delete(order);
            await _unitOfWork.SaveAsync();


            return order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
        
          return await _unitOfWork.OrderRepo.GetAsync(includeString:"Stock");
        }

        public async Task<Order> GetOrderById(int Id)
        {
            var order = _unitOfWork.OrderRepo.Get(e=>e.Id ==Id,null,"Stock").FirstOrDefault() ;

            if(order == null)
            {
                throw new NotFoundException("Order Not Found");
            }

            return await Task.FromResult(order);
        }
    }
}
