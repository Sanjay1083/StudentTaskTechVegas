using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace StudentTaskTechVegas.Helpers
{
    public static class JwtHelper
    {
        public static string? GetClaimFromToken(string token, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim?.Value;
        }
    }
}
