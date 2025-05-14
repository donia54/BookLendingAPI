using BookLendingAPI.Models;

namespace BookLendingAPI.Interfaces
{
    public interface IBorrowingRepository
    {

        Task AddAsync(Borrowing borrowing);
        Task<Borrowing?> GetActiveBorrowingByUserAsync(string userId);
        Task<Borrowing?> GetBorrowingByIdAsync(int id);
        Task SaveChangesAsync();

        Task<List<Borrowing>> GetByConditionAsync(System.Linq.Expressions.Expression<Func<Borrowing, bool>> expression);
    }
}
