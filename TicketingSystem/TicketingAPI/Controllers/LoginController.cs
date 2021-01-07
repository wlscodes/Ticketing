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
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public LoginController(IUserRepository userRepository, IHashService hashService, ITokenGeneratorService tokenGeneratorService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _tokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] LoginResource resource)
        {
            var user = await _userRepository.GetUserByEmailAsync(resource.Email);

            if(user is null)
            {
                return NotFound("User doesn't exists");
            }

            if(_hashService.VerifyPassword(resource.Password, user.Password))
            {
                var token = _tokenGeneratorService.GenerateJwtToken(user.UserId);

                return Ok(token);
            }

            return NotFound("Wrong password");
        }
    }
}
