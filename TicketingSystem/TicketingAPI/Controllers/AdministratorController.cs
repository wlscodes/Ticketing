using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;

namespace TicketingAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly IOrganizatorRepository _organizatorRepository;

        public AdministratorController(IOrganizatorRepository organizatorRepository)
        {
            _organizatorRepository = organizatorRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Index([FromQuery] int userId)
        {
            var organizators = _organizatorRepository.GetOrganizatorsByAdministratorId(userId);

            if(organizators is null)
            {
                return NotFound();
            }

            return Ok(organizators);
        }
    }
}
