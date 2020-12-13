using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.Services.Interfaces
{
    public interface IHashService
    {
        public string HashPassword(string password);

        public bool VerifyPassword(string password, string hash);
    }
}
