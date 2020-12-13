using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration _configuration;
        private readonly IClaimsRepository _claimsRepository;

        public TokenGeneratorService(IConfiguration configuration,
            IClaimsRepository claimsRepository)
        {
            _configuration = configuration;
            _claimsRepository = claimsRepository;
        }

        /// <summary>
        /// Get byte array of symmetric key
        /// </summary>
        /// <returns>Security Key from settings</returns>
        private byte[] GetSymmetricKey()
        {
            string key = _configuration.GetSection("Security:SymmetricKey")?.Value;
            return Encoding.UTF8.GetBytes(key);
        }

        /// <summary>
        /// Get token duration time
        /// </summary>
        /// <returns>Token duration in hours. Default is 1 hour</returns>
        private int GetTokenDuration()
        {
            string duration = _configuration.GetSection("Security:TokenDuration")?.Value;
            int hours;
            if (!Int32.TryParse(duration, out hours))
                hours = 1;
            return hours;
        }

        /// <summary>
        /// Prepare claims for user
        /// </summary>
        /// <param name="userId">User identificator</param>
        /// <returns>List with user claims and expiration date</returns>
        private IList<Claim> GetClaims(int userId)
        {
            List<Claim> claims = new List<Claim>(GetClaimsFromDatabase(userId))
            {
                CreateNbfClaim(),
                CreateExpClaim()
            };

            return claims;
        }

        /// <summary>
        /// The "nbf" (not before) claim identifies the time before which the JWT
        /// MUST NOT be accepted for processing.The processing of the "nbf"
        /// claim requires that the current date/time MUST be after or equal to
        /// the not-before date/time listed in the "nbf" claim.
        /// </summary>
        /// <returns>Nbf claim</returns>
        private Claim CreateNbfClaim()
            => new Claim(JwtRegisteredClaimNames.Nbf,
                new DateTimeOffset(DateTime.Now)
                    .ToUnixTimeSeconds()
                    .ToString()
            );

        /// <summary>
        /// The "exp" (expiration time) claim identifies the expiration time on
        /// or after which the JWT MUST NOT be accepted for processing.
        /// </summary>
        /// <param name="hours"><Duration/param>
        /// <returns></returns>
        private Claim CreateExpClaim()
            => new Claim(JwtRegisteredClaimNames.Exp,
                new DateTimeOffset(DateTime.Now.AddHours(GetTokenDuration()))
                    .ToUnixTimeSeconds()
                    .ToString()
            );

        /// <summary>
        /// Get user claims from database
        /// </summary>
        /// <param name="userId">User identificator</param>
        /// <returns>List of user claims from database</returns>
        private IList<Claim> GetClaimsFromDatabase(int userId) => _claimsRepository.GetClaimsAsync(userId).Result;
        

        /// <summary>
        /// Generate SymmetricSecurityKey
        /// </summary>
        /// <returns>New instance of SymmetricSecurityKey</returns>
        private SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(GetSymmetricKey());

        private SigningCredentials GetSigningCredentials() => new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

        private JwtHeader GetJwtHeader() => new JwtHeader(GetSigningCredentials());

        private JwtPayload GetJwtPayload(int userId) => new JwtPayload(GetClaims(userId));

        private JwtSecurityToken GetSecurityToken(int userId) => new JwtSecurityToken(GetJwtHeader(), GetJwtPayload(userId));

        public string GenerateJwtToken(int userId) => new JwtSecurityTokenHandler().WriteToken(GetSecurityToken(userId));
        
    }
}
