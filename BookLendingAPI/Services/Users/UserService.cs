using AutoMapper;
using BookLendingAPI.Dtos;
using BookLendingAPI.Interfaces;
using BookLendingAPI.Middlewares;
using BookLendingAPI.Models;
using BookLendingAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace BookLendingAPI.Services.Users
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IAuthService _AuthService;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IAuthService authService, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _UserManager = userManager;
            _AuthService = authService;
            _SignInManager = signInManager;
            _RoleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ResponseResult<AuthDTO>> RegisterUserAsync(RegistrUserIdentity model)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var existingUserByEmail = await _UserManager.FindByEmailAsync(model.Email);
                if (existingUserByEmail != null)
                {
                    return ResponseResult<AuthDTO>.Error("Email is already registered.", 400);
                }

                var user = _InitializeUserRegisterationAsync(model);
                var result = await _UserManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    if (result.Errors.Any(e => e.Code.Contains("Password")))
                    {
                        errors.Add("Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
                    }
                    return ResponseResult<AuthDTO>.Error(errors, 400);
                }

                var UserRole = model.Role;
                var RolesAdded = await _UserManager.AddToRolesAsync(user, UserRole == null ? new List<string> { "User" } : new List<string> { UserRole });
                if (!RolesAdded.Succeeded)
                {
             await _UserManager.DeleteAsync(user);
                    return ResponseResult<AuthDTO>.Error($"Failed to add roles to user", 500);
                }

              //transaction.Complete();

                var AuthTokenResult = await _AuthService.GetAuthResponseAsync(user, model.Password);
                if (AuthTokenResult.IsSuccess && AuthTokenResult.Data != null)
                {
                    return ResponseResult<AuthDTO>.Created(AuthTokenResult.Data);
                }
                else
                {
                    return ResponseResult<AuthDTO>.Error($"Registration failed: {AuthTokenResult.ErrorMessage}", AuthTokenResult.StatusCode);
                }

            }
            catch (Exception ex)
            {
                return ResponseResult<AuthDTO>.Error($"Registration failed: {ex.Message}", 500);
            }
        }

        public async Task<ResponseResult<AuthDTO>> LoginAsync(LoginDTO login)
        {
            if (string.IsNullOrWhiteSpace(login.Email))
            {
                return ResponseResult<AuthDTO>.Error("Invalid email or password", 400);
            }

            var user = await _UserManager.FindByEmailAsync(login.Email);
            if (user == null || string.IsNullOrWhiteSpace(user.UserName))
            {
                return ResponseResult<AuthDTO>.Error("Invalid email or password", 400);
            }
            var signInResult = await _SignInManager.PasswordSignInAsync(user.UserName, login.Password, true, true);
            if (!signInResult.Succeeded)
            {
                return ResponseResult<AuthDTO>.Error("Invalid email or password", 400);
            }
            var AuthTokenResult = await _AuthService.GetAuthResponseAsync(user, login.Password);

            if (AuthTokenResult.IsSuccess && AuthTokenResult.Data != null)
                return ResponseResult<AuthDTO>.Success(AuthTokenResult.Data);
            else
                return AuthTokenResult;
        }


        public async Task<UserDto> IsUserExistAsync(string userId)
        {

            var user = await _UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null; 
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<ResponseResult<UserDto>> GetUserByIdAsync(string userId)
        {
            var user = await _UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ResponseResult<UserDto>.NotFound("User not found.");
            }
            var userDto = _mapper.Map<UserDto>(user);
            return ResponseResult<UserDto>.Success(userDto);
        }

        private ApplicationUser _InitializeUserRegisterationAsync(RegistrUserIdentity model)
        {
            return new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                IsActive = true
            };
        }
    }
}