using StockApp.Application.DTOS;
using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Features.OrderService
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetAll();

        public Task<Order> CreateOrder(CreateOrderDTO createOrder);
        public Task<Order> UpdateOrder(updateOrderDTO UpdateOrder);
        public Task<Order> DeleteOrder(DeleteOrderDTO deleteOrderDTO);

        public Task<Order> GetOrderById(int Id);
    }
}
