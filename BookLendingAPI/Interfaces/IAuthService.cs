using BookLendingAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using BookLendingAPI.Dtos;
using BookLendingAPI.Middlewares;

namespace BookLendingAPI.Interfaces
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);

        Task<ResponseResult<AuthDTO>> GetAuthResponseAsync(ApplicationUser user, string enteredPassword);
    }
}
