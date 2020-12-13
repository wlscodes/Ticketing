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
    public class OrganizatorController : ControllerBase
    {
        private readonly IOrganizatorRepository _organizatorRepository;

        public OrganizatorController(IOrganizatorRepository organizatorRepository)
        {
            _organizatorRepository = organizatorRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrganizatorAsync([FromQuery]int organizatorID)
        {
            var organizator = _organizatorRepository.GetOrganizatorAsync(organizatorID);

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
    }
}
