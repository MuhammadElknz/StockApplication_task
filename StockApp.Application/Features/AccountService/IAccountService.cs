using StockApp.Application.DTOS;
using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Features.AccountService
{
    public interface IAccountService
    {
        public Task<CreateUserDTO> CreateUser(CreateUserDTO createUser);

        public Task<LoginResponseDTO> Login(string userName, string password);

        public Task<ApplicationUser> GetUserById(string userId);


    }
}
