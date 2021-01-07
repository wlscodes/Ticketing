using Microsoft.AspNetCore.Authorization;
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
    public class PlaceController : Controller
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IPlaceService _placeService;


        public PlaceController(IAdministratorRepository administratorRepository, IPlaceRepository placeRepository, IPlaceService placeService)
        {
            _administratorRepository = administratorRepository;
            _placeRepository = placeRepository;
            _placeService = placeService;
        }

        [Authorize(Roles = "admin, superadmin")]
        [HttpPost]
        public async Task<IActionResult> IndexAsync([FromForm] CreatePlaceResource resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var isAdminOfOrganization = await _administratorRepository.IsUserAdministratorOfOrganizator(resource.UserId, resource.OrganizatorId);
            if(!isAdminOfOrganization)
            {
                return NotFound();
            }

            var isPlaceExists = await _placeRepository.IsPlaceExistsInOrganizator(resource.OrganizatorId, resource.PlaceName);
            if(isPlaceExists)
            {
                //TODO: content exists
                return BadRequest();
            }

            var IsSuccess = await _placeService.AddPlace(resource);

            return Ok(IsSuccess);
        }
    }
}
