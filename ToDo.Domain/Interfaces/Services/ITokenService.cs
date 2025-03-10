using System.Security.Claims;

namespace ToDo.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    
    string GenerateRefreshToken();
}