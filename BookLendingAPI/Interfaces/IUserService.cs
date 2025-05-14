using BookLendingAPI.Dtos;
using BookLendingAPI.Middlewares;

namespace BookLendingAPI.Interfaces
{
    public interface IUserService
    {
        Task<ResponseResult<AuthDTO>> RegisterUserAsync(RegistrUserIdentity model);
        Task<ResponseResult<AuthDTO>> LoginAsync(LoginDTO login);
        Task<UserDto> IsUserExistAsync(string userId);

        Task<ResponseResult<UserDto>> GetUserByIdAsync(string userId);
    }
}
