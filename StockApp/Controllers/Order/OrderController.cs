using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Common.Exceptions;
using StockApp.Application.Common.Interfaces;
using StockApp.Application.DTOS;
using StockApp.Application.Features.OrderService;

namespace StockApp.WebApi.Controllers.Order
{


    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public OrderController(IOrderService orderService,IMapper  mapper, ICurrentUserService currentUserService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Route("{id}")]
        
        public async Task<IActionResult> GetOrder(int Id)
        {


            var order = await _orderService.GetOrderById(Id);

            return Ok(_mapper.Map<OrderDTO>(order));
        }

        [HttpGet]
        [Route("GetAll")]
 
        public async Task<IActionResult> GetAll()
        {

            var response = await _orderService.GetAll();

            return Ok(_mapper.Map<List<OrderDTO>>(response));
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO createOrder)
        {
            if(createOrder.UserId != _currentUserService.UserId)
            {
                return Unauthorized();
            }   

            var order = await _orderService.CreateOrder(createOrder);

            return Ok(_mapper.Map<OrderDTO>(order));
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateOrder(updateOrderDTO UpdateOrder)
        {
            if (UpdateOrder.UserId != _currentUserService.UserId)
            {
                return Unauthorized();
            }

            var order = await _orderService.UpdateOrder(UpdateOrder);

            return Ok(_mapper.Map<OrderDTO>(order));
        }
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteOrder(DeleteOrderDTO deleteOrderDTO)
        {
            if (deleteOrderDTO.UserId != _currentUserService.UserId)
            {
                return Unauthorized();
            }

            var order = await _orderService.DeleteOrder(deleteOrderDTO);

            return Ok(_mapper.Map<OrderDTO>(order));
        }
    }
}
