using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TokenController : ControllerBase
    {
        private ITokenGeneratorService _tokenGeneratorService;

        public TokenController(ITokenGeneratorService tokenGeneratorService)
        {
            _tokenGeneratorService = tokenGeneratorService;
        }

        [HttpGet]
        public async Task<IActionResult> GenerateTokenAsync()
        {
            var t = _tokenGeneratorService.GenerateJwtToken(21);

            return Ok(t);
        }
    }
}
