using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;

namespace TicketingAPI.Repositories
{
    public class ClaimsRepository : IClaimsRepository
    {
        private readonly IUserRepository _userRepository;

        public ClaimsRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IList<Claim>> GetClaimsAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            return new List<Claim>()
            {
                new Claim(ClaimTypes.Role, ReadClaim(user.Role))
            };
        }

        private string ReadClaim(int role)
        {
            switch(role)
            {
                case 1: return "superadmin";
                case 2: return "admin";
                case 3: return "editor";
                case 4: return "user";
                default: return "none";
            }
        }
    }
}
