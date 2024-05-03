using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOS;
using StockApp.Application.Features.AccountService;
using StockApp.Application.Features.TokenService;

namespace StockApp.WebApi.Controllers.Account
{ 
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {

            _accountService = accountService;
        
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDto)
        {
            var user = await _accountService.CreateUser(createUserDto);
            return Ok(user);

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var response = await _accountService.Login(loginDto.UserName, loginDto.Password);
            return Ok(response);
        }



    }
}
