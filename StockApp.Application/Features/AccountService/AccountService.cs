using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockApp.Application.Common.Exceptions;
using StockApp.Application.DTOS;
using StockApp.Application.Features.TokenService;
using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Features.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager,ITokenService tokenService,IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

       

        public async Task<CreateUserDTO> CreateUser(CreateUserDTO createUser)
        {
            if (await _userManager.Users.AnyAsync(e => e.UserName.Equals(createUser.UserName)))
            {
                throw new BadRequestException("User Already Exists");
            }

            var user = _mapper.Map<ApplicationUser>(createUser);

       

            var result = await _userManager.CreateAsync(user, createUser.Password);

            if (!result.Succeeded) throw new BadRequestException(result.Errors.Select(e => e.Description).FirstOrDefault());


            return createUser;

        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<LoginResponseDTO> Login(string userName, string password)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName.Equals(userName));

            if (user == null) throw new NotFoundException("User Not Found");

            var result = await _userManager.CheckPasswordAsync(user, password);

            if (!result) throw new BadRequestException("Incorrect Password or User Name");

            return new LoginResponseDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.GenerateTokenAsync(user),
            };

        }
    }
}
