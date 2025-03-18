using System.Security.Claims;
using ToDo.Domain.Dto;
using ToDo.Domain.Result;

namespace ToDo.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    
    string GenerateRefreshToken();
    
    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    
    Task<BaseResult<TokenDto>> RefreshToken(TokenDto tokenDto);
}