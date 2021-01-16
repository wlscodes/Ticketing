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
using TicketingAPI.ResponseModels;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PlaceController : Controller
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IPlaceService _placeService;
        private readonly IClaimsService _claimsService;


        public PlaceController(IAdministratorRepository administratorRepository, IPlaceRepository placeRepository, IPlaceService placeService, IClaimsService claimsService)
        {
            _administratorRepository = administratorRepository;
            _placeRepository = placeRepository;
            _placeService = placeService;
            _claimsService = claimsService;
        }

        [Authorize(Roles = "admin, superadmin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> IndexAsync([FromForm] CreatePlaceResource resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var claimId = _claimsService.GetUserId(HttpContext.User.Identity as ClaimsIdentity);
            if (claimId == 0)
            {
                return Unauthorized("You don't have permission to create a place");
            }
            resource.UserId = claimId;

            var isAdminOfOrganization = await _administratorRepository.IsUserAdministratorOfOrganizator(resource.UserId, resource.OrganizatorId);
            if(!isAdminOfOrganization)
            {
                return NotFound("You are not an administrator of this organizator");
            }

            var isPlaceExists = await _placeRepository.IsPlaceExistsInOrganizator(resource.OrganizatorId, resource.PlaceName);
            if(isPlaceExists)
            {
                return BadRequest("Place with this name already exists in this organizator");
            }

            var IsSuccess = await _placeService.AddPlace(resource);

            if(IsSuccess)
            {
                return Ok("Place was created");
            }

            return BadRequest("There was an unexpected error while creating place");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrganizatorPlaces([FromQuery] int organizatorId)
        {
            if(organizatorId < 0)
            {
                return NotFound("Organizator doesn't exists");
            }

            return Ok(_placeService.GetPlacesByOrganizatorId(organizatorId));
        }
    }
}
