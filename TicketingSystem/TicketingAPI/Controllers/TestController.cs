using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly IHashService _hash;

        public TestController(IHashService hash)
        {
            _hash = hash;
        }

        [HttpGet]
        public string Index([FromQuery] string pass)
        {
            //return _hash.HashPassword(pass);
            return _hash.VerifyPassword("Qwerty", "$2a$11$AZwYTefeVqTac6ox25Exu.JnNpQsHLp3JjINAs590AieGIjg56xkS").ToString();
        }
    }
}
