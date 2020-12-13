using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Services
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUser(RegisterResource registerResource, string hashPassword)
        {
            User user = new User()
            {
                Email = registerResource.Email,
                IsActive = true,
                Login = registerResource.Login,
                Name = registerResource.Name,
                Surname = registerResource.Surname,
                Password = hashPassword
            };

            await _userRepository.AddUser(user);

            return true;
        }
    }
}
