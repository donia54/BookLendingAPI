using BookLendingAPI.Models;

namespace BookLendingAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(string userId);

        
    }
}
