using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        private readonly IRegisterUserService _registerUserService;

        public UserController(IUserRepository userRepository, IHashService hashService, IRegisterUserService registerUserService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _registerUserService = registerUserService;
        }

        /// <summary>
        /// Register new user method
        /// </summary>
        /// <param name="resource">Register information</param>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterResource resource)
        {
            var login = await _userRepository.GetUserByLoginAsync(resource.Login);

            if (login != null)
            {
                return BadRequest($"Login {resource.Login} already exists");
            }

            var email = await _userRepository.GetUserByEmailAsync(resource.Email);

            if(email != null)
            {
                return BadRequest($"Email {resource.Email} already exists");
            }

            var hash = _hashService.HashPassword(resource.Password);

            var register = await _registerUserService.RegisterUser(resource, hash);

            if(register)
            {
                return Ok("Account has been created");
            }

            return BadRequest("There was an unexpected error while creating account");
        }
    }
}
