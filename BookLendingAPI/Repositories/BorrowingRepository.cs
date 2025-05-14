using BookLendingAPI.Data;
using BookLendingAPI.Interfaces;
using BookLendingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLendingAPI.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly AppDbContext _context;

        public BorrowingRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Borrowing borrowing)
        {
            await _context.Borrowings.AddAsync(borrowing);
        }


        public async Task<Borrowing?> GetActiveBorrowingByUserAsync(string userId)
        {
            return await _context.Borrowings
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.UserId == userId && !b.Returned);
        }


        public async Task<Borrowing?> GetBorrowingByIdAsync(int id)
        {
            return await _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }


        public async Task<List<Borrowing>> GetByConditionAsync(System.Linq.Expressions.Expression<Func<Borrowing, bool>> expression)
        {
            return await _context.Borrowings
                .Where(expression)
                .Include(b => b.Book) 
                .Include(b => b.User) 
                .ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
