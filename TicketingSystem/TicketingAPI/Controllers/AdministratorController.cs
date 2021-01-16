using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;
        private readonly IClaimsService _claimsService;

        public AdministratorController(IAdministratorService administratorService, IClaimsService claimsService)
        {
            _administratorService = administratorService;
            _claimsService = claimsService;
        }

        [HttpGet]
        [Authorize(Roles = "admin, superadmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Index()
        {
            var userId = _claimsService.GetUserId(HttpContext.User.Identity as ClaimsIdentity);
            if (userId == 0)
            {
                return Unauthorized("You don't have permission to get an organizations list");
            }
            var organizators = await _administratorService.GetOrganizatorSelectsAsync(userId);

            if (organizators is null)
            {
                return NotFound("User is not an administrator of any organization");
            }

            return Ok(organizators);
        }
    }
}
