using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOS;
using StockApp.Application.Features.StockService;

namespace StockApp.WebApi.Controllers.Stock
{
    //[Authorize]
    public class StockController : BaseController
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public StockController(IStockService stockService,IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(StockListDTO param)
        {

            var response =await _stockService.GetAll(param);

            return Ok(_mapper.Map<PagedListDto<StockDTO>>(response));
        }
        
        

    }
}
