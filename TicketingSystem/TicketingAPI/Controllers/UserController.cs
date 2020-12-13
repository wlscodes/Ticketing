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
        public async Task<IActionResult> RegisterUser([FromForm] RegisterResource resource)
        {
            var login = _userRepository.GetUserByLoginAsync(resource.Login);

            if (await login != null)
            {
                return BadRequest();
            }

            var email = _userRepository.GetUserByEmailAsync(resource.Email);

            if(await email != null)
            {
                return BadRequest();
            }

            var hash = _hashService.HashPassword(resource.Password);

            var register = _registerUserService.RegisterUser(resource, hash);

            if(await register)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
