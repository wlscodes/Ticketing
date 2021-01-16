using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly IClaimsService _claimsService;

        public TokenController(ITokenGeneratorService tokenGeneratorService, IClaimsService claimsService)
        {
            _tokenGeneratorService = tokenGeneratorService;
            _claimsService = claimsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GenerateTokenAsync()
        {
            var claimId = _claimsService.GetUserId(HttpContext.User.Identity as ClaimsIdentity);
            if (claimId == 0)
            {
                return Unauthorized("You don't have permission to create a place");
            }

            var t = _tokenGeneratorService.GenerateJwtToken(claimId);

            return Ok(t);
        }
    }
}
