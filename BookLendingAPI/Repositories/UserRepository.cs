using BookLendingAPI.Data;
using BookLendingAPI.Interfaces;
using BookLendingAPI.Models;

namespace BookLendingAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
