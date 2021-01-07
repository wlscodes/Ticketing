using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class OrganizatorController : ControllerBase
    {
        private readonly IOrganizatorRepository _organizatorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizatorService _organizatorService;
        private readonly IClaimsService _claimsService;

        public OrganizatorController(IOrganizatorRepository organizatorRepository, IUserRepository userRepository, IOrganizatorService organizatorService, IClaimsService claimsService)
        {
            _organizatorRepository = organizatorRepository;
            _userRepository = userRepository;
            _organizatorService = organizatorService;
            _claimsService = claimsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrganizatorAsync([FromQuery]int organizatorID)
        {
            var organizator = _organizatorRepository.GetOrganizatorByIdAsync(organizatorID);

            if(await organizator is null)
            {
                return NotFound();
            }

            return Ok(organizator);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrganizators()
        {
            var organizator = _organizatorRepository.GetOrganizators();

            if (organizator is null)
            {
                return NotFound();
            }

            return Ok(organizator);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOrganizator([FromForm] OrganizatorResource resource)
        {
            var claimId = _claimsService.GetUserId(HttpContext.User.Identity as ClaimsIdentity);
            if(claimId == 0)
            {
                return Unauthorized("You don't have permission to create a new organization");
            }

            resource.CreatorId = claimId;

            var user = await _userRepository.GetUserByIdAsync(resource.CreatorId);
            if(user is null)
            {
                return NotFound("There was an error with your account");
            }

            var result = await _organizatorService.CreateNewOrganizator(resource);

            if(result is null)
            {
                return BadRequest("There was an unexpected error while creating an organization");
            }

            return Ok(result.OrganizatorId);
            
        }
    }
}
